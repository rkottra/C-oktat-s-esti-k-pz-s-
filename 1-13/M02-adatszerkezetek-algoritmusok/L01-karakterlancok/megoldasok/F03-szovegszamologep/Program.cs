Console.OutputEncoding = System.Text.Encoding.UTF8;

char[] muveletiJelek = { '+', '-', '*', '/' };

while (true)
{
    Console.Write("Kifejezés: ");
    string kifejezes = (Console.ReadLine() ?? "").Replace(" ", "");
    if (kifejezes == "")
        break;

    // Az 1. indextől keresünk, így a negatív első szám mínuszjele nem számít műveleti jelnek
    int hely = kifejezes.IndexOfAny(muveletiJelek, 1);
    if (hely < 0)
    {
        Console.WriteLine("Hiba: nem találok műveleti jelet.");
        continue;
    }

    char muvelet = kifejezes[hely];
    string bal = kifejezes[..hely];
    string jobb = kifejezes[(hely + 1)..];

    if (!int.TryParse(bal, out int a) || !int.TryParse(jobb, out int b))
    {
        Console.WriteLine("Hiba: a műveleti jel mindkét oldalán egész számnak kell állnia.");
        continue;
    }

    switch (muvelet)
    {
        case '+': Console.WriteLine($"= {a + b}"); break;
        case '-': Console.WriteLine($"= {a - b}"); break;
        case '*': Console.WriteLine($"= {a * b}"); break;
        case '/':
            if (b == 0)
                Console.WriteLine("Hiba: nullával nem lehet osztani.");
            else
                Console.WriteLine($"= {(double)a / b:F2}");
            break;
    }
}
Console.WriteLine("Viszlát!");
