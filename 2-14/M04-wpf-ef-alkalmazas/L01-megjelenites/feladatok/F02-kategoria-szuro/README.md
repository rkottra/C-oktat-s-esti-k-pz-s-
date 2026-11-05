# 14-M04-L01-F02 · Szűrés kategóriára, részletek panel

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Bővítsd az F01 alkalmazást (új projekt: `RaktarSzuro`):

1. Az ablak tetején egy `ComboBox` sorolja fel a kategóriákat **név szerint**, az első elem „(összes)” legyen.
   Kiválasztáskor a táblázat csak az adott kategória termékeit mutassa (az „(összes)” mindent).
2. A táblázat mellett jobb oldalon egy **részletek panel** mutassa a kijelölt termék cikkszámát, nevét és darabszámát
   (csak olvasható `TextBox`-ok), valamint egy letiltott `ComboBox`-ot, amely a termék kategóriáját mutatja
   – a `KategoriaId`-hez kötve.
3. Az alsó sor a **szűrt** listára írja ki: darabszám, összérték, hány hiánycikk (0 darab) van benne.
4. A 0 darabos termékek sora legyen halványpiros hátterű.

## Minta kimenet

```
Kategória: [ Kertészet ▼ ]
┌──────────┬────────────────────┬───────────┬───────┬───────┐   ┌ Részletek ─────────┐
│ KT-003   │ Ásó                │ Kertészet │ 5 490 │     2 │   │ Cikkszám: KT-002   │
│ KT-002   │ Locsolótömlő 20 m  │ Kertészet │ 6 490 │     9 │◄──│ Név: Locsolótömlő… │
│ KT-001   │ Metszőolló         │ Kertészet │ 3 990 │    18 │   │ Kategória: Kertész.│
└──────────┴────────────────────┴───────────┴───────┴───────┘   └────────────────────┘
3 termék, összérték: 141 210 Ft, ebből 0 hiánycikk
```

## Tippek

- Az „(összes)” elem: `new Kategoria { Id = 0, Nev = "(összes)" }` a lista elejére; szűrni csak `Id != 0` esetén kell.
- A szűrést a `SelectionChanged` eseményben végezd: `IQueryable<Termek>` lekérdezés, amelyhez feltételesen fűzöd a `Where`-t.
- Részletek panel: `DataContext="{Binding ElementName=dgTermekek, Path=SelectedItem}"`.
- Kategória-ComboBox: `DisplayMemberPath="Nev"`, `SelectedValuePath="Id"`, `SelectedValue="{Binding KategoriaId, Mode=OneWay}"`.
- Sorszínezés: `DataGrid.RowStyle` → `Style` → `DataTrigger Binding="{Binding Darab}" Value="0"`.

Megoldás: [../../megoldasok/F02-kategoria-szuro/](../../megoldasok/F02-kategoria-szuro/)
