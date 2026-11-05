using UmlKonyvtar;

// A program a lecke README-jében lévő osztálydiagram megvalósítása.
var konyvtar = new Konyvtar();

var egri = new Konyv("Egri csillagok", "Gárdonyi Géza", 1901);
var pal = new Konyv("A Pál utcai fiúk", "Molnár Ferenc", 1907);
var tuskevar = new Konyv("Tüskevár", "Fekete István", 1957);
konyvtar.UjKonyv(egri);
konyvtar.UjKonyv(pal);
konyvtar.UjKonyv(tuskevar);

var anna = new Olvaso("Minta Anna", 1001);
var bela = new Olvaso("Teszt Béla", 1002);
konyvtar.UjOlvaso(anna);
konyvtar.UjOlvaso(bela);

Szemely konyvtaros = new Konyvtaros("Kovács Kata");   // ősosztály típusú változóban leszármazott
Console.WriteLine($"Ügyeletes: {konyvtaros}");

konyvtar.Kolcsonoz(anna, egri, new DateOnly(2026, 10, 1));
var palKolcsonzes = konyvtar.Kolcsonoz(bela, pal, new DateOnly(2026, 10, 20));

try
{
    konyvtar.Kolcsonoz(bela, egri, new DateOnly(2026, 10, 21));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}

Console.WriteLine("\nKeresés: „fe”");
foreach (var k in konyvtar.Keres("fe"))
    Console.WriteLine($"  {k} – {(konyvtar.Elerheto(k) ? "elérhető" : "kikölcsönözve")}");

palKolcsonzes.Visszahozva = true;

var ma = new DateOnly(2026, 11, 5);
Console.WriteLine($"\nKésésben ({ma}):");
foreach (var k in konyvtar.Kesesben(ma))
    Console.WriteLine($"  {k.Olvaso.Nev} – {k.Konyv.Cim}, határidő: {k.Hatarido()}");
