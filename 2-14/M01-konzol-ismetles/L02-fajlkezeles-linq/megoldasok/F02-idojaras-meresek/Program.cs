using System.Globalization;
using IdojarasMeresek;

// 1.
var hibak = new List<string>();
List<Meres> meresek = Meres.FajlbolBetolt("meresek.csv", hibak);
Console.WriteLine($"1. Érvényes mérések: {meresek.Count}, hibás sorok: {hibak.Count}");
hibak.ForEach(h => Console.WriteLine($"   {h}"));

// 2.
Console.WriteLine("2. Városonként:");
var varosok = meresek.GroupBy(m => m.Varos)
                     .Select(cs => new
                     {
                         Varos = cs.Key,
                         Atlag = cs.Average(m => m.Homerseklet),
                         Min = cs.Min(m => m.Homerseklet),
                         Max = cs.Max(m => m.Homerseklet),
                         Csapadek = cs.Sum(m => m.Csapadek),
                     })
                     .OrderBy(v => v.Varos)
                     .ToList();
foreach (var v in varosok)
    Console.WriteLine($"   {v.Varos,-9} átlag {v.Atlag,5:F1} °C  min {v.Min,5:F1}  max {v.Max,5:F1}");

// 3.
Meres legmelegebb = meresek.MaxBy(m => m.Homerseklet)!;
Console.WriteLine($"3. Legmelegebb mérés: {legmelegebb}");

// 4.
Console.WriteLine($"4. Esős napok Budapesten: {meresek.Count(m => m.Varos == "Budapest" && m.Esos)}");

// 5.
var legcsapadekosabb = varosok.MaxBy(v => v.Csapadek)!;
Console.WriteLine($"5. Legcsapadékosabb város: {legcsapadekosabb.Varos} ({legcsapadekosabb.Csapadek:F1} mm)");

// 6.
Console.WriteLine($"6. Volt fagy? {(meresek.Any(m => m.Homerseklet <= 0) ? "igen" : "nem")}");

// 7.
var legmelegebbNap = meresek.GroupBy(m => m.Datum)
                            .Select(cs => new { Datum = cs.Key, Atlag = cs.Average(m => m.Homerseklet) })
                            .OrderByDescending(n => n.Atlag)
                            .First();
Console.WriteLine($"7. Legmelegebb nap (városok átlaga): {legmelegebbNap.Datum:yyyy.MM.dd.} – {legmelegebbNap.Atlag:F1} °C");

// 8.
File.WriteAllLines("varosok.txt",
    varosok.Select(v => string.Join(";", v.Varos,
                                         v.Atlag.ToString("F1", CultureInfo.InvariantCulture),
                                         v.Csapadek.ToString("F1", CultureInfo.InvariantCulture))));
Console.WriteLine("8. varosok.txt kiírva.");
