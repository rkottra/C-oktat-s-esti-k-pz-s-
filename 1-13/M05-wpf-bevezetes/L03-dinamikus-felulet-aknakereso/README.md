---
id: 13-M05-L03
cim: Dinamikus felület – Aknakereső
evfolyam: 1-13
modul: M05 – Grafikus alkalmazások – WPF bevezetés
orak: 61–64.
het:
oraszam: 4
elofeltetel: 13-M05-L02
leiras: Vezérlők létrehozása kódból, rácsos játéktér, eseménykezelés több vezérlőn, játéklogika.
---

# 13-M05-L03 · Dinamikus felület – Aknakereső

## Célok

A lecke végére a tanuló:

- vezérlőket **kódból** hoz létre, és egy panelhez (`Grid`, `UniformGrid`) adja őket;
- egy N×N-es gombrácsot épít fel, a gombokat kétdimenziós tömbben tárolja, és a méretet futás közben is változtatja;
- **egyetlen eseménykezelővel** kezeli az összes gombot, és a `sender` és a `Tag` alapján megállapítja, melyik cellára kattintottak; a bal és a jobb egérgombot külön kezeli;
- szétválasztja a játék **állapotát** (modell: tömbök) és a **megjelenítést** (a gombok);
- szomszédos cellákat vizsgál a tömb határainak ellenőrzésével, és elárasztásos felfedést ír rekurzióval vagy várakozó sorral;
- időzítőt (`DispatcherTimer`) használ;
- elkészít egy teljes aknakereső játékot.

## A játék szabályai

Egy négyzetrácsos táblán rejtett aknák vannak. Minden lépésben egy fedett mezőt lehet felfedni:

- ha **akna** van rajta, a játék véget ér;
- ha nincs, a mezőn megjelenik, hány akna van a **8 szomszédjában**. Ha ez 0, a szomszédok automatikusan felfedődnek, és ez tovább terjed;
- a jobb egérgombbal **zászlót** tehetünk egy mezőre, ahol aknát sejtünk. A zászlós mezőt nem lehet véletlenül felfedni;
- a játékos **nyer**, ha minden akna nélküli mezőt felfedett.

## Elmélet

### Vezérlő létrehozása kódból

Amit XAML-ben írunk, az kódból is elkészíthető. A két alábbi megoldás ugyanazt a gombot hozza létre:

```xml
<Button Content="OK" Margin="2" FontSize="20" Click="Gomb_Click"/>
```

```csharp
Button gomb = new Button { Content = "OK", Margin = new Thickness(2), FontSize = 20 };
gomb.Click += Gomb_Click;                 // eseménykezelő bekötése
JatekPanel.Children.Add(gomb);            // hozzáadás egy panelhez
```

Ha a vezérlők **száma futás közben dől el** (például a játékos választja a pálya méretét), csak a kódból létrehozás jöhet szóba.

### Rács kódból: Grid vagy UniformGrid

**`UniformGrid`**: csupa egyforma cella. Csak a sorok és oszlopok számát kell megadni, és a gyerekeket sorfolytonosan tölti ki. Az aknamezőhöz ez a legegyszerűbb.

```xml
<UniformGrid Name="Tabla"/>
```

```csharp
Tabla.Children.Clear();
Tabla.Rows = n;
Tabla.Columns = n;
for (int sor = 0; sor < n; sor++)
    for (int oszlop = 0; oszlop < n; oszlop++)
        Tabla.Children.Add(new Button());     // a sorrend határozza meg a helyet
```

**`Grid`**: ha a cellák eltérő méretűek, vagy egy elem több cellát foglal el. Kódból a sor- és oszlopdefiníciókat is fel kell venni, a helyet pedig statikus metódus állítja:

```csharp
for (int i = 0; i < n; i++)
{
    Racs.RowDefinitions.Add(new RowDefinition());
    Racs.ColumnDefinitions.Add(new ColumnDefinition());
}
Grid.SetRow(gomb, sor);
Grid.SetColumn(gomb, oszlop);
Racs.Children.Add(gomb);
```

### Melyik gombot nyomták meg? – sender és Tag

