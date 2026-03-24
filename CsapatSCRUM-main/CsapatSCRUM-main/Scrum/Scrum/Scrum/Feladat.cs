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

            public override string ToString()
            {
                string keszvagy = kesz ? "Kész" : "Nincs kész";
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
                    listaa.Add(egy);
                }
                Console.Clear();
                Console.WriteLine($"Adj meg egy opciót: (1-5)");
                Console.WriteLine("[1]Feladat hozzáadás");
                Console.WriteLine("[2]Kilistázás");
                Console.WriteLine("[3]Feladat törlés");
                Console.WriteLine("[4]Feladat szerkesztés");
                Console.WriteLine("[5]Feladat státusza");

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
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Feladat státusza");
                        Status(listaa);
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
                Console.WriteLine(item);
            }
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
                Console.WriteLine($"{i + 1} - {lista[i]}");
            }
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
                Console.WriteLine($"[{i + 1}] {lista[i]}");
            }

            if (!int.TryParse(Console.ReadLine(), out int sorszam))
            {
                Console.WriteLine("Érvénytelen bemenet!");
                return;
            }

            if (sorszam < 1 || sorszam > lista.Count)
            {
                Console.WriteLine("Érvénytelen sorszám!");
                return;
            }

            lista.RemoveAt(sorszam - 1);
            HozzaadasUj(lista);
            Console.WriteLine("Sikeresen törölte!");
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
                Console.WriteLine($"{i + 1} - {lista[i]}");
            }
            Console.WriteLine("Melyiket szeretnéd módosítani? ");

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
            Console.WriteLine("Legyen 'Kész'? (i/n): ");
            string valasz = Console.ReadLine().ToLower();

            listK temp = lista[index];
            temp.kesz = (valasz == "i");
            lista[index] = temp;

            HozzaadasUj(lista);
            Console.WriteLine("Státusz sikeresen módosítva!");
        }
    }
}