// 13-M01-L02-F01 · Számkitalálós játék

Random r = new Random();
bool ujra;

do
{
    int gondolt = r.Next(1, 101);    // 1..100
    int tippekSzama = 0;
    int tipp = 0;

    Console.WriteLine("Gondoltam egy számra 1 és 100 között.");
    do
    {
        Console.Write("Tipp: ");
        if (!int.TryParse(Console.ReadLine(), out tipp) || tipp < 1 || tipp > 100)
        {
            Console.WriteLine("Ez nem 1 és 100 közötti egész szám!");
            continue;    // a ciklusfeltételre ugrik; az érvénytelen tipp sosem egyezik a gondolt számmal
        }

        tippekSzama++;
        if (tipp < gondolt)
        {
            Console.WriteLine("Nagyobbra gondoltam.");
        }
        else if (tipp > gondolt)
        {
            Console.WriteLine("Kisebbre gondoltam.");
        }
    } while (tipp != gondolt);

    Console.WriteLine($"Eltaláltad {tippekSzama} tippből!");

    // Csak i vagy n fogadható el: addig kérdezünk, amíg a válasz NEM i ÉS NEM n
    char valasz;
    do
    {
        Console.Write("Új játék? (i/n): ");
        valasz = char.ToLower(Console.ReadKey().KeyChar);
        Console.WriteLine();
    } while (valasz != 'i' && valasz != 'n');

    ujra = valasz == 'i';
} while (ujra);

Console.WriteLine("Viszlát!");
