---
id: 14-M02-L01
cim: WPF alapok: XAML, elrendezés, vezérlők
evfolyam: 2-14
modul: M02 – WPF alkalmazások
orak: 9–12.
het:
oraszam: 4
elofeltetel: 14-M01-L02
leiras: XAML szerkezet, Grid és StackPanel, alapvezérlők és eseménykezelés.
---

# 14-M02-L01 · WPF alapok: XAML, elrendezés, vezérlők

## Célok

A lecke végére a tanuló:

- létrehoz egy WPF-projektet (.NET 8), és eligazodik a fájljai között (`App.xaml`, `MainWindow.xaml`, `MainWindow.xaml.cs`);
- XAML-ben felépít egy ablakot `Grid` és `StackPanel` segítségével, sorokkal, oszlopokkal, margókkal, igazítással;
- használja az alapvezérlőket (`Label`, `TextBox`, `Button`, `CheckBox`, `RadioButton`, `ComboBox`, `ListBox`, `GroupBox`);
- eseménykezelőt ír (`Click`, `TextChanged`, `SelectionChanged`), és a mögöttes kódból olvassa és írja a vezérlőket;
- `MessageBox`-szal üzen és kérdez, és ellenőrzi a felhasználó által beírt adatokat.

## Elmélet

### Mi a WPF?

A **WPF** (*Windows Presentation Foundation*) a .NET keretrendszere asztali, grafikus felületű alkalmazásokhoz.
A Windows Forms utódja; főbb jellemzői:

- a felületet egy XML-alapú leírónyelven, **XAML**-ben írjuk le, a működést C#-ban – a kinézet és a logika szétválik;
- **vektoros** rajzolás és **eszközfüggetlen egység** (DIP, 1/96 hüvelyk): az ablak minden kijelzőn ugyanakkorának látszik;
- az elemek **elrendező panelekbe** kerülnek, amelyek átméretezéskor maguk számolják újra a helyet (nincsenek fix koordináták);
- erős **adatkötés** (Binding) – ezzel a következő lecke foglalkozik.

Új projekt Visual Studióban: *Create a new project → **WPF Application*** (nem a „WPF App (.NET Framework)” változat!), keretrendszer: **.NET 8**.
A projektfájlban ez látszik:

```xml
<TargetFramework>net8.0-windows</TargetFramework>
<UseWPF>true</UseWPF>
```

| Fájl | Szerepe |
|---|---|
| `App.xaml` (+ `.cs`) | az alkalmazás; a `StartupUri` mondja meg, melyik ablak nyílik meg induláskor |
| `MainWindow.xaml` | a főablak **kinézete** (XAML) |
| `MainWindow.xaml.cs` | a főablak **mögöttes kódja** (*code-behind*): eseménykezelők, logika |

### A XAML szerkezete

A XAML minden eleme egy .NET-osztály példánya, az attribútumai a tulajdonságai:

```xml
<Button Name="MentesGomb" Content="Mentés" Width="100" Margin="5" Click="MentesGomb_Click"/>
```

Ez nagyjából annyi, mintha C#-ban ezt írnánk: `var MentesGomb = new Button { Content = "Mentés", Width = 100, ... };`

- **`Name`** – ezen a néven érjük el a vezérlőt a mögöttes kódból (`MentesGomb.IsEnabled = false;`).
- **Tulajdonságelem-szintaxis** – ha egy érték nem fér el egy attribútumban: `<Grid.RowDefinitions>…</Grid.RowDefinitions>`, `<GroupBox.Header>…</GroupBox.Header>`.
- **Csatolt tulajdonság** – a szülő panel „kér” adatot a gyerekelemtől: `Grid.Row="1"`, `Grid.Column="2"`, `DockPanel.Dock="Top"`.
- **Tartalom** – a `Window`-nak, `Button`-nek, `Label`-nek **egyetlen** tartalma (`Content`) lehet; több elemhez panel kell.
- A `MainWindow` konstruktorában az `InitializeComponent()` építi fel a XAML-ben leírt felületet: előtte a vezérlők még nem léteznek.

### Elrendezés: panelek

