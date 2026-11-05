// LINQ és lambda kifejezések egyszerű számlistán.
var szamok = new List<int> { 12, 7, 45, 3, 98, 7, 21, 64, 3, 50 };
Console.WriteLine($"Lista: {string.Join(", ", szamok)}");

// ----- 1. Ugyanaz a feltétel háromféleképpen -----
// a) elnevezett metódus
Console.WriteLine($"\nPárosak (metódussal):  {szamok.Count(ParosE)}");
// b) névtelen metódus (delegate) – régebbi forma
Console.WriteLine($"Párosak (delegate-tel): {szamok.Count(delegate (int x) { return x % 2 == 0; })}");
// c) lambda kifejezés: paraméter => kifejezés
Console.WriteLine($"Párosak (lambdával):   {szamok.Count(x => x % 2 == 0)}");

// A lambda változóba is tehető: Func<bemenet, kimenet>
Func<int, bool> nagy = x => x > 40;
Func<int, int, int> osszeg = (a, b) => a + b;
Console.WriteLine($"40-nél nagyobbak: {szamok.Count(nagy)}, osszeg(3, 4) = {osszeg(3, 4)}");

// ----- 2. A leggyakoribb LINQ-metódusok -----
Console.WriteLine($"\nWhere   (> 20):        {string.Join(", ", szamok.Where(x => x > 20))}");
Console.WriteLine($"Select  (négyzet):     {string.Join(", ", szamok.Select(x => x * x))}");
Console.WriteLine($"OrderBy:               {string.Join(", ", szamok.OrderBy(x => x))}");
Console.WriteLine($"OrderByDescending:     {string.Join(", ", szamok.OrderByDescending(x => x))}");
Console.WriteLine($"Distinct:              {string.Join(", ", szamok.Distinct())}");
Console.WriteLine($"Skip(2).Take(3):       {string.Join(", ", szamok.Skip(2).Take(3))}");
Console.WriteLine($"Sum / Average:         {szamok.Sum()} / {szamok.Average():F2}");
Console.WriteLine($"Min / Max:             {szamok.Min()} / {szamok.Max()}");
Console.WriteLine($"Any(x > 90):           {szamok.Any(x => x > 90)}");
Console.WriteLine($"All(x > 0):            {szamok.All(x => x > 0)}");
Console.WriteLine($"First(x > 40):         {szamok.First(x => x > 40)}");
Console.WriteLine($"FirstOrDefault(> 100): {szamok.FirstOrDefault(x => x > 100)}   (nincs ilyen → alapérték: 0)");

// ----- 3. Láncolás: a metódusok egymás után fűzhetők -----
var eredmeny = szamok.Where(x => x % 2 == 1)      // páratlanok
                     .Distinct()                  // ismétlődés nélkül
                     .OrderByDescending(x => x)   // csökkenő sorrendben
                     .Select(x => $"[{x}]");      // szöveggé alakítva
Console.WriteLine($"\nPáratlanok csökkenőben: {string.Join(" ", eredmeny)}");

// Ugyanez lekérdező (query) szintaxissal
var eredmeny2 = from x in szamok.Distinct()
                where x % 2 == 1
                orderby x descending
                select $"[{x}]";
Console.WriteLine($"Lekérdező szintaxissal: {string.Join(" ", eredmeny2)}");

// ----- 4. Csoportosítás -----
var csoportok = szamok.GroupBy(x => x % 2 == 0 ? "páros" : "páratlan");
foreach (var cs in csoportok)
    Console.WriteLine($"{cs.Key}: {cs.Count()} db, összeg {cs.Sum()}");

// ----- 5. Késleltetett végrehajtás -----
// A Where csak egy "receptet" ad vissza; a lekérdezés a bejáráskor fut le.
var kicsik = szamok.Where(x => x < 10);
szamok.Add(1);
Console.WriteLine($"\n10-nél kisebbek (a később hozzáadott 1 is benne van): {string.Join(", ", kicsik)}");
// A ToList() azonnal végrehajtja, és rögzíti az eredményt.
var kicsikMost = szamok.Where(x => x < 10).ToList();
szamok.Add(2);
Console.WriteLine($"ToList után hozzáadott 2 már nincs benne: {string.Join(", ", kicsikMost)}");

// ----- 6. Számsorozat előállítása -----
Console.WriteLine($"\n1..10 négyzetösszege: {Enumerable.Range(1, 10).Sum(x => x * x)}");
Console.WriteLine($"Prímek 50-ig: {string.Join(", ", Enumerable.Range(2, 49).Where(n => Enumerable.Range(2, n - 2).All(o => n % o != 0)))}");

static bool ParosE(int x) => x % 2 == 0;
