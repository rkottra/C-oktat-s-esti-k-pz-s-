---
id: 14-M04-L01
cim: Adatok megjelenítése: DataGrid, ComboBox
evfolyam: 2-14
modul: M04 – WPF + EF Core adatkezelő alkalmazás
orak: 37–40.
het:
oraszam: 4
elofeltetel: 14-M03-L04
leiras: Adatbázis-tartalom betöltése WPF-be a DbContexten keresztül, DataGrid-oszlopok, ComboBox kapcsolt táblával.
---

# 14-M04-L01 · Adatok megjelenítése: DataGrid, ComboBox

## Célok

A lecke végére a tanuló:

- WPF-alkalmazásból EF Core `DbContext`-en keresztül adatbázist ér el;
- adatbázis-tartalmat tölt be `DataGrid`-be, kézzel megadott oszlopokkal és formázással;
- `Include`-dal betölti a kapcsolt tábla adatait, és megjeleníti őket a táblázatban;
- `ComboBox`-ot tölt fel kapcsolt táblából (`DisplayMemberPath`, `SelectedValuePath`, `SelectedValue`), és szűr vele;
- a kijelölt sort részletező panelben jeleníti meg adatkötéssel.

## A modul közös példája: a könyvesbolt

A modul négy leckéje **egyetlen, fokozatosan bővülő alkalmazást** épít fel. Mindegyik lecke példaprojektje önálló, de ugyanazt az adatbázist (`estis_konyvesbolt`) és ugyanazt a modellt használja:

| Lecke | A könyvesbolt-alkalmazás új tudása |
|---|---|
| **14-M04-L01** *(ez a lecke)* | könyvlista `DataGrid`-ben, szűrés kiadóra `ComboBox`-szal, részletek panel |
| 14-M04-L02 | új könyv felvétele, módosítás, törlés megerősítéssel |
| 14-M04-L03 | élő keresés, bemenet ellenőrzése, kivételkezelés |
| 14-M04-L04 | CSV-import, JSON- és XML-export |

A gyakorló feladatok ugyanígy egy saját témán, egy **barkácsáruház raktárán** (`estis_raktar`) haladnak végig.

```
Kiado (1) ──────< (N) Konyv
  Id                Id, Cim, Szerzo, KiadasEve, Ar, Keszlet
  Nev               KiadoId  (idegen kulcs)
  Konyvek           Kiado    (navigációs tulajdonság)
```

## Elmélet

### Az adatbázis létrehozása: `EnsureCreated`

A modell (entitásosztályok + `DbContext`) ugyanúgy készül, mint az M03 modulban. Az `OnModelCreating`-ben `HasData`-val **kezdő adatokat** is megadunk, így az első indításkor nem üres a tábla.

Ebben a modulban a migráció helyett egyszerűbb megoldást használunk: az ablak konstruktora meghívja a

```csharp
db.Database.EnsureCreated();   // ha nincs adatbázis: létrehozza a táblákat és a kezdő adatokat
```

metódust. Ha az adatbázis már létezik, nem csinál semmit. Előnye, hogy a modul mind a négy példája ugyanazt az adatbázist használhatja. Hátránya: a modell későbbi módosítását **nem** vezeti át (ilyenkor phpMyAdminban törölni kell az adatbázist, vagy migrációt kell használni – lásd 14-M03-L01). A kettőt ne keverjük: ha egy adatbázist `EnsureCreated` hozott létre, arra nem futtatható `Update-Database`.

### Egy ablak – egy DbContext

```csharp
public partial class MainWindow : Window
{
    private readonly KonyvesboltContext db = new();   // amíg az ablak él, ezt használjuk
    ...
}
```

Kis, egyablakos alkalmazásban ez a legegyszerűbb: a betöltött objektumokat a context **követi**, így a következő leckében a módosításuk után elég egy `SaveChanges()`.

### DataGrid: oszlopok kézzel

| Tulajdonság | Jelentés |
|---|---|
| `AutoGenerateColumns="False"` | ne minden tulajdonságból legyen oszlop – mi adjuk meg őket |
| `IsReadOnly="True"` | a cellák nem szerkeszthetők (a szerkesztés űrlapon történik, L02) |
| `SelectionMode="Single"` | egyszerre egy sor jelölhető ki |
| `CanUserAddRows="False"` | eltűnik az alsó üres „új sor” |

```xml
<DataGrid x:Name="dgKonyvek" AutoGenerateColumns="False" IsReadOnly="True">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Cím" Binding="{Binding Cim}" Width="*" />
        <DataGridTextColumn Header="Ár (Ft)" Binding="{Binding Ar, StringFormat={}{0:N0}}" Width="80" />
        <DataGridTextColumn Header="Kiadó" Binding="{Binding Kiado.Nev}" Width="100" />
    </DataGrid.Columns>
</DataGrid>
```

- `Width="*"`: kitölti a maradék helyet; szám: fix szélesség.
- `StringFormat={}{0:N0}`: ezres tagolás (3 490). A `{}` azért kell, mert a formátum `{`-vel kezdődik.
- `Binding="{Binding Kiado.Nev}"`: a **navigációs tulajdonságon át** a kapcsolt tábla mezője is kiírható.

