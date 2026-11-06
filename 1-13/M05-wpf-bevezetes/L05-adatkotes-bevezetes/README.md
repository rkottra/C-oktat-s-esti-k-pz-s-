---
id: 13-M05-L05
cim: Adatkötés bevezetés
evfolyam: 1-13
modul: M05 – Grafikus alkalmazások – WPF bevezetés
orak: 69–72.
het:
oraszam: 4
elofeltetel: 13-M05-L04
leiras: Stílusok és erőforrások, egyszerű adatkötés (Binding), objektumlista megjelenítése.
---

# 13-M05-L05 · Adatkötés bevezetés

## Célok

A lecke végére a tanuló:

- közös megjelenést **erőforrásokkal** (színek, ecsetek) és **stílusokkal** (`Style`, `Setter`, `BasedOn`, `Trigger`) ad meg, egy helyen;
- két vezérlő tulajdonságát adatkötéssel összeköti (`ElementName`);
- egy objektum tulajdonságait a felülethez köti (`DataContext`, `{Binding Nev}`), és az `INotifyPropertyChanged` segítségével a változások maguktól megjelennek;
- objektumlistát `ObservableCollection<T>`-ben tárol, `ItemsSource`-szal köt a listához, és `DataTemplate`-tel formázza az elemeit;
- a kijelölt elem részleteit kötéssel jeleníti meg és szerkeszti (`SelectedItem`, `Mode=TwoWay`).

## Elmélet

### Erőforrások

Ha ugyanazt a színt, betűméretet vagy margót sok helyen használjuk, adjuk meg egyszer, **erőforrásként**, és hivatkozzunk rá:

```xml
<Window.Resources>
    <SolidColorBrush x:Key="FoSzin" Color="#2E86AB"/>
    <Thickness x:Key="Hely">5</Thickness>
</Window.Resources>

<Button Background="{StaticResource FoSzin}" Margin="{StaticResource Hely}" Content="OK"/>
```

- `Window.Resources`: az adott ablakban érvényes. `App.xaml` → `Application.Resources`: az egész alkalmazásban.
- `StaticResource`: egyszer, betöltéskor keresi meg az értéket. `DynamicResource`: futás közben is követi a változást (például témaváltásnál).

### Stílusok

A stílus egy vezérlőtípus tulajdonságainak együttes beállítása:

```xml
<Window.Resources>
    <!-- x:Key nélkül: MINDEN Button ilyen lesz az ablakban (implicit stílus) -->
    <Style TargetType="Button">
        <Setter Property="Margin" Value="5"/>
        <Setter Property="Padding" Value="12,4"/>
        <Setter Property="Background" Value="{StaticResource FoSzin}"/>
        <Setter Property="Foreground" Value="White"/>
        <Style.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Foreground" Value="Yellow"/>
            </Trigger>
        </Style.Triggers>
    </Style>

    <!-- x:Key-jel: csak ott, ahol kérjük; BasedOn: egy másik stílus kiterjesztése -->
    <Style x:Key="Veszelyes" TargetType="Button" BasedOn="{StaticResource {x:Type Button}}">
        <Setter Property="Background" Value="Firebrick"/>
    </Style>
</Window.Resources>

<Button Content="Mentés"/>
<Button Content="Törlés" Style="{StaticResource Veszelyes}"/>
```

A közvetlenül az elemen megadott érték **felülírja** a stílusét. A `Trigger` feltételhez kötött beállítás: csak addig érvényes, amíg a feltétel igaz.

### Adatkötés két vezérlő között

```xml
<Slider Name="MeretSlider" Minimum="10" Maximum="40" Value="16"/>
<TextBlock Text="Minta szöveg" FontSize="{Binding ElementName=MeretSlider, Path=Value}"/>
<TextBlock Text="{Binding ElementName=MeretSlider, Path=Value, StringFormat=Méret: {0:F0} pont}"/>
```

A kötés **cél**ja (target) a `TextBlock.FontSize`, **forrás**a (source) a `MeretSlider.Value`. Egyetlen sor C#-kód nélkül követi a változást.

### Adatkötés objektumhoz: DataContext

```csharp
public partial class MainWindow : Window
{
    private readonly Szemely szemely = new Szemely { Nev = "Kiss Anna", Kor = 17 };

    public MainWindow()
    {
        InitializeComponent();
        DataContext = szemely;            // ettől kezdve a {Binding …} a szemely tulajdonságait látja
    }
}
```

```xml
<TextBox Text="{Binding Nev, UpdateSourceTrigger=PropertyChanged}"/>
<TextBlock Text="{Binding Nev}"/>
<TextBlock Text="{Binding Kor, StringFormat={}{0} éves}"/>
```

- A `DataContext` öröklődik: amit az ablakra állítunk, azt minden benne lévő elem látja.
- A `TextBox` alapból **kétirányú** (`TwoWay`) kötés: a beírt érték visszakerül az objektumba. Az `UpdateSourceTrigger=PropertyChanged` miatt már gépelés közben, nem csak a fókusz elhagyásakor.
- Kötni **tulajdonsághoz** lehet (`{ get; set; }`), mezőhöz nem.

