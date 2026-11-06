using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace AknakeresoPlusz;

// Egy nehézségi szint: név, táblaméret, aknaszám
public record Szint(string Nev, int Meret, int Akna)
{
    public override string ToString() => $"{Nev} ({Meret}×{Meret}, {Akna} akna)";
}

public partial class MainWindow : Window
{
    private static readonly Szint[] Szintek =
    {
        new Szint("Kezdő", 8, 9),
        new Szint("Haladó", 12, 22),
        new Szint("Mester", 16, 45),
    };

    // A jelölés állapotai (jobb klikk körbe léptet)
    private const int Nincs = 0, Zaszlo = 1, Kerdojel = 2;

    private static readonly string RekordFajl = Path.Combine(AppContext.BaseDirectory, "rekordok.txt");
    private static readonly Brush FedettSzin = new SolidColorBrush(Color.FromRgb(0x5B, 0x7D, 0xB1));
    private static readonly Brush FelfedettSzin = new SolidColorBrush(Color.FromRgb(0xF4, 0xEF, 0xE6));

    private readonly Random rnd = new Random();
    private readonly DispatcherTimer ora = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
    private readonly Dictionary<string, int> rekordok = new Dictionary<string, int>();

    // MODELL
    private Szint szint = Szintek[0];
    private int n;
    private bool[,] akna = new bool[0, 0];
    private int[,] szam = new int[0, 0];
    private bool[,] felfedve = new bool[0, 0];
    private int[,] jel = new int[0, 0];
    private bool aknakLerakva;
    private int felfedettDb;
    private int zaszloDb;
    private int masodperc;
    private bool vege;

    // NÉZET
    private Button[,] gombok = new Button[0, 0];

    public MainWindow()
    {
        InitializeComponent();

        ora.Tick += (s, e) =>
        {
            masodperc++;
            IdoLabel.Content = $"Idő: {masodperc} mp";
        };

        RekordokBetoltese();
        foreach (Szint sz in Szintek)
            SzintCombo.Items.Add(sz);
        SzintCombo.SelectedIndex = 0;          // → SelectionChanged → UjJatek
    }

