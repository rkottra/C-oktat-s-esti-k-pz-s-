# Megoldás – 14-M02-L02-F03 · Könyvtár DataGriddel

Nyisd meg a `KonyvtarDataGrid.csproj` fájlt Visual Studióban, és futtasd (F5).

- `Konyv.Allapot` számított tulajdonság; a `Kolcsonozve` settere róla is értesítést küld.
- Az ablak maga is `INotifyPropertyChanged`: a `Kivalasztott` és a `KolcsonzottDb` tulajdonságok változását jelzi.
- A számláló úgy frissül, hogy az `UjKonyvFelvetele` metódus minden könyv `PropertyChanged` eseményére feliratkozik.
- A `DataGrid` `SelectedItem`-je kétirányúan kötött, ezért az új könyv a `Kivalasztott = uj;` sorral ki is jelölődik.
