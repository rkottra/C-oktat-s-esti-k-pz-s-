using HosokCsataja;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Random rnd = new Random(7);

List<Hos> kek = new List<Hos>
{
    new Harcos("Brutusz", 100, 11),
    new Ijasz("Fürge", 70, 10),
    new Magus("Merlin", 60, 8),
};
List<Hos> voros = new List<Hos>
{
    new Harcos("Vasököl", 100, 10),
    new Ijasz("Szilánk", 70, 11),
    new Magus("Morgána", 60, 9),
};

int kor = 0;
while (VanElo(kek) && VanElo(voros) && kor < 30)
{
    kor++;
    Console.WriteLine($"--- {kor}. kör ---");
    Tamad(kek, voros, rnd);
    Tamad(voros, kek, rnd);

    foreach (Hos h in kek) if (h.El) h.KorVege();
    foreach (Hos h in voros) if (h.El) h.KorVege();
}

string gyoztes = VanElo(kek) && !VanElo(voros) ? "Kék csapat"
               : VanElo(voros) && !VanElo(kek) ? "Vörös csapat"
               : "döntetlen";
Console.WriteLine();
Console.WriteLine($"=== A csata {kor} kör után véget ért. Győztes: {gyoztes} ===");

List<string> tulelok = new List<string>();
foreach (Hos h in kek) if (h.El) tulelok.Add(h.ToString());
foreach (Hos h in voros) if (h.El) tulelok.Add(h.ToString());
Console.WriteLine($"Túlélők: {string.Join(", ", tulelok)}");

Hos legjobb = kek[0];
foreach (Hos h in kek) if (h.OkozottSebzes > legjobb.OkozottSebzes) legjobb = h;
foreach (Hos h in voros) if (h.OkozottSebzes > legjobb.OkozottSebzes) legjobb = h;
Console.WriteLine($"Legtöbb sebzést okozta: {legjobb.Nev} ({legjobb.Kaszt()}), {legjobb.OkozottSebzes} pont");

// ---------- Segédmetódusok ----------

// Eldöntés: van-e élő hős a csapatban?
static bool VanElo(List<Hos> csapat)
{
    int i = 0;
    while (i < csapat.Count && !csapat[i].El)
        i++;
    return i < csapat.Count;
}

// A támadó csapat minden élő hőse egy véletlen élő ellenfelet támad meg.
static void Tamad(List<Hos> tamadok, List<Hos> vedok, Random rnd)
{
    foreach (Hos t in tamadok)
    {
        if (!t.El)
            continue;

        List<Hos> elok = new List<Hos>();
        foreach (Hos v in vedok)
            if (v.El)
                elok.Add(v);
        if (elok.Count == 0)
            return;

        t.Megtamad(elok[rnd.Next(elok.Count)], rnd);   // polimorf hívások a Megtamad belsejében
    }
}
