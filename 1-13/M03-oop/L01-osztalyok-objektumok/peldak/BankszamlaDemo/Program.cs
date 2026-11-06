using BankszamlaDemo;

// --- 1. Példányosítás: két külön objektum ugyanabból az osztályból ---
Bankszamla anna = new Bankszamla("Kiss Anna", 20000);
Bankszamla bela = new Bankszamla("Nagy Béla");      // a másik konstruktor fut le

anna.Befizet(5000);
Console.WriteLine("Két külön objektum, külön adatokkal:");
Console.WriteLine(anna);        // automatikusan a ToString() hívódik meg
Console.WriteLine(bela);
Console.WriteLine($"Béla számlája üres? {bela.Ures}");

// anna.Egyenleg = 1000000;     // FORDÍTÁSI HIBA: a set privát!

// --- 2. Metódus visszatérési értékkel ---
Console.WriteLine();
if (anna.Kivesz(100000))
    Console.WriteLine("Sikeres kivét.");
else
    Console.WriteLine("Nincs elég fedezet, a kivét nem sikerült.");

// --- 3. Hibás adat: a tulajdonság kivételt dob ---
try
{
    Bankszamla hibas = new Bankszamla("   ");
    Console.WriteLine(hibas);   // ide már nem jut el
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}

// --- 4. Referencia típus: a változó csak "mutat" az objektumra ---
Console.WriteLine();
Bankszamla masikNev = anna;     // NEM másolat, ugyanaz a számla!
masikNev.Befizet(1000);
Console.WriteLine($"Anna egyenlege a masikNev-en keresztüli befizetés után: {anna.Egyenleg:N0} Ft");

// --- 5. Objektumok listában ---
List<Bankszamla> szamlak = new List<Bankszamla>
{
    anna,
    bela,
    new Bankszamla("Tóth Csilla", 154000),
    new Bankszamla("Szabó Dénes", 3500),
    new Bankszamla("Fehér Emese", 87000)
};

Console.WriteLine("\nAz összes számla:");
foreach (Bankszamla sz in szamlak)
    Console.WriteLine("  " + sz);

// Összegzés
int osszes = 0;
foreach (Bankszamla sz in szamlak)
    osszes += sz.Egyenleg;
Console.WriteLine($"\nA bankban lévő összes pénz: {osszes:N0} Ft");

// Megszámlálás
int tizezerAlatt = 0;
foreach (Bankszamla sz in szamlak)
    if (sz.Egyenleg < 10000)
        tizezerAlatt++;
Console.WriteLine($"10 000 Ft alatti számlák: {tizezerAlatt} db");

// Maximumkeresés: az eredmény maga az objektum, így minden adata elérhető
Bankszamla legtobb = szamlak[0];
foreach (Bankszamla sz in szamlak)
    if (sz.Egyenleg > legtobb.Egyenleg)
        legtobb = sz;
Console.WriteLine($"Legnagyobb egyenleg: {legtobb.Tulajdonos} ({legtobb.Egyenleg:N0} Ft)");
