---
id: 14-M04-L02
cim: Felvétel, módosítás, törlés (CRUD)
evfolyam: 2-14
modul: M04 – WPF + EF Core adatkezelő alkalmazás
orak: 41–44.
het:
oraszam: 4
elofeltetel: 14-M04-L01
leiras: Új rekord felvétele, kiválasztott rekord szerkesztése és törlése megerősítéssel, felület frissítése.
---

# 14-M04-L02 · Felvétel, módosítás, törlés (CRUD)

## Célok

A lecke végére a tanuló:

- WPF-űrlapról új rekordot vesz fel az adatbázisba (`Add` + `SaveChanges`);
- a táblázatban kijelölt rekordot betölti az űrlapra, módosítja és visszamenti;
- törlés előtt megerősítést kér (`MessageBox`, Igen/Nem), majd töröl (`Remove`);
- minden művelet után frissíti a felületet, és visszajelzést ad a felhasználónak;
- felismeri, mikor törölne az EF Core kapcsolt rekordokat is (kaszkádolt törlés).

## Elmélet

### A könyvesbolt-alkalmazás bővítése

Az előző lecke csak olvasott; most a jobb oldali panel **szerkesztő űrlap** lesz, alatta három gombbal:

```
┌───────────────────────────────────────┬─────────────────────┐
│ Cím            Szerző   Év  Ár  Kiadó │ Kijelölt könyv      │
│ 1984           Orwell  1949 ... Európa│ Cím    [__________] │
│ Abigél  ◄── kijelölve                 │ Szerző [__________] │
│ ...                                   │ ...   Kiadó [▼    ] │
│                                       │ [Új] [Mentés] [Törlés]
└───────────────────────────────────────┴─────────────────────┘
```

| Gomb | Mit csinál |
|---|---|
| **Új** | kiüríti az űrlapot, megszünteti a kijelölést |
| **Mentés** | ha van kijelölt sor → **módosítás**, ha nincs → **felvétel** |
| **Törlés** | megerősítés után törli a kijelölt sort (kijelölés nélkül le van tiltva) |

### Űrlap kitöltése a kijelölésből

Az űrlap mezőit **nem kötjük** közvetlenül az entitáshoz, hanem kódból töltjük ki. Így a félig begépelt módosítás nem kerül azonnal az objektumba, és a „Mentés” gomb dönti el, mikor írunk.

```csharp
private void DgKonyvek_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (dgKonyvek.SelectedItem is Konyv k)
    {
        txtCim.Text = k.Cim;
        txtEv.Text = k.KiadasEve.ToString();
        cmbKiado.SelectedValue = k.KiadoId;     // a ComboBox a kiadót mutatja
        ...
    }
}
```

### Felvétel és módosítás egy metódusban

```csharp
Konyv konyv;
if (dgKonyvek.SelectedItem is Konyv kijelolt)
    konyv = kijelolt;                 // MÓDOSÍTÁS: a betöltött, követett entitás
else
{
    konyv = new Konyv();
    db.Konyvek.Add(konyv);            // FELVÉTEL: Added állapot
}
konyv.Cim = txtCim.Text.Trim();
konyv.KiadoId = (int)cmbKiado.SelectedValue;
...
db.SaveChanges();                     // INSERT vagy UPDATE – az EF tudja, melyik kell
```

| Entitás állapota | Honnan lett ilyen? | `SaveChanges` hatása |
|---|---|---|
| `Unchanged` | lekérdezéssel betöltöttük | semmi |
| `Modified` | betöltött entitás tulajdonságát átírtuk | `UPDATE` |
| `Added` | `Add` / `AddRange` | `INSERT`, utána az `Id` ki van töltve |
| `Deleted` | `Remove` | `DELETE` |

Az idegen kulcsot elég a `KiadoId`-ben megadni; a `Kiado` navigációs tulajdonságot nem kell beállítani.

