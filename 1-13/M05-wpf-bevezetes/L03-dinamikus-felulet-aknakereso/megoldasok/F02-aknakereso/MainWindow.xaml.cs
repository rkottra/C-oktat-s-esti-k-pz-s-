using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Aknakereso;

public partial class MainWindow : Window
{
    private const int N = 10;
    private const int AknaDb = 15;

    private static readonly Brush FedettSzin = new SolidColorBrush(Color.FromRgb(0x5B, 0x7D, 0xB1));
    private static readonly Brush FelfedettSzin = new SolidColorBrush(Color.FromRgb(0xF4, 0xEF, 0xE6));

    private readonly Random rnd = new Random();
    private readonly DispatcherTimer ora = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

    // MODELL
    private bool[,] akna = new bool[N, N];
    private int[,] szam = new int[N, N];
    private bool[,] felfedve = new bool[N, N];
    private bool[,] zaszlo = new bool[N, N];
    private bool aknakLerakva;    // az aknák csak az első kattintás után kerülnek le
    private int felfedettDb;
    private int zaszloDb;
    private int masodperc;
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
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    Tag = (s, o),
                };
                gomb.Click += Cella_Click;                   // bal gomb
                gomb.MouseRightButtonUp += Cella_JobbKlikk;  // jobb gomb
                gombok[s, o] = gomb;
                Tabla.Children.Add(gomb);
            }

        ora.Tick += (s, e) =>
        {
            masodperc++;
            IdoLabel.Content = $"Idő: {masodperc} mp";
        };

        UjJatek();
    }

    private void UjJatekGomb_Click(object sender, RoutedEventArgs e) => UjJatek();

    private void UjJatek()
    {
        ora.Stop();
        akna = new bool[N, N];
        szam = new int[N, N];
        felfedve = new bool[N, N];
        zaszlo = new bool[N, N];
        aknakLerakva = false;
        felfedettDb = 0;
        zaszloDb = 0;
        masodperc = 0;
        vege = false;

        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
                Rajzol(s, o);

        AknaLabel.Content = $"Aknák: {AknaDb}";
        IdoLabel.Content = "Idő: 0 mp";
        AllapotLabel.Foreground = Brushes.Black;
        AllapotLabel.Content = "Kattints valahova!";
    }

    // Aknák lerakása az első kattintás után: a kattintott mezőre nem kerülhet akna
    private void Lerak(int kiSor, int kiOszlop)
    {
        int lerakott = 0;
        while (lerakott < AknaDb)
        {
            int s = rnd.Next(N), o = rnd.Next(N);
            if (!akna[s, o] && !(s == kiSor && o == kiOszlop))
            {
                akna[s, o] = true;
                lerakott++;
            }
        }

        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
                szam[s, o] = SzomszedAknak(s, o);

        aknakLerakva = true;
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

    // ---- Bal klikk: felfedés ----
    private void Cella_Click(object sender, RoutedEventArgs e)
    {
        (int s, int o) = ((int, int))((Button)sender).Tag;
        if (vege || felfedve[s, o] || zaszlo[s, o])
            return;

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

        if (felfedettDb == N * N - AknaDb)
            Nyert();
    }

    // Elárasztásos felfedés: a 0-s mező szomszédai is felfedődnek, rekurzívan
    private void Felfed(int s, int o)
    {
        if (s < 0 || s >= N || o < 0 || o >= N)   // a táblán kívül
            return;
        if (felfedve[s, o] || zaszlo[s, o])         // már kész, vagy megjelölt
            return;

        felfedve[s, o] = true;
        felfedettDb++;
        Rajzol(s, o);

        if (szam[s, o] == 0)                        // nincs akna a környéken: mehetünk tovább
            for (int ds = -1; ds <= 1; ds++)
                for (int dO = -1; dO <= 1; dO++)
                    if (ds != 0 || dO != 0)
                        Felfed(s + ds, o + dO);
    }

    // ---- Jobb klikk: zászló ----
    private void Cella_JobbKlikk(object sender, MouseButtonEventArgs e)
    {
        (int s, int o) = ((int, int))((Button)sender).Tag;
        if (vege || felfedve[s, o])
            return;

        zaszlo[s, o] = !zaszlo[s, o];
        zaszloDb += zaszlo[s, o] ? 1 : -1;
        Rajzol(s, o);
        AknaLabel.Content = $"Aknák: {AknaDb - zaszloDb}";
        e.Handled = true;
    }

    // ---- Játék vége ----
    private void Vesztett(int sor, int oszlop)
    {
        vege = true;
        ora.Stop();
        for (int s = 0; s < N; s++)
            for (int o = 0; o < N; o++)
            {
                if (akna[s, o] && !zaszlo[s, o])
                {
                    felfedve[s, o] = true;
                    Rajzol(s, o);
                }
                else if (zaszlo[s, o] && !akna[s, o])
                {
                    gombok[s, o].Content = "✗";              // tévesen megjelölt mező
                    gombok[s, o].Foreground = Brushes.Firebrick;
                    gombok[s, o].Background = Brushes.LightGray;
                }
            }
        gombok[sor, oszlop].Background = Brushes.Red;
        AllapotLabel.Foreground = Brushes.Firebrick;
        AllapotLabel.Content = "Akna! Vesztettél.";
    }

    private void Nyert()
    {
        vege = true;
        ora.Stop();
        for (int s = 0; s < N; s++)                          // minden akna kap zászlót
            for (int o = 0; o < N; o++)
                if (akna[s, o] && !zaszlo[s, o])
                {
                    zaszlo[s, o] = true;
                    Rajzol(s, o);
                }
        AknaLabel.Content = "Aknák: 0";
        AllapotLabel.Foreground = Brushes.ForestGreen;
        AllapotLabel.Content = "Nyertél!";
        MessageBox.Show($"Minden aknát megtaláltál {masodperc} másodperc alatt.", "Gratulálok!");
    }

    // ---- Nézet ----
    private void Rajzol(int s, int o)
    {
        Button g = gombok[s, o];
        if (!felfedve[s, o])
        {
            g.Content = zaszlo[s, o] ? "⚑" : "";
            g.Foreground = Brushes.Gold;
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
