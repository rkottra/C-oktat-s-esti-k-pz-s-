// Klasszikus programszerkezet: névtér, osztály, Main metódus.
// (A felső szintű utasításokat a fordító pontosan ilyen formába csomagolja.)
using System;
using System.Text;

namespace Metodusok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // --- Visszatérési érték ---
            double t = TeglalapTerulet(3, 4.5);
            Console.WriteLine($"A 3 × 4,5-ös téglalap területe: {t}");
            Console.WriteLine($"Az 5 négyzete: {Negyzet(5)}");

            // --- void metódus, alapértelmezett paraméterekkel ---
            Vonal();
            Vonal(30);
            Vonal(30, '=');

            // --- Érték szerinti átadás: a hívó változója nem változik ---
            int x = 10;
            Novel(x);
            Console.WriteLine($"Novel(x) után x = {x}");

            // --- ref: a metódus a hívó változóját módosítja ---
            int a = 1, b = 2;
            Csere(ref a, ref b);
            Console.WriteLine($"Csere után a = {a}, b = {b}");

            // --- out: több eredmény egy hívásból ---
            int[] szamok = { 7, -3, 12, 5, 0, 9 };
            MinMax(szamok, out int min, out int max);
            Console.WriteLine($"Legkisebb: {min}, legnagyobb: {max}");

            // --- Túlterhelés: ugyanaz a név, más paraméterek ---
            Console.WriteLine($"Egész tömb összege: {Osszeg(szamok)}");
            Console.WriteLine($"Valós tömb összege: {Osszeg(new double[] { 1.5, 2.25, 3 })}");

            // --- Tömb paraméter: az elemei módosíthatók ref nélkül is ---
            Duplaz(szamok);
            Console.WriteLine($"Duplázva: {string.Join(" ", szamok)}");

            // --- Ellenőrzött beolvasás metódusba szervezve ---
            int kor = BekerEgesz("Hány éves vagy? ", 0, 120);
            Console.WriteLine($"{(kor >= 18 ? "Nagykorú" : "Kiskorú")} vagy.");
        }

        static double TeglalapTerulet(double a, double b)
        {
            return a * b;
        }

        // Egysoros metódus „nyíl” szintaxissal
        static int Negyzet(int n) => n * n;

        static void Vonal(int hossz = 20, char jel = '-')
        {
            Console.WriteLine(new string(jel, hossz));
        }

        static void Novel(int szam)
        {
            szam++;   // csak a másolat változik
        }

        static void Csere(ref int p, ref int q)
        {
            (p, q) = (q, p);
        }

        static void MinMax(int[] t, out int min, out int max)
        {
            min = t[0];
            max = t[0];
            foreach (int e in t)
            {
                if (e < min) min = e;
                if (e > max) max = e;
            }
        }

        static int Osszeg(int[] t)
        {
            int s = 0;
            foreach (int e in t) s += e;
            return s;
        }

        static double Osszeg(double[] t)
        {
            double s = 0;
            foreach (double e in t) s += e;
            return s;
        }

        static void Duplaz(int[] t)
        {
            for (int i = 0; i < t.Length; i++)
                t[i] *= 2;
        }

        // Addig kérdez, amíg min és max közötti egész számot nem kap
        static int BekerEgesz(string kerdes, int min, int max)
        {
            while (true)
            {
                Console.Write(kerdes);
                if (int.TryParse(Console.ReadLine(), out int ertek) && ertek >= min && ertek <= max)
                    return ertek;
                Console.WriteLine($"{min} és {max} közötti egész számot adj meg!");
            }
        }
    }
}
