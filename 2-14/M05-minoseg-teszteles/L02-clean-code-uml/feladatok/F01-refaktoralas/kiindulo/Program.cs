using ParkolohazRegi;

var p = new P();
int[] t = { 20, 30, 31, 90, 61, 600, 700, 1440, 1500 };
bool[] m = { false, false, false, false, true, false, false, true, false };
bool[] tv = { false, false, false, true, false, false, true, false, false };

Console.WriteLine("Parkolási díjak");
for (int i = 0; i < t.Length; i++)
{
    int x = p.D(t[i], m[i], tv[i]);
    if (x == -1)
    {
        if (m[i]) Console.WriteLine(t[i] + " perc, motor" + (tv[i] ? ", törzsvendég" : "") + ": HIBA (24 óránál hosszabb)");
        else Console.WriteLine(t[i] + " perc, autó" + (tv[i] ? ", törzsvendég" : "") + ": HIBA (24 óránál hosszabb)");
    }
    else
    {
        if (m[i]) Console.WriteLine(t[i] + " perc, motor" + (tv[i] ? ", törzsvendég" : "") + ": " + x + " Ft");
        else Console.WriteLine(t[i] + " perc, autó" + (tv[i] ? ", törzsvendég" : "") + ": " + x + " Ft");
    }
}
