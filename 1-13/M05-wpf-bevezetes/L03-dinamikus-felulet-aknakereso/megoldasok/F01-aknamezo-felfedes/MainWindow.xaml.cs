using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AknamezoFelfedes;

public partial class MainWindow : Window
{
    private const int N = 8;
    private const int AknaDb = 9;

    private static readonly Brush FedettSzin = new SolidColorBrush(Color.FromRgb(0x5B, 0x7D, 0xB1));
    private static readonly Brush FelfedettSzin = new SolidColorBrush(Color.FromRgb(0xF4, 0xEF, 0xE6));

    private readonly Random rnd = new Random();

    // MODELL
    private bool[,] akna = new bool[N, N];
    private int[,] szam = new int[N, N];
    private bool[,] felfedve = new bool[N, N];
    private int felfedettDb;      // felfedett biztonságos mezők száma
    private bool vege;

    // NÉZET
    private readonly Button[,] gombok = new Button[N, N];

    public MainWindow()
    {
        InitializeComponent();

        Tabla.Rows = N;
        Tabla.Columns = N;
        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
            {
                Button gomb = new Button
                {
                    Margin = new Thickness(1),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Tag = (s, o),
                };
                gomb.Click += Cella_Click;
                gombok[s, o] = gomb;
                Tabla.Children.Add(gomb);
            }

        UjJatek();
    }

    private void UjJatekGomb_Click(object sender, RoutedEventArgs e) => UjJatek();

    private void UjJatek()
    {
        akna = new bool[N, N];
        felfedve = new bool[N, N];
        felfedettDb = 0;
        vege = false;

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

        szam = new int[N, N];
        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
                szam[s, o] = SzomszedAknak(s, o);

        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
                Rajzol(s, o);

        AllapotLabel.Foreground = Brushes.Black;
        AllapotLabel.Content = $"Fedd fel mind a {N * N - AknaDb} biztonságos mezőt!";
    }

    private int SzomszedAknak(int sor, int oszlop)
    {
        int db = 0;
        for (int ds = -1; ds <= 1; ds++)
            for (int dO = -1; dO <= 1; dO++)
            {
                int s = sor + ds, o = oszlop + dO;
                if ((ds != 0 || dO != 0) && s >= 0 && s < N && o >= 0 && o < N && akna[s, o])
                    db++;
            }
        return db;
    }

    private void Cella_Click(object sender, RoutedEventArgs e)
    {
        (int s, int o) = ((int, int))((Button)sender).Tag;
        if (vege || felfedve[s, o])
            return;

        felfedve[s, o] = true;

        if (akna[s, o])
        {
            vege = true;
            for (int i = 0; i < N; i++)          // minden akna előkerül
                for (int j = 0; j < N; j++)
                    if (akna[i, j])
                    {
                        felfedve[i, j] = true;
                        Rajzol(i, j);
                    }
            gombok[s, o].Background = Brushes.Red;     // erre léptünk rá
            AllapotLabel.Foreground = Brushes.Firebrick;
            AllapotLabel.Content = "Akna! Vesztettél.";
            return;
        }

        felfedettDb++;
        Rajzol(s, o);

        if (felfedettDb == N * N - AknaDb)
        {
            vege = true;
            for (int i = 0; i < N; i++)          // az aknák helye zölden
                for (int j = 0; j < N; j++)
                    if (akna[i, j])
                    {
                        gombok[i, j].Content = "✱";
                        gombok[i, j].Background = Brushes.MediumSeaGreen;
                    }
            AllapotLabel.Foreground = Brushes.ForestGreen;
            AllapotLabel.Content = "Gratulálok, minden biztonságos mezőt megtaláltál!";
        }
        else
        {
            AllapotLabel.Content = $"Felfedve: {felfedettDb} / {N * N - AknaDb}";
        }
    }

    // Egy cella nézete a modell alapján
    private void Rajzol(int s, int o)
    {
        Button g = gombok[s, o];
        if (!felfedve[s, o])
        {
            g.Content = "";
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
