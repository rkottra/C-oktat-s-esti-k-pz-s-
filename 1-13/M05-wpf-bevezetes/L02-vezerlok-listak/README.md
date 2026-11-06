---
id: 13-M05-L02
cim: Vezérlők és listák
evfolyam: 1-13
modul: M05 – Grafikus alkalmazások – WPF bevezetés
orak: 57–60.
het:
oraszam: 4
elofeltetel: 13-M05-L01
leiras: Alapvezérlők (TextBox, ListBox, ComboBox, CheckBox), elemek hozzáadása, törlése, kiválasztás kezelése.
---

# 13-M05-L02 · Vezérlők és listák

## Célok

A lecke végére a tanuló:

- űrlapot készít `TextBox`, `ComboBox`, `CheckBox`, `RadioButton` és `GroupBox` vezérlőkkel, és kiolvassa az értéküket;
- `ListBox`-ba elemet vesz fel, töröl, áthelyez, és kezeli a kijelölést (`SelectedIndex`, `SelectedItem`);
- saját osztályú **objektumokat** tesz a listába, és a kijelölt objektum adatait megjeleníti;
- az események (`Click`, `TextChanged`, `SelectionChanged`) alapján a felület állapotát egy `Frissit()` metódusban tartja karban (gombok engedélyezése, számlálók);
- a lista tartalmát fájlba menti és onnan betölti (`SaveFileDialog`, `OpenFileDialog`).

## Elmélet

### Beviteli vezérlők

| Vezérlő | Mire való | Az érték kiolvasása |
|---|---|---|
| `TextBox` | szabad szöveg | `.Text` (mindig `string`) |
| `ComboBox` | választás egy lenyíló listából | `.SelectedIndex` (`-1`, ha nincs), `.SelectedItem` |
| `CheckBox` | igen/nem, egymástól független | `.IsChecked == true` |
| `RadioButton` | egy a csoportból (egy panelen belül) | `.IsChecked == true` |
| `GroupBox` | keret felirattal, csoportosításhoz | – (tartalma egy panel) |

Az `IsChecked` típusa **`bool?`** (igen / nem / határozatlan), ezért nem írhatjuk `if (TankonyvCheck.IsChecked)` alakban; helyesen `if (TankonyvCheck.IsChecked == true)`.

A `ComboBox` elemei megadhatók XAML-ben (`<ComboBoxItem Content="…"/>`) vagy kódból (`TanfolyamComboBox.Items.Add("…")`). XAML-elemnél a `SelectedItem` egy `ComboBoxItem`, a szöveg a `Content`-jében van; kódból felvett szövegnél maga a szöveg.

### A ListBox

```csharp
NevekLista.Items.Add("Anna");            // hozzáadás a végére
NevekLista.Items.Insert(0, "Bence");     // beszúrás adott helyre
NevekLista.Items.Remove("Anna");         // törlés érték szerint
NevekLista.Items.RemoveAt(2);            // törlés index szerint
NevekLista.Items.Clear();                // minden elem törlése
int db = NevekLista.Items.Count;         // elemszám
bool van = NevekLista.Items.Contains("Anna");

int index = NevekLista.SelectedIndex;    // -1, ha nincs kijelölés
object? elem = NevekLista.SelectedItem;  // null, ha nincs kijelölés
NevekLista.SelectedIndex = 0;            // kijelölés kódból
```

- Az `Items` gyűjteménybe **bármilyen objektum** tehető. A lista az objektum `ToString()`-jét jeleníti meg, ezért saját osztálynál írjuk felül.
- A `SelectedItem` típusa `object`. Az `is` mintával egyszerre ellenőrzünk és alakítunk: `if (Lista.SelectedItem is Jelentkezes j) { … j.Nev … }`.
- Az `Items`-et nem módosíthatjuk, miközben `foreach`-csel bejárjuk. Előbb másoljuk le: `List<string> elemek = Lista.Items.Cast<string>().ToList();`
- Többes kijelöléshez `SelectionMode="Extended"`; ilyenkor a `SelectedItems` gyűjtemény tartalmazza a kijelölteket.

### Események és a Frissit() minta

