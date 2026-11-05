---
id: 14-M02-L03
cim: Integrált feladat: CSV-ből WPF-felület
evfolyam: 2-14
modul: M02 – WPF alkalmazások
orak: 17–20.
het:
oraszam: 4
elofeltetel: 14-M02-L02
leiras: Fájlból beolvasott adatok megjelenítése, szűrése és szerkesztése WPF-ben (Híres épületek).
---

# 14-M02-L03 · Integrált feladat: CSV-ből WPF-felület

## Célok

A lecke végére a tanuló:

- egy teljes kis alkalmazást épít: CSV-fájl → objektumlista → WPF-felület;
- az adatfájlt a projekthez adja úgy, hogy futáskor a program megtalálja;
- kultúrafüggetlenül olvas tizedes számot, és kezeli a hibás vagy hiányzó fájlt;
- `ListBox`/`DataGrid` elemeit LINQ-kel szűri (`ComboBox`, `TextBox`, `CheckBox`), és a szűrt listáról statisztikát számol;
- a kiválasztott elemet adatkötéssel jeleníti meg és szerkeszti, és a változásokat visszaírja a fájlba;
- saját értékátalakítót (`IValueConverter`) ír.

## Elmélet

### Az alkalmazás rétegei

```
epuletek.csv  ──beolvasás──►  List<Epulet> / ObservableCollection<Epulet>  ──Binding──►  ListBox, DataGrid, részletek
     ▲                                     │
     └──────────────── mentés ◄────────────┘   (szerkesztés után, bezáráskor)
```

Érdemes szétválasztani: az **adatosztály** (`Epulet`) csak adatot és CSV-átalakítást tartalmaz, a **beolvasás/mentés** egy
külön osztályban van (`EpuletTar`), a **felület** (`MainWindow`) csak ezeket használja. Így a beolvasó kód konzolos programban is működne
(lásd 14-M01-L02).

### Az adatfájl a projektben

1. Másold a CSV-t a projekt mappájába (vagy egy `adatok` almappába): *Solution Explorer → Add → Existing Item*.
2. A fájl tulajdonságainál: **Copy to Output Directory: Copy if newer**. A csproj-ban ez lesz belőle:
   ```xml
   <None Update="adatok/epuletek.csv">
     <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
   </None>
   ```
3. A kódban a program mappájához képest hivatkozz rá:
   ```csharp
   string fajl = Path.Combine(AppContext.BaseDirectory, "adatok", "epuletek.csv");
   ```

> Figyelem: a program a `bin/Debug/net8.0-windows/adatok/` mappában lévő **másolatot** olvassa és írja. A mentés tehát nem a projektben lévő
> eredeti fájlt módosítja – újrafordításkor az eredeti visszamásolódhat.

A WPF-projektek automatikus `using`-jai között **nincs** benne a `System.IO`, ezért a `File` és a `Path` használatához ki kell írni: `using System.IO;`.

### Beolvasás objektumokba

```csharp
public static Epulet CsvSorbol(string sor)
{
    string[] r = sor.Split(';');
    return new Epulet
    {
        Id = int.Parse(r[0]),
        Nev = r[1],
        EpitesEve = int.Parse(r[4]),                                   // -432 = i. e. 432
        Magassag = double.Parse(r[5], CultureInfo.InvariantCulture),   // "553.3"
        Latogathato = r[6].Trim().ToLower() == "true"
    };
}
```

| Buktató | Megoldás |
|---|---|
| A fájlban tizedes**pont** van, a magyar Windows tizedes**vesszőt** vár | `double.Parse(szoveg, CultureInfo.InvariantCulture)`, és mentéskor `ToString(CultureInfo.InvariantCulture)` |
| Fejlécsor | `File.ReadAllLines(fajl).Skip(1)` |
| Üres sor a fájl végén | `if (string.IsNullOrWhiteSpace(sor)) continue;` |
| Hiányzó vagy hibás fájl | `try`–`catch` (`IOException`, `FormatException`, `IndexOutOfRangeException`) + `MessageBox` |
| `true`/`True`/`TRUE` | kisbetűsítés vagy `StringComparison.OrdinalIgnoreCase` |

### Szűrés LINQ-kel

Minden szűrő (`ComboBox.SelectionChanged`, `TextBox.TextChanged`, `CheckBox.Checked`/`Unchecked`) **ugyanazt** a metódust hívja,
amely mindig a teljes listából indul ki:

```csharp
private void Szures()
{
    IEnumerable<Epulet> szurt = epuletek;                      // a teljes lista
    if (OrszagValaszto.SelectedItem is string orszag && orszag != "(összes)")
        szurt = szurt.Where(e => e.Orszag == orszag);
    if (KeresoMezo.Text.Trim() != "")
        szurt = szurt.Where(e => e.Nev.Contains(KeresoMezo.Text.Trim(), StringComparison.CurrentCultureIgnoreCase));
    if (CsakLatogathato.IsChecked == true)
        szurt = szurt.Where(e => e.Latogathato);

    var eredmeny = szurt.OrderBy(e => e.Nev).ToList();
    EpuletTabla.ItemsSource = eredmeny;                         // új lista a táblázatnak
    DarabSzoveg.Text = $"Találatok: {eredmeny.Count} db";
}
```

