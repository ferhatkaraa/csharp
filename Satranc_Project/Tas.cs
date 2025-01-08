using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Ferhat_Kara_Proje_231041008
{
    public enum Harfler
    {//tahta harflerini sayısallaştırma
        a, b, c, d, e, f, g, h,
    }
    
    public interface IKontrol
    {//kontroller interface içerisinde Sah classına kalıtılır
        bool Sahkontrol(string _renk,string _sembol);
        public bool Sahkontrol(int[] __konum);
        bool Matkontrol();
        bool Patkontrol();
    }

    internal abstract class Tas
    {//tüm classlar bu classı kalıtımına alır
        //ikinci ve üçüncü maçlar başladığında taşların yeniden dizilmesi için bu listeler lazım
        public static List<Tas> Alls = new List<Tas>();
        public static Dictionary<Tas, int[]> AllLocate = new Dictionary<Tas, int[]>();
        public bool check = false;
        public int[] konum;
        protected internal string sembol;
        protected internal bool hareket_etti = false;
        protected internal string __renk;
        public string renk
        {
            get
            {
                return __renk;
            }
            set
            {
                if (!new List<string>() { "beyaz", "siyah" }.Contains(value))
                {
                    if ((konum[0] == 0 || konum[0] == 1) && !hareket_etti)
                    {
                        __renk = "beyaz";
                    }
                    else if ((konum[0] == 6 || konum[0] == 7) && !hareket_etti)
                    {
                        __renk = "siyah";
                    }
                }
                else
                {
                    __renk = value;
                }
            }
        }
        protected internal Tas(int[] _konum,string _sembol,string _renk)
        {
            this.konum = _konum;
            this.sembol = _sembol;  
            this.renk = _renk;
            AllLocate.Add(this, _konum);
            Alls.Add(this); 
        }

        //her class bu methodu override ederek kendi karelerini belirlediler
        public abstract List<int[]> gidilecekkareler() ;

        //taşl class özelliklerinin stringlerle karşılaştırması için gerekli
        public static bool operator ==(Tas left, string right)
        {
            return left.renk == right || left.sembol == right;
        }
        public static bool operator !=(Tas left, string right)
        {
            return !(left.renk == right) || !(left.sembol == right);
        }

        //hamle gidilecekkarelerde şah mı bunu kontrol eder
        internal void hamle_kontrol(string hml,string konum_yardım,ref bool found,ref bool mate,ref bool pat,ref int p1,ref int p2, dynamic[,] sonkonumlar)
        {
            bool xbool = hml.Contains("x");
            Console.WriteLine(xbool);
            bool artibool = hml.Contains("+");
            Console.WriteLine(artibool);
            int[] nereye;
            int[] eskikonum = new int[2];
            dynamic yedek;

            if (artibool)
            {
                nereye = new int[] { int.Parse(hml.Reverse().ToArray()[1].ToString()) - 1, (int)Enum.Parse(typeof(Harfler), hml.Reverse().ToArray()[2].ToString()) };
            }
            else
            {
                nereye = new int[] { int.Parse(hml.Reverse().ToArray()[0].ToString()) - 1, (int)Enum.Parse(typeof(Harfler), hml.Reverse().ToArray()[1].ToString()) };

            }
            //hedef konuma gidebilecek karesi var mı booleni
            bool varmi = false;
            List<int[]> gecici = new List<int[]>(this.gidilecekkareler());
            foreach (int[] item in gecici)
            {//contains methodu çalışmadığı için bu şekilde yaptım
                if (nereye[0] == item[0] && nereye[1] == item[1])
                {
                    varmi = true;
                }
            }
            
            if (!(varmi) || (konum_yardım != " " && (this.konum[1] != (int)Enum.Parse(typeof(Harfler), konum_yardım))))
            {
                return;
            }

            //konum yedekleme ve değişiklik
            yedek = sonkonumlar[nereye[0], nereye[1]];
            sonkonumlar[nereye[0], nereye[1]] = this;
            sonkonumlar[this.konum[0],this.konum[1]] = " ";
            eskikonum[0] = this.konum[0];
            eskikonum[1] = this.konum[1];
            this.konum[0] = nereye[0];
            this.konum[1] = nereye[1];
            foreach (Sah item in Alls.OfType<Sah>())
            {
                if (this.renk== item.renk)
                    {
                        if (item.Sahkontrol(this.renk,this.sembol))
                        {
                            sonkonumlar[nereye[0], nereye[1]] = yedek;
                            sonkonumlar[eskikonum[0], eskikonum[1]] = this;
                        this.konum[0] = eskikonum[0];
                        this.konum[1] = eskikonum[1];

                        }else
                        {
                        Console.WriteLine("konum değiştirildi");
                        
                            found = true;
                            if (yedek.GetType() != typeof(string))
                            {
                                Alls.Remove(yedek);
                            }
                        foreach (Sah s in Alls.OfType<Sah>())
                        {
                            if (s.renk != this.renk)
                            {
                                Console.WriteLine("şah kontrol");
                                if (s.Sahkontrol(s.renk, s.sembol))
                                {
                                    Console.WriteLine("mat kontrol");
                                    mate = s.Matkontrol();
                                }
                                else
                                {
                                    Console.WriteLine("pat kontrol"); 
                                    pat = s.Patkontrol();
                                }
                            }
                        }
                    }
                }
                
                
            }

        }

        //taşların belirli bir listeye eklenmesi sağlanır
        public void addtable(dynamic[,] taslar)
        {
            if (this.konum.Length==2)
            {
                taslar[this.konum[0], this.konum[1]] = this;
            }
        }

    }

    internal class Sah : Tas,IKontrol
    {
       // override sahkontrol method
        public bool Sahkontrol(string _renk,string _sembol)
        {
            foreach (Tas item in Alls)
            {
                if (this.renk != item.renk)
                {
                    if (item.check)
                    {
                        return true;
                    }
                    foreach (int[] i in item.gidilecekkareler())
                    {
                        if (i[0] == this.konum[0] && i[1] == this.konum[1])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //overload Sahkontrol method
        public bool Sahkontrol(int[] __konum)
        {
            this.check = false;
            int[] eskikonum = new int[] { this.konum[0], this.konum[1] };
            dynamic sakla = " ";
            foreach (Tas item in Alls)
            {
                if (this.renk != item.renk)
                {
                    if (item.konum == __konum)
                    {
                        sakla = item;
                        AllLocate.Remove(item);
                        this.konum = __konum;
                        break;
                    }
                    
                }
            }
            this.check = Sahkontrol(this.renk,"S");
            if (sakla != " ")
            {
                Alls.Add(sakla);
            }
            this.konum = eskikonum;
            return this.check;
        }


        public bool Matkontrol()
        {
            if (this.Sahkontrol(this.renk,this.sembol))
            {
                List<int[]> gecici = new List<int[]>(this.gidilecekkareler());
                foreach (int[] item in gecici)
                {//eğer gidebileceği herhangi bir karede şah olmuyorsa mat değildir
                    if (!this.Sahkontrol(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public bool Patkontrol()
        {
            List<int[]> gecici;
            if (!this.Sahkontrol(this.renk,this.sembol))
            {
                foreach (Tas item in Alls)
                {
                    if (item.renk == this.renk)
                    {
                        //yapabilecek bir hamlesi bile varsa pat olmaz
                        gecici = new List<int[]>(this.gidilecekkareler());
                        if (gecici.Count > 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        

        protected internal Sah(int[] _konum, string _sembol, string _renk) : base(_konum, _sembol, _renk) { }

        public void rok()
        {
            Console.WriteLine("rok attım.");
        }

        public override List<int[]> gidilecekkareler()
        {
            List<int[]> hareketler = new List<int[]>()
            {
                new int[] {this.konum[0]-1,this.konum[1]+1 },
                new int[] {this.konum[0],this.konum[1]+1 },
                new int[] {this.konum[0]+1,this.konum[1]+1 },
                new int[] {this.konum[0]-1,this.konum[1] },
                new int[] {this.konum[0]+1,this.konum[1] },
                new int[] {this.konum[0]-1,this.konum[1]-1 },
                new int[] {this.konum[0],this.konum[1]-1 },
                new int[] {this.konum[0]+1,this.konum[1]-1 },
            };
            List<int[]> gecici = new List<int[]>(hareketler);
            foreach (var item in gecici)
            {
                if (!((item[0] > -1 && item[0] < 8 && item[1] > -1 && item[1] < 8)))
                {
                    hareketler.Remove(item);
                }
            }

            foreach (Tas t in Alls)
            {
                if (hareketler.Contains(t.konum))
                {
                    if (t.renk == this.renk)
                    {
                        hareketler.Remove(t.konum);
                    }
                }


            }

            return hareketler;
        }

    }
    internal class Vezir : Tas
    {
        protected internal Vezir(int[] _konum, string _sembol, string _renk) : base(_konum, _sembol, _renk) { }

        //overload gidilecek kareler
        public List<int[]> gidilecekkareler(bool kale)
        {
            if (kale)
        {
            List<int[]> yeni_hareketler = new List<int[]>();

            int artisx = 1;
            int artisy = 0;
            void kareekle(int[] kare, int sayx, int sayy, bool sor = false)
            {

                while (kare[0] + sayx < 8 && kare[0] + sayx > -1 && kare[1] + sayy > -1 && kare[1] + sayy < 8)
                {

                    foreach (Tas t in Alls)
                    {
                        if (t.konum == new int[] { kare[0] + sayx, kare[1] + sayy })
                        {
                            sor = true;
                            if (t.renk != this.renk)
                            {
                                
                                yeni_hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (sor) sor = !sor;
                    else yeni_hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                    if (sayx < 0)
                    {
                        sayx--;
                    }
                    else if (sayx > 0)
                    {
                        sayx++;
                    }
                    if (sayy < 0)
                    {
                        sayy--;
                    }
                    else if (sayy > 0)
                    {
                        sayy++;
                    }
                }

            }

            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);
            artisx = 0;
            artisy = -1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisx = -1;
            artisy = 0;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisx = 0;
            artisy = 1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            return yeni_hareketler;
        }


            else return new List<int[]>();
        }
        //override gidilecek kareler
        public override List<int[]> gidilecekkareler()
        {
            List<int[]> hareketler = new List<int[]>();
            this.check = false;
            int artisx = 1;
            int artisy = 1;
            void kareekle(int[] kare, int sayx, int sayy, bool sor = false)
            {

                while (kare[0] + sayx < 8 && kare[0] + sayx > -1 && kare[1] + sayy > -1 && kare[1] + sayy < 8)
                {

                    foreach (Tas t in Alls)
                    {
                        if (t.konum == new int[] { kare[0] + sayx, kare[1] + sayy })
                        {
                            sor = true;
                            if (t.renk != this.renk)
                            {
                                if (t.sembol == "s")
                                {
                                    this.check = true;
                                }

                                hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (sor) sor = !sor;
                    else hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                    if (sayx < 0)
                    {
                        sayx--;
                    }
                    else if (sayx > 0)
                    {
                        sayx++;
                    }
                    if (sayy < 0)
                    {
                        sayy--;
                    }
                    else if (sayy > 0)
                    {
                        sayy++;
                    }
                }

            }

            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisy = -1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisx = -1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisy = 1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            hareketler.AddRange(gidilecekkareler(true));
            return hareketler;
        }

    }
    internal class Kale : Tas
        {
            protected internal Kale(int[] _konum, string _sembol, string _renk) : base(_konum, _sembol, _renk) { }
        public override List<int[]> gidilecekkareler()
        {
            List<int[]> hareketler = new List<int[]>();
            this.check = false;
            int artisx = 1;
            int artisy = 0;
            void kareekle(int[] kare, int sayx, int sayy, bool sor = false)
            {

                while (kare[0] + sayx < 8 && kare[0] + sayx > -1 && kare[1] + sayy > -1 && kare[1] + sayy < 8)
                {

                    foreach (Tas t in Alls)
                    {
                        if (t.konum == new int[] { kare[0] + sayx, kare[1] + sayy })
                        {
                            sor = true;
                            if (t.renk != this.renk)
                            {
                                if (t.sembol == "s")
                                {
                                    this.check = true;
                                }

                                hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (sor) sor = !sor;
                    else hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                    if (sayx < 0)
                    {
                        sayx--;
                    }
                    else if (sayx > 0)
                    {
                        sayx++;
                    }
                    if (sayy < 0)
                    {
                        sayy--;
                    }
                    else if (sayy > 0)
                    {
                        sayy++;
                    }
                }

            }

            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);
            artisx = 0;
            artisy = -1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisx = -1;
            artisy = 0;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisx = 0;
            artisy = 1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            return hareketler;
        }

    }
    internal class Fil : Tas
    {
        protected internal Fil(int[] _konum, string _sembol, string _renk) : base(_konum, _sembol, _renk) { }
        public override List<int[]> gidilecekkareler()
        {
            List<int[]> hareketler = new List<int[]>();
            this.check = false;
            int artisx = 1;
            int artisy = 1;
            void  kareekle(int[] kare,int sayx,int sayy, bool sor = false)
            {
                
                while (kare[0] + sayx < 8 && kare[0] + sayx > -1 && kare[1] + sayy > -1 && kare[1] + sayy < 8)
                {

                    foreach (Tas t in Alls)
                    {
                        if (t.konum == new int[] { kare[0] + sayx, kare[1] + sayy })
                        {
                            sor = true;
                            if (t.renk != this.renk)
                            {
                                if (t.sembol == "s")
                                {
                                    this.check = true;
                                }

                                hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (sor) sor = !sor;
                    else hareketler.Add(new int[] { kare[0] + sayx, kare[1] + sayy });
                    if (sayx < 0)
                    {
                        sayx--;
                    }
                    else if(sayx > 0)
                    {
                        sayx++;
                    }
                    if (sayy < 0)
                    {
                        sayy--;
                    }
                    else if(sayy > 0)
                    {
                        sayy++;
                    }
                }
                
            }
            
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1]+artisy },artisx,artisy);
            
            artisy = -1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisx = -1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            artisy = 1;
            kareekle(new int[] { this.konum[0] + artisx, this.konum[1] + artisy }, artisx, artisy);

            return hareketler;
        }

    }
    internal class At : Tas
    {
        protected internal At(int[] _konum, string _sembol, string _renk) : base(_konum, _sembol, _renk) { }
        public override List<int[]> gidilecekkareler()
        {
            List<int[]> hareketler = new List<int[]>()
            {
                new int[] {this.konum[0]+2,this.konum[1]+1 },
                new int[] {this.konum[0]+2,this.konum[1]-1 },
                new int[] {this.konum[0]+1,this.konum[1]+2 },
                new int[] {this.konum[0]-1,this.konum[1]+2 },
                new int[] {this.konum[0]-2,this.konum[1]+1 },
                new int[] {this.konum[0]-2,this.konum[1]-1 },
                new int[] {this.konum[0]+1,this.konum[1]-2 },
                new int[] {this.konum[0]-1,this.konum[1]-2 },
            };
            this.check = false;
            List<int[]> gecici = new List<int[]>(hareketler);
            foreach (var item in gecici)
            {
                if (!((item[0] > -1 && item[0] < 8 && item[1] > -1 && item[1] < 8)))
                {
                    hareketler.Remove(item);
                }
            }

            foreach (Tas t in Alls)
            {
                if (hareketler.Contains(t.konum))
                {
                    if (t.renk == this.renk)
                    {
                        hareketler.Remove(t.konum);
                    }
                    else
                    {
                        if (t.sembol == "s")
                        {
                            this.check = true;
                        }

                    }
                }


            }

            return hareketler;
        }

    }
    internal class Piyon : Tas
    {
        
        protected internal Piyon(int[] _konum, string _sembol, string _renk) : base(_konum, _sembol, _renk) { }
        
        public override List<int[]> gidilecekkareler() 
        {
            this.check = false;
            int artis = 1;
            if (this.renk == "siyah") artis = -1;
            List<int[]> hareketler = new List<int[]>()
            {
                new int[] {this.konum[0]+artis,this.konum[1] },
                new int[] {this.konum[0]+artis,this.konum[1]+1 },
                new int[] {this.konum[0]+artis,this.konum[1]-1 },
            };
            if (!hareket_etti)
            {
                hareketler.Add(new int[]{ this.konum[0] + 2*artis,this.konum[1] });
            }
            List<int[]> gecici = new List<int[]>(hareketler);
            foreach (var item in gecici)
            {
                if (!((item[0] > -1 && item[0] < 8 && item[1] > -1 && item[1] < 8)))
                {
                    hareketler.Remove(item);
                }
            }

            foreach (Tas t in Alls)
            {
                 if (hareketler.Contains(t.konum))
                {
                    if (t.konum[1] == this.konum[1])
                    {
                        hareketler.Remove(t.konum);
                        if (t.konum[0] - this.konum[0] == 1 || t.konum[0] - this.konum[0] == -1)
                        {
                            if (!hareket_etti)
                            {
                                hareketler.Remove(new int[] { t.konum[0] + artis, t.konum[1] });
                            }
                        }
                    }else
                    {
                        if (t.renk == this.renk)
                        {
                            hareketler.Remove(t.konum);
                        }
                        else
                        {
                            if (t.sembol == "s")
                            {
                                this.check = true;
                            }

                        }
                    }
                    
                }


            }


            return hareketler; 
        }
        

        protected internal void terfi()
        {

        }
    }


}


 