| Panel | Mire való |
|---|---|
| `Grid` | sorok és oszlopok rácsa – **a leggyakoribb**, ablakszintű elrendezéshez |
| `StackPanel` | egymás alá (vagy `Orientation="Horizontal"`: egymás mellé) rakja az elemeket – űrlapokhoz |
| `DockPanel` | a széleihez tapaszt (`Top`, `Bottom`, `Left`, `Right`), az utolsó elem kitölti a maradékot |
| `WrapPanel` | sorba rakja, és ha nem fér ki, új sorba tördel |
| `UniformGrid` | egyforma méretű cellák (pl. billentyűzet) |
| `Canvas` | fix koordináták (`Canvas.Left`, `Canvas.Top`) – rajzokhoz; űrlaphoz **ne** használd |

A **Grid** sor- és oszlopméretei:

| Érték | Jelentés |
|---|---|
| `Height="50"` | fix 50 egység |
| `Height="Auto"` | akkora, amekkora a tartalom |
| `Height="*"` | a maradék hely; a `2*` kétszer annyit kap, mint az `*` |

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="*"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="1*"/>
        <ColumnDefinition Width="3*"/>
    </Grid.ColumnDefinitions>
    <Label Grid.ColumnSpan="2" Content="Cím"/>         <!-- 0. sor, mindkét oszlop -->
    <ListBox Grid.Row="1" Grid.Column="0"/>
    <StackPanel Grid.Row="1" Grid.Column="1"> … </StackPanel>
