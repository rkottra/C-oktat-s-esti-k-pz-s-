# 14-M02-L01-F03 · Bevásárlólista két listával

**Nehézség:** ★★☆ **Becsült idő:** 40 perc

## Feladat

Készíts WPF-alkalmazást `Bevasarlolista` néven.

1. Felül egy sor (`DockPanel`): „Új tétel:” felirat, kitöltő `TextBox`, jobb szélen **Hozzáadás** gomb. A gomb **Enter** lenyomására is működjön.
2. Alatta két egymás melletti `ListBox` fejléccel: **Megveendő** és **Már a kosárban**.
3. Hozzáadás: a beírt szöveg (szóközök levágásával) a *Megveendő* listába kerül, a mező kiürül, és visszakapja a fókuszt.
   Üres szöveget és a listában már szereplő tételt ne vegyen fel (üzenettel jelezze).
4. Ha a *Megveendő* lista egy elemére **duplán kattintunk**, az átkerül a *Már a kosárban* listába.
5. Alul gombok: **Kijelölt törlése** (a *Megveendő* listából) és **Lista ürítése** (megerősítés után mindkét lista).
6. Egy `Label` mindig mutassa: `Hátra: 3 db, kosárban: 2 db`.

## Minta

```
Új tétel: [kenyér              ] [Hozzáadás]
Megveendő (dupla kattintás…)   Már a kosárban
  tej                            alma
  kenyér                         sajt
  vaj
[Kijelölt törlése] [Lista ürítése]   Hátra: 3 db, kosárban: 2 db
```

## Tippek

- `IsDefault="True"` a gombon → az Enter megnyomja.
- A lista elemei: `MegveendoLista.Items.Add(...)`, `.Remove(...)`, `.Contains(...)`, `.Clear()`, `.Count`.
- Dupla kattintás: `MouseDoubleClick="..."` esemény a `ListBox`-on (paramétere `MouseButtonEventArgs`).
- A számláló frissítését tedd külön metódusba, és hívd meg minden változás után.

Megoldás: [../../megoldasok/F03-bevasarlolista/](../../megoldasok/F03-bevasarlolista/)
