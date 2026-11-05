using System.Collections.ObjectModel;
using System.ComponentModel;
using PilotakSzures.Models;

namespace PilotakSzures;

// Az F01 nézetmodellje szűréssel kiegészítve.
// INotifyPropertyChanged: a "Megjelenítve: x / y" felirat így frissül a szűrés után.
public class PilotakViewModel : INotifyPropertyChanged
{
    public const string Osszes = "(összes)";

    private readonly PilotakContext db = new PilotakContext();

    public ObservableCollection<Pilota> Pilotak { get; } = new ObservableCollection<Pilota>();
    public ObservableCollection<string> Nemzetisegek { get; } = new ObservableCollection<string>();

    private string allapot = "";
    public string Allapot
    {
        get => allapot;
        private set { allapot = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Allapot))); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public PilotakViewModel()
    {
        NemzetisegekBetoltese();
        Szures(Osszes, "");
    }

    public void NemzetisegekBetoltese()
    {
        Nemzetisegek.Clear();
        Nemzetisegek.Add(Osszes);
        foreach (string n in db.Pilotak.Select(p => p.Nemzetiseg).Distinct().OrderBy(n => n))
        {
            Nemzetisegek.Add(n);
        }
    }

    // A szűrés az adatbázisban fut: a LINQ-feltételekből WHERE ... LIKE ... lesz
    public void Szures(string nemzetiseg, string nevReszlet)
    {
        IQueryable<Pilota> lekerdezes = db.Pilotak;
        if (nemzetiseg != Osszes && nemzetiseg != "")
        {
            lekerdezes = lekerdezes.Where(p => p.Nemzetiseg == nemzetiseg);
        }
        if (nevReszlet != "")
        {
            lekerdezes = lekerdezes.Where(p => p.Nev.Contains(nevReszlet));
        }

        Pilotak.Clear();
        foreach (Pilota p in lekerdezes.OrderBy(p => p.Nev))
        {
            Pilotak.Add(p);
        }
        Allapot = $"Megjelenítve: {Pilotak.Count} / {db.Pilotak.Count()}";
    }

    public void Hozzaad(Pilota uj)
    {
        db.Pilotak.Add(uj);
        db.SaveChanges();
        NemzetisegekBetoltese();   // új nemzetiség is kerülhetett az adatbázisba
    }

    public void Torol(Pilota pilota)
    {
        db.Pilotak.Remove(pilota);
        db.SaveChanges();
    }
}
