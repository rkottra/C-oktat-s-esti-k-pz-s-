---
id: 13-M05-L01
cim: WPF alapok: XAML, elrendezés – Számológép
evfolyam: 1-13
modul: M05 – Grafikus alkalmazások – WPF bevezetés
orak: 53–56.
het:
oraszam: 4
elofeltetel: 13-M04-L04
leiras: A WPF felépítése, XAML, ablak, Grid és StackPanel, gombok és eseménykezelés egy számológép elkészítésén keresztül.
---

# 13-M05-L01 · WPF alapok: XAML, elrendezés – Számológép

## Célok

A lecke végére a tanuló:

- elmagyarázza, miben más egy grafikus (WPF) program, mint a konzolos: eseményekre reagál, nem sorról sorra fut le;
- létrehoz egy WPF-projektet (.NET 8), és tudja, mire valók a fájljai (`App.xaml`, `MainWindow.xaml`, `MainWindow.xaml.cs`);
- XAML-ben felépít egy ablakot `Grid` és `StackPanel` segítségével (sorok, oszlopok, összevonás, margó, igazítás);
- gombra kattintásra eseménykezelőt futtat, a vezérlőket a nevükkel éri el és módosítja;
- egy eseménykezelőt több gombhoz is használ (`sender`), és működő számológépet készít.

## Elmélet

### Konzol helyett ablak

Az eddigi programjaink felülről lefelé futottak le: kiírtak, bekértek, számoltak, majd véget értek. Egy grafikus program másképp működik:
elindul, megjeleníti az ablakot, és **vár**. Amikor a felhasználó csinál valamit (kattint, gépel), bekövetkezik egy **esemény**,
és lefut a hozzá rendelt metódus, az **eseménykezelő**. Utána a program újra vár.

A **WPF** (*Windows Presentation Foundation*) a .NET grafikus felülete Windowsra:

- a felület kinézetét egy XML-alapú leírónyelven, **XAML**-ben írjuk le, a működést C#-ban – a kettő külön fájlban van;
- a méreteket nem képpontban, hanem **eszközfüggetlen egységben** adjuk meg (1/96 hüvelyk), ezért az ablak minden kijelzőn ugyanakkora;
- az elemek **elrendező panelekbe** kerülnek, amelyek átméretezéskor maguk számolják újra a helyet – nincsenek fix koordináták.

### Új WPF-projekt

