using KonyvtarMigracio;

using var db = new KonyvtarContext();

var egri = db.Konyvek.FirstOrDefault(k => k.Cim == "Egri csillagok");
if (egri != null && egri.Kolcsonozheto)
{
    egri.Kolcsonozheto = false;
    db.SaveChanges();
}

Console.WriteLine("Kölcsönözhető:");
foreach (var k in db.Konyvek.Where(k => k.Kolcsonozheto))
    Console.WriteLine($"  {k}");

Console.WriteLine("Nem kölcsönözhető:");
foreach (var k in db.Konyvek.Where(k => !k.Kolcsonozheto))
    Console.WriteLine($"  {k}");
