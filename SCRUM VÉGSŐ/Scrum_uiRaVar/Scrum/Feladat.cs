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
            public bool halad { get; set; }

            public override string ToString()
            {
                string keszvagy = kesz ? "Kész" : (halad ? "Halad" : "Nincs kész");
                return $"{feladat} - {keszvagy}";
            }
        }

        public void Menu()
        {

            while (true)
            {

                List<listK> listaa = new List<listK>();
                string[] temp = File.ReadAllLines("bazis.txt");
                foreach (var s in temp)
                {
                    if (string.IsNullOrWhiteSpace(s)) continue;

                    string[] darab = s.Split(',');
                    listK egy = new listK();
                    egy.feladat = darab[0];
                    egy.kesz = Convert.ToBoolean(darab[1]);
                    if (darab.Length > 2)
                        egy.halad = Convert.ToBoolean(darab[2]);
                    else
                        egy.halad = false;
                    listaa.Add(egy);
                }
                Console.Clear();
                Console.WriteLine($"Adj meg egy opciót: (1-8)");
                Console.WriteLine("[1] Feladat hozzáadás");
                Console.WriteLine("[2] Kilistázás");
                Console.WriteLine("[3] Feladat törlés");
                Console.WriteLine("[4] Feladat szerkesztés");
                Console.WriteLine("[5] Feladat státusza (Kész/Nem kész)");
                Console.WriteLine("[6] Kiszűrés");
                Console.WriteLine("[7] Keresés");
                Console.WriteLine("[8] Haladás állítása");

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
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Kiszűrés");
                        Szures(listaa);
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Keresés");
                        Kereses(listaa);
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Haladás állítása");
                        Haladas(listaa);
                        break;
                }
                Console.ReadLine();
            }


        }

        void SzinesKiiras(listK item)
        {
            if (item.kesz)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (item.halad)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(item);
            Console.ResetColor();
        }

        public void Hozzaadas(string feladat)
        {
            File.AppendAllText("bazis.txt", feladat + ",False,False" + Environment.NewLine);
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
                SzinesKiiras(item);
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
                Console.Write($"{i + 1} - ");
                SzinesKiiras(lista[i]);
            }

            Console.WriteLine("Számok (pl: 1 3 5) -> státusz váltás");
            Console.WriteLine("'s' -> szöveg szerkesztés");

            string input = Console.ReadLine();

            if (input.ToLower() == "s")
            {
                Console.WriteLine("Melyiket szeretnéd szerkeszteni?");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Érvénytelen szám!");
                    return;
                }

                if (id < 1 || id > lista.Count)
                {
                    Console.WriteLine("Érvénytelen sorszám!");
                    return;
                }

                int index = id - 1;

                Console.WriteLine("Új szöveg:");
                string uj = Console.ReadLine();

                listK temp = lista[index];
                temp.feladat = uj;
                lista[index] = temp;
            }
            else
            {
                string[] darabok = input.Split(' ');

                foreach (var d in darabok)
                {
                    if (!int.TryParse(d, out int id))
                    {
                        Console.WriteLine($"Hibás szám: {d}");
                        continue;
                    }

                    if (id < 1 || id > lista.Count)
                    {
                        Console.WriteLine($"Érvénytelen sorszám: {id}");
                        continue;
                    }

                    int index = id - 1;

                    listK temp = lista[index];
                    temp.kesz = !temp.kesz;
                    if (temp.kesz) temp.halad = false;
                    lista[index] = temp;
                }
            }

            HozzaadasUj(lista);
            Console.WriteLine("Sikeres módosítás!");
        }

        public void HozzaadasUj(List<listK> lista)
        {
            List<string> sorok = new List<string>();
            foreach (var item in lista)
            {
                sorok.Add(item.feladat + "," + item.kesz + "," + item.halad);
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
                Console.Write($"[{i + 1}] ");
                SzinesKiiras(lista[i]);
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
                Console.Write($"{i + 1} - ");
                SzinesKiiras(lista[i]);
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
            if (temp.kesz) temp.halad = false;
            lista[index] = temp;

            HozzaadasUj(lista);
            Console.WriteLine("Státusz sikeresen módosítva!");
        }

        public void Haladas(List<listK> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs feladat!");
                return;
            }

            for (int i = 0; i < lista.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                SzinesKiiras(lista[i]);
            }
            Console.WriteLine("Melyik feladat haladási állapotát szeretnéd módosítani? ");

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
            Console.WriteLine("Folyamatban van? (i/n): ");
            string valasz = Console.ReadLine().ToLower();

            listK temp = lista[index];
            if (temp.kesz)
            {
                Console.WriteLine("Ez a feladat már kész, a haladás nem értelmezhető.");
                return;
            }
            temp.halad = (valasz == "i");
            lista[index] = temp;

            HozzaadasUj(lista);
            Console.WriteLine("Haladási állapot sikeresen módosítva!");
        }


        public void Szures(List<listK> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs feladat!");
                return;
            }

            Console.WriteLine("Kész feladatok");
            foreach (var item in lista)
            {
                if (item.kesz)
                {
                    SzinesKiiras(item);
                }
            }

            Console.WriteLine("\nNem kész feladatok");
            foreach (var item in lista)
            {
                if (!item.kesz)
                {
                    SzinesKiiras(item);
                }
            }
        }

        public void Kereses(List<listK> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nincs feladat!");
                return;
            }

            Console.WriteLine("Adj meg egy keresési szót: ");
            string keresett = Console.ReadLine().ToLower();

            bool talalat = false;
            Console.Clear();

            Console.WriteLine("Találatok:");

            foreach (var item in lista)
            {
                if (item.feladat.ToLower().Contains(keresett))
                {
                    SzinesKiiras(item);
                    talalat = true;
                }
            }

            if (!talalat)
            {
                Console.WriteLine("Nincs találat!");
            }
        }
    }
}