</Grid>
```

A sorok és oszlopok számozása **0-tól** indul; ha nem adod meg, az elem a 0. sorba és a 0. oszlopba kerül.
Cellák összevonása: `Grid.ColumnSpan`, `Grid.RowSpan`.

**Méretezés és igazítás** (minden elemen):

| Tulajdonság | Példa | Megjegyzés |
|---|---|---|
| `Width`, `Height` | `Width="100"` | fix méret – csak ha tényleg kell |
| `MinWidth`, `MaxWidth`, … | `MinWidth="150"` | a fix méret helyett ajánlott |
| `Margin` | `"5"` / `"5,0"` / `"5,10,5,0"` | **külső** távolság: mindenhol / bal-jobb és fent-lent / bal, fent, jobb, lent |
| `Padding` | `"10,2"` | **belső** távolság a szegély és a tartalom között |
| `HorizontalAlignment` | `Left`, `Center`, `Right`, `Stretch` | az elem helye a rendelkezésre álló területen |
| `VerticalAlignment` | `Top`, `Center`, `Bottom`, `Stretch` | |
| `HorizontalContentAlignment` | `Right` | a **tartalom** igazítása az elemen belül (pl. számok a TextBoxban) |
| `Visibility` | `Visible`, `Hidden`, `Collapsed` | a `Hidden` helyet foglal, a `Collapsed` nem |

### Alapvezérlők

| Vezérlő | Fontos tulajdonságok | Fontos esemény |
|---|---|---|
| `Label` | `Content` | – |
| `TextBlock` | `Text`, `TextWrapping` | – (csak megjelenítés, egyszerűbb a Labelnél) |
| `TextBox` | `Text`, `IsReadOnly`, `MaxLength` | `TextChanged` |
| `Button` | `Content`, `IsEnabled`, `IsDefault` (Enter), `IsCancel` (Esc) | `Click` |
| `CheckBox` | `IsChecked` (**`bool?`**) | `Checked`, `Unchecked` |
| `RadioButton` | `IsChecked`, `GroupName` | `Checked` |
| `ComboBox` | `Items`, `SelectedItem`, `SelectedIndex` | `SelectionChanged` |
| `ListBox` | `Items`, `SelectedItem`, `SelectedIndex`, `SelectionMode` | `SelectionChanged`, `MouseDoubleClick` |
| `GroupBox` | `Header` | – (keretez, csoportosít) |
| `TabControl` / `TabItem` | `Header` | `SelectionChanged` |

- Az egy panelben lévő `RadioButton`-ok automatikusan egy csoportot alkotnak; több csoporthoz `GroupName` kell.
- Az `IsChecked` típusa `bool?`, ezért így vizsgáljuk: `if (HirlevelCheck.IsChecked == true)`.
- `SelectedIndex = -1` → nincs kiválasztott elem; a `SelectedItem` ilyenkor `null`.
- Minden vezérlőnek van `Tag` tulajdonsága, amelybe tetszőleges adatot tehetünk (pl. a gomb számjegyét, egy termék árát).

### Események

A XAML-ben az eseményhez egy metódusnevet rendelünk; a Visual Studio legenerálja a mögöttes kódba (a XAML-ben a metódusnéven *F12*: odaugrik):

```xml
<Button Name="JelentkezesGomb" Content="Jelentkezés" Click="JelentkezesGomb_Click"/>
```

```csharp
private void JelentkezesGomb_Click(object sender, RoutedEventArgs e)
{
    if (string.IsNullOrWhiteSpace(NevMezo.Text))
    {
        MessageBox.Show("Add meg a neved!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }
    KivalasztottCimke.Content = $"Üdv, {NevMezo.Text}!";
}
```

- A **`sender`** az az elem, amelyik az eseményt kiváltotta. Ha több gomb ugyanazt a kezelőt használja (pl. számológép),
  a `sender`-ből tudjuk meg, melyikre kattintottak: `var gomb = (Button)sender;`.
- A `MessageBox.Show` visszaadja, melyik gombot nyomták meg:
  ```csharp
  if (MessageBox.Show("Biztosan törlöd?", "Megerősítés", MessageBoxButton.YesNo) == MessageBoxResult.Yes) { … }
  ```
- Számot a `TextBox`-ból mindig **`int.TryParse`**-szal olvass: a felhasználó bármit beírhat.

### Tipikus hibák

- **Eseménykezelő törlése csak a .cs fájlból**: a XAML-ben maradt `Click="..."` miatt a projekt nem fordul („does not contain a definition for…”). Mindkét helyről töröld!
- **`NullReferenceException` induláskor**: egy `TextChanged`/`SelectionChanged` kezelő már az `InitializeComponent` közben lefut, és egy *később* definiált vezérlőre hivatkozik, ami még `null`. Ilyenkor ellenőrizd: `if (Kijelzo == null) return;`.
- **„The property 'Content' is set more than once”**: egy `Button`/`Window` közvetlen tartalmának két elemet adtál – tedd őket egy panelbe.
- **Minden elem egymáson van**: a `Grid`-ben elmaradt a `Grid.Row` / `Grid.Column` megadása.
- **Fix `Width`/`Height` mindenhol**: átméretezéskor szétesik a felület. Használj `*`, `Auto`, `MinWidth` értékeket és margót.
- **Windows Forms-szokás**: az elemek húzogatása a tervezőben `Margin="312,148,0,0"` jellegű abszolút pozíciókat eredményez. Inkább írd kézzel a XAML-t.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/WpfAlapokUrlap](peldak/WpfAlapokUrlap/) | `Grid` sorokkal és oszlopokkal, `StackPanel`-es űrlap, `ListBox`, `RadioButton`-csoport, `CheckBox`; `Click`, `TextChanged`, `SelectionChanged`, adatellenőrzés és `MessageBox` |
| [peldak/WpfPanelek](peldak/WpfPanelek/) | a hat elrendező panel egy-egy fülön (`TabControl`): StackPanel, WrapPanel, DockPanel, Grid (`Auto`/`*`, span, `GridSplitter`), UniformGrid, Canvas |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. Méretezd át az ablakot, és figyeld az elrendezést!

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M02-L01-F01 | [Pizzarendelő űrlap](feladatok/F01-pizzarendeles/) | ★☆☆ | [megoldás](megoldasok/F01-pizzarendeles/) |
| 14-M02-L01-F02 | [Számológép Grid-elrendezéssel](feladatok/F02-szamologep/) | ★★☆ | [megoldás](megoldasok/F02-szamologep/) |
| 14-M02-L01-F03 | [Bevásárlólista két listával](feladatok/F03-bevasarlolista/) | ★★☆ | [megoldás](megoldasok/F03-bevasarlolista/) |

## Kapcsolódó

- Előző lecke: 14-M01-L02 (Fájlkezelés és LINQ)
- Következő lecke: 14-M02-L02 (Adatkötés és DataContext)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
