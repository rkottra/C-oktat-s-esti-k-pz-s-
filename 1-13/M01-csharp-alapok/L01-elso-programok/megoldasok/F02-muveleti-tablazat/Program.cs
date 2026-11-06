// 13-M01-L01-F02 · Műveleti táblázat

Console.Write("a = ");
int a = int.Parse(Console.ReadLine()!);
Console.Write("b = ");
int b = int.Parse(Console.ReadLine()!);

// Fejléc
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"{"MŰVELET",-20}{"EREDMÉNY",10}");
Console.ResetColor();

// -20: balra igazítva 20 széles mezőben, 10: jobbra igazítva 10 széles mezőben
Console.WriteLine($"{"összeg",-20}{a + b,10}");
Console.WriteLine($"{"különbség",-20}{a - b,10}");
Console.WriteLine($"{"szorzat",-20}{a * b,10}");

if (b != 0)
{
    Console.WriteLine($"{"egész osztás",-20}{a / b,10}");
    Console.WriteLine($"{"maradék",-20}{a % b,10}");
    Console.WriteLine($"{"valós osztás",-20}{(double)a / b,10:F3}");
}
else
{
    Console.WriteLine("nullával nem osztunk");
}

bool paros = a % 2 == 0;
bool oszthato = b != 0 && a % b == 0;   // && rövidzár: b == 0 esetén a % b ki sem értékelődik
bool pozitivak = a > 0 && b > 0;

Console.Write($"{"a páros?",-20}");
Console.ForegroundColor = paros ? ConsoleColor.Green : ConsoleColor.Red;
Console.WriteLine($"{paros,10}");
Console.ResetColor();

Console.Write($"{"a osztható b-vel?",-20}");
Console.ForegroundColor = oszthato ? ConsoleColor.Green : ConsoleColor.Red;
Console.WriteLine($"{oszthato,10}");
Console.ResetColor();

Console.Write($"{"mindkettő pozitív?",-20}");
Console.ForegroundColor = pozitivak ? ConsoleColor.Green : ConsoleColor.Red;
Console.WriteLine($"{pozitivak,10}");
Console.ResetColor();
