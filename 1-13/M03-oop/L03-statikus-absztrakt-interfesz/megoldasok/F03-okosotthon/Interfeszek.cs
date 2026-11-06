namespace Okosotthon;

public interface IKapcsolhato
{
    bool Bekapcsolva { get; }
    void Be();
    void Ki();
}

public interface ISzabalyozhato
{
    int Szint { get; set; }
    int MinSzint { get; }
    int MaxSzint { get; }
}

public interface IFogyaszto
{
    double Teljesitmeny();   // watt
}