Száz gombhoz nem írunk száz eseménykezelőt. Mindegyik ugyanazt kapja, és a `sender`-ből derül ki, melyiket nyomták meg. A gomb **helyét** a `Tag` tulajdonságban tároljuk; ebbe bármilyen objektum tehető:

```csharp
gomb.Tag = (sor, oszlop);                         // tuple a koordinátákkal
gomb.Click += Cella_Click;

private void Cella_Click(object sender, RoutedEventArgs e)
{
    Button gomb = (Button)sender;
    (int sor, int oszlop) = ((int, int))gomb.Tag;
    …
}
```

A gombokat egy `Button[,] gombok` tömbben is érdemes eltenni. Így a koordinátából megkapjuk a gombot: `gombok[sor, oszlop].Content = "3";`

### Bal és jobb egérgomb

A `Click` csak a **bal** gombra fut le. A jobb gombhoz a `MouseRightButtonUp` eseményt kötjük be; ennek a paramétere `MouseButtonEventArgs` (`using System.Windows.Input;`):

```csharp
gomb.Click += Cella_Click;                    // felfedés
gomb.MouseRightButtonUp += Cella_JobbKlikk;   // zászló

private void Cella_JobbKlikk(object sender, MouseButtonEventArgs e)
{
    (int sor, int oszlop) = ((int, int))((Button)sender).Tag;
    zaszlo[sor, oszlop] = !zaszlo[sor, oszlop];
    Rajzol(sor, oszlop);
    e.Handled = true;                         // az esemény itt elintézve
}
```

### Modell és nézet

A játék állapotát **ne a gombok feliratából** olvassuk vissza, hanem külön adatszerkezetekben tartsuk:

```csharp
// MODELL
private bool[,] akna;          // hol van akna
private int[,] szam;           // hány akna van a 8 szomszédban (egyszer kiszámoljuk)
private bool[,] felfedve;      // mely mezők látszanak már
private bool[,] zaszlo;        // hova tett zászlót a játékos

// NÉZET
private Button[,] gombok;

private void Rajzol(int s, int o)              // a modellből frissíti egy cella nézetét
{
    Button g = gombok[s, o];
    if (!felfedve[s, o])      g.Content = zaszlo[s, o] ? "⚑" : "";
    else if (akna[s, o])      g.Content = "✱";
    else                      g.Content = szam[s, o] == 0 ? "" : szam[s, o].ToString();
}
```

Ennek előnyei: a játéklogika (aknák, szomszédok, nyerés) egyszerű tömbműveletekre épül, és tesztelhető; a megjelenítés (szöveg, szín, ikon) pedig bármikor lecserélhető.

### Aknák lerakása

Véletlen helyet sorsolunk, és ha ott már van akna, újra sorsolunk. Így pontosan a kívánt számú akna kerül le:

```csharp
int lerakott = 0;
while (lerakott < aknaDb)
{
    int s = rnd.Next(n), o = rnd.Next(n);
    if (!akna[s, o])
    {
        akna[s, o] = true;
        lerakott++;
    }
}
```

Érdemes az aknákat **csak az első kattintás után** lerakni, és a kattintott mezőt kihagyni. Így az első lépés soha nem veszít.

### Szomszédok és határok

Az aknakereső szinte minden lépése a 8 szomszéd bejárása. A határok vizsgálata nélkül `IndexOutOfRangeException` lesz a vége:

```csharp
int db = 0;
for (int ds = -1; ds <= 1; ds++)
    for (int dO = -1; dO <= 1; dO++)
    {
        if (ds == 0 && dO == 0) continue;               // önmaga nem szomszéd
        int s = sor + ds, o = oszlop + dO;
        if (s >= 0 && s < n && o >= 0 && o < n && akna[s, o])   // a táblán belül van-e?
            db++;
    }
```

Ha több helyen is kell a szomszédok listája, írjunk rá **egy** metódust (`IEnumerable<(int, int)> Szomszedok(sor, oszlop)` a `yield return`-nel), és mindenhol azt használjuk.

### Elárasztásos felfedés

