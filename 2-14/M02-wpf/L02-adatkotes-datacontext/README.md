---
id: 14-M02-L02
cim: Adatkötés és DataContext
evfolyam: 2-14
modul: M02 – WPF alkalmazások
orak: 13–16.
het:
oraszam: 4
elofeltetel: 14-M02-L01
leiras: DataContext, Binding XAML-ben, ItemsSource, SelectedItem, INotifyPropertyChanged alapjai, DataGrid.
---

# 14-M02-L02 · Adatkötés és DataContext

## Célok

A lecke végére a tanuló:

- két vezérlő tulajdonságát összeköti XAML-ben (`{Binding ElementName=…, Path=…}`), és beállítja a kötés irányát és frissítését;
- érti a `DataContext` szerepét és öröklődését, és egy C#-objektum tulajdonságaihoz köt;
- `ObservableCollection<T>`-t köt `ListBox`/`DataGrid` `ItemsSource`-ához, `DataTemplate`-tel formázza az elemeket;
- a kiválasztott elem (`SelectedItem`) adatait részletező panelen mutatja és szerkeszti;
- `INotifyPropertyChanged`-et valósít meg, hogy a kódból végzett módosítás megjelenjen a felületen.

## Elmélet

### Miért adatkötés?

Az előző órán a vezérlők között eseménykezelőkkel „pakoltuk át” az adatokat
(`Csuszka_ValueChanged` → `Mezo.Text = ...`, `Mezo_TextChanged` → `Csuszka.Value = ...`). Ez sok, hibalehetőséges kód.
Az **adatkötés** (*binding*) deklaratív módon, a XAML-ben mondja meg, hogy egy **cél**tulajdonság egy **forrás**tulajdonság értékét kövesse:

```
forrás (objektum vagy vezérlő) . tulajdonság   ⇄   cél (vezérlő) . tulajdonság
          pl. Slider.Value                              TextBox.Text
```

A cél mindig egy vezérlő *függőségi tulajdonsága* (*dependency property* – a vezérlők szinte minden tulajdonsága ilyen);
a forrás lehet egy másik vezérlő vagy **bármilyen C#-objektum publikus tulajdonsága**.

### Kötés vezérlők között: `ElementName`

```xml
<Slider Name="Homerseklet" Minimum="-30" Maximum="50"/>
<TextBox Text="{Binding ElementName=Homerseklet, Path=Value, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"/>
<TextBlock Text="{Binding ElementName=Homerseklet, Path=Value, StringFormat='Kint {0} °C van.'}"/>
```

| Beállítás | Értékek | Jelentés |
|---|---|---|
| `ElementName` | a forrásvezérlő `Name`-je | honnan |
| `Path` | tulajdonság (akár több szint: `SelectedItem.Content`, `Text.Length`) | mit |
| `Mode` | `OneWay`, `TwoWay`, `OneTime`, `OneWayToSource` | merre áramlik az adat; a `TextBox.Text`, `CheckBox.IsChecked`, `Slider.Value` alapértéke `TwoWay`, a legtöbb másé `OneWay` |
| `UpdateSourceTrigger` | `PropertyChanged`, `LostFocus` | mikor írja vissza a TextBox a forrásba: minden leütéskor vagy csak fókuszvesztéskor (alapértelmezés) |
| `StringFormat` | `'{}{0} Ft'`, `'{}{0:F1}'`, `'Ár: {0} Ft'` | formázott megjelenítés; ha a szöveg `{`-vel kezdődik, elé `{}` kell |
| `Converter` | pl. `{StaticResource BoolToVis}` | átalakítás (pl. `bool` → `Visibility` a beépített `BooleanToVisibilityConverter`-rel) |

### DataContext – a kötések alapértelmezett forrása

Ha a `Binding`-ban nincs `ElementName`, a forrás az elem **`DataContext`-je**. A `DataContext` **öröklődik**: amit az ablakra
beállítunk, azt minden benne lévő vezérlő látja – hacsak egy belső elem felül nem írja.

```csharp
public partial class MainWindow : Window
{
    public ObservableCollection<Szemely> Szemelyek { get; } = new();   // publikus TULAJDONSÁG kell!

    public MainWindow()
    {
        InitializeComponent();
        Szemelyek.Add(new Szemely { Nev = "Minta Anna", Kor = 34, Varos = "Győr" });
        DataContext = this;          // minden kötés az ablak tulajdonságaiból indul
    }
}
```

```xml
<ListBox Name="SzemelyLista" ItemsSource="{Binding Szemelyek}">
    <ListBox.ItemTemplate>
        <DataTemplate>                                   <!-- itt a DataContext: EGY Szemely -->
            <TextBlock Text="{Binding Nev}"/>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>

<!-- a részletező panel DataContextje: a kiválasztott elem -->
<StackPanel DataContext="{Binding ElementName=SzemelyLista, Path=SelectedItem}">
    <TextBox Text="{Binding Nev}"/>
    <TextBox Text="{Binding Kor}"/>
</StackPanel>
```

- **`ItemsSource`** – a lista elemei egy gyűjteményből jönnek (ilyenkor az `Items.Add` már nem használható!).
- **`ItemTemplate` / `DataTemplate`** – egy elem kinézete. `ItemTemplate` nélkül a lista az objektum `ToString()`-jét írja ki.
- **`SelectedItem`** – a kiválasztott objektum (nem szöveg!): kódban `SzemelyLista.SelectedItem as Szemely`.
- A szerkesztőmezőkbe írt érték **közvetlenül az objektumba** kerül – a kódban már az objektumot olvassuk, nem a `TextBox`-ot.

