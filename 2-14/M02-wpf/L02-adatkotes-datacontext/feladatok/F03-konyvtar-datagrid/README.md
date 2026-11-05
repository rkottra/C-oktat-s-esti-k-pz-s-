# 14-M02-L02-F03 · Könyvtár DataGriddel

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Készíts WPF-alkalmazást `KonyvtarDataGrid` néven egy kis könyvtár nyilvántartásához.

1. `Konyv` osztály (`INotifyPropertyChanged`): `Cim`, `Szerzo`, `KiadasEve` (egész), `Kolcsonozve` (logikai),
   valamint egy **számított**, csak olvasható `Allapot` tulajdonság: `"kölcsönözve"` vagy `"a polcon"`.
2. Az ablakban: `ObservableCollection<Konyv> Konyvek` legalább 5 könyvvel, és egy `Konyv? Kivalasztott` tulajdonság.
   Maga az ablak is valósítsa meg az `INotifyPropertyChanged`-et.
3. Bal oldalon `DataGrid`, **saját oszlopokkal** (`AutoGenerateColumns="False"`): Cím, Szerző, Év, Kölcsönözve (`DataGridCheckBoxColumn`), Állapot.
   A `SelectedItem` legyen kétirányúan a `Kivalasztott` tulajdonsághoz kötve.
4. Jobb oldalon „Részletek” panel, amelynek `DataContext`-je a `Kivalasztott`: nagy betűs cím, szerző, kiadás éve, állapot,
   és egy **Kölcsönzés / visszahozás** gomb, amely átbillenti a `Kolcsonozve` értékét.
5. Alul: **Új könyv**, **Törlés** gomb és egy felirat: `Kölcsönzött könyvek: 2 db`.
   A számláló frissüljön akkor is, ha a táblázat jelölőnégyzetével, és akkor is, ha a gombbal kölcsönöznek.

## Minta

```
| Cím                 | Szerző        | Év   | Kölcsönözve | Állapot     |   Részletek
| Egri csillagok      | Gárdonyi Géza | 1901 | [ ]         | a polcon    |   Abigél
| A Pál utcai fiúk    | Molnár Ferenc | 1907 | [x]         | kölcsönözve |   Szabó Magda
| Abigél              | Szabó Magda   | 1970 | [x]         | kölcsönözve |   Kiadás éve: 1970
                                                                            Állapot: kölcsönözve
[Új könyv] [Törlés]   Kölcsönzött könyvek: 2 db                            [Kölcsönzés / visszahozás]
```

## Tippek

- A `Kolcsonozve` setterében az `Allapot` változását is jelezni kell: `Valtozott(nameof(Allapot));`.
- A `KolcsonzottDb` lehet számított tulajdonság az ablakban: `Konyvek.Count(k => k.Kolcsonozve)`. Ahhoz, hogy frissüljön,
  iratkozz fel minden könyv `PropertyChanged` eseményére, és onnan küldj értesítést a `KolcsonzottDb`-ről.
- A jelölőnégyzet-oszlop azonnali visszaírásához: `Binding="{Binding Kolcsonozve, UpdateSourceTrigger=PropertyChanged}"`.
- Új könyv kijelölése: elég a `Kivalasztott = uj;` – a kétirányú kötés kijelöli a táblázatban.

Megoldás: [../../megoldasok/F03-konyvtar-datagrid/](../../megoldasok/F03-konyvtar-datagrid/)
