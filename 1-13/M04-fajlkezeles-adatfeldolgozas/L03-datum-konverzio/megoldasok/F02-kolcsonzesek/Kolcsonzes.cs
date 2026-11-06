using System.Globalization;

namespace Kolcsonzesek;

// kolcsonzesek.csv: Olvaso;Cim;Kolcsonzes;Visszahozas   (dátum: yyyy-MM-dd, üres = még kint van)
public class Kolcsonzes
{
    public const int KolcsonzesiIdo = 21;
    public const int NapiDij = 50;
    public const int MaxDij = 1500;

    public string Olvaso { get; }
    public string Cim { get; }
    public DateTime KolcsonzesDatum { get; }
    public DateTime? Visszahozas { get; }          // nullázható: null, ha még nem hozták vissza

    public Kolcsonzes(string sor)
    {
        string[] m = sor.Split(';');
        Olvaso = m[0];
        Cim = m[1];
        KolcsonzesDatum = DateTime.ParseExact(m[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Visszahozas = m[3] == "" ? null : DateTime.ParseExact(m[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public DateTime Hatarido => KolcsonzesDatum.AddDays(KolcsonzesiIdo);
    public bool Kint => !Visszahozas.HasValue;

    // Visszahozott könyvnél a visszahozásig, kint lévőnél a mai napig számolunk.
    public int KesesNapok(DateTime ma)
    {
        DateTime vege = Visszahozas ?? ma;           // ?? : ha null, akkor a jobb oldali érték
        int keses = (vege - Hatarido).Days;
        return keses > 0 ? keses : 0;
    }

    public int Dij(DateTime ma) => Math.Min(KesesNapok(ma) * NapiDij, MaxDij);

    // Csak visszahozott könyvre értelmes
    public int KolcsonzesiNapok => Visszahozas.HasValue ? (Visszahozas.Value - KolcsonzesDatum).Days : 0;
}
