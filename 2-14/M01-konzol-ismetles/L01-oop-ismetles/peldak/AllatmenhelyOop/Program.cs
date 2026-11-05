using AllatmenhelyOop;

// Allat a = new Allat(...);   // fordítási hiba: absztrakt osztály nem példányosítható

// Polimorfizmus: az Allat típusú listában Kutya és Macska objektumok is lehetnek.
var menhely = new List<Allat>
{
    new Kutya(1, "Bodri", 5, 18.5, "puli", true),
    new Macska(2, "Cirmi", 3, 4.2, true),
    new Kutya(3, "Morzsa", 11, 9.8, "tacskó", false),
    new Macska(4, "Szotyi", 7, 5.1, false),
    new Kutya(5, "  Frakk  "),        // a Nev tulajdonság levágja a szóközöket
};

Console.WriteLine("A menhely lakói:");
foreach (var allat in menhely)
    Console.WriteLine(allat);           // mindig a valódi típus ToString-je fut le

Console.WriteLine("\nBemutatkozás és hang:");
foreach (var allat in menhely)
{
    // Ugyanaz a hívás, de típustól függően más metódus fut (késői kötés).
    Console.WriteLine($"  {allat.Bemutatkozik()} {allat.HangotAd()}");
}

// Típusvizsgálat: "is" mintával egyszerre ellenőrzünk és át is alakítunk.
int kutyakSzama = 0;
int chipesek = 0;
foreach (var allat in menhely)
{
    if (allat is Kutya kutya)
    {
        kutyakSzama++;
        if (kutya.Chipes) chipesek++;   // a Chipes csak Kutya-ként érhető el
    }
}
Console.WriteLine($"\nKutyák: {kutyakSzama} db, ebből chipes: {chipesek} db");

// Maximumkeresés objektumlistában (a LINQ-es változat a következő leckében jön).
Allat legidosebb = menhely[0];
foreach (var allat in menhely)
{
    if (allat.Kor > legidosebb.Kor)
        legidosebb = allat;
}
Console.WriteLine($"Legidősebb: {legidosebb.Nev} ({legidosebb.Kor} év)");

// Az ellenőrzött tulajdonság hibát jelez érvénytelen értékre.
try
{
    menhely[1].Kor = -3;
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}
