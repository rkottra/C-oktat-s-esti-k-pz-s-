// A termekek.csv egy sora: Nev;Kategoria;GyartasiEv;Ar;Keszlet
class Termek
{
    public string Nev { get; set; }
    public string Kategoria { get; set; }
    public int GyartasiEv { get; set; }
    public int Ar { get; set; }
    public int Keszlet { get; set; }

    public Termek(string sor)
    {
        string[] d = sor.Split(';');
        Nev = d[0];
        Kategoria = d[1];
        GyartasiEv = int.Parse(d[2]);
        Ar = int.Parse(d[3]);
        Keszlet = int.Parse(d[4]);
    }

    // a raktáron lévő mennyiség teljes értéke
    public int KeszletErtek => Ar * Keszlet;

    public override string ToString()
    {
        return $"{Nev} ({Kategoria}, {GyartasiEv}) – {Ar:N0} Ft, készlet: {Keszlet} db";
    }
}