### Értesítés a változásról: `INotifyPropertyChanged` és `ObservableCollection`

A felület csak akkor frissül, ha a forrás **szól**, hogy változott:

| Mi változik? | Mi kell hozzá? |
|---|---|
| egy objektum tulajdonsága (pl. kódból `szemely.Kor++`) | az osztály valósítsa meg az **`INotifyPropertyChanged`**-et |
| a gyűjtemény elemszáma (`Add`, `Remove`) | **`ObservableCollection<T>`** a `List<T>` helyett |

```csharp
public class Szemely : INotifyPropertyChanged
{
    private string nev = "";
    public event PropertyChangedEventHandler? PropertyChanged;

    public string Nev
    {
        get => nev;
        set
        {
            nev = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nev)));
        }
    }
}
```

Számított tulajdonságnál (pl. `public string Allapot => Kolcsonozve ? "kölcsönözve" : "a polcon";`) a *forrás* tulajdonság
setterében a számítottról is értesítést kell küldeni (`nameof(Allapot)`).

### DataGrid

```xml
<DataGrid ItemsSource="{Binding Konyvek}" SelectedItem="{Binding Kivalasztott, Mode=TwoWay}"
          AutoGenerateColumns="False" CanUserAddRows="False" IsReadOnly="True">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Cím" Binding="{Binding Cim}" Width="*"/>
        <DataGridTextColumn Header="Év" Binding="{Binding KiadasEve}" Width="60"/>
        <DataGridCheckBoxColumn Header="Kölcsönözve" Binding="{Binding Kolcsonozve}"/>
    </DataGrid.Columns>
</DataGrid>
```

- `AutoGenerateColumns="True"` (alapértelmezés) minden publikus tulajdonságból oszlopot készít, a tulajdonság nevével fejlécként – gyors, de nem szép.
- `CanUserAddRows="False"` eltünteti az üres „új sor” sort; `IsReadOnly="True"` letiltja a cellák szerkesztését.
- Oszlopszélesség: fix szám, `Auto` vagy `*` (mint a Gridnél).

### Tipikus hibák

- **Nem jelenik meg semmi, és nincs hibaüzenet**: a kötési hibák csendesek! Nézd meg Visual Studióban az *Output* ablakot (*System.Windows.Data Error…*), vagy a *XAML Binding Failures* ablakot. Gyakori okok: elírt tulajdonságnév, **mező** tulajdonság helyett (`public List<T> Lista;` – hiányzik a `{ get; set; }`), nem publikus osztály vagy tulajdonság, elmaradt `DataContext = this;`.
- **Kódból módosítom az objektumot, de a felület nem frissül**: hiányzik az `INotifyPropertyChanged`, vagy rossz nevet adtál a `PropertyChangedEventArgs`-nak (használd a `nameof`-ot).
- **Új elem nem jelenik meg a listában**: `List<T>`-t kötöttél, nem `ObservableCollection<T>`-t.
- **„Items collection must be empty before using ItemsSource”**: egyszerre használtad az `Items.Add`-ot és az `ItemsSource`-t.
- **A TextBox értéke csak kilépéskor kerül az objektumba**: ez az alapértelmezés; gépelés közbeni frissítéshez `UpdateSourceTrigger=PropertyChanged`.
- **Tizedes számok**: a kötések alapból angol formátumot használnak, ezért a `double` tulajdonsághoz kötött mezőbe tizedes**pontot** kell írni (`553.3`); a hibás bevitelt piros keret jelzi, és az érték nem kerül az objektumba.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/BindingElemekKozott](peldak/BindingElemekKozott/) | `ElementName`-es kötések mögöttes kód nélkül: Slider ⇄ TextBox (`TwoWay`, `UpdateSourceTrigger`), `StringFormat`, egy forrás több céllal, `IsChecked` → `IsEnabled`, `SelectedItem.Content` → háttérszín |
| [peldak/DataContextSzemelyek](peldak/DataContextSzemelyek/) | `DataContext = this`, `ObservableCollection<Szemely>`, `ListBox` `ItemTemplate`-tel, részletező panel a `SelectedItem`-re kötve, `INotifyPropertyChanged` (Születésnap gomb), új elem és törlés, ugyanaz a gyűjtemény `DataGrid`-ben |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. A *DataContextSzemelyek* példában írd át egy nevet, és figyeld, hogyan változik egyszerre a lista és a táblázat!

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M02-L02-F01 | [Szövegformázó – kötések kód nélkül](feladatok/F01-szovegformazo/) | ★☆☆ | [megoldás](megoldasok/F01-szovegformazo/) |
| 14-M02-L02-F02 | [Termékkatalógus ListBoxszal](feladatok/F02-termekkatalogus/) | ★★☆ | [megoldás](megoldasok/F02-termekkatalogus/) |
| 14-M02-L02-F03 | [Könyvtár DataGriddel](feladatok/F03-konyvtar-datagrid/) | ★★★ | [megoldás](megoldasok/F03-konyvtar-datagrid/) |

## Kapcsolódó

- Előző lecke: 14-M02-L01 (WPF alapok: XAML, elrendezés, vezérlők)
- Következő lecke: 14-M02-L03 (Integrált feladat: CSV-ből WPF-felület)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
