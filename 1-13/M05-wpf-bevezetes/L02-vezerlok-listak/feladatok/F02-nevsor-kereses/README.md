# 13-M05-L02-F02 · Névsor kereséssel és szűréssel

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Készíts `NevsorKereses` nevű WPF-alkalmazást, amely egy iskolai névsorban keres és szűr. Az adatok legyenek a programban (legalább 15 diák, legalább 3 osztályból):

```csharp
public class Diak
{
    public string Nev { get; set; } = "";
    public string Osztaly { get; set; } = "";
    public int Szuletesiev { get; set; }
    public double Atlag { get; set; }
    public override string ToString() => $"{Nev} ({Osztaly})";
}
```

1. **Bal oldal (szűrők):**
   - `TextBox` a névrészletre, gépelés közben szűr (`TextChanged`), kis- és nagybetűtől függetlenül;
   - `ComboBox` az osztályra: első eleme `(mind)`, utána az előforduló osztályok. Ezeket a program az adatokból gyűjtse ki és vegye fel kódból;
   - `CheckBox`: „csak jeles (4,5 felett)”;
   - `RadioButton`-csoport a rendezéshez: név szerint, átlag szerint csökkenő;
   - egy **Szűrők törlése** gomb, amely mindent alaphelyzetbe állít.
2. **Jobb oldal:** egy `Label` a találatok számával (`6 találat / 18 diák`), alatta a `ListBox` a találatokkal. A kijelölt diák adatai (név, osztály, születési év, átlag) egy keretes `TextBlock`-ban jelennek meg.
3. Minden szűrőváltozás után a lista **újraépül**. Ezt egyetlen `Szur()` metódus végezze, amelyet minden eseménykezelő meghív.
4. Ha egy szűrés után a korábban kijelölt diák is a találatok között van, maradjon kijelölve.

## Tippek

- A teljes névsort egy `List<Diak>` mező tárolja; a `ListBox` mindig csak a szűrt nézetet mutatja: `Lista.Items.Clear();`, majd a feltételeknek megfelelő elemeket egyenként felveszi.
- A szűrés kiválogatás, több feltétel `&&` kapcsolatával. Az osztályfeltétel: `OsztalyCombo.SelectedIndex == 0 || d.Osztaly == (string)OsztalyCombo.SelectedItem`.
- Rendezés: a kiválogatott elemeket egy segédlistába gyűjtsd, rendezd (`Sort` lambdával), és csak utána tedd a `ListBox`-ba.
- Figyelem: a `Szur()` már az `InitializeComponent()` alatt is lefuthat, mert az XAML-ben beállított `IsChecked` eseményt vált ki. Ilyenkor még nem minden vezérlő létezik. Vizsgáld a `IsLoaded`-et, vagy az eseménykezelőket kódból kösd be.

Megoldás: [../../megoldasok/F02-nevsor-kereses/](../../megoldasok/F02-nevsor-kereses/)
