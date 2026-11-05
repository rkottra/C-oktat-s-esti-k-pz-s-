namespace Edzoterem;

public abstract class Berlet
{
    public int Ar { get; }

    protected Berlet(int ar)
    {
        Ar = ar;
    }

    public abstract bool Ervenyes(DateOnly nap);

    // Alapértelmezésben a belépés nem változtat a bérleten; az alkalmas bérlet felülírja.
    public virtual void Belepes(DateOnly nap) { }
}

public class AlkalmasBerlet : Berlet
{
    public const int AlkalmakSzama = 10;

    public int MaradekAlkalom { get; private set; } = AlkalmakSzama;

    public AlkalmasBerlet(int ar) : base(ar) { }

    public override bool Ervenyes(DateOnly nap) => MaradekAlkalom > 0;

    public override void Belepes(DateOnly nap) => MaradekAlkalom--;
}

public class IdoszakosBerlet : Berlet
{
    public DateOnly Kezdet { get; }
    public DateOnly Vege { get; }

    public IdoszakosBerlet(int ar, DateOnly kezdet, DateOnly vege) : base(ar)
    {
        Kezdet = kezdet;
        Vege = vege;
    }

    public override bool Ervenyes(DateOnly nap) => Kezdet <= nap && nap <= Vege;
}
