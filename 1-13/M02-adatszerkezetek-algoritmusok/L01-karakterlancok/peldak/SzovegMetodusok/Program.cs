// A string legfontosabb metódusai – zárójelben a Python-megfelelő
Console.OutputEncoding = System.Text.Encoding.UTF8;

string szoveg = "Helló Világ!";

// --- Hossz, indexelés ---
Console.WriteLine($"Szöveg: \"{szoveg}\"");
Console.WriteLine($"Hossz: {szoveg.Length}");                 // len(s)
char elso = szoveg[0];                                        // s[0] – C#-ban char!
char utolso = szoveg[^1];                                     // s[-1]
Console.WriteLine($"Első karakter: '{elso}', utolsó: '{utolso}'");

// --- Tartományok (szeletelés) – a vég itt is KIZÁRT ---
Console.WriteLine($"[6..]   -> \"{szoveg[6..]}\"");           // s[6:]
Console.WriteLine($"[..5]   -> \"{szoveg[..5]}\"");           // s[:5]
Console.WriteLine($"[6..8]  -> \"{szoveg[6..8]}\"");          // s[6:8] – két karakter
Console.WriteLine($"[6..^1] -> \"{szoveg[6..^1]}\"");         // s[6:-1] – a felkiáltójel nélkül

// --- Substring(kezdet, HOSSZ) ---
Console.WriteLine($"Substring(6, 5) -> \"{szoveg.Substring(6, 5)}\"");

// --- Keresés ---
int hely = szoveg.IndexOf("Világ");                           // s.find("Világ")
Console.WriteLine($"\"Világ\" helye: {hely}");
Console.WriteLine($"\"alma\" helye: {szoveg.IndexOf("alma")}  (-1 = nincs benne)");
Console.WriteLine($"Tartalmaz 'ló'-t? {szoveg.Contains("ló")}"); // "ló" in s
Console.WriteLine($"'Hel'-lel kezdődik? {szoveg.StartsWith("Hel")}");

// --- Átalakítás: mindig ÚJ szöveget kapunk, az eredeti nem változik ---
Console.WriteLine($"Nagybetűs: {szoveg.ToUpper()}");
Console.WriteLine($"Csere:     {szoveg.Replace("Világ", "C#")}");
Console.WriteLine($"Beszúrás:  {szoveg.Insert(5, " kedves")}");
Console.WriteLine($"Törlés:    {szoveg.Remove(5)}");           // az 5. indextől a végéig törli
Console.WriteLine($"Eredeti:   {szoveg}");

// --- Felesleges szóközök levágása ---
string bekert = "   sok szóköz   ";
Console.WriteLine($"Trim előtt: '{bekert}', után: '{bekert.Trim()}'");

// --- Darabolás és összefűzés ---
string sor = "alma;körte;szilva;barack";
string[] gyumolcsok = sor.Split(';');                         // s.split(";")
Console.WriteLine($"Darabok száma: {gyumolcsok.Length}, a második: {gyumolcsok[1]}");
Console.WriteLine("Összefűzve: " + string.Join(", ", gyumolcsok));  // ", ".join(lista)

string mondat = "Ez  egy   mondat, sok-sok szóközzel.";
string[] szavak = mondat.Split(new[] { ' ', ',', '.', '-' }, StringSplitOptions.RemoveEmptyEntries);
Console.WriteLine($"Szavak ({szavak.Length} db): {string.Join(" | ", szavak)}");

// --- Összehasonlítás ---
string a = "Alma", b = "alma";
Console.WriteLine($"\"{a}\" == \"{b}\": {a == b}");
Console.WriteLine($"Kisbetű-érzéketlenül: {string.Equals(a, b, StringComparison.OrdinalIgnoreCase)}");
Console.WriteLine($"Ábécérend (CompareTo): {"barack".CompareTo("alma")}  (pozitív = később jön)");

// --- Számok és szövegek ---
int pont = 7;
Console.WriteLine($"Kitöltés nullákkal: {pont.ToString().PadLeft(3, '0')}");   // str(7).zfill(3)
Console.WriteLine($"Igazítás: [{"bal",-8}] [{"jobb",8}] [{3.14159,8:F2}]");
int szam = int.Parse("42");                                   // int("42")
Console.WriteLine($"int.Parse(\"42\") + 1 = {szam + 1}");

// --- Speciális karakterek ---
Console.WriteLine("Tabulátor:\tután\nÚj sor, idézőjel: \"szia\"");
Console.WriteLine(@"Szó szerinti szöveg: C:\adatok\uj.txt");
