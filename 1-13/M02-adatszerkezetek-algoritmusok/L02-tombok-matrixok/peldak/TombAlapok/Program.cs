// Egydimenziós tömbök: létrehozás, feltöltés, bejárás, beépített műveletek
Console.OutputEncoding = System.Text.Encoding.UTF8;

// --- 1. Létrehozás háromféleképpen ---
int[] nullak = new int[10];                     // 10 elem, mind 0  (Python: [0] * 10)
int[] primek = new int[] { 2, 3, 5, 7 };
string[] napok = { "hétfő", "kedd", "szerda", "csütörtök", "péntek" };

Console.WriteLine($"Nullák:  {string.Join(", ", nullak)}");
Console.WriteLine($"Prímek:  {string.Join(", ", primek)} (elemszám: {primek.Length})");
Console.WriteLine($"Első nap: {napok[0]}, utolsó nap: {napok[^1]}");

// --- 2. Feltöltés képlettel (for: kell az index, és írunk a tömbbe) ---
int[] parosak = new int[10];
for (int i = 0; i < parosak.Length; i++)
    parosak[i] = 10 + 2 * i;                    // 10, 12, 14, ...
Console.WriteLine($"Párosak 10-től: {string.Join(", ", parosak)}");

// Fibonacci: az első két elem 1, a többi az előző kettő összege
int[] fib = new int[12];
fib[0] = 1;
fib[1] = 1;
for (int i = 2; i < fib.Length; i++)
    fib[i] = fib[i - 1] + fib[i - 2];
Console.WriteLine($"Fibonacci: {string.Join(", ", fib)}");

// --- 3. Feltöltés véletlenszámokkal – egyetlen Random a ciklus előtt ---
Random rnd = new Random(2026);                  // rögzített mag: minden futáskor ugyanaz a sorozat
int[] dobasok = new int[15];
for (int i = 0; i < dobasok.Length; i++)
    dobasok[i] = rnd.Next(1, 7);                // 1..6 (a felső határ kizárt!)
Console.WriteLine($"\nKockadobások: {string.Join(" ", dobasok)}");

// --- 4. Bejárás foreach-csel (csak olvasás) ---
int hatosok = 0;
foreach (int d in dobasok)
    if (d == 6) hatosok++;
Console.WriteLine($"Hatosok száma: {hatosok}");

// --- 5. Beépített műveletek ---
Console.WriteLine($"Összeg: {dobasok.Sum()}, legkisebb: {dobasok.Min()}, legnagyobb: {dobasok.Max()}");
Console.WriteLine($"Az első 4-es indexe: {Array.IndexOf(dobasok, 4)}, van-e 7-es: {dobasok.Contains(7)}");

// --- 6. Referencia vs. másolat ---
int[] ugyanaz = dobasok;                        // NEM másolat, ugyanarra a tömbre mutat
int[] masolat = (int[])dobasok.Clone();         // valódi másolat
Array.Sort(masolat);
Console.WriteLine($"Rendezett másolat: {string.Join(" ", masolat)}");
Console.WriteLine($"Eredeti (változatlan): {string.Join(" ", dobasok)}");
ugyanaz[0] = 100;
Console.WriteLine($"ugyanaz[0] = 100 után az eredeti első eleme: {dobasok[0]}");

// --- 7. Törlés léptetéssel: a 2. indexű elem eltávolítása ---
char[] betuk = { 'a', 'f', 'z', 'r', 'n', 'w' };
int torlendo = 2;
for (int i = torlendo; i < betuk.Length - 1; i++)
    betuk[i] = betuk[i + 1];                    // mindenki egyet balra lép
Array.Resize(ref betuk, betuk.Length - 1);
Console.WriteLine($"\nTörlés után: {string.Join(" ", betuk)}");

// Beszúrás: a 3. indexre egy '*' kerül
int ide = 3;
Array.Resize(ref betuk, betuk.Length + 1);
for (int i = betuk.Length - 1; i > ide; i--)
    betuk[i] = betuk[i - 1];                    // hátulról haladva mindenki egyet jobbra lép
betuk[ide] = '*';
Console.WriteLine($"Beszúrás után: {string.Join(" ", betuk)}");

// --- 8. Ugyanez listával (részletesen: 13-M02-L04) ---
List<char> lista = new List<char> { 'a', 'f', 'z', 'r', 'n', 'w' };
lista.RemoveAt(2);
lista.Insert(3, '*');
lista.Add('!');                                 // a tömbbel ellentétben a lista bővíthető
Console.WriteLine($"Listával: {string.Join(" ", lista)} (elemszám: {lista.Count})");
