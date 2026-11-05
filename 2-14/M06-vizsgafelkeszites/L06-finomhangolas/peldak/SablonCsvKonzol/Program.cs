using System.Globalization;
using SablonCsvKonzol;

// SABLON – a konzolos vizsgarész leggyakoribb mintái egy helyen.

// Magyar ábécérend (rendezéshez és összehasonlításhoz)
var magyar = StringComparer.Create(new CultureInfo("hu-HU"), ignoreCase: true);

// 2. feladat: beolvasás
List<Rekord> lista = Rekord.BetoltCsv("minta.csv");

// 3. feladat: megszámolás
Console.WriteLine($"3. feladat: Rekordok száma: {lista.Count}");

// 4. feladat: szélsőérték – a null értékeket előbb kiszűrjük!
Rekord? legnagyobb = lista.Where(r => r.Ertek != null).MaxBy(r => r.Ertek);
Console.WriteLine(legnagyobb is null
    ? "4. feladat: Nincs értékkel rendelkező rekord."
    : $"4. feladat: Legnagyobb érték: {legnagyobb.Nev} ({legnagyobb.ErtekSzoveg})");

// 5. feladat: átlag – üres sorozatra az Average kivételt dob, ezért előbb Any()
var ertekek = lista.Where(r => r.Ertek != null).Select(r => r.Ertek!.Value).ToList();
Console.WriteLine(ertekek.Any()
    ? $"5. feladat: Átlag: {ertekek.Average():F2}"
    : "5. feladat: Nincs miből átlagot számolni.");

// 6. feladat: bekérés + keresés (kis-/nagybetű nem számít), "nincs találat" ág
Console.Write("6. feladat: Adjon meg egy névrészletet: ");
string resz = (Console.ReadLine() ?? "").Trim();
var talalatok = lista.Where(r => r.Nev.Contains(resz, StringComparison.OrdinalIgnoreCase)).ToList();
if (resz == "" || talalatok.Count == 0)
{
    Console.WriteLine("\tNincs találat.");
}
else
{
    talalatok.ForEach(r => Console.WriteLine($"\t{r}"));
}

// 7. feladat: csoportosítás + rendezés
Console.WriteLine("7. feladat: Rekordok évenként:");
foreach (var g in lista.Where(r => r.Datum != null).GroupBy(r => r.Datum!.Value.Year).OrderBy(g => g.Key))
{
    Console.WriteLine($"\t{g.Key}: {g.Count()} db – {string.Join(", ", g.Select(r => r.Nev).OrderBy(n => n, magyar))}");
}

// 8. feladat: fájlba írás hibakezeléssel
try
{
    using StreamWriter iro = new StreamWriter("eredmeny.txt");
    foreach (Rekord r in lista.OrderBy(r => r.Nev, magyar))
    {
        iro.WriteLine(r);
    }
    Console.WriteLine("8. feladat: Az eredmeny.txt elkészült.");
}
catch (Exception)
{
    Console.WriteLine("8. feladat: Hiba a fájl írásakor!");
}
