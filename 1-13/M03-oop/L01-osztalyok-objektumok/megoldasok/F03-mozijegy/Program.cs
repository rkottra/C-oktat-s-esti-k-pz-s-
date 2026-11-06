using Mozijegy;

List<Eloadas> eloadasok = new List<Eloadas>
{
    new Eloadas("Kincsem", "14:00", 60, 1990),
    new Eloadas("Toy Story 4", "16:15", 80, 1990),
    new Eloadas("Dűne – második rész", "20:45", 30, 2890),
    new Eloadas("A Pál utcai fiúk", "18:30", 45, 2490)
};

// Néhány előre lefoglalt jegy, hogy legyen mit nézni
eloadasok[0].Foglal(12);
eloadasok[1].Foglal(35);
eloadasok[2].Foglal(25);

bool fut = true;
while (fut)
{
    Console.WriteLine("\n1 – Előadások listázása  2 – Jegyfoglalás  3 – Lemondás  4 – Statisztika  0 – Kilépés");
    Console.Write("Választás: ");
    string valasztas = Console.ReadLine() ?? "";

    switch (valasztas)
    {
        case "1":
            for (int i = 0; i < eloadasok.Count; i++)
                Console.WriteLine($"{i + 1}. {eloadasok[i]}");
            break;

        case "2":
        case "3":
            Eloadas? e = EloadasBekerese(eloadasok);
            if (e == null)
                break;
            int db = SzamBekerese("Jegyek száma: ");
            if (valasztas == "2")
            {
                if (e.Foglal(db))
                    Console.WriteLine($"Sikeres foglalás: {e}");
                else
                    Console.WriteLine($"Nincs elég szabad hely (szabad: {e.Szabad}).");
            }
            else
            {
                if (e.Lemond(db))
                    Console.WriteLine($"Sikeres lemondás: {e}");
                else
                    Console.WriteLine($"Ennyi jegyet nem lehet lemondani (foglalt: {e.Foglalt}).");
            }
            break;

        case "4":
            int jegyek = 0;
            int bevetel = 0;
            int teltHaz = 0;
            Eloadas legjobb = eloadasok[0];
            foreach (Eloadas el in eloadasok)
            {
                jegyek += el.Foglalt;
                bevetel += el.Bevetel;
                if (el.Telt)
                    teltHaz++;
                if (el.Kihasznaltsag > legjobb.Kihasznaltsag)
                    legjobb = el;
            }
            Console.WriteLine($"Eladott jegyek: {jegyek} db");
            Console.WriteLine($"Bevétel: {bevetel:N0} Ft");
            Console.WriteLine($"Legnagyobb kihasználtság: {legjobb.Kezdes} {legjobb.FilmCim} ({legjobb.Kihasznaltsag:F0} %)");
            Console.WriteLine($"Telt házas előadás: {teltHaz}");
            break;

        case "0":
            fut = false;
            break;

        default:
            Console.WriteLine("Nincs ilyen menüpont.");
            break;
    }
}

// Addig kér be, amíg egész számot nem kap.
static int SzamBekerese(string uzenet)
{
    while (true)
    {
        Console.Write(uzenet);
        if (int.TryParse(Console.ReadLine(), out int szam))
            return szam;
        Console.WriteLine("Egész számot adj meg!");
    }
}

// Sorszám alapján visszaadja az előadást, vagy null-t, ha nincs ilyen.
static Eloadas? EloadasBekerese(List<Eloadas> lista)
{
    int sorszam = SzamBekerese("Előadás sorszáma: ");
    if (sorszam < 1 || sorszam > lista.Count)
    {
        Console.WriteLine($"Nincs ilyen előadás (1–{lista.Count}).");
        return null;
    }
    return lista[sorszam - 1];
}
