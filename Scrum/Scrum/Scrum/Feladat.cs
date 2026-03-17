using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrum
{
    public class Feladat
    {
        
        public Feladat()
        {
        }
        public struct listK
        {
            public string feladat;
        }
        
        public void Menu()
        {
            
            while (true)
            {

                List<string> listaa = new List<string>();
                listK egy = new listK();
                string[] temp = File.ReadAllLines("bazis.txt");
                foreach (var s in temp)
                {
                    egy.feladat = s;
                    listaa.Add(s);
                }
                Console.Clear();
                Console.WriteLine($"Adj meg egy opciót: (1-3)");
                Console.WriteLine("[1]Feladat hozzáadás");
                Console.WriteLine("[2]Kilistázás");
                Console.WriteLine("[3]Feladat törlés");
                int megadottszam = Convert.ToInt32(Console.ReadLine());
                switch (megadottszam)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Feladat hozzáadás");
                        Console.WriteLine("Adjon meg egy feladatot: ");
                        string megadottfeladat = Console.ReadLine();
                        Hozzaadas(megadottfeladat, listaa);
                        Console.WriteLine($"Sikeresen hozzáadva");
                        
                        
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Kilistázás");
                        Listazas(listaa);
                       
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine($"Feladat törlés");
                        break;
                }
                Console.ReadLine();
            }

            
        }

        public void Hozzaadas(string feladat, List<string> lista)
        {
            
             File.AppendAllText("bazis.txt", feladat + Environment.NewLine);
            
        }

        public void Listazas(List<string> lista)
        {
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
        }
    }
}
