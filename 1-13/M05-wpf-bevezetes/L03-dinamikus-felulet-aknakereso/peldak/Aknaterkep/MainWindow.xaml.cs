using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Aknaterkep;

// Az aknakereső MODELLJE, felfedett állapotban: aknák lerakása és a szomszédos aknák megszámolása.
// Kattintásra a cella 8 szomszédja kiemelődik, így látszik, honnan jön a szám.
public partial class MainWindow : Window
{
    private const int N = 10;          // a tábla mérete
    private const int AknaDb = 15;     // az aknák száma

    private readonly Random rnd = new Random();

    // MODELL
    private bool[,] akna = new bool[N, N];   // hol van akna
    private int[,] szam = new int[N, N];     // hány akna van a 8 szomszédban

    // NÉZET
    private readonly Button[,] gombok = new Button[N, N];

    public MainWindow()
    {
        InitializeComponent();

        Tabla.Rows = N;
        Tabla.Columns = N;
        for (int sor = 0; sor < N; sor++)
            for (int oszlop = 0; oszlop < N; oszlop++)
            {
                Button gomb = new Button
                {
                    Margin = new Thickness(1),
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    Tag = (sor, oszlop),
                };
                gomb.Click += Cella_Click;
                gombok[sor, oszlop] = gomb;
                Tabla.Children.Add(gomb);
            }

        UjTerkep();
    }

    private void UjTerkepGomb_Click(object sender, RoutedEventArgs e) => UjTerkep();

    private void UjTerkep()
    {
        // 1. Aknák lerakása véletlen helyre: ha a hely foglalt, újra sorsolunk
        akna = new bool[N, N];
        int lerakott = 0;
        while (lerakott < AknaDb)
        {
            int s = rnd.Next(N), o = rnd.Next(N);
            if (!akna[s, o])
            {
                akna[s, o] = true;
                lerakott++;
            }
        }

        // 2. Minden cellára: hány akna van körülötte?
        szam = new int[N, N];
        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
                szam[s, o] = SzomszedAknak(s, o);

        Rajzol(-1, -1);
        InfoLabel.Content = $"{N}×{N} mező, {AknaDb} akna. Kattints egy cellára!";
    }

    // A 8 szomszéd bejárása határ-ellenőrzéssel
    private int SzomszedAknak(int sor, int oszlop)
    {
        int db = 0;
        for (int ds = -1; ds <= 1; ds++)
            for (int dO = -1; dO <= 1; dO++)
            {
                if (ds == 0 && dO == 0) continue;            // önmaga nem szomszéd
                int s = sor + ds, o = oszlop + dO;
                if (s >= 0 && s < N && o >= 0 && o < N && akna[s, o])
                    db++;
            }
        return db;
    }

    // A nézet frissítése a modellből; (kivSor, kivOszlop) a kiválasztott cella, vagy (-1, -1)
    private void Rajzol(int kivSor, int kivOszlop)
    {
        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
            {
                Button g = gombok[s, o];
                g.Content = akna[s, o] ? "✱" : szam[s, o] == 0 ? "" : szam[s, o].ToString();
                g.Foreground = akna[s, o] ? Brushes.White : SzamSzin(szam[s, o]);

                bool kivalasztott = s == kivSor && o == kivOszlop;
                bool szomszed = kivSor >= 0 && !kivalasztott
                             && Math.Abs(s - kivSor) <= 1 && Math.Abs(o - kivOszlop) <= 1;

                g.Background = kivalasztott ? Brushes.Orange
                             : akna[s, o] ? Brushes.IndianRed
                             : szomszed ? Brushes.LightYellow
                             : Brushes.WhiteSmoke;
            }
    }

    private void Cella_Click(object sender, RoutedEventArgs e)
    {
        (int sor, int oszlop) = ((int, int))((Button)sender).Tag;
        Rajzol(sor, oszlop);
        InfoLabel.Content = akna[sor, oszlop]
            ? $"({sor + 1}. sor, {oszlop + 1}. oszlop): ez akna"
            : $"({sor + 1}. sor, {oszlop + 1}. oszlop): {szam[sor, oszlop]} szomszédos akna";
    }

    // Számonként más szín, hogy a tábla ránézésre is olvasható legyen
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
