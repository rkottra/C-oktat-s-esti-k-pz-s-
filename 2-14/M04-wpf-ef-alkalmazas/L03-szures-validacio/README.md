---
id: 14-M04-L03
cim: Szűrés, keresés, validáció, kivételkezelés
evfolyam: 2-14
modul: M04 – WPF + EF Core adatkezelő alkalmazás
orak: 45–48.
het:
oraszam: 4
elofeltetel: 14-M04-L02
leiras: Élő keresés LINQ-kel, bemenet ellenőrzése mentés előtt, try-catch az adatbázis-műveletek körül.
---

# 14-M04-L03 · Szűrés, keresés, validáció, kivételkezelés

## Célok

A lecke végére a tanuló:

- élő (gépelés közben frissülő) keresést készít LINQ-kel, több szűrőfeltétel kombinálásával;
- mentés előtt ellenőrzi a bemenetet, a hibás mezőket megjelöli és érthető üzenetet ír;
- `try-catch`-csel kezeli az adatbázis-műveletek hibáit (kapcsolódás, mentés);
- sikertelen mentés után visszaállítja a `DbContext` és a felület konzisztens állapotát.

## Elmélet

### Élő keresés

A keresőmező `TextChanged` eseménye minden leütésre lefut. Ha minden leütésre az adatbázishoz fordulnánk, az lassú lehet, ezért a könyveket **egyszer betöltjük a memóriába**, és a szűrés a listán fut:

```csharp
private List<Konyv> osszesKonyv = new();      // betöltéskor és minden mentés után frissül

private void Szures()
{
    string keresett = txtKereses.Text.Trim();
    IEnumerable<Konyv> talalatok = osszesKonyv;

    if (keresett != "")
        talalatok = talalatok.Where(k =>
            k.Cim.Contains(keresett, StringComparison.OrdinalIgnoreCase) ||
            k.Szerzo.Contains(keresett, StringComparison.OrdinalIgnoreCase));

    if (cmbSzuroKiado.SelectedItem is Kiado kiado && kiado.Id != 0)
        talalatok = talalatok.Where(k => k.KiadoId == kiado.Id);

    if (chkCsakKeszleten.IsChecked == true)
        talalatok = talalatok.Where(k => k.Keszlet > 0);

    dgKonyvek.ItemsSource = talalatok.OrderBy(k => k.Cim).ToList();
}
```

- A feltételeket **egymás után fűzzük**, csak ha az adott szűrő aktív – így bármelyik kombináció működik.
- `StringComparison.OrdinalIgnoreCase`: kis- és nagybetű nem számít („szabó” = „Szabó”).
- Több vezérlő (TextBox, ComboBox, CheckBox) **ugyanazt az eseménykezelőt** is használhatja.
- Rendezés választható ComboBoxszal: `switch` kifejezéssel `OrderBy` / `OrderByDescending`.

| Hol fut a szűrés? | Mikor jó? |
|---|---|
| memóriában (`List<T>` + LINQ) | kevés (néhány ezer) rekord, gyors gépelés közbeni frissítés |
| adatbázisban (`db.Konyvek.Where(...)`) | sok rekord; ilyenkor a `Contains` SQL `LIKE`-ká fordul |

### Validáció mentés előtt

Az adatbázisba csak ellenőrzött adat kerülhet. A szabályokat egy metódusba gyűjtjük, amely **az összes hibát** összegyűjti (nem csak az elsőt), és a hibás mezőt pirosra színezi:

```csharp
if (!int.TryParse(txtEv.Text, out int ev) || ev < 1450 || ev > DateTime.Now.Year)
    Hiba(hibak, txtEv, $"A kiadás éve 1450 és {DateTime.Now.Year} közötti egész szám legyen.");

private static void Hiba(List<string> hibak, Control mezo, string uzenet)
{
    mezo.BorderBrush = Brushes.Red;
    hibak.Add(uzenet);
}
```

| Ellenőrzés típusa | Példa |
|---|---|
| kötelező mező | a cím nem lehet üres |
| hossz | a cím legfeljebb 100 karakter (a `[MaxLength(100)]` oszlophoz igazítva) |
| típus | az ár egész szám (`int.TryParse` – nem dob kivételt) |
| tartomány | az év 1450 és az aktuális év között, az ár pozitív |
| formátum | cikkszám: `Regex.IsMatch(s, @"^[A-Z]{2}-\d{3}$")` |
| kiválasztás | van kiválasztott kiadó (`SelectedValue is int`) |
| egyediség (üzleti szabály) | ugyanaz a cím + szerző nem lehet kétszer; módosításkor önmagát ne számítsuk! |

