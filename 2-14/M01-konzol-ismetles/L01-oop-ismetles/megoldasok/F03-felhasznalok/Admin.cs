namespace Felhasznalok;

public class Admin : Felhasznalo
{
    public List<string> Modulok { get; } = new();

    public Admin(string felhasznalonev, string jelszo, params string[] modulok)
        : base(felhasznalonev, jelszo)
    {
        Modulok.AddRange(modulok);
    }

    public override string Jogosultsag => "rendszergazda";

    public override string ToString() => base.ToString() + $" modulok: {string.Join(", ", Modulok)}";
}
