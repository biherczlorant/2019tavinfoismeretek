using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2019tavinfoismeretek
{
    class Beolvas
    {
        public string nev;
        public int rajtszam;
        public string kategoria;
        public string ido;
        public int tavszazalek;
        
        public Beolvas(string sor)
        {
            string[] bonto = sor.Split(';');
            nev = bonto[0];
            rajtszam = Convert.ToInt32(bonto[1]);
            kategoria = bonto[2];
            //string[] idobonto = bonto[3].Split(':');
            //ora = Convert.ToInt32(idobonto[0]);
            //perc = Convert.ToInt32(idobonto[1]);
            //sec = Convert.ToInt32(idobonto[2]);
            ido = bonto[3];
            tavszazalek = Convert.ToInt32(bonto[4]);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // 2. feladat
            List<Beolvas> data = new List<Beolvas>();
            foreach(var i in File.ReadAllLines("ub2017egyeni.txt").Skip(1))
            {
                data.Add(new Beolvas(i));
            }
            // 3. feladat
            Console.WriteLine($"3. feladat: Egyéni indulók: {data.Count} fő");
            // 4. feladat
            int teljesit = 0;
            foreach(var i in data)
            {
                if(i.kategoria == "Noi" && i.tavszazalek == 100)
                {
                    teljesit++;
                }
            }
            Console.WriteLine($"4. feladat: Célba érkező női sportolók: {teljesit} fő");
            // 5. feladat
            Console.Write("5. feladat: Kérem a sportoló nevét: ");
            string bekernev = Console.ReadLine();
            int segi = 0;
            foreach (var i in data)
            {
                if (i.nev == bekernev)
                {
                    segi++;
                    Console.WriteLine("\tIndult egyéniben a sportoló? Igen");
                    if (i.tavszazalek == 100)
                    {
                        Console.WriteLine("\tTeljesítette a teljes távot? Igen");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\tTeljesítette a teljes távot? Nem");
                        break;
                    }
                }
            }
            if(segi == 0)
            {
                Console.WriteLine("\tIndult egyéniben a sportoló? Nem");
            }
            // 6. feladat
            static double IdőÓrában(int ora, int perc, int masodperc)
            {
                return perc / 60 + masodperc / 3600 + ora;
            }
            // 7. feladat
            double osszora = 0;
            int atlagsegit = 0;
            foreach(var i in data)
            {
                if(i.kategoria == "Ferfi" && i.tavszazalek == 100)
                {
                    atlagsegit++;
                    string[] orabontva = i.ido.Split(':');
                    osszora += IdőÓrában(Convert.ToInt32(orabontva[0]), Convert.ToInt32(orabontva[1]), Convert.ToInt32(orabontva[2]));
                }
            }
            Console.WriteLine($"7. feladat: Átlagos idő: {osszora/atlagsegit} óra");
            // 8. feladat
            double noiminido = 100;
            double ferfiminido = 100;
            string ferfido = "";
            string noido = "";
            string ferfid = "";
            string noid = "";
            string fernev = "";
            string nonev = "";
            foreach(var i in data)
            {
                if(i.tavszazalek == 100)
                {
                    if(i.kategoria == "Noi")
                    {
                        string[] orabontva = i.ido.Split(":");
                        if(noiminido > IdőÓrában(Convert.ToInt32(orabontva[0]), Convert.ToInt32(orabontva[1]), Convert.ToInt32(orabontva[2])))
                        {
                            noiminido = IdőÓrában(Convert.ToInt32(orabontva[0]), Convert.ToInt32(orabontva[1]), Convert.ToInt32(orabontva[2]));
                            noido = i.ido;
                            noid = Convert.ToString(i.rajtszam);
                            nonev = i.nev;

                        }
                    }
                    if(i.kategoria == "Ferfi")
                    {
                            string[] orabontva = i.ido.Split(":");
                            if (ferfiminido > IdőÓrában(Convert.ToInt32(orabontva[0]), Convert.ToInt32(orabontva[1]), Convert.ToInt32(orabontva[2])))
                            {
                                ferfiminido = IdőÓrában(Convert.ToInt32(orabontva[0]), Convert.ToInt32(orabontva[1]), Convert.ToInt32(orabontva[2]));
                                ferfido = i.ido;
                                ferfid = Convert.ToString(i.rajtszam);
                                fernev = i.nev;
                            }
                    }
                }
            }
            Console.WriteLine($"8. feladat: Verseny győztesei\n\tNők: {nonev} ({noid}.) - {noido}\n\tFérfiak: {fernev} ({ferfid}.) - {ferfido}");
        }
    }
}
