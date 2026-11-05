using Edzoterem;

var terem = new Terem();

var anna = new Tag(1, "Kovács Anna") { Berlet = new AlkalmasBerlet(18000) };
var bence = new Tag(2, "Tóth Bence")
{
    Berlet = new IdoszakosBerlet(14900, new DateOnly(2026, 11, 1), new DateOnly(2026, 11, 30))
};
var csilla = new Tag(3, "Nagy Csilla");   // nincs bérlete
terem.UjTag(anna);
terem.UjTag(bence);
terem.UjTag(csilla);

var nap = new DateOnly(2026, 11, 21);
int sikeres = 0;
for (int i = 0; i < AlkalmasBerlet.AlkalmakSzama; i++)
{
    if (terem.Belep(anna.TagSzam, nap.AddDays(i)))
        sikeres++;
}
string tizenegyedik = terem.Belep(anna.TagSzam, nap.AddDays(10)) ? "sikeres" : "sikertelen";
Console.WriteLine($"{anna.Nev} – {AlkalmasBerlet.AlkalmakSzama} alkalom: {sikeres} sikeres belépés, a 11. {tizenegyedik}");

var utolsoNap = new DateOnly(2026, 11, 30);
Console.WriteLine($"{bence.Nev} – {utolsoNap:yyyy.MM.dd.}: {Eredmeny(terem.Belep(bence.TagSzam, utolsoNap))}");
Console.WriteLine($"{bence.Nev} – {utolsoNap.AddDays(1):yyyy.MM.dd.}: {Eredmeny(terem.Belep(bence.TagSzam, utolsoNap.AddDays(1)))}");
Console.WriteLine($"Bérlet nélküli tag: {Eredmeny(terem.Belep(csilla.TagSzam, utolsoNap))}");
Console.WriteLine($"Nem létező tagszám: {Eredmeny(terem.Belep(99, utolsoNap))}");

Console.WriteLine($"Belépések {utolsoNap:yyyy.MM.dd}-án: {terem.NapiBelepesek(utolsoNap)}");

static string Eredmeny(bool siker) => siker ? "beléphet" : "nem léphet be";
