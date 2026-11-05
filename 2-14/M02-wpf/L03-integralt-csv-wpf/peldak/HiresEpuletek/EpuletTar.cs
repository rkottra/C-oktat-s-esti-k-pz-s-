using System.Collections.ObjectModel;
using System.IO;
using HiresEpuletek.Models;

namespace HiresEpuletek;

// Az adatok betöltése és mentése egy helyen – a felület (MainWindow) csak használja.
public static class EpuletTar
{
    // A CSV a program mellé másolódik (csproj: CopyToOutputDirectory), ezért a futtatható fájl mappájához képest keressük.
    public static readonly string FajlUtvonal = Path.Combine(AppContext.BaseDirectory, "adatok", "epuletek.csv");

    public static ObservableCollection<Epulet> Beolvas()
    {
        var lista = new ObservableCollection<Epulet>();
        foreach (string sor in File.ReadAllLines(FajlUtvonal).Skip(1))   // Skip(1): fejléc
        {
            if (string.IsNullOrWhiteSpace(sor))
                continue;
            lista.Add(Epulet.CsvSorbol(sor));
        }
        return lista;
    }

    public static void Ment(IEnumerable<Epulet> epuletek)
    {
        var sorok = new List<string> { "Id;Nev;Varos;Orszag;EpitettEv;MagassagMeter;Latogathato" };
        sorok.AddRange(epuletek.Select(e => e.CsvSorra()));
        File.WriteAllLines(FajlUtvonal, sorok);
    }
}
