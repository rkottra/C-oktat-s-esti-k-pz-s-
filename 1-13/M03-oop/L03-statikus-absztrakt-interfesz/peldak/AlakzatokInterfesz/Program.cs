using AlakzatokInterfesz;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Interfész típusú lista: bármi bekerülhet, ami megvalósítja az ISikidom-ot.
List<ISikidom> alakzatok = new List<ISikidom>
{
    new Teglalap(4, 3),
    new Kor(2),
    new Haromszog(3, 4, 5),
    new Teglalap(1.5, 10),
    new Kor(1),
};

Kiir("Eredeti sorrend:", alakzatok);

// Rendezés: a List<T>.Sort() az elemek IComparable<T> megvalósítását (CompareTo) használja.
alakzatok.Sort();
Kiir("Terület szerint rendezve:", alakzatok);

// Interfészre is lehet típust vizsgálni: csak az átméretezhetőket nagyítjuk.
int nagyitott = 0;
foreach (ISikidom s in alakzatok)
    if (s is IAtmeretezheto m)
    {
        m.Nagyit(2);
        nagyitott++;
    }
Console.WriteLine($"Kétszeresére nagyítva: {nagyitott} alakzat (a háromszög nem átméretezhető)");
Kiir("Nagyítás után:", alakzatok);

// Összegzés interfészen keresztül
double osszTerulet = 0;
foreach (ISikidom s in alakzatok)
    osszTerulet += s.Terulet();
Console.WriteLine($"Összes terület: {osszTerulet:F2}");

static void Kiir(string cim, List<ISikidom> lista)
{
    Console.WriteLine(cim);
    foreach (ISikidom s in lista)
        Console.WriteLine($"  {s.Nev,-24} T = {s.Terulet(),7:F2}   K = {s.Kerulet(),6:F2}");
    Console.WriteLine();
}
