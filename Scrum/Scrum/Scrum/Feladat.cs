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
                Console.WriteLine($"Adj meg egy opciót: (1-4)");
                Console.WriteLine("[1]Feladat hozzáadás");
                Console.WriteLine("[2]Kilistázás");
                Console.WriteLine("[3]Feladat törlés");
                Console.WriteLine("[4]Feladat szerkesztés");
                int megadottszam = Convert.ToInt32(Console.ReadLine());
                switch (megadottszam)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Feladat hozzáadás");
                        Console.WriteLine("Adjon meg egy feladatot: ");
                        string megadottfeladat = Console.ReadLine();
                        Hozzaadas(megadottfeladat);
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
                        Torles(listaa);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Feladat szerkesztése");
                        Szerkesztes(listaa);
                        break;
                }
                Console.ReadLine();
            }

            
        }

        public void Hozzaadas(string feladat)
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
        public void Szerkesztes(List<string> lista)
        {
            
            for (int i = 0; i < lista.Count(); i++)
            {
                 Console.WriteLine($"{i+1}-{lista[i]}");
            }
            Console.WriteLine("Melyiket szeretnéd szerkeszteni? ");
            int szerkesztesID = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < lista.Count(); i++)
            {
                if (i == szerkesztesID-1)
                {
                    string regifeladat = lista[i];
                    Console.WriteLine("Mi legyen az új feladat: ");
                    string ujfeladat = Console.ReadLine();
                    lista[i] = lista[i].Replace(regifeladat, ujfeladat);
                }
            }
            HozzaadasUj(lista);


        }
        public void HozzaadasUj(List<string> lista)
        {

            File.WriteAllLines("bazis.txt",lista);

        }
        public void Torles(List<string> lista)
        {
            Console.WriteLine("Törlendő feladat sorszáma (0-tól kezdve): ");


            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"[{i}] {lista[i]}");
            }
            int sorszam = Convert.ToInt32(Console.ReadLine());
            if (sorszam < 0 || sorszam >= lista.Count)
            {
                Console.WriteLine("Érvénytelen sorszám!");
                return;
            }
            lista.RemoveAt(sorszam);
            File.WriteAllLines("bazis.txt", lista);

            Console.WriteLine("Sikeresen törölte!");
        }

    }
}
