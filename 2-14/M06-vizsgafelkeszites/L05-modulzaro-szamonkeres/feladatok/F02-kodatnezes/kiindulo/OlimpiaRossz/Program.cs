// FIGYELEM: ez a program SZÁNDÉKOSAN rossz minőségű – a kódátnézés gyakorlásához készült.
// Lefordul és lefut, de hibás eredményeket ad, és nehezen olvasható. Ne másold!

namespace OlimpiaRossz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] s = File.ReadAllLines("olimpiak.csv");
            List<string[]> l = new List<string[]>();
            for (int i = 1; i < s.Length - 1; i++)
            {
                l.Add(s[i].Split(';'));
            }

            Console.WriteLine("2. feladat: " + l.Count);

            int db = 0;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i][3] == "Európa") db++;
            }
            Console.WriteLine("3. feladat: Európa: " + db);
            int db2 = 0;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i][3] == "Ázsia") db2++;
            }
            Console.WriteLine("3. feladat: Ázsia: " + db2);

            //for (int i = 0; i < l.Count; i++)
            //{
            //    Console.WriteLine(l[i][1]);
            //}

            Console.Write("4. feladat: ");
            for (int i = 0; i < l.Count; i++)
            {
                for (int j = 0; j < l.Count; j++)
                {
                    if (i != j && l[i][1] == l[j][1])
                    {
                        Console.Write(l[i][1] + " ");
                    }
                }
            }
            Console.WriteLine();

            Console.Write("6. feladat: Ország: ");
            string x = Console.ReadLine();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i][2] == x)
                {
                    Console.WriteLine(l[i][0]);
                }
                else
                {
                    Console.WriteLine("Nem rendezett.");
                }
            }

            try
            {
                StreamWriter sw = new StreamWriter("europai.txt");
                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i][3] == "Európa")
                        sw.WriteLine(l[i][0] + " " + l[i][1]);
                }
                Console.WriteLine("7. feladat: kész");
            }
            catch
            {
            }

            Console.ReadKey();
        }
    }
}
