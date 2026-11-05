# 14-M04-L02-F01 · Termékek felvétele, módosítása, törlése

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Készíts `RaktarCrud` néven WPF-alkalmazást az L01-es raktármodellre (`estis_raktar`), amellyel a termékek kezelhetők.

1. Bal oldalon `DataGrid` a termékekkel (cikkszám szerint rendezve), jobb oldalon űrlap:
   Cikkszám, Név, Ár, Darab (`TextBox`), Kategória (`ComboBox`), alatta **Új**, **Mentés**, **Törlés** gomb.
2. Sor kijelölésekor az űrlap a kijelölt termék adatait mutassa, a cím legyen „Termék módosítása”.
3. **Új**: üres űrlap, nincs kijelölt sor, a cím „Új termék”, a Törlés gomb letiltva.
4. **Mentés**: ha van kijelölt sor, azt módosítja, különben új terméket vesz fel. A cikkszámot nagybetűssé alakítva mentse.
   Ha az ár vagy a darab nem egész szám, vagy nincs kategória kiválasztva, üzenetet adjon, és ne mentsen.
5. **Törlés**: megerősítést kér (Igen/Nem, alapértelmezett a Nem). Ha a termékből még van készleten, a kérdés ezt is írja ki.
6. Minden művelet után frissüljön a táblázat, a mentett termék maradjon kijelölve, és az állapotsor írja ki, mi történt.

## Minta működés

```
[Új] → Cikkszám: KS-004, Név: Vízmérték 60 cm, Ár: 3590, Darab: 10, Kategória: Kéziszerszám → [Mentés]
Állapotsor: Mentve: KS-004 Vízmérték 60 cm

KS-001 kijelölése → [Törlés]
┌ Törlés megerősítése ──────────────────────────────┐
│ Biztosan törlöd: KS-001 Kalapács 500 g?            │
│ Figyelem: még 25 db van belőle raktáron!          │
│                               [ Igen ]  [ Nem ]    │
└───────────────────────────────────────────────────┘
```

## Tippek

- Az űrlapot kódból töltsd ki a `SelectionChanged` eseményben; a ComboBoxhoz `cmbKategoria.SelectedValue = t.KategoriaId`.
- Felvétel/módosítás: `if (dgTermekek.SelectedItem is Termek kijelolt) ... else { new Termek(); db.Termekek.Add(...) }`.
- Frissítés: új lista az `ItemsSource`-nak, majd `SelectedItem = termek`.
- `MessageBox.Show(szoveg, cim, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)`.

Megoldás: [../../megoldasok/F01-raktar-crud/](../../megoldasok/F01-raktar-crud/)
