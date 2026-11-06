using System.Globalization;
using Parkolohaz;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
CultureInfo hu = new CultureInfo("hu-HU");

// 1. feladat – beolvasás
List<Parkolas> p = new List<Parkolas>();
string[] sorok = File.ReadAllLines("parkolas.csv");
for (int i = 1; i < sorok.Length; i++)
    if (sorok[i].Trim() != "")
        p.Add(new Parkolas(sorok[i]));

// 2. feladat – különböző autók
List<string> autok = new List<string>();
foreach (Parkolas x in p)
    if (!autok.Contains(x.Rendszam))
        autok.Add(x.Rendszam);
Console.WriteLine($"2. feladat: Parkolások: {p.Count}, különböző autók: {autok.Count}");

// 3. feladat – bevétel és ingyenes parkolások
long bevetel = 0;
int ingyenes = 0;
foreach (Parkolas x in p)
{
    bevetel += x.Dij;
    if (x.Dij == 0) ingyenes++;
}
Console.WriteLine($"3. feladat: Bevétel: {bevetel:N0} Ft, ingyenes parkolás: {ingyenes}");

// 4. feladat – leghosszabb parkolás
Parkolas leghosszabb = p[0];
foreach (Parkolas x in p)
    if (x.Idotartam > leghosszabb.Idotartam)
        leghosszabb = x;
Console.WriteLine($"4. feladat: Leghosszabb: {leghosszabb.Rendszam}, behajtás {leghosszabb.Be:yyyy.MM.dd. HH:mm}, " +
                  $"{Parkolas.OraPerc(leghosszabb.Idotartam)}, díj: {leghosszabb.Dij:N0} Ft");

// 5. feladat – rendszámformátumok a különböző autók között
int regi = 0, uj = 0;
List<string> hibas = new List<string>();
foreach (string r in autok)
{
    if (Parkolas.RegiFormatum(r)) regi++;
    else if (Parkolas.UjFormatum(r)) uj++;
    else hibas.Add(r);
}
Console.WriteLine($"5. feladat: Régi formátum: {regi}, új formátum: {uj}" +
                  (hibas.Count > 0 ? $", ismeretlen: {string.Join(", ", hibas)}" : ""));

// 6. feladat – egy autó parkolásai
Console.Write("6. feladat: Rendszám: ");
string keresett = (Console.ReadLine() ?? "").Trim();
int fizetett = 0, db = 0;
foreach (Parkolas x in p)
    if (string.Equals(x.Rendszam, keresett, StringComparison.OrdinalIgnoreCase))
    {
        db++;
        fizetett += x.Dij;
        Console.WriteLine($"   {x.Be:yyyy.MM.dd. HH:mm}{Parkolas.OraPerc(x.Idotartam),7}{x.Dij,10:N0} Ft{(x.Berlet ? "  (bérlet)" : "")}");
    }
Console.WriteLine(db == 0 ? "   Ez az autó nem parkolt itt." : $"   Összesen {db} parkolás, fizetett: {fizetett:N0} Ft");

// 7. feladat – napi kimutatás fájlba
List<DateTime> napok = new List<DateTime>();
foreach (Parkolas x in p)
    if (!napok.Contains(x.Be.Date))
        napok.Add(x.Be.Date);
napok.Sort();

List<string> napi = new List<string>();
foreach (DateTime nap in napok)
{
    int napiDb = 0, napiBevetel = 0;
    foreach (Parkolas x in p)
        if (x.Be.Date == nap)
        {
            napiDb++;
            napiBevetel += x.Dij;
        }
    napi.Add($"{nap:yyyy.MM.dd.};{nap.ToString("dddd", hu)};{napiDb};{napiBevetel}");
}
File.WriteAllLines("napi.txt", napi);
Console.WriteLine($"7. feladat: napi.txt elkészült ({napi.Count} nap)");

// 8. feladat – óránkénti telítettség egy adott napon
DateTime vizsgalt;
while (true)
{
    Console.Write("8. feladat: Nap (ÉÉÉÉ.HH.NN.): ");
    if (DateTime.TryParseExact((Console.ReadLine() ?? "").Trim(), "yyyy.MM.dd.", CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out vizsgalt))
        break;
    Console.WriteLine("   Érvénytelen dátum!");
}

int csucsOra = 7, csucsDb = -1;
for (int ora = 7; ora <= 20; ora++)
{
    DateTime idopont = vizsgalt.AddHours(ora);
    int bent = 0;
    foreach (Parkolas x in p)
        if (x.BentVolt(idopont))
            bent++;
    Console.WriteLine($"   {ora:00}:00 {bent,3} {new string('#', bent)}");
    if (bent > csucsDb)
    {
        csucsDb = bent;
        csucsOra = ora;
    }
}
Console.WriteLine($"   A legtöbb autó {csucsOra:00}:00-kor volt bent ({csucsDb} db).");
