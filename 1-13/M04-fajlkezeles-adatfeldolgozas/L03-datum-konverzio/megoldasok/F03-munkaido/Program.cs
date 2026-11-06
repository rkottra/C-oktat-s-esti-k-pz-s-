using System.Globalization;
using Munkaido;

Console.OutputEncoding = System.Text.Encoding.UTF8;
CultureInfo hu = new CultureInfo("hu-HU");

// Beolvasás
List<Jelenlet> naplo = new List<Jelenlet>();
string[] sorok = File.ReadAllLines("munkaido.csv");
for (int i = 1; i < sorok.Length; i++)
    if (sorok[i].Trim() != "")
        naplo.Add(new Jelenlet(sorok[i]));

// 2. Különböző dolgozók
List<string> dolgozok = new List<string>();
foreach (Jelenlet j in naplo)
    if (!dolgozok.Contains(j.Dolgozo))
        dolgozok.Add(j.Dolgozo);
Console.WriteLine($"2. Bejegyzések: {naplo.Count}, dolgozók: {dolgozok.Count}");

// 3. Dolgozónkénti táblázat – TimeSpan összegzés
Console.WriteLine($"3. {"Dolgozó",-16}{"Napok",6}{"Ledolgozott",13}{"Túlóra",9}{"Késés",7}");
List<string> jelentes = new List<string>();
foreach (string d in dolgozok)
{
    int napok = 0, kesesek = 0;
    TimeSpan ledolgozott = TimeSpan.Zero, tulora = TimeSpan.Zero;
    foreach (Jelenlet j in naplo)
        if (j.Dolgozo == d)
        {
            napok++;
            ledolgozott += j.Ledolgozott;
            tulora += j.Tulora;
            if (j.Kesett) kesesek++;
        }
    Console.WriteLine($"   {d,-16}{napok,6}{Jelenlet.OraPerc(ledolgozott),13}{Jelenlet.OraPerc(tulora),9}{kesesek,7}");

    jelentes.Add($"{d};{ledolgozott.TotalHours.ToString("F2", CultureInfo.InvariantCulture)};" +
                 $"{tulora.TotalHours.ToString("F2", CultureInfo.InvariantCulture)};{kesesek}");
}

// 4. Legkorábbi érkezés, legkésőbbi távozás
Jelenlet legkorabban = naplo[0], legkesobben = naplo[0];
foreach (Jelenlet j in naplo)
{
    if (j.Be < legkorabban.Be) legkorabban = j;
    if (j.Ki > legkesobben.Ki) legkesobben = j;
}
Console.WriteLine($"4. Legkorábban: {legkorabban.Dolgozo}, {legkorabban.Datum:yyyy.MM.dd.} {legkorabban.Be:hh\\:mm};  " +
                  $"legkésőbb: {legkesobben.Dolgozo}, {legkesobben.Datum:yyyy.MM.dd.} {legkesobben.Ki:hh\\:mm}");

// 5. Leghosszabb munkanap
Jelenlet leghosszabb = naplo[0];
foreach (Jelenlet j in naplo)
    if (j.Ledolgozott > leghosszabb.Ledolgozott)
        leghosszabb = j;
Console.WriteLine($"5. Leghosszabb munkanap: {leghosszabb.Dolgozo}, {leghosszabb.Datum:yyyy.MM.dd.}, " +
                  $"{leghosszabb.Be:hh\\:mm}–{leghosszabb.Ki:hh\\:mm} ({Jelenlet.OraPerc(leghosszabb.Ledolgozott)})");

// 6. Hétvégi munka
Console.WriteLine("6. Hétvégi munka:");
foreach (Jelenlet j in naplo)
    if (j.Hetvege)
        Console.WriteLine($"   {j.Dolgozo}, {j.Datum.ToString("yyyy.MM.dd. dddd", hu)}, {Jelenlet.OraPerc(j.Ledolgozott)}");

// 7. Hiányzások – munkanap: olyan hétköznap, amelyen legalább valakinek van bejegyzése
List<DateTime> munkanapok = new List<DateTime>();
foreach (Jelenlet j in naplo)
    if (!j.Hetvege && !munkanapok.Contains(j.Datum))
        munkanapok.Add(j.Datum);
munkanapok.Sort();
Console.WriteLine($"7. Hiányzások ({munkanapok.Count} munkanapból):");
foreach (string d in dolgozok)
{
    List<string> hianyzott = new List<string>();
    foreach (DateTime nap in munkanapok)
    {
        int i = 0;
        while (i < naplo.Count && !(naplo[i].Dolgozo == d && naplo[i].Datum == nap))
            i++;
        if (i == naplo.Count)
            hianyzott.Add(nap.ToString("MM.dd."));
    }
    Console.WriteLine($"   {d,-16}{hianyzott.Count} nap{(hianyzott.Count > 0 ? ": " + string.Join(", ", hianyzott) : "")}");
}

// 8. Jelentés fájlba (tizedesponttal, hogy más program is be tudja olvasni)
File.WriteAllLines("jelentes.txt", jelentes);
Console.WriteLine("8. A jelentés elkészült: jelentes.txt");
