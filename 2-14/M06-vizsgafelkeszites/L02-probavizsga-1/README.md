---
id: 14-M06-L02
cim: 1. próbavizsga: CSV → MySQL → WPF
evfolyam: 2-14
modul: M06 – Vizsgafelkészítés
orak: 69–72.
het:
oraszam: 4
elofeltetel: 14-M06-L01
leiras: Teljes feladatsor időre: adatbázis, EF Core, WPF-felület CRUD-dal (Pilots).
---

# 14-M06-L02 · 1. próbavizsga: CSV → MySQL → WPF

## Célok

A lecke végére a tanuló:

- időre végigviszi a vizsga grafikus részének teljes folyamatát: SQL-szkript importálása → EF Core-kapcsolat → WPF-felület;
- meglévő táblához entitásosztályt köt (`[Table]`/`[Column]` attribútummal vagy `Scaffold-DbContext`-tel);
- reszponzív főablakot épít táblázattal és beviteli űrlappal;
- új rekordot vesz fel ellenőrzéssel, kijelölt rekordot töröl megerősítéssel;
- a javítókulcs alapján pontozza a saját megoldását.

## Elmélet

### A grafikus rész lépései (és ajánlott időbeosztás, 120 perc)

| Perc | Lépés | Ellenőrzés |
|---|---|---|
| 0–10 | XAMPP: MySQL indítása, SQL-szkript importálása (phpMyAdmin → *Importálás*) | a táblában látszanak a sorok |
| 10–30 | kiinduló projekt megnyitása, NuGet-csomagok, entitás + `DbContext` | egy próba-`Count()` lefut |
| 30–55 | XAML: elrendezés, vezérlők, feliratok | átméretezésre jól viselkedik |
| 55–70 | adatok betöltése a táblázatba | minden sor és oszlop látszik |
| 70–100 | új rekord: ellenőrzések, mentés, frissítés | hibás és jó adattal is kipróbálva |
| 100–115 | törlés megerősítéssel | „Nem” válaszra nem töröl |
| 115–120 | átnézés: ablakcím, feliratok, felesleges kód | |

### Adatbázis előkészítése

- **SQL-szkript**: phpMyAdmin → *Importálás* → fájl → *Indítás*. A szkript maga hozza létre az adatbázist (`CREATE DATABASE … utf8mb4_hungarian_ci`).
- **CSV-ből** (ha csak CSV van): előbb a tábla létrehozása, majd phpMyAdmin → tábla → *Importálás* → formátum: CSV, *Oszlopelválasztó*: `;`, *Kihagyott sorok*: 1. Importálás után mindig nézd meg a hiányzó értékeket (pl. a `null` szövegből lett-e valódi NULL vagy hibás dátum) – ezért kényelmesebb, ha van SQL-szkript.

### Entitás meglévő táblához – két út

| | Kézi entitás attribútumokkal | `Scaffold-DbContext` (Database First) |
|---|---|---|
| Mikor? | kevés tábla, magyar tulajdonságnevek kellenek | sok tábla / kapcsolat, gyorsaság |
| Hogyan? | `[Table("pilots")]`, `[Column("name")] public string Nev …` | Package Manager Console-ban egy parancs |
| Migráció | **nem kell** (az adatbázis már létezik) | nem kell |

```csharp
[Table("pilots")]
public class Pilota
{
    [Column("id")] public int Id { get; set; }
    [Column("name")] public string Nev { get; set; } = "";
    [Column("birthdate")] public DateTime? SzuletesiDatum { get; set; }   // NULL → nullable!
}
```

```
Scaffold-DbContext "server=localhost;database=estis_f1pilots;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Context PilotakContext
```

> Ha a táblát az SQL-szkript hozta létre, **ne** futtass `Add-Migration`/`Update-Database` parancsot – az a Code First módszerhez kell (14-M03-L01).

### A felület váza