### Törlés megerősítéssel

```csharp
MessageBoxResult valasz = MessageBox.Show(
    $"Biztosan törlöd?\n\n{konyv.Cim}", "Törlés megerősítése",
    MessageBoxButton.YesNo, MessageBoxImage.Question,
    MessageBoxResult.No);             // alapértelmezett gomb: Nem (véletlen Enter ellen)

if (valasz != MessageBoxResult.Yes) return;

db.Konyvek.Remove(konyv);
db.SaveChanges();
```

### A felület frissítése

Az entitásosztályaink nem jelzik a felületnek, ha egy tulajdonságuk megváltozik (ehhez `INotifyPropertyChanged` kellene). Ezért minden mentés után **újratöltjük a listát**, és új listát adunk a `DataGrid`-nek – ez a sorokat is újrarajzolja:

```csharp
private void Frissites(Konyv? kijelolendo)
{
    dgKonyvek.ItemsSource = db.Konyvek.Include(k => k.Kiado).OrderBy(k => k.Cim).ToList();
    dgKonyvek.SelectedItem = kijelolendo;     // a mentett könyv maradjon kijelölve
}
```

Az újratöltés a módosított kiadó nevét is frissíti (a `Kiado` navigációs tulajdonságot az `Include` újra beállítja). Mivel ugyanazt a contextet használjuk, a lekérdezés **ugyanazokat az objektumpéldányokat** adja vissza, ezért működik a `SelectedItem = kijelolendo`.

Ezen kívül mindig jelezzük az eredményt (állapotsor: „Mentve: …”, „Törölve: …”).

### Kapcsolt rekordok törlése

Ha egy „egy” oldali rekordot (pl. kiadót) törlünk, amelyhez kötelező idegen kulccsal „több” oldali rekordok (könyvek) tartoznak, az EF Core alapbeállítással **a könyveket is törli** (*cascade delete*). Ezt ritkán akarjuk – törlés előtt nézzük meg, van-e kapcsolódó rekord, és ha igen, tiltsuk meg a törlést (lásd F02 feladat).

### Tipikus hibák

- **Nem kerül be semmi az adatbázisba:** elmaradt a `SaveChanges()`.
- **Mentés után a táblázat a régi adatot mutatja:** nincs frissítés (új `ItemsSource`).
- **Módosítás helyett új rekord jön létre:** a mentés előtt elveszett a kijelölés (pl. az „Új” gomb után), vagy a kód mindig `Add`-ot hív.
- **`InvalidCastException` / `NullReferenceException` a ComboBoxnál:** nincs kiválasztott elem, a `SelectedValue` `null`. Mentés előtt ellenőrizd (`is not int`).
- **`FormatException`:** `int.Parse` hibás szövegre. Használj `int.TryParse`-t – a teljes ellenőrzés a következő lecke.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KonyvesboltCrud](peldak/KonyvesboltCrud/) | űrlap kitöltése kijelölésből, közös Mentés (felvétel/módosítás), törlés Igen/Nem megerősítéssel, frissítés és újrakijelölés, állapotsor |

Futtatás: XAMPP MySQL indítása, majd F5. Ugyanazt az `estis_konyvesbolt` adatbázist használja, mint az L01 példája; ha az még nincs meg, létrehozza.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M04-L02-F01 | [Termékek felvétele, módosítása, törlése](feladatok/F01-raktar-crud/) | ★★☆ | [megoldás](megoldasok/F01-raktar-crud/) |
| 14-M04-L02-F02 | [Kategóriák kezelése](feladatok/F02-kategoriak-kezelese/) | ★★★ | [megoldás](megoldasok/F02-kategoriak-kezelese/) |

## Kapcsolódó

- Előző lecke: 14-M04-L01 (Adatok megjelenítése: DataGrid, ComboBox)
- Következő lecke: 14-M04-L03 (Szűrés, keresés, validáció, kivételkezelés)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
