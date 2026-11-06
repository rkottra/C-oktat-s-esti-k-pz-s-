// 13-M01-L01-F01 · Üzemidő átváltása

Console.Write("Üzemidő másodpercben: ");
long osszesMp = long.Parse(Console.ReadLine()!);   // long: több milliárd is belefér

// Egész osztás + maradék: lépésenként „levágjuk” a nagyobb egységeket
long nap = osszesMp / 86400;
long maradek = osszesMp % 86400;

long ora = maradek / 3600;
maradek %= 3600;

long perc = maradek / 60;
long mp = maradek % 60;

Console.WriteLine($"{osszesMp} másodperc = {nap} nap {ora} óra {perc} perc {mp} másodperc");

// Valós osztáshoz az egyik operandus legyen valós (3600.0)
double orakban = osszesMp / 3600.0;
Console.WriteLine($"Ez összesen {orakban:F2} óra.");