- **Reszponzivitás**: a fő `Grid` oszlopai csillagos szélességűek (`2*` és `*`), a táblázat `DockPanel` utolsó elemeként kitölti a maradék helyet. Fix `Width`/`Height` csak gombokon és kis mezőkön legyen.
- **DataGrid**: `AutoGenerateColumns="False"`, saját oszlopok magyar fejlécekkel, `IsReadOnly="True"`, `CanUserAddRows="False"`.
- **Dátum**: `Binding="{Binding SzuletesiDatum, StringFormat=yyyy.MM.dd}"`.
- **Darabszám**: `Text="{Binding Pilotak.Count, StringFormat=Pilóták száma: {0}}"` – az `ObservableCollection` jelzi a `Count` változását.

### Nézetmodell (ViewModel) – egyszerűen

```csharp
public class PilotakViewModel
{
    private readonly PilotakContext db = new PilotakContext();
    public ObservableCollection<Pilota> Pilotak { get; } = new();

    public void Betoltes() { Pilotak.Clear(); foreach (var p in db.Pilotak) Pilotak.Add(p); }
    public void Hozzaad(Pilota uj) { db.Pilotak.Add(uj); db.SaveChanges(); Betoltes(); }
    public void Torol(Pilota p)    { db.Pilotak.Remove(p); db.SaveChanges(); Betoltes(); }
}
```

Az ablak konstruktorában: `DataContext = new PilotakViewModel();`, a XAML-ben `ItemsSource="{Binding Pilotak}"`.

### Ellenőrzés (validáció) mintája

```csharp
if (txtNev.Text.Trim() == "")
{
    MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
    return;                                   // ← enélkül hibás adat is mentésre kerülne
}
if (dpSzuletes.SelectedDate is not DateTime d || d >= DateTime.Today) { … }
if (cbNem.SelectedItem is not ComboBoxItem elem) { … }   // nincs kiválasztva
```

Törlés megerősítéssel:

```csharp
if (MessageBox.Show($"Biztosan törlöd: {p.Nev}?", "Megerősítés",
        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) { … }
```

### Tipikus hibák

- **„Access denied for user 'root'”**: a kapcsolati szövegben hiányzik vagy rossz a jelszó (XAMPP-ban alapból üres: `password=`).
- **„Table 'estis_f1pilots.pilota' doesn't exist”**: az osztály neve alapján keresi a táblát – hiányzik a `[Table("pilots")]`.
- **`InvalidCastException` / `Unable to cast DateTime`**: a nullable oszlophoz nem nullable tulajdonság tartozik (`DateTime` helyett `DateTime?`).
- **Üres táblázat**: elmaradt a `DataContext` beállítása, vagy elírt a `Binding` útvonal (a *Output* ablakban látszik a binding-hiba).
- **A mentett sor újraindítás után eltűnik**: hiányzik a `SaveChanges()`.
- **A felvitt nem `Férfi` szöveg lesz `M` helyett**: a `ComboBoxItem.Content`-et mentetted a `Tag` helyett – és a CHAR(1) oszlopba nem fér bele.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/NemzetiParkokWpf](peldak/NemzetiParkokWpf/) | kis, egytáblás bemutató a teljes folyamatra: `nemzeti_parkok.sql` → entitás `[Table]`/`[Column]` attribútumokkal → `DbContext` → reszponzív ablak `DataGrid`-del, felvétel ellenőrzéssel, törlés megerősítéssel |

Futtatás: phpMyAdminban importáld a `peldak/NemzetiParkokWpf/nemzeti_parkok.sql` fájlt (létrehozza az `estis_nemzetiparkok` adatbázist), majd F5. Migráció nem kell.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M06-L02-F01 | [1. próbavizsga: F1-pilóták – adatbázis és WPF-felület (120 perc, 50 pont)](feladatok/F01-pilotak-wpf/) | ★★☆ | [megoldás](megoldasok/F01-pilotak-wpf/) |
| 14-M06-L02-F02 | [Szűrés a pilóták között](feladatok/F02-pilotak-szures/) | ★★★ | [megoldás](megoldasok/F02-pilotak-szures/) |

## Kapcsolódó

- Előző lecke: 14-M06-L01 (Konzolos vizsgafeladat)
- Következő lecke: 14-M06-L03 (2. próbavizsga: több tábla, szűrések)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
