using ElfedesVagyFeluliras;

// Az mr változó típusa Madar, de egy Rigo objektumra mutat.
Madar m = new Madar("Madár");
Rigo r = new Rigo("Feketerigó");
Madar mr = new Rigo("Rigó a Madar változóban");

Console.WriteLine("--- ELFEDÉS (new): a VÁLTOZÓ típusa dönt (korai kötés) ---");
m.Bemutatkozik();
r.Bemutatkozik();
mr.Bemutatkozik();      // a Madar változata fut, pedig Rigo az objektum!

Console.WriteLine("\n--- FELÜLÍRÁS (virtual/override): az OBJEKTUM típusa dönt (késői kötés) ---");
m.Enekel();
r.Enekel();
mr.Enekel();            // a Rigo változata fut

Console.WriteLine("\n--- Ugyanez listában ---");
List<Madar> madarak = new List<Madar> { new Madar("Galamb"), new Rigo("Rigó"), new Cinege("Széncinege") };
foreach (Madar madar in madarak)
{
    madar.Bemutatkozik();   // mindig a Madar változata
    madar.Enekel();         // mindig a saját változata
}
