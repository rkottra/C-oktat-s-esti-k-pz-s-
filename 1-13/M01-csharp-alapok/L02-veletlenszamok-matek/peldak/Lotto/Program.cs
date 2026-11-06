// Lottó-szimuláció: ismétlés nélküli véletlen húzás
// Ötöslottó: 90 számból 5, hatoslottó: 45 számból 6.

Random r = new Random();

Console.WriteLine("Melyik lottó legyen?");
Console.WriteLine("  5 – ötöslottó (5/90)");
Console.WriteLine("  6 – hatoslottó (6/45)");
Console.Write("Választás: ");
char valasztas = Console.ReadKey().KeyChar;
Console.WriteLine();

int darab;
int maximum;
switch (valasztas)        // több ág egy érték alapján (Pythonban if/elif vagy match)
{
    case '5':
        darab = 5;
        maximum = 90;
        break;            // C#-ban minden ágat le kell zárni (break)
    case '6':
        darab = 6;
        maximum = 45;
        break;
    default:
        Console.WriteLine("Ismeretlen választás, ötöslottót húzok.");
        darab = 5;
        maximum = 90;
        break;
}

// ---------- 1. megoldás: listával, ellenőrzéssel ----------
// A List<int> a Python listájának megfelelője (részletesen: 13-M02-L04).
List<int> huzottSzamok = new List<int>();
int probalkozas = 0;
while (huzottSzamok.Count < darab)
{
    int uj = r.Next(1, maximum + 1);   // a felső határ kizárt, ezért +1
    probalkozas++;
    if (!huzottSzamok.Contains(uj))    // Pythonban: if uj not in huzott_szamok
    {
        huzottSzamok.Add(uj);
    }
}
huzottSzamok.Sort();
Console.WriteLine($"\nA nyerőszámok: {string.Join(", ", huzottSzamok)}");
Console.WriteLine($"({probalkozas} húzásból jött ki {darab} különböző szám.)");

// ---------- 2. megoldás: lista nélkül, csak változókkal (ötöslottó) ----------
// Így látszik, miért jó a lista: minden új számot az összes eddigivel össze kell vetni.
int a = r.Next(1, 91);
int b;
do { b = r.Next(1, 91); } while (b == a);
int c;
do { c = r.Next(1, 91); } while (c == a || c == b);
int d;
do { d = r.Next(1, 91); } while (d == a || d == b || d == c);
int e;
do { e = r.Next(1, 91); } while (e == a || e == b || e == c || e == d);
Console.WriteLine($"\nLista nélkül húzott öt szám: {a}, {b}, {c}, {d}, {e}");

// ---------- Tipp: egy szelvény ellenőrzése ----------
Console.WriteLine($"\nAdd meg a(z) {darab} tipped!");
int talalat = 0;
for (int i = 1; i <= darab; i++)       // Pythonban: for i in range(1, darab + 1):
{
    Console.Write($"{i}. tipp: ");
    // TryParse: nem dob hibát rossz bemenetre, hanem false-t ad vissza
    if (int.TryParse(Console.ReadLine(), out int tipp) && huzottSzamok.Contains(tipp))
    {
        talalat++;
    }
}
Console.WriteLine($"Találatok száma: {talalat}");