Ha a felfedett mezőn 0 áll, a szomszédjai is felfedhetők, és ha köztük is van 0-s, annak a szomszédjai is, és így tovább. Ez **rekurzióval** a legtömörebb:

```csharp
private void Felfed(int s, int o)
{
    if (s < 0 || s >= n || o < 0 || o >= n) return;      // a táblán kívül
    if (felfedve[s, o] || zaszlo[s, o]) return;           // már kész: itt áll meg a rekurzió!

    felfedve[s, o] = true;
    Rajzol(s, o);
    if (szam[s, o] == 0)
        for (int ds = -1; ds <= 1; ds++)
            for (int dO = -1; dO <= 1; dO++)
                if (ds != 0 || dO != 0)
                    Felfed(s + ds, o + dO);
}
```

A `felfedve` ellenőrzése nélkül két 0-s szomszéd a végtelenségig hívná egymást (`StackOverflowException`). Nagyon nagy táblán a rekurzió mélysége is gond lehet. Ilyenkor ugyanez **várakozó sorral** (`Queue<(int, int)>`) is megírható: a felfedendő mezőket a sorba tesszük, és egy ciklus addig veszi ki őket, amíg el nem fogynak.

### Időzítő: DispatcherTimer

```csharp
using System.Windows.Threading;

private readonly DispatcherTimer ora = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

ora.Tick += (s, e) => { masodperc++; IdoLabel.Content = $"Idő: {masodperc} mp"; };
ora.Start();      // … ora.Stop();
```

A `Tick` esemény a felület szálán fut, ezért a vezérlők közvetlenül módosíthatók benne. Az aknakeresőben az óra az első kattintással indul, és a játék végén megáll.

### Tipikus hibák

- **Új játéknál megduplázódnak a gombok**: elmaradt a `Children.Clear()`.
- **`InvalidCastException` a `Tag`-nél**: a kiolvasott típus nem egyezik a betettel. `(sor, oszlop)` tuple-t tettél be, akkor `((int, int))` alakban kell kiolvasni.
- **Az állapot a gombfeliratokban van**: a logika bonyolult és törékeny lesz. Legyen külön modell.
- **Határ-ellenőrzés hiánya** a szomszédoknál: a szélső mezőkre kattintva kivétel keletkezik.
- **Végtelen rekurzió** az elárasztásnál: hiányzik a „már felfedve” feltétel.
- **Kevesebb akna kerül le**: a sorsolás nem nézte, hogy a hely foglalt-e.
- **Az időzítő tovább fut** új játéknál vagy a játék vége után: a `Stop()`-ot ne felejtsd el.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/GombRacs](peldak/GombRacs/) | N×N gomb létrehozása kódból `UniformGrid`-be, a méret választása `ComboBox`-ból, közös `Click` a `Tag`-ben tárolt koordinátákkal, `Button[,]` tömb, a sor és az oszlop kiemelése, kattintásszámláló |
| [peldak/Aknaterkep](peldak/Aknaterkep/) | az aknakereső modellje felfedett táblán: aknák véletlen lerakása, a szomszédos aknák megszámolása határ-ellenőrzéssel, számonkénti színezés; kattintásra a cella 8 szomszédja kiemelődik |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, és indítsd el (F5).

## Feladatok

A három feladat egymásra épül: az F01 kódját bővítheted az F02-ben, azt pedig az F03-ban.

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M05-L03-F01 | [Aknamező felfedése](feladatok/F01-aknamezo-felfedes/) | ★☆☆ | [megoldás](megoldasok/F01-aknamezo-felfedes/) |
| 13-M05-L03-F02 | [Aknakereső: elárasztás, zászló, időmérés](feladatok/F02-aknakereso/) | ★★☆ | [megoldás](megoldasok/F02-aknakereso/) |
| 13-M05-L03-F03 | [Aknakereső: szintek, gyors felfedés, rekordok](feladatok/F03-aknakereso-szintek/) | ★★★ | [megoldás](megoldasok/F03-aknakereso-szintek/) |

## Kapcsolódó

- Előző lecke: 13-M05-L02 (Vezérlők és listák)
- Következő lecke: 13-M05-L04 (Szövegfeldolgozás grafikus felületen – Titkosítás)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