### INotifyPropertyChanged – hogy a felület tudjon a változásról

Ha a kód módosítja az objektumot (`szemely.Kor++`), a felület erről nem értesül, hacsak az osztály nem jelzi. Erre szolgál az `INotifyPropertyChanged` interfész:

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Szemely : INotifyPropertyChanged
{
    private string nev = "";
    public string Nev
    {
        get => nev;
        set { nev = value; Valtozott(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    // A [CallerMemberName] miatt a hívó tulajdonság neve magától kerül a paraméterbe
    protected void Valtozott([CallerMemberName] string? nev = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nev));
}
```

Ha egy **számított** tulajdonság (például `Bemutatkozas => $"{Nev}, {Kor} éves"`) más tulajdonságokból jön, azok setterében érte is jelezni kell: `Valtozott(nameof(Bemutatkozas));`.

### Lista kötése: ObservableCollection és DataTemplate

```csharp
public ObservableCollection<Konyv> Konyvek { get; } = new ObservableCollection<Konyv>();
…
KonyvLista.ItemsSource = Konyvek;     // vagy XAML-ben: ItemsSource="{Binding Konyvek}"
Konyvek.Add(new Konyv { Cim = "Abigél", Szerzo = "Szabó Magda" });   // a lista magától frissül
```

- Az `ObservableCollection<T>` a `List<T>` „figyelhető” változata: hozzáadáskor és törléskor értesíti a felületet. (A sima `List<T>` nem!)
- Ha `ItemsSource`-ot használunk, az `Items.Add` már nem megy: a gyűjteményt kell módosítani.
- Az elemek megjelenése: egyszerűen `DisplayMemberPath="Cim"`, vagy tetszőleges felépítés `DataTemplate`-tel:

```xml
<ListBox Name="KonyvLista" ItemsSource="{Binding Konyvek}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel>
                <TextBlock Text="{Binding Cim}" FontWeight="Bold"/>
                <TextBlock Text="{Binding Szerzo}" Foreground="Gray"/>
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

### Részletek panel a kijelölt elemhez

A részleteket mutató panel `DataContext`-je a lista kijelölt eleme legyen. A panelen belül a kötések már ennek a tulajdonságaira vonatkoznak, és a szerkesztés azonnal megjelenik a listában is:

```xml
<StackPanel DataContext="{Binding SelectedItem, ElementName=KonyvLista}">
    <TextBox Text="{Binding Cim, UpdateSourceTrigger=PropertyChanged}"/>
    <TextBox Text="{Binding Szerzo, UpdateSourceTrigger=PropertyChanged}"/>
</StackPanel>
```

### Tipikus hibák

- **A kötés „nem csinál semmit”**, és nincs hibaüzenet sem: elírtuk a tulajdonság nevét, vagy nincs `DataContext`. A Visual Studio *Output* ablakában a `System.Windows.Data Error` sor mutatja az okát.
- **Mezőhöz kötünk** (`public string Nev;`): tulajdonság kell (`{ get; set; }`).
- **A kódból módosított érték nem jelenik meg**: hiányzik az `INotifyPropertyChanged`, vagy a setter nem hívja a `Valtozott()`-at.
- **Az új elem nem jelenik meg a listában**: `List<T>` van `ObservableCollection<T>` helyett.
- **„Items collection must be empty before using ItemsSource”**: az `ItemsSource` mellett `Items.Add`-ot is használtunk.
- **Az implicit stílus nem hat**: a `TargetType` más típus (például `TextBlock` helyett `Label`), vagy az elemen közvetlenül megadott érték felülírja.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/StilusokEroforrasok](peldak/StilusokEroforrasok/) | színek és méretek erőforrásként, implicit és kulcsos stílus, `BasedOn`, `Trigger` (`IsMouseOver`), alkalmazásszintű erőforrás az `App.xaml`-ben, kötés két vezérlő között (`Slider` → `FontSize`, `StringFormat`) |
| [peldak/BindingLista](peldak/BindingLista/) | `Konyv : INotifyPropertyChanged`, `ObservableCollection<Konyv>`, `ItemsSource` + `DataTemplate`, részletek panel a `SelectedItem`-hez kötve és szerkeszthetően, számított tulajdonság értesítéssel, új elem és törlés kód nélküli listafrissítéssel |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, és indítsd el (F5).

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M05-L05-F01 | [Profilkártya](feladatok/F01-profilkartya/) | ★☆☆ | [megoldás](megoldasok/F01-profilkartya/) |
| 13-M05-L05-F02 | [Filmkatalógus](feladatok/F02-filmkatalogus/) | ★★☆ | [megoldás](megoldasok/F02-filmkatalogus/) |
| 13-M05-L05-F03 | [Költségvetés-napló](feladatok/F03-koltsegvetes/) | ★★★ | [megoldás](megoldasok/F03-koltsegvetes/) |

## Kapcsolódó

- Előző lecke: 13-M05-L04 (Szövegfeldolgozás grafikus felületen – Titkosítás)
- Folytatás a 2/14. évfolyamon: adatkötés, MVVM, `DataGrid` (14-M02)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
