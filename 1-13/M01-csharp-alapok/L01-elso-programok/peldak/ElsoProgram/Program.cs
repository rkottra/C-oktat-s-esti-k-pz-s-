// Klasszikus programszerkezet: névtér → osztály → Main metódus.
// (Visual Studióban: új projektnél a „Do not use top-level statements” be van pipálva.)
namespace ElsoProgram
{
    internal class Program
    {
        /// <summary>
        /// A program belépési pontja: innen indul a végrehajtás.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Téglalap kerülete és területe");
            Console.WriteLine("-----------------------------");

            #region Adatok bekérése
            // Pythonban: a = float(input("a = "))
            // C#-ban a bekérés és az átalakítás két külön lépés.
            Console.Write("a oldal (cm): ");
            double a = double.Parse(Console.ReadLine()!);   // magyar Windowson tizedesvessző: 2,5

            Console.Write("b oldal (cm): ");
            double b = double.Parse(Console.ReadLine()!);
            #endregion

            #region Számolás
            // Tegyél ide töréspontot (F9), és nézd meg a Locals ablakban a változókat!
            double kerulet = 2 * (a + b);
            double terulet = a * b;
            bool negyzet = a == b;
            #endregion

            #region Kiírás
            // Szövegbe ágyazás: Pythonban f"...", C#-ban $"..."
            Console.WriteLine($"Kerület: {kerulet:F2} cm");
            Console.WriteLine($"Terület: {terulet:F2} cm²");

            /* Feltételes operátor (?:) – Pythonban:
               "négyzet" if negyzet else "nem négyzet" */
            Console.WriteLine($"Az alakzat {(negyzet ? "négyzet" : "nem négyzet")}.");
            #endregion

            Console.Write("\nA kilépéshez nyomj meg egy billentyűt...");
            Console.ReadKey();
        }
    }
}
