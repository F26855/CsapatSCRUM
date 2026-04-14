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
            public string feladat { get; set; }
            public bool kesz { get; set; }

            public bool halado { get; set; }

            public override string ToString()
            {
                string keszvagy = kesz ? "Kész" : "Nincs kész";
                string halad = halado ? "Haladásban" : "Jelenleg nem halad";
                return $"{feladat} - {keszvagy}";
            }
        }

        public void Menu()
        {

            while (true)
            {

                List<listK> listaa = new List<listK>();
                listK egy = new listK();
                string[] temp = File.ReadAllLines("bazis.txt");
                foreach (var s in temp)
                {
                    if (string.IsNullOrWhiteSpace(s)) continue;

                    string[] darab = s.Split(',');
                    egy.feladat = darab[0];
                    egy.kesz = Convert.ToBoolean(darab[1]);
                    egy.halado = (darab.Length > 2) ? Convert.ToBoolean(darab[2]) : false;
                    listaa.Add(egy);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine($"Adj meg egy opciót: (1-6)");
                Console.WriteLine("[1]Feladat hozzáadás");
                Console.WriteLine("[2]Kilistázás");
                Console.WriteLine("[3]Feladat törlés");
                Console.WriteLine("[4]Feladat szerkesztés");
                Console.WriteLine("[5]Feladat státusza");
                Console.WriteLine("[6]Kiszűrés");

                if (!int.TryParse(Console.ReadLine(), out int megadottszam))
                {
                    Console.WriteLine("Érvénytelen bemenet!");
                    Console.ReadLine();
                    continue;
                }

                switch (megadottszam)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Feladat hozzáadás");
                        Console.WriteLine("Adjon meg egy feladatot: ");
                        string megadottfeladat = Console.ReadLine();
                        Hozzaadas(megadottfeladat);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Sikeresen hozzáadva");
                        Console.ForegroundColor = ConsoleColor.White;


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
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Feladat státusza");
                        Status(listaa);
                        break;
                    case 6:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("Kiszűrés");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Szures(listaa);
                        break;
                }
                Console.ReadLine();
            }


        }

        public void Hozzaadas(string feladat)
        {
            File.AppendAllText("bazis.txt", feladat + ",False" + Environment.NewLine);
        }

        public void Listazas(List<listK> lista)
        {
            
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs feladat!");
                return;
            }

            foreach (var item in lista)
            {
                if (item.kesz)
                {
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item);
                }
                else
                {
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(item);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Szerkesztes(List<listK> lista)
        {
            
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs feladat!");
                return;
            }

            for (int i = 0; i < lista.Count; i++)
            {
                

                if (lista[i].kesz)
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i + 1} - {lista[i]}");
                }
                else
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{i + 1} - {lista[i]}");
                }


            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Melyiket szeretnéd szerkeszteni? ");
            


            if (!int.TryParse(Console.ReadLine(), out int szerkesztesID))
            {
                Console.WriteLine("Érvénytelen bemenet!");
                return;
            }

            if (szerkesztesID < 1 || szerkesztesID > lista.Count)
            {
                Console.WriteLine("Érvénytelen sorszám!");
                return;
            }

            int index = szerkesztesID - 1;
            Console.WriteLine("Mi legyen az új feladat: ");
            string ujfeladat = Console.ReadLine();

            listK temp = lista[index];
            temp.feladat = ujfeladat;
            lista[index] = temp;

            HozzaadasUj(lista);
            Console.WriteLine("Sikeresen szerkesztve!");
        }

        public void HozzaadasUj(List<listK> lista)
        {
            List<string> sorok = new List<string>();
            foreach (var item in lista)
            {
                sorok.Add(item.feladat + "," + item.kesz);
            }
            File.WriteAllLines("bazis.txt", sorok);
        }

        public void Torles(List<listK> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs feladat!");
                return;
            }

            Console.WriteLine("Törlendő feladat sorszáma: ");

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].kesz)
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i + 1} - {lista[i]}");
                }
                else
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{i + 1} - {lista[i]}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (!int.TryParse(Console.ReadLine(), out int sorszam))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Érvénytelen bemenet!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (sorszam < 1 || sorszam > lista.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Érvénytelen sorszám!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            lista.RemoveAt(sorszam - 1);
            HozzaadasUj(lista);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sikeresen törölte!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Status(List<listK> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs feladat!");
                return;
            }

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].kesz)
                {
                    string haladvagy = lista[i].halado ? "Haladásban" : "Jelenleg nem halad";
                    string kesz = lista[i].halado ? "Kész" : "Nincs kész";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{i + 1} - {lista[i]}");
                    if (haladvagy == "Haladásban" && kesz == "Nincs Kész")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Haladásban");
                    }
                    else if (haladvagy == "Jelenleg nem halad" && kesz == "Nincs Kész")
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{haladvagy}");
                    }

                }
                else
                {
                    string haladvagy = lista[i].halado ? "Haladásban" : "Jelenleg nem halad";
                    string kesz = lista[i].halado ? "Kész" : "Nincs kész";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{i + 1} - {lista[i]}");
                    if (haladvagy == "Haladásban" )
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Haladásban");
                    }
                    else if (haladvagy == "Jelenleg nem halad")
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{haladvagy}");
                    }
                    
                    
                    
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("Melyiket szeretnéd módosítani? ");

            if (!int.TryParse(Console.ReadLine(), out int szerkesztesID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Érvénytelen bemenet!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (szerkesztesID < 1 || szerkesztesID > lista.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Érvénytelen sorszám!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            int index = szerkesztesID - 1;
            Console.WriteLine("Legyen 'Kész'? (i/n): ");
            string valasz = Console.ReadLine().ToLower();
            Console.WriteLine("Legyen 'Haladásba'? (i/n): ");
            string valasz2 = Console.ReadLine().ToLower();

            // update both flags on the same struct instance, using the correct inputs
            listK temp = lista[index];
            temp.kesz = (valasz == "i");
            temp.halado = (valasz2 == "i");
            lista[index] = temp;

            HozzaadasUj(lista);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Státusz sikeresen módosítva!");
            Console.ForegroundColor = ConsoleColor.White;


        }


        public void Szures(List<listK> lista)
        {
            
            Console.WriteLine("A kész feladatok:");
            
            foreach (var item in lista)
            {
                
                if (item.kesz == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    Console.WriteLine($"- {item.feladat}");
                    Console.ForegroundColor = ConsoleColor.White;

                }
              
            }
            Console.WriteLine("A nem megcsinált feladatok: ");
            foreach (var item in lista)
            {
                if (item.kesz == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    Console.WriteLine($"- {item.feladat}");
                }
            }
             
        }
    }
}