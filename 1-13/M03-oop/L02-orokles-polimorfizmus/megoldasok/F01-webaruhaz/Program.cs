using Webaruhaz;

Console.OutputEncoding = System.Text.Encoding.UTF8;

List<Termek> kosar = new List<Termek>
{
    new Konyv("A Pál utcai fiúk", 3800, "Molnár Ferenc"),
    new Elelmiszer("Trappista sajt", 2200, true),
    new Termek("USB-pendrive 64 GB", 3500),
    new Elelmiszer("Tej 2,8% 1 l", 390, true),
    new Konyv("Egri csillagok", 4200, "Gárdonyi Géza"),
    new Elelmiszer("Zabpehely 500 g", 650, false),
    new Termek("Vonalas füzet A4", 690),
};

Console.WriteLine("Kosár tartalma:");
foreach (Termek t in kosar)
    Console.WriteLine($"  {t}");

// Összegzés – a BruttoAr() mindenkinél a saját ÁFA-kulccsal számol
int brutto = 0, netto = 0;
foreach (Termek t in kosar)
{
    brutto += t.BruttoAr();
    netto += t.NettoAr;
}
Console.WriteLine($"Végösszeg: {brutto:N0} Ft, ebből ÁFA: {brutto - netto:N0} Ft");

// Megszámlálás típus szerint
int konyvek = 0;
foreach (Termek t in kosar)
    if (t is Konyv)
        konyvek++;
Console.WriteLine($"Könyvek száma: {konyvek}");

// Maximumkiválasztás
Termek legdragabb = kosar[0];
foreach (Termek t in kosar)
    if (t.BruttoAr() > legdragabb.BruttoAr())
        legdragabb = t;
Console.WriteLine($"Legdrágább: {legdragabb.Nev} ({legdragabb.BruttoAr():N0} Ft)");

// Kiválogatás – a Romlando csak Elelmiszer típusként érhető el
List<string> hutott = new List<string>();
foreach (Termek t in kosar)
    if (t is Elelmiszer e && e.Romlando)
        hutott.Add(e.Nev);
Console.WriteLine($"Hűtve tárolandó: {string.Join(", ", hutott)}");