| Esemény | Mikor fut le |
|---|---|
| `Click` | gombnyomás |
| `TextChanged` | a `TextBox` minden karakterének változásakor |
| `SelectionChanged` | a `ListBox` / `ComboBox` kijelölésének változásakor |
| `Checked`, `Unchecked` | a `CheckBox` / `RadioButton` bejelölésekor, illetve a jelölés megszüntetésekor |
| `KeyDown` | billentyű lenyomásakor (`e.Key == Key.Enter`) |

Az a jó gyakorlat, hogy **minden** állapotfüggő beállítást (gombok `IsEnabled`-je, darabszámok, összegek) egyetlen `Frissit()` metódus számol ki, és minden eseménykezelő a végén ezt hívja. Így nem fordulhat elő, hogy egy törlés után a „Töröl” gomb engedélyezett marad, holott már nincs mit törölni.

```csharp
private void Frissit()
{
    TorlesGomb.IsEnabled = NevekLista.SelectedIndex != -1;
    HozzaadGomb.IsEnabled = NevTextBox.Text.Trim() != "";
    DarabLabel.Content = $"Elemek: {NevekLista.Items.Count}";
}
```

### Párbeszédablakok

```csharp
MessageBoxResult v = MessageBox.Show("Biztosan törlöd?", "Törlés",
                                     MessageBoxButton.YesNo, MessageBoxImage.Question);
if (v == MessageBoxResult.Yes) { … }

SaveFileDialog mentes = new SaveFileDialog { Filter = "Szövegfájl|*.txt", FileName = "lista.txt" };
if (mentes.ShowDialog() == true)                         // bool?: true, ha a felhasználó a Mentés gombot nyomta
    File.WriteAllLines(mentes.FileName, NevekLista.Items.Cast<string>());
```

A `SaveFileDialog` és az `OpenFileDialog` a `Microsoft.Win32` névtérben van, a `File` osztályhoz pedig WPF-projektben `using System.IO;` kell.

### Tipikus hibák

- **`InvalidCastException` a `(Jelentkezes)Lista.SelectedItem`-nél**: nincs kijelölés (`null`), vagy más típusú elem van a listában. Használd az `is` mintát.
- **A törlés után eltűnik a kijelölés, és a részletek panel „beragad”**: a `SelectionChanged` törléskor is lefut, ilyenkor a `SelectedItem` `null`. A `Frissit()` kezelje ezt is.
- **`if (CheckBox.IsChecked)` nem fordul**: `bool?` értéket nem lehet közvetlenül feltételnek használni, ezért `== true` kell.
- **Az Enter nem nyom gombot**: a gombnak `IsDefault="True"` kell (az Esc-hez `IsCancel="True"`).
- **Kétszer is felkerül ugyanaz az elem**: hozzáadás előtt ellenőrizd az `Items.Contains`-szal.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KetLista](peldak/KetLista/) | két `ListBox` közös segédmetódusokkal (`Hozzaad`, `Torol`, `Athelyez`), elemek átpakolása egyesével és mind, `TextChanged` és `SelectionChanged`, `Frissit()`, mentés és betöltés `SaveFileDialog`/`OpenFileDialog`-gal |
| [peldak/TanfolyamJelentkezes](peldak/TanfolyamJelentkezes/) | űrlap `TextBox`-szal, `ComboBox`-szal, `RadioButton`- és `CheckBox`-csoporttal; `Jelentkezes` objektumok a `ListBox`-ban, a kijelölt elem részletei, törlés megerősítéssel |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, és indítsd el (F5).

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M05-L02-F01 | [Teendőlista](feladatok/F01-teendolista/) | ★☆☆ | [megoldás](megoldasok/F01-teendolista/) |
| 13-M05-L02-F02 | [Névsor kereséssel és szűréssel](feladatok/F02-nevsor-kereses/) | ★★☆ | [megoldás](megoldasok/F02-nevsor-kereses/) |
| 13-M05-L02-F03 | [Webshop-kosár](feladatok/F03-kosar/) | ★★★ | [megoldás](megoldasok/F03-kosar/) |

## Kapcsolódó

- Előző lecke: 13-M05-L01 (WPF alapok: XAML, elrendezés – Számológép)
- Következő lecke: 13-M05-L03 (Dinamikus felület – Aknakereső)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