A hibalistát az űrlap alatt piros `TextBlock`-ban jelenítjük meg (`string.Join("\n", hibak)`). Új kijelöléskor vagy ürítéskor a jelöléseket töröljük.

### Kivételkezelés az adatbázis körül

A validáció nem véd meg mindentől: a MySQL leállhat, a kapcsolati szöveg lehet hibás, az adatbázis elutasíthat egy értéket. Ezek **kivételt** dobnak – ha nem kapjuk el, a program összeomlik.

```csharp
try
{
    db.SaveChanges();
}
catch (DbUpdateException ex)          // az adatbázis utasította el (túl hosszú szöveg, kulcsütközés…)
{
    MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
}
catch (Exception ex)                  // minden más (pl. megszakadt a kapcsolat)
{
    MessageBox.Show(ex.Message);
}
```

| Hol? | Mi történhet? | Teendő |
|---|---|---|
| ablak konstruktora (betöltés) | nem fut a MySQL, hibás jelszó | üzenet, az űrlap letiltása (`IsEnabled = false`) |
| `SaveChanges` | `DbUpdateException` | üzenet az `InnerException` szövegével, állapot visszaállítása |
| fájlművelet (L04) | `IOException` | üzenet |

- A `catch` ágak sorrendje számít: a **specifikusabb** kivétel legyen elöl, az általános `Exception` a végén.
- A `DbUpdateException` saját üzenete általános („An error occurred while saving…”); a valódi okot az **`InnerException`** mondja meg.

### Sikertelen mentés után: ChangeTracker.Clear

Ha a `SaveChanges` hibát dobott, a context **továbbra is emlékszik** a mentetlen változásokra: a következő `SaveChanges` újra megpróbálná őket, és újra elbukna. Ezért eldobjuk őket, és mindent újratöltünk:

```csharp
db.ChangeTracker.Clear();   // a követett entitások elfelejtése (a mentetlen változásokkal együtt)
AdatokBetoltese();          // a valós, adatbázisbeli állapot
UrlapUrites();
Szures(null);
```

### Tipikus hibák

- **A keresés csak pontos egyezést talál:** `==` helyett `Contains` kell; a kis/nagybetűhöz `OrdinalIgnoreCase`.
- **`NullReferenceException` induláskor a szűrőben:** az `InitializeComponent` közben (pl. `IsSelected="True"` miatt) már lefut az esemény, amikor a később deklarált vezérlők még `null`-ok – a metódus elején ellenőrizd őket.
- **Csak az első hibát látja a felhasználó:** a validáció a hibák gyűjtése helyett az első után `return`-öl.
- **Módosításkor „már létezik” hibát kap a saját rekordjára:** az egyediségvizsgálatból ki kell hagyni a saját `Id`-t.
- **Üres `catch { }`:** elnyeli a hibát, a felhasználó nem tudja, hogy nem mentődött semmi. Mindig adj visszajelzést.
- **`XamlParseException` induláskor:** a konstruktorban dobott (pl. MySQL-) kivétel így jelenik meg – ezért kell a betöltést `try-catch`-be tenni.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KonyvesboltKereses](peldak/KonyvesboltKereses/) | az L02 CRUD-alkalmazás bővítve: élő keresés címre/szerzőre, kiadószűrő, „csak készleten” jelölő; validáció hibalistával és piros kerettel; `try-catch` betöltéskor és mentéskor, `ChangeTracker.Clear()` |

Kipróbálandó:

1. Állítsd le a MySQL-t a XAMPP-ban, és indítsd el a programot – összeomlás helyett üzenetet kapsz.
2. Hagyj üresen mezőket, írj betűt az árba – az összes hiba egyszerre látszik.
3. Kommenteld ki a cím hosszának ellenőrzését, és ments 100 karakternél hosszabb címet: az adatbázis utasítja el (`DbUpdateException`, „Data too long”).

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M04-L03-F01 | [Élő keresés és rendezés a raktárban](feladatok/F01-elo-kereses/) | ★★☆ | [megoldás](megoldasok/F01-elo-kereses/) |
| 14-M04-L03-F02 | [Ellenőrzött mentés és hibakezelés](feladatok/F02-validacio/) | ★★★ | [megoldás](megoldasok/F02-validacio/) |

## Kapcsolódó

- Előző lecke: 14-M04-L02 (Felvétel, módosítás, törlés (CRUD))
- Következő lecke: 14-M04-L04 (Adatimport és -export (CSV, JSON, XML))
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
