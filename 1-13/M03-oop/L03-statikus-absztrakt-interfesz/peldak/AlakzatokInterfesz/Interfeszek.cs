namespace AlakzatokInterfesz;

// Szerződés: aki síkidom, annak tudnia kell a nevét, területét, kerületét.
// Az interfész egy másik interfészből is örökölhet: így minden síkidom
// összehasonlítható is (CompareTo), és a List<ISikidom>.Sort() magától rendez.
public interface ISikidom : IComparable<ISikidom>
{
    string Nev { get; }
    double Terulet();
    double Kerulet();
}

// Egy másik, független képesség: átméretezhetőség.
public interface IAtmeretezheto
{
    void Nagyit(double szorzo);
}
