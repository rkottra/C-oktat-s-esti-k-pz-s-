using Allatorvos;

const int Evszam = 2026;

var peter = new Gazdi("Nagy Péter", "Szeged");
peter.AllatotFelvesz(new Kutya("Bodri", 2019, "puli"));
peter.AllatotFelvesz(new Macska("Cirmi", 2023, benti: true));

var eszter = new Gazdi("Kiss Eszter", "Pécs");
eszter.AllatotFelvesz(new Macska("Mirr", 2021, benti: false));

var gabor = new Gazdi("Szabó Gábor", "Győr");
gabor.AllatotFelvesz(new Kutya("Morzsa", 2024, "vizsla"));
gabor.AllatotFelvesz(new Kutya("Pamacs", 2022, "keverék"));

var rendelo = new Rendelo();
rendelo.UjGazdi(peter);
rendelo.UjGazdi(eszter);
rendelo.UjGazdi(gabor);

foreach (var gazdi in rendelo.Gazdik)
{
    Console.WriteLine($"{gazdi.Nev} ({gazdi.Telepules})");
    foreach (var allat in gazdi.Allatok)
        Console.WriteLine($"  {allat.Nev} – {allat.Leiras()}, {allat.Kor(Evszam)} éves, oltás: {allat.OltasiDij():N0} Ft");
}

Console.WriteLine($"Oltási bevétel: {rendelo.OltasiBevetel():N0} Ft");

var legidosebb = rendelo.Legidosebb(Evszam);
if (legidosebb != null)
    Console.WriteLine($"Legidősebb: {legidosebb.Nev} ({legidosebb.Kor(Evszam)} éves)");
