Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

string[] robotika = { "Kiss Anna", "Nagy Bence", "Tóth Csaba", "Szabó Dóra", "Horváth Emese", "Varga Gergő", "Molnár Hanna" };
string[] sakk     = { "Farkas Ivett", "Nagy Bence", "Balogh János", "Szabó Dóra", "Papp Kitti", "Molnár Hanna", "Lakatos Márk", "Kiss Anna" };

// 1. Eldöntés mindkét tömbben, kis- és nagybetű nélkül
Console.Write("Név: ");
string keresett = (Console.ReadLine() ?? "").Trim();

int r = 0;
while (r < robotika.Length && !string.Equals(robotika[r], keresett, StringComparison.OrdinalIgnoreCase))
    r++;
int s = 0;
while (s < sakk.Length && !string.Equals(sakk[s], keresett, StringComparison.OrdinalIgnoreCase))
    s++;

bool robotos = r < robotika.Length;
bool sakkos = s < sakk.Length;
string pontosNev = robotos ? robotika[r] : sakkos ? sakk[s] : keresett;
if (robotos && sakkos) Console.WriteLine($"{pontosNev} mindkét szakkör tagja.");
else if (robotos) Console.WriteLine($"{pontosNev} csak a robotika szakkör tagja.");
else if (sakkos) Console.WriteLine($"{pontosNev} csak a sakkszakkör tagja.");
else Console.WriteLine($"{pontosNev} egyik szakkörnek sem tagja.");
Console.WriteLine();

// 2. Metszet: robotika elemei, amelyek a sakkban is szerepelnek
string[] metszet = new string[robotika.Length];
int dbM = 0;
foreach (string nev in robotika)
{
    int j = 0;
    while (j < sakk.Length && sakk[j] != nev)
        j++;
    if (j < sakk.Length)
        metszet[dbM++] = nev;
}
Array.Resize(ref metszet, dbM);
Kiir("Mindkettőben", metszet);

// 3. Unió: robotika teljesen + a sakkból azok, akik a robotikában nincsenek
string[] unio = new string[robotika.Length + sakk.Length];
int dbU = 0;
foreach (string nev in robotika)
    unio[dbU++] = nev;
foreach (string nev in sakk)
{
    int i = 0;
    while (i < robotika.Length && robotika[i] != nev)
        i++;
    if (i == robotika.Length)
        unio[dbU++] = nev;
}
Array.Resize(ref unio, dbU);

// ... ábécérendbe minimumkiválasztásos rendezéssel
for (int i = 0; i < unio.Length - 1; i++)
{
    int minI = i;
    for (int j = i + 1; j < unio.Length; j++)
        if (string.Compare(unio[j], unio[minI]) < 0)
            minI = j;
    (unio[i], unio[minI]) = (unio[minI], unio[i]);
}
Kiir("Legalább az egyikben", unio);

// 4. Különbség: robotika elemei, amelyek a sakkban NEM szerepelnek
string[] csakRobot = new string[robotika.Length];
int dbR = 0;
foreach (string nev in robotika)
{
    int j = 0;
    while (j < sakk.Length && sakk[j] != nev)
        j++;
    if (j == sakk.Length)
        csakRobot[dbR++] = nev;
}
Array.Resize(ref csakRobot, dbR);
Kiir("Csak robotika", csakRobot);

// 5. Kiválogatás: magánhangzóval kezdődő keresztnév
const string Maganhangzok = "AÁEÉIÍOÓÖŐUÚÜŰ";
string[] maganhangzos = new string[unio.Length];
int dbV = 0;
foreach (string nev in unio)
{
    string keresztnev = nev.Split(' ')[1];
    if (Maganhangzok.Contains(keresztnev[0]))
        maganhangzos[dbV++] = nev;
}
Array.Resize(ref maganhangzos, dbV);
Kiir("Magánhangzós keresztnév", maganhangzos);

// 6. Szétválogatás vezetéknév kezdőbetűje szerint
string[] busz1 = new string[unio.Length];
string[] busz2 = new string[unio.Length];
int db1 = 0, db2 = 0;
foreach (string nev in unio)
{
    if (nev[0] <= 'K')
        busz1[db1++] = nev;
    else
        busz2[db2++] = nev;
}
Array.Resize(ref busz1, db1);
Array.Resize(ref busz2, db2);
Kiir("1. busz, A–K", busz1);
Kiir("2. busz, L–Z", busz2);

static void Kiir(string cim, string[] nevek)
{
    Console.WriteLine($"{cim} ({nevek.Length}): {string.Join(", ", nevek)}");
}