    private void SzintCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        szint = (Szint)SzintCombo.SelectedItem;
        UjJatek();
    }

    private void UjJatekGomb_Click(object sender, RoutedEventArgs e) => UjJatek();

    // ---- Új játék: a tábla újraépítése az aktuális szint méretével ----
    private void UjJatek()
    {
        ora.Stop();
        n = szint.Meret;
        akna = new bool[n, n];
        szam = new int[n, n];
        felfedve = new bool[n, n];
        jel = new int[n, n];
        aknakLerakva = false;
        felfedettDb = 0;
        zaszloDb = 0;
        masodperc = 0;
        vege = false;

        Tabla.Children.Clear();
        Tabla.Rows = n;
        Tabla.Columns = n;
        gombok = new Button[n, n];
        for (int s = 0; s < n; s++)
            for (int o = 0; o < n; o++)
            {
                Button gomb = new Button
                {
                    Margin = new Thickness(1),
                    Padding = new Thickness(0),
                    FontSize = 200.0 / n,
                    FontWeight = FontWeights.Bold,
                    Tag = (s, o),
                };
                gomb.Click += Cella_Click;
                gomb.MouseRightButtonUp += Cella_JobbKlikk;
                gombok[s, o] = gomb;
                Tabla.Children.Add(gomb);
                Rajzol(s, o);
            }

        AknaLabel.Content = $"Aknák: {szint.Akna}";
        IdoLabel.Content = "Idő: 0 mp";
        AllapotLabel.Foreground = Brushes.Black;
        AllapotLabel.Content = "Kattints valahova!";
        RekordKiirasa();
    }

    // Az első kattintás és a 8 szomszédja biztosan akna nélküli → az első kattintás mindig „nyit”
    private void Lerak(int kiSor, int kiOszlop)
    {
        int lerakott = 0;
        while (lerakott < szint.Akna)
        {
            int s = rnd.Next(n), o = rnd.Next(n);
            bool kezdoKornyek = Math.Abs(s - kiSor) <= 1 && Math.Abs(o - kiOszlop) <= 1;
            if (!akna[s, o] && !kezdoKornyek)
            {
                akna[s, o] = true;
                lerakott++;
            }
        }

        for (int s = 0; s < n; s++)
            for (int o = 0; o < n; o++)
                foreach ((int ss, int oo) in Szomszedok(s, o))
                    if (akna[ss, oo])
                        szam[s, o]++;

        aknakLerakva = true;
    }

    // A táblán belüli szomszédok – egy helyen a határ-ellenőrzés
    private IEnumerable<(int, int)> Szomszedok(int sor, int oszlop)
    {
        for (int ds = -1; ds <= 1; ds++)
            for (int dO = -1; dO <= 1; dO++)
            {
                int s = sor + ds, o = oszlop + dO;
                if ((ds != 0 || dO != 0) && s >= 0 && s < n && o >= 0 && o < n)
                    yield return (s, o);
            }
    }

    // ---- Bal klikk ----
    private void Cella_Click(object sender, RoutedEventArgs e)
    {
        (int s, int o) = ((int, int))((Button)sender).Tag;
        if (vege || jel[s, o] == Zaszlo)
            return;

        if (felfedve[s, o])
        {
            GyorsFelfedes(s, o);
            return;
        }

        if (!aknakLerakva)
        {
            Lerak(s, o);
            ora.Start();
            AllapotLabel.Content = "";
        }

        if (akna[s, o])
        {
            Vesztett(s, o);
            return;
        }

        Felfed(s, o);
        NyertE();
    }

    // Gyors felfedés: ha egy felfedett szám körül pontosan annyi zászló van, amennyi a szám,
    // a többi szomszéd egyszerre felfedhető. (Rossz zászló esetén ez aknára léphet!)
    private void GyorsFelfedes(int sor, int oszlop)
    {
        int zaszlok = 0;
        foreach ((int s, int o) in Szomszedok(sor, oszlop))
            if (jel[s, o] == Zaszlo)
                zaszlok++;

        if (szam[sor, oszlop] == 0 || zaszlok != szam[sor, oszlop])
            return;

        foreach ((int s, int o) in Szomszedok(sor, oszlop))
        {
            if (felfedve[s, o] || jel[s, o] == Zaszlo)
                continue;
            if (akna[s, o])
            {
                Vesztett(s, o);
                return;
            }
            Felfed(s, o);
        }
        NyertE();
    }

    // Elárasztásos felfedés VÁRAKOZÓ SORRAL (rekurzió nélkül): nagy táblán sem fogy el a verem
    private void Felfed(int kezdoSor, int kezdoOszlop)
    {
        Queue<(int, int)> varolista = new Queue<(int, int)>();
        varolista.Enqueue((kezdoSor, kezdoOszlop));

        while (varolista.Count > 0)
        {
            (int s, int o) = varolista.Dequeue();
            if (felfedve[s, o] || jel[s, o] == Zaszlo)
                continue;

            felfedve[s, o] = true;
            jel[s, o] = Nincs;                 // az esetleges kérdőjel eltűnik
            felfedettDb++;
            Rajzol(s, o);

            if (szam[s, o] == 0)
                foreach ((int ss, int oo) in Szomszedok(s, o))
                    if (!felfedve[ss, oo])
                        varolista.Enqueue((ss, oo));
        }
    }

    // ---- Jobb klikk: semmi → zászló → kérdőjel → semmi ----
    private void Cella_JobbKlikk(object sender, MouseButtonEventArgs e)
    {
        (int s, int o) = ((int, int))((Button)sender).Tag;
        e.Handled = true;
        if (vege || felfedve[s, o])
            return;

        if (jel[s, o] == Zaszlo) zaszloDb--;
        jel[s, o] = (jel[s, o] + 1) % 3;
        if (jel[s, o] == Zaszlo) zaszloDb++;

        Rajzol(s, o);
        AknaLabel.Content = $"Aknák: {szint.Akna - zaszloDb}";
    }

    // ---- Játék vége ----
    private void NyertE()
    {
        if (vege || felfedettDb != n * n - szint.Akna)
            return;

        vege = true;
        ora.Stop();
        for (int s = 0; s < n; s++)
            for (int o = 0; o < n; o++)
                if (akna[s, o])
                {
                    jel[s, o] = Zaszlo;
                    Rajzol(s, o);
                }
        AknaLabel.Content = "Aknák: 0";
        AllapotLabel.Foreground = Brushes.ForestGreen;

        string uzenet = $"Minden aknát megtaláltál {masodperc} másodperc alatt.";
        if (!rekordok.TryGetValue(szint.Nev, out int rekord) || masodperc < rekord)
        {
            rekordok[szint.Nev] = masodperc;
            RekordokMentese();
            RekordKiirasa();
            uzenet += "\nEz új rekord ezen a szinten!";
            AllapotLabel.Content = "Nyertél – új rekord!";
        }
        else
        {
            AllapotLabel.Content = "Nyertél!";
        }
        MessageBox.Show(uzenet, "Gratulálok!");
    }

    private void Vesztett(int sor, int oszlop)
    {
        vege = true;
        ora.Stop();
        for (int s = 0; s < n; s++)
            for (int o = 0; o < n; o++)
            {
                if (akna[s, o] && jel[s, o] != Zaszlo)
                {
                    felfedve[s, o] = true;
                    Rajzol(s, o);
                }
                else if (jel[s, o] == Zaszlo && !akna[s, o])
                {
                    gombok[s, o].Content = "✗";
                    gombok[s, o].Foreground = Brushes.Firebrick;
                    gombok[s, o].Background = Brushes.LightGray;
                }
            }
        gombok[sor, oszlop].Background = Brushes.Red;
        AllapotLabel.Foreground = Brushes.Firebrick;
        AllapotLabel.Content = "Akna! Vesztettél.";
    }

    // ---- Rekordok: szintenként a legjobb idő, „Szint;másodperc” soronként ----
    private void RekordokBetoltese()
    {
        if (!File.Exists(RekordFajl))
            return;
        foreach (string sor in File.ReadAllLines(RekordFajl))
        {
            string[] d = sor.Split(';');
            if (d.Length == 2 && int.TryParse(d[1], out int mp))
                rekordok[d[0]] = mp;
        }
    }

    private void RekordokMentese()
    {
        List<string> sorok = new List<string>();
        foreach (KeyValuePair<string, int> r in rekordok)
            sorok.Add($"{r.Key};{r.Value}");
        File.WriteAllLines(RekordFajl, sorok);
    }

    private void RekordKiirasa()
    {
        RekordLabel.Content = rekordok.TryGetValue(szint.Nev, out int mp)
            ? $"Rekord: {mp} mp"
            : "Rekord: még nincs";
    }

    // ---- Nézet ----
    private void Rajzol(int s, int o)
    {
        Button g = gombok[s, o];
        if (!felfedve[s, o])
        {
            g.Content = jel[s, o] switch { Zaszlo => "⚑", Kerdojel => "?", _ => "" };
            g.Foreground = jel[s, o] == Zaszlo ? Brushes.Gold : Brushes.White;
            g.Background = FedettSzin;
        }
        else if (akna[s, o])
        {
            g.Content = "✱";
            g.Foreground = Brushes.White;
            g.Background = Brushes.IndianRed;
        }
        else
        {
            g.Content = szam[s, o] == 0 ? "" : szam[s, o].ToString();
            g.Foreground = SzamSzin(szam[s, o]);
            g.Background = FelfedettSzin;
        }
    }

    private static Brush SzamSzin(int n) => n switch
    {
        1 => Brushes.SteelBlue,
        2 => Brushes.SeaGreen,
        3 => Brushes.Crimson,
        4 => Brushes.Indigo,
        5 => Brushes.Chocolate,
        6 => Brushes.Teal,
        7 => Brushes.DimGray,
        _ => Brushes.Black,
    };
}
