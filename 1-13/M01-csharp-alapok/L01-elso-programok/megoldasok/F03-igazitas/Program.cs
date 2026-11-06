// 13-M01-L01-F03 · Jobbra és középre igazítás

const int Oszlop = 40;   // const: a programban nem változhat

Console.Write("Igazítás (J = jobbra, K = középre): ");
char valasz = char.ToUpper(Console.ReadKey().KeyChar);   // egy billentyű, Enter nélkül
Console.WriteLine();
bool jobbra = valasz == 'J';

Console.ForegroundColor = ConsoleColor.White;
string sor = Console.ReadLine() ?? "";

while (sor != "")
{
    // Hány szóközzel kell beljebb kezdeni?
    int behuzas = jobbra ? Oszlop - sor.Length : Oszlop - sor.Length / 2;
    behuzas = Math.Max(0, behuzas);   // túl hosszú sornál ne legyen negatív

    Console.ForegroundColor = ConsoleColor.Cyan;

    // 1. megoldás: szóközök kiírása ciklussal
    // for (int i = 0; i < behuzas; i++)
    // {
    //     Console.Write(' ');
    // }

    // 2. megoldás: kurzor pozicionálása az aktuális sorban
    Console.SetCursorPosition(behuzas, Console.CursorTop);
    Console.WriteLine(sor);

    Console.ForegroundColor = ConsoleColor.White;
    sor = Console.ReadLine() ?? "";
}

Console.ResetColor();
