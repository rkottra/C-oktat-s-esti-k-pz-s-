// Vizsgafeladat-megoldás sablonja: másold le, és cseréld ki a Rekord osztályt és a feladatokat.
using VizsgaSablon;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// ---- Beolvasás (az 1. feladat szinte mindig ez) ----
List<Rekord> lista = new List<Rekord>();
foreach (string sor in File.ReadAllLines("adatok.csv").Skip(1))   // Skip(1): fejléc kihagyása
    if (sor.Trim() != "")
        lista.Add(new Rekord(sor));

Feladat2(lista);
Feladat3(lista);
Feladat4(lista);
Feladat5(lista);
Feladat6(lista);

// ---- Feladatonként egy metódus: ha az egyik hibás, a többi ettől még működik ----

static void Feladat2(List<Rekord> lista)
{
    Console.WriteLine($"2. feladat: A rekordok száma: {lista.Count}");
}

static void Feladat3(List<Rekord> lista)            // összegzés + átlag
{
    int osszeg = 0;
    foreach (Rekord r in lista)
        osszeg += r.Ertek;
    Console.WriteLine($"3. feladat: Az értékek átlaga: {(double)osszeg / lista.Count:F2}");
}

static void Feladat4(List<Rekord> lista)            // maximumkiválasztás
{
    Rekord max = lista[0];
    foreach (Rekord r in lista)
        if (r.Ertek > max.Ertek)
            max = r;
    Console.WriteLine($"4. feladat: A legnagyobb érték: {max}");
}

static void Feladat5(List<Rekord> lista)            // bekérés + keresés
{
    Console.Write("5. feladat: Kérek egy nevet: ");
    string nev = (Console.ReadLine() ?? "").Trim();
    int i = 0;
    while (i < lista.Count && !string.Equals(lista[i].Nev, nev, StringComparison.OrdinalIgnoreCase))
        i++;
    Console.WriteLine(i < lista.Count ? $"\tAz első előfordulás: {lista[i]}" : "\tNincs ilyen név.");
}

static void Feladat6(List<Rekord> lista)            // csoportosítás + fájlba írás
{
    List<string> kimenet = new List<string>();
    foreach (string k in Kulonbozo(lista))
    {
        int db = 0;
        foreach (Rekord r in lista)
            if (r.Kategoria == k)
                db++;
        kimenet.Add($"{k};{db}");
    }
    File.WriteAllLines("kategoriak.txt", kimenet);
    Console.WriteLine($"6. feladat: kategoriak.txt ({kimenet.Count} sor)");
}

// ---- Segédmetódus: a különböző kategóriák kigyűjtése (csoportosítás első lépése) ----
static List<string> Kulonbozo(List<Rekord> lista)
{
    List<string> eredmeny = new List<string>();
    foreach (Rekord r in lista)
        if (!eredmeny.Contains(r.Kategoria))
            eredmeny.Add(r.Kategoria);
    return eredmeny;
}
