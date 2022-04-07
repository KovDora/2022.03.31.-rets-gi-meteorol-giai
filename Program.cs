using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace _2022._03._31.Éretségi_meteorológiai
{
    class Program
    {
        class Távirat
        {
            public string telepules;
            public TimeSpan ido;
            public string szelirany;
            public int szelerosseg;
            public int homerseklet;

            public Távirat(string[] sortomb)
            {
                this.telepules = sortomb[0];
                this.ido = new TimeSpan(int.Parse(sortomb[1].Substring(0, 2)),
                                        int.Parse(sortomb[1].Substring(2, 2)),
                                        0);
                this.szelirany = sortomb[2].Substring(0, 3);
                this.szelerosseg = int.Parse(sortomb[2].Substring(3, 2));
                this.homerseklet = int.Parse(sortomb[3]);
            }
        }

        static int kozephomerseklet_ora_alapjan(string telepules, List<Távirat> lista, int ora)
        {
            int i = 0;
            while (i < lista.Count &&
                    !(lista[i].ido.Hours == ora && lista[i].telepules == telepules))
            {
                i++;
            }
            return i < lista.Count ? i : -1;
        }




        static string kozephomerseklet(string telepules, List<Távirat> lista)
        {
            // 1 órás

            int holvan_1 = kozephomerseklet_ora_alapjan(telepules, lista, 1);
            if (holvan_1 == -1)
            {
                return "NA";
            }


            // 7 órás
            int holvan_7 = kozephomerseklet_ora_alapjan(telepules, lista, 7);
            if (holvan_7 == -1)
            {
                return "NA";
            }

            // 13 órás
            int holvan_13 = kozephomerseklet_ora_alapjan(telepules, lista, 13);
            if (holvan_13 == -1)
            {
                return "NA";
            }

            // 19 órás
            int holvan_19 = kozephomerseklet_ora_alapjan(telepules, lista, 19);
            if (holvan_19 == -1)
            {
                return "NA";
            }
            int osszeg = lista[holvan_1].homerseklet + lista[holvan_7].homerseklet + lista[holvan_13].homerseklet + lista[holvan_19].homerseklet;
            return ((double)osszeg / 4).ToString("0");
        }
        static void Main(string[] args)
        {
            string[] sorok = File.ReadAllLines("tavirathu13", Encoding.UTF8);

            List<Távirat> taviratlista = new List<Távirat>();

            foreach (string sor in sorok)
            {
                string[] sortomb = sor.Split(' ');
                Távirat t = new Távirat(sortomb);
                taviratlista.Add(t);
            }
            Console.WriteLine(taviratlista.Count);

            {
                Console.WriteLine("2. feladat");
                Console.WriteLine("Adja meg egy település kódját! Település: ");
                string felhasznalo_adat = Console.ReadLine();

                int i = taviratlista.Count - 1;
                while (i>=0&&!(taviratlista[i].telepules==felhasznalo_adat))
                {
                    i--;
                }
                Console.WriteLine( $"Az utolsó mérési adat a megadott településről {taviratlista[i].ido.Hours}:{taviratlista[i].ido.Hours}-kor érkezett.");
            }

            {
                Console.WriteLine("3. feladat");
                Console.WriteLine("Mikor mérték a legnagyobb és a legkisebb hőmérsékletet?"); //+ telepüülés, időpont, hőmérséklet
                int min = taviratlista[0].homerseklet;
                int max = taviratlista[0].homerseklet;

                Távirat leghidegebb = taviratlista[0];
                Távirat legmelegebb = taviratlista[0];
                
                for (int i = 1; i < taviratlista.Count; i++)
                {
                    if (taviratlista[i].homerseklet<min)
                    {
                        min = taviratlista[i].homerseklet;
                        leghidegebb = taviratlista[i];
                    }
                    if (taviratlista[i].homerseklet > min)
                    {
                        max = taviratlista[i].homerseklet;
                        legmelegebb = taviratlista[i];
                    }
                }
                Console.WriteLine($"A leghidegebb hőmérséklet:{leghidegebb.telepules} {leghidegebb.ido.Hours}:{leghidegebb.ido.Minutes} {leghidegebb.homerseklet}");
                Console.WriteLine($"A legmelegebb hőmérséklet:{legmelegebb.telepules} {legmelegebb.ido.Hours}:{legmelegebb.ido.Minutes} {legmelegebb.homerseklet}");
            }
            {
                Console.WriteLine("4. feladat");//+település, idő, szélerősség
                Console.WriteLine("Írjuk ki a azokat a településeket, ahol szélcsend van");
                bool volte = false;
                for (int i = 0; i < taviratlista.Count; i++)
                {
                    if (taviratlista[i].szelerosseg == 0 && taviratlista[i].szelirany == "000")
                    {
                        Console.WriteLine($"{taviratlista[i].telepules} {taviratlista[i].ido.Hours.ToString("00")}:{taviratlista[i].ido.Minutes.ToString("00")}");
                        volte = true;
                    }
                }

                if (!volte)
                {
                    Console.WriteLine("Nem volt szélcsend a mérések idején");
                }
            }
            {
                Console.WriteLine("5. feladat");
                Console.WriteLine("Határozza meg a települések napi hőmérsékletét és a hőmérséklet ingadozást");
                Dictionary<string, string> szotar = new Dictionary<string, string>();
                foreach (Távirat távirat in taviratlista)
                {
                    if (!szotar.ContainsKey(távirat.telepules))
                    {
                        szotar[távirat.telepules]= szotar[távirat.telepules] = kozephomerseklet(távirat.telepules,taviratlista);
                    }
                }
            }

        }

    }
}
