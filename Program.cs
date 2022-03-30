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

        }

    }
}