Visual Studio: *Create a new project* → **WPF Application** (C#, *nem* a „.NET Framework” változat) → keretrendszer: **.NET 8**.

| Fájl | Szerepe |
|---|---|
| `App.xaml` (+ `.cs`) | maga az alkalmazás; a `StartupUri="MainWindow.xaml"` mondja meg, melyik ablak nyílik meg induláskor |
| `MainWindow.xaml` | a főablak **kinézete** |
| `MainWindow.xaml.cs` | a főablak **mögöttes kódja** (*code-behind*): az eseménykezelők |
| `*.csproj` | a projekt beállításai: `net8.0-windows`, `<UseWPF>true</UseWPF>` |

A kettő összetartozik: a XAML-ben `x:Class="Szamologep.MainWindow"` áll, a C#-ban `public partial class MainWindow : Window`.
A `partial` azt jelenti, hogy az osztály egyik fele a XAML-ből generálódik, a másikat mi írjuk.

### A XAML szerkezete

```xml
<Window x:Class="ElsoAblak.MainWindow" ... Title="Első WPF ablakom" Height="300" Width="420">
    <Grid>
        <Button Name="UdvozlesGomb" Content="Üdvözlés" Margin="3" Click="UdvozlesGomb_Click"/>
    </Grid>
</Window>
```

- Minden **elem** (tag) egy WPF-osztály példánya: a `<Button>` egy `Button` objektum. Az elemek fát alkotnak, a gyökér a `Window`.
- Az **attribútumok** a tulajdonságokat állítják be: `Content="Üdvözlés"` ugyanaz, mint C#-ban a `gomb.Content = "Üdvözlés";`.
- Tartalom nélküli elem rövidíthető: `<Button ... />`.
- A **`Name`** tulajdonsággal kap nevet az elem; ezen a néven érjük el a mögöttes kódból (`EredmenyLabel.Content = "Szia!";`).
- A XAML **kis- és nagybetűérzékeny**, és a speciális karaktereket kódolni kell: `<` → `&lt;`, `>` → `&gt;`, `&` → `&amp;`.
- **Tulajdonságelem**: ha egy érték nem fér egy attribútumba, külön elembe írjuk: `<Grid.RowDefinitions>…</Grid.RowDefinitions>`.
- **Csatolt tulajdonság**: a szülő panel „kér” adatot a gyerektől: `<Button Grid.Row="2" Grid.Column="1"/>` – a gombnak nincs `Row` tulajdonsága, a `Grid`-nek kell tudnia, hova tegye.
- A `Window` (és a `Button`, `Label`) tartalma **egyetlen** elem lehet. Ha több kell, egy panelbe tesszük őket.

### Elrendezés: Grid és StackPanel

A **`Grid`** sorok és oszlopok rácsa – ez a leggyakoribb elrendezés. Először megadjuk a sorokat és oszlopokat, majd minden elemnél, hogy hányadik cellába kerül (a számozás **0-tól** indul).

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>   <!-- akkora, amekkora a tartalom -->
        <RowDefinition Height="*"/>      <!-- a maradék hely -->
        <RowDefinition Height="2*"/>     <!-- kétszer annyi, mint a * -->
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="100"/>  <!-- fix 100 egység -->
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>

    <Label Grid.Row="0" Grid.Column="0" Content="Név:"/>
    <TextBox Grid.Row="0" Grid.Column="1"/>
    <Label Grid.Row="1" Grid.ColumnSpan="2" Content="Két oszlopon át"/>
</Grid>
```

- `Grid.ColumnSpan` / `Grid.RowSpan`: az elem több oszlopon / soron át ér (cellaösszevonás).
- Fejlesztés közben a `ShowGridLines="True"` láthatóvá teszi a rács vonalait.
- A **`StackPanel`** egymás alá (vagy `Orientation="Horizontal"` esetén egymás mellé) rakja az elemeit – például egy sor gombhoz vagy egy egyszerű űrlaphoz. Egy Grid-cellába is tehetünk StackPanelt.

| Tulajdonság | Példa | Jelentés |
|---|---|---|
| `Margin` | `"5"` / `"5,0"` / `"5,10,5,0"` | **külső** távolság: mindenhol / bal-jobb és fent-lent / bal, fent, jobb, lent |
| `Padding` | `"10,2"` | **belső** távolság a keret és a tartalom között |
| `HorizontalAlignment` | `Left`, `Center`, `Right`, `Stretch` | az elem helye a cellán belül (alapérték: kitölti) |
| `HorizontalContentAlignment` | `Right` | a **tartalom** igazítása az elemen belül (pl. számok a kijelzőn) |
| `MinWidth`, `MinHeight` | `MinWidth="250"` | fix méret helyett inkább alsó határt adjunk |
| `IsEnabled` | `False` | letiltott, szürke vezérlő |

### Az első vezérlők

| Vezérlő | Mire való | Fontos tulajdonság |
|---|---|---|
| `Label` | felirat, kiírt eredmény | `Content` (bármi lehet, ezért `object`) |
| `TextBlock` | egyszerű szöveg | `Text` |
| `TextBox` | beviteli mező | `Text` (mindig `string`!), `IsReadOnly` |
| `Button` | gomb | `Content`, `IsEnabled`, `IsDefault` (Enter-re „megnyomódik”) |

### Eseménykezelés

A XAML-ben a `Click="UdvozlesGomb_Click"` attribútum köti a gombot a metódushoz. Ha a Visual Studióban a `Click="` után a *&lt;New Event Handler&gt;* lehetőséget választjuk, a metódust is létrehozza:

```csharp
private void UdvozlesGomb_Click(object sender, RoutedEventArgs e)
{
    if (int.TryParse(SzamTextBox.Text, out int szam))
        EredmenyLabel.Content = $"{szam} kétszerese: {szam * 2}";
    else
        MessageBox.Show("Ez nem egész szám!", "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Error);
}
```

- A `TextBox.Text` mindig szöveg – számot **`TryParse`**-szal olvasunk, mert a felhasználó bármit beírhat.
- A `MessageBox.Show(üzenet, cím, gombok, ikon)` felugró üzenetet mutat.
- Az ablak „memóriája” az osztály **mezőiben** van (`private long elsoSzam;`): ezt minden eseménykezelő látja. A metóduson belüli változó a metódus végén elvész.

### Egy kezelő – sok gomb: a `sender`

A számológép tíz számgombjához nem írunk tíz metódust. Mindegyik gomb ugyanazt a kezelőt kapja (`Click="Szamjegy_Click"`), és a **`sender`** paraméterből derül ki, melyik gombot nyomták meg:

```csharp
private void Szamjegy_Click(object sender, RoutedEventArgs e)
{
    Button gomb = (Button)sender;                 // a megnyomott gomb
    string szamjegy = gomb.Content.ToString()!;   // "7"
    Kijelzo.Text = Kijelzo.Text == "0" ? szamjegy : Kijelzo.Text + szamjegy;
}
```

A számológép állapota három mezőben van: az **első szám**, a **művelet** (`"+"`, `"-"`, …) és egy jelző, hogy a következő számjegy **új számot kezd-e**. Ha egy gomb engedélyezése az állapottól függ, azt egy közös `Frissit()` metódusban számoljuk ki, és minden kezelő végén meghívjuk.

### Tipikus hibák

- **Eseménykezelő törlése csak a .cs fájlból**: a XAML-ben maradt `Click="…"` miatt a projekt nem fordul („does not contain a definition for…”). Mindkét helyről töröld, vagy ne töröld egyiket se.
- **Minden elem egymáson van a bal felső sarokban**: elmaradt a `Grid.Row` / `Grid.Column` – alapértéke 0.
- **„The property 'Content' is set more than once”**: a `Window` vagy egy `Button` közvetlen tartalmába két elemet tettél – tedd őket egy panelbe.
- **Húzogatás a tervezőben**: a Toolboxból behúzott vezérlők `Margin="312,148,0,0"` jellegű abszolút helyet kapnak, és átméretezéskor szétesik a felület. Írd inkább kézzel a XAML-t, sorokkal és oszlopokkal.
- **`FormatException` a `int.Parse`-nál**: a felhasználó betűt vagy üres szöveget adott meg – `TryParse`-t használj.
- **`Convert.ToInt32(Kijelzo.Text)` túlcsordul**: egy hosszú számsor nem fér el az `int`-ben. Korlátozd a számjegyek számát, vagy használj `long`-ot.
- **A gomb szövege nem `string`**: a `Content` típusa `object`, ezért `.ToString()` kell, ha szövegként akarod használni.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/ElsoAblak](peldak/ElsoAblak/) | `Grid` `Auto`/`*` sorokkal, `StackPanel` a gombsorban, `ColumnSpan`; `Click`-kezelők, `TryParse`, `MessageBox` |
| [peldak/Szamologep](peldak/Szamologep/) | 4 oszlopos rács, közös eseménykezelő a szám- és a műveleti gombokhoz (`sender`), állapot mezőkben, `Frissit()`, nullával osztás kezelése |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, és indítsd el (F5).

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M05-L01-F01 | [Számláló ablak](feladatok/F01-szamlalo/) | ★☆☆ | [megoldás](megoldasok/F01-szamlalo/) |
| 13-M05-L01-F02 | [Számológép valós számokkal](feladatok/F02-szamologep-bovites/) | ★★☆ | [megoldás](megoldasok/F02-szamologep-bovites/) |
| 13-M05-L01-F03 | [Láncművelet](feladatok/F03-lancmuvelet/) | ★★★ | [megoldás](megoldasok/F03-lancmuvelet/) |

## Kapcsolódó

- Előző lecke: 13-M04-L04 (Komplex feladatok, házidolgozat)
- Következő lecke: 13-M05-L02 (Vezérlők és listák)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