- A `ComboBox` elemei: `epuletek.Select(e => e.Orszag).Distinct().OrderBy(o => o)`, elöl egy `(összes)` elemmel.
- Statisztika üres listán: az `Average` és a `Max` **kivételt dob** üres sorozatra – előbb vizsgáld a `Count`-ot!
- Ha az esemény az `InitializeComponent` közben fut le, egyes vezérlők még `null`-ok: `if (EpuletTabla == null) return;`.

### Kiválasztás és szerkesztés

- **Megjelenítés**: a részletező panel `DataContext`-je a kiválasztott elem: `DataContext="{Binding ElementName=EpuletTabla, Path=SelectedItem}"`.
- **Szerkesztés másolaton**: ha közvetlenül a kiválasztott objektumot kötjük a `TextBox`-okhoz, minden leütés azonnal módosítja az adatot,
  és nincs „Mégse”. Ezért a kiválasztáskor másolatot készítünk (`MemberwiseClone()`), azt szerkesztjük, és csak a **Mentés** gomb írja
  vissza az eredetibe. A **Mégse** egyszerűen eldobja a másolatot.
- **Mentés fájlba bezáráskor**: a `Window` `Closing` eseményében kérdezünk rá; `e.Cancel = true` megakadályozza a bezárást.

### Saját értékátalakító (IValueConverter)

Ha a forrás és a cél típusa eltér, és nincs beépített átalakító, sajátot írunk:

```csharp
public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value == null ? Visibility.Hidden : Visibility.Visible;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
```

```xml
<Window ... xmlns:local="clr-namespace:HiresEpuletek">
    <Window.Resources>
        <local:NullToVisibilityConverter x:Key="NullToVis"/>
    </Window.Resources>
    ...
    <StackPanel Visibility="{Binding ElementName=Szerkeszto, Path=DataContext, Converter={StaticResource NullToVis}}">
```

### Tipikus hibák

- **`FileNotFoundException`**: elmaradt a *Copy to Output Directory* beállítás, vagy a kód a projektmappát keresi a `bin` helyett.
- **`FormatException` a magasságnál** (vagy más nyelvi beállítású gépen hibás érték): kultúrafüggő `double.Parse` – használd az `InvariantCulture`-t. A `Replace(".", ",")` trükk csak magyar beállítású gépen működik.
- **Mentés után a fájl tizedesvesszőt tartalmaz**, és a következő beolvasás elszáll: a mentésnél is `InvariantCulture` kell.
- **`InvalidOperationException: Sequence contains no elements`**: `Average`/`Max` üres szűrt listán.
- **A szűrés után a régi kiválasztás „beragad”**: új `ItemsSource` beállításakor a kiválasztás elveszhet – a részletező panel ezt kezelje (`null`).
- **A szerkesztett értéket a „Mégse” után is megtartja**: nem másolatot, hanem az eredeti objektumot kötötted a szerkesztőhöz.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/HiresEpuletek](peldak/HiresEpuletek/) | Az órai „Híres épületek” alkalmazás: `Epulet` modell (`INotifyPropertyChanged`, CSV-átalakítás `InvariantCulture`-rel), `EpuletTar` (beolvasás, mentés), `ListBox` `DataTemplate`-tel, szerkesztés másolaton Mentés/Mégse gombokkal, saját `NullToVisibilityConverter`, mentés rákérdezéssel bezáráskor |

Adatfájl: [peldak/HiresEpuletek/adatok/epuletek.csv](peldak/HiresEpuletek/adatok/epuletek.csv) – 15 híres épület (név, város, ország, építés éve, magasság, látogathatóság).

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. Módosíts egy épületet, zárd be az ablakot „Igen” válasszal, majd nézd meg
a `bin/Debug/net8.0-windows/adatok/epuletek.csv` tartalmát.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M02-L03-F01 | [Híres épületek – vizsgaszerű feladat (90 perc, 100 pont)](feladatok/F01-hires-epuletek-vizsga/) | ★★★ | [megoldás](megoldasok/F01-hires-epuletek-vizsga/) |
| 14-M02-L03-F02 | [Hegycsúcsok böngészője](feladatok/F02-hegycsucsok/) | ★★☆ | [megoldás](megoldasok/F02-hegycsucsok/) |

## Kapcsolódó

- Előző lecke: 14-M02-L02 (Adatkötés és DataContext)
- Következő lecke: 14-M03-L01 (EF Core – Code First)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
