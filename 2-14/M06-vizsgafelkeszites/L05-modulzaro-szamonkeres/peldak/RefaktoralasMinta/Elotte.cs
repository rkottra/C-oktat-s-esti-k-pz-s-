namespace RefaktoralasMinta;

// ÁTNÉZÉS ELŐTT – a kódátnézés tipikus megállapításai a sorok mellett
public static class Elotte
{
    public static void Futtat(string[] s)                 // [olvashatóság] "s": semmitmondó név
    {
        int c = 0;                                         // [olvashatóság] "c": mit számol?
        int m = 0;
        string mn = "";
        for (int i = 1; i < s.Length; i++)
        {
            string[] t = s[i].Split(';');
            if (int.Parse(t[1]) * 100 / int.Parse(t[2]) >= 40) c++;   // [olvashatóság] varázsszám: 40
            if (int.Parse(t[1]) > m)                                  // [ismétlés] ugyanaz a Parse többször
            {
                m = int.Parse(t[1]);                                  // [hiba] a pontot hasonlítja, nem a százalékot
                mn = t[0];
            }
        }
        Console.WriteLine("\tMegfelelt: " + c + " fő");               // [felelősség] számol ÉS kiír egyszerre
        Console.WriteLine("\tLegjobb: " + mn);                        // [tesztelhetőség] nincs visszatérési érték
    }
}