Kódból: `dgKonyvek.ItemsSource = lista;` – a lista elemei a sorok.

### Betöltés: `Include` és `ToList`

```csharp
List<Konyv> konyvek = db.Konyvek
    .Include(k => k.Kiado)        // a kapcsolt kiadót is betölti (JOIN)
    .OrderBy(k => k.Cim)
    .ToList();                    // itt fut le az SQL
dgKonyvek.ItemsSource = konyvek;
```

`Include` nélkül a `Kiado` tulajdonság `null`, és a „Kiadó” oszlop üres marad – hibaüzenet nélkül!

### ComboBox kapcsolt táblával

A `ComboBox` elemei teljes objektumok (`Kiado`). Három tulajdonság mondja meg, mit lássunk és mit kapjunk vissza:

| Tulajdonság | Példa | Jelentés |
|---|---|---|
| `ItemsSource` | `db.Kiadok.ToList()` | a lista elemei |
| `DisplayMemberPath` | `Nev` | ezt a tulajdonságot írja ki |
| `SelectedValuePath` | `Id` | a `SelectedValue` ennek az értéke lesz |
| `SelectedItem` | – | a kiválasztott **objektum** (`Kiado`) |
| `SelectedValue` | – | a kiválasztott elem `Id`-je (`int`) |

Két jellemző felhasználás:

1. **Szűrés:** a kiválasztott kiadó könyveit mutatjuk. Az „összes” lehetőséghez a lista elejére egy `Id = 0` „álkiadót” teszünk (ilyen az adatbázisban nincs):
   ```csharp
   var szuroLista = new List<Kiado> { new Kiado { Id = 0, Nev = "(összes kiadó)" } };
   szuroLista.AddRange(kiadok);
   ```
   A `SelectionChanged` eseményben újratöltjük a táblázatot; a `Where`-t csak akkor fűzzük a lekérdezéshez, ha `Id != 0`.
2. **Idegen kulcs megjelenítése:** a könyv `KiadoId`-jét a `SelectedValue`-hoz kötjük, így a ComboBox a könyv kiadóját mutatja:
   ```xml
   <ComboBox DisplayMemberPath="Nev" SelectedValuePath="Id"
             SelectedValue="{Binding KiadoId, Mode=OneWay}" />
   ```

### Részletek panel: kötés a kijelölt sorra

```xml
<Border DataContext="{Binding ElementName=dgKonyvek, Path=SelectedItem}">
    <TextBox Text="{Binding Cim, Mode=OneWay}" IsReadOnly="True" />
</Border>
```

A panel `DataContext`-je a táblázat kijelölt eleme; a benne lévő kötések ehhez viszonyítanak. Kijelölés nélkül a mezők üresek. (A `Mode=OneWay` jelzi, hogy itt csak megjelenítünk; a szerkesztés a következő lecke.)

### Tipikus hibák

- **Üres „Kiadó” oszlop:** elmaradt az `Include(k => k.Kiado)`.
- **A ComboBox `Konyvesbolt.Models.Kiado` szöveget mutat:** hiányzik a `DisplayMemberPath`.
- **Az ablak el sem indul, `XamlParseException` a konstruktorban:** a konstruktorban dobott kivételt a WPF így csomagolja be. A belső kivétel (*InnerException*) általában `MySqlException` – nem fut a MySQL, vagy hibás a kapcsolati szöveg (pl. a gépen van jelszava a `root`-nak). Ennek kezelése a 14-M04-L03 témája.
- **„Table … doesn't exist”:** az adatbázis már létezett (pl. üresen, kézzel létrehozva), ezért az `EnsureCreated` nem csinált semmit. Töröld az adatbázist phpMyAdminban, és indítsd újra a programot.
- **`ItemsSource` és `Items` keverése:** ha `ItemsSource` van beállítva, az `Items.Add` kivételt dob – mindig a listát cseréld le.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KonyvesboltMegjelenites](peldak/KonyvesboltMegjelenites/) | `DataGrid` kézi oszlopokkal, `Include`, szűrés kiadóra „(összes)” elemmel, részletek panel `SelectedItem`-kötéssel, kiadó-`ComboBox` `SelectedValue`-val, összesítő sor |

Futtatás: XAMPP-ban indítsd el a MySQL-t, majd F5. Az első indítás létrehozza az `estis_konyvesbolt` adatbázist a kezdő adatokkal (migráció nem kell). Ha a `root` felhasználónak jelszava van, írd be a `KonyvesboltContext.cs` kapcsolati szövegébe.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M04-L01-F01 | [Raktári terméklista](feladatok/F01-raktar-lista/) | ★☆☆ | [megoldás](megoldasok/F01-raktar-lista/) |
| 14-M04-L01-F02 | [Szűrés kategóriára, részletek panel](feladatok/F02-kategoria-szuro/) | ★★☆ | [megoldás](megoldasok/F02-kategoria-szuro/) |

## Kapcsolódó

- Előző lecke: 14-M03-L04 (Kapcsolatok és LINQ-lekérdezések EF-ben)
- Következő lecke: 14-M04-L02 (Felvétel, módosítás, törlés (CRUD))
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
