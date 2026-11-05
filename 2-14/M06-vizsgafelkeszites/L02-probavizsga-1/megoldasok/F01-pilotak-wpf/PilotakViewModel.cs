using System.Collections.ObjectModel;
using PilotakWpf.Models;

namespace PilotakWpf;

// Az ablak adatai és az adatbázis-műveletek egy helyen – az ablak ezt kapja DataContext-nek.
public class PilotakViewModel
{
    private readonly PilotakContext db = new PilotakContext();

    public ObservableCollection<Pilota> Pilotak { get; } = new ObservableCollection<Pilota>();

    // Az "Új pilóta" nemzetiség-listájához: a már meglévő nemzetiségek ábécérendben
    public ObservableCollection<string> Nemzetisegek { get; } = new ObservableCollection<string>();

    public PilotakViewModel()
    {
        Betoltes();
    }

    public void Betoltes()
    {
        Pilotak.Clear();
        foreach (Pilota p in db.Pilotak.OrderBy(p => p.Id))
        {
            Pilotak.Add(p);
        }

        Nemzetisegek.Clear();
        foreach (string n in db.Pilotak.Select(p => p.Nemzetiseg).Distinct().OrderBy(n => n))
        {
            Nemzetisegek.Add(n);
        }
    }

    public void Hozzaad(Pilota uj)
    {
        db.Pilotak.Add(uj);
        db.SaveChanges();
        Betoltes();
    }

    public void Torol(Pilota pilota)
    {
        db.Pilotak.Remove(pilota);
        db.SaveChanges();
        Betoltes();
    }
}
