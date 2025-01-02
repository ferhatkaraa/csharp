using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhat_Kara_Proje_231041008
{
    static class Tahta
    {
        private static string Color;
        //Class çağırılır ve tek sefer olacak şekilde tahta rengi sorulur ve belirlenir daha sonra asıl method maketable çalışır.
        static Tahta()
        {
            
            Color="-1";
            Console.WriteLine("Tahtanın rengini seç");
            //önce tahtalar bastırılır daha sonra renk seçtirilir
            //Tahta.cs dosyasında kullanılabilecek bircolor değişkenine atanır
            while (Color == "-1")
               {
                Console.WriteLine("1-");
                //tahta seçenekleri bastırılır 
                writetable("b-r", true);
                Console.WriteLine("2-");
                //tahta rengi-2
                writetable("b-y", true);
                Console.WriteLine("3-");
                //tahta rengi-3
                writetable("r-y", true);
                Console.WriteLine();
                Console.Write("renk seç:");
                Color = Console.ReadLine();
                switch (Color)
                   {
                       case "1":
                        Color = "b-r";
                        break;
                       case "2":
                        Color = "b-y"; 
                        break;
                       case "3":
                        Color = "r-y"; 
                        break;
                       default:
                        Console.WriteLine("HATA: 1-2-3 rakamlarından birini girmelisiniz.");
                        Color = "-1";
                        break;
                   }
   }     
             
        }
        //_color: renk seçer
        //picture: taşların olup olmadığına karar verir
        //taslar: taşların olduğu liste
        //göstermelik ve maçlık olmak üzere iki türlü tahta gösterimini sağlat
        internal static void writetable(string _color = "b-r", bool picture = false, dynamic[,] taslar = null!)
        {
            int color_bg1 = 9;
            int color_bg2 = 12;
            string ortala=" ";//tahtayı ortalama
            if (_color == "b-r")
            {
                color_bg1 = 9;
                color_bg2 = 12;

            }
            else if (_color == "b-y")
            {
                color_bg1 = 9;
                color_bg2 = 6;
            }
            else if (_color == "r-y")
            {
                color_bg1 = 12;
                color_bg2 = 6;
            }
            else { Console.WriteLine("HATA:tanımlanmayan renk hatası"); }
            if (!picture)
            {
                ortala = "                              ";
                

            }
            //tahta hazırlama kısmı
            //----------------------------------------------
            //renklerin kare oluşturması ayarlanır eğer picure true ise taşlar da konumlarına göre eklenir
            try
            {
                int a;

                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(ortala);
                Console.BackgroundColor = (ConsoleColor)color_bg1;
                Console.Write("╔");
                for (int i = 0; i < 8; i++)
                {
                    Console.BackgroundColor = i % 2 == 0 ? (ConsoleColor)color_bg1 : (ConsoleColor)color_bg2;
                    if (i != 7)
                    {
                        Console.Write("═══╦");
                    }
                    else
                    {
                        Console.Write("═══╗");
                        Console.ResetColor();
                        Console.WriteLine();
                    }

                }


                for (int i = 0; i < 8; i++)
                {
                    Console.ResetColor();
                    Console.Write(ortala);
                    for (int j = 0; j < 8; j++)
                    {
                        Console.BackgroundColor = j % 2 == 0 ? (ConsoleColor)color_bg1 : (ConsoleColor)color_bg2;

                        if (taslar == null || taslar[7 - i, j] == " ")
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("║   ");
                        }
                        else
                        {
                            //burada taş rengine göre değişiklik gerekli
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = j % 2 == 0 ? (ConsoleColor)color_bg1 : (ConsoleColor)color_bg2;
                            Console.Write($"║ ");
                            Console.ForegroundColor = taslar[7 - i, j].renk == "beyaz" ? ConsoleColor.White : ConsoleColor.Black;
                            Console.Write($"{taslar[7 - i, j].sembol} ");
                        }

                    }
                    Console.ForegroundColor= ConsoleColor.Black;
                    Console.Write("║");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.Write(ortala);
                    if (i != 7)
                    {
                        a = color_bg1;
                        color_bg1 = color_bg2;
                        color_bg2 = a;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = (ConsoleColor)color_bg1;
                        Console.Write("╠");
                        for (int k = 0; k < 8; k++)
                        {
                            Console.BackgroundColor = k % 2 == 0 ? (ConsoleColor)color_bg1 : (ConsoleColor)color_bg2;
                            if (k != 7)
                            {
                                Console.Write("═══╬");
                            }
                            else
                            {
                                Console.Write("═══╣");
                                Console.ResetColor();
                                Console.WriteLine();
                            }

                        }

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = (ConsoleColor)color_bg1;
                        Console.Write("╚");
                        for (int q = 0; q < 8; q++)
                        {
                            Console.BackgroundColor = q % 2 == 1 ? (ConsoleColor)color_bg2 : (ConsoleColor)color_bg1;
                            if (q != 7)
                            {
                                Console.Write("═══╩");
                            }
                            else
                            {
                                Console.Write("═══╝");
                                Console.ResetColor();
                            }

                        }

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"HATA:{e}");
            }


            //--------------------------------------------------------

            Console.ResetColor();
        Console.WriteLine();
        }
       
        //sayfayı 30 ENTER ile temizler sadece tahta kalmasını sağlar
        internal static void yenile()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine();
            }
        }
        
        //hamlenin kontrolünden hangi taş olduğuna kadar tespit edilir
        //mat ve pat durumu puanlar burada ayarlanır
        //taşlar güncellenir
        internal static void hamle_yap(ref bool renk,string _hamle,dynamic[,] taslar,ref int _fps,ref int _sps)
        {
            bool _pat = false;
            bool _mat = false;
            bool finish = false;
            bool _hata = false;
            for (int i = 0; i < 8; i++)
            {            
                if (finish || _hata) break;
                for(int j = 0; j < 8; j++)
                {
                    if (finish || _hata) break;
                    if (taslar[i, j] != " ")
                    {//type Taş
                        if (renk == (taslar[i, j].renk == "beyaz"))
                        {//ilgili renk

                            //hatalı notasyon yazımlarının doğurduğu exceptionlardan kurtulma
                            try
                            {
                                if ((taslar[i, j].sembol == "S") && (_hamle.Contains("0")))
                                {//rok

                                    taslar[i, j].rok();
                                    finish = true;
                                    break;

                                }
                                else if (taslar[i, j].sembol.ToString() == _hamle[0].ToString())
                                {//büyük taşlar
                                    Console.WriteLine("büyüktaş hamlesi");
                                    if ((_hamle.Contains("x") && _hamle[1].ToString() != "x") || !(int.TryParse(_hamle[2].ToString(), out int a)))
                                    {//konum bulmaya yardımcı değer araması
                                        Console.WriteLine("if dalı");
                                        //hamlenin kontrol edilmesi şahmat pat durumlarının analizedilmesi için gönderilen ilk method
                                        taslar[i, j].hamle_kontrol(_hamle, _hamle[1].ToString(), ref finish, ref _mat, ref _pat, ref _fps, ref _sps, taslar);

                                    }
                                    else
                                    {//yardımcıkarakter yok bu yüzden " " verilir
                                        Console.WriteLine("else dalı");
                                        taslar[i, j].hamle_kontrol(_hamle, " ", ref finish, ref _mat, ref _pat, ref _fps, ref _sps, taslar);
                                    }

                                }
                                else if (taslar[i, j].sembol == "P" && char.IsLower(_hamle[0]))
                                {//piyon
                                 //konuma yardımcı karakter burada ilk karakterdir
                                    taslar[i, j].hamle_kontrol(_hamle, _hamle[0].ToString(), ref finish, ref _mat, ref _pat, ref _fps, ref _sps, taslar);
                                }

                            }
                            catch (Exception e)
                            {
                                _hata = true;
                                Console.WriteLine("hatalı notasyon yazımı");
                                Console.WriteLine(e.Message);
                                break;
                            }
                        }
                    }
                }
            }
            //hamlenin doğru yapılıp yapılmadığı mat pat bilgisi ve puanlama yapılır
            if (finish)
            {
                if (_mat)
                {
                    if (renk) Console.WriteLine("beyaz kazandı.");
                    else Console.WriteLine("siyah kazandı");
                    if (renk == (_fps == _sps))
                    {
                        
                        _fps++;
                        renk = false;
                    }
                    else
                    {
                        _sps++;
                        renk = true;
                    }
                    
                }else renk = !renk;
                if (_pat)
                {
                    Console.WriteLine("Maç berabere");
                }
                finish = false;
            }
            else
            {
                Console.WriteLine("Hamle yapılamadı. Yazımınıza dikkat edin");
                Console.WriteLine("ÖRNEK YAZIM:Kaxa5+");
                Console.WriteLine("K:büyük harf taşı simgeler");
                Console.WriteLine("a:iki kale de aynı kareye gidiyorsa ayırt edici olması için yazılır.");
                Console.WriteLine("a5: gidilecek kare");
                Console.WriteLine("x:taş alma +:şah çekme");
            }
            

        }
        
        //taşların hepsini bir method altında listeye ekledik
        static void taslari_ekle(dynamic[,] hepsi)
        {
            new Kale(new int[] { 0, 0 }, "K", "beyaz").addtable(hepsi);
            new Kale(new int[] { 0, 7 }, "K", "beyaz").addtable(hepsi);
            new Kale(new int[] { 7, 0 }, "K", "siyah").addtable(hepsi);
            new Kale(new int[] { 7, 7 }, "K", "siyah").addtable(hepsi);

            new At(new int[] { 0, 1 }, "A", "beyaz").addtable(hepsi);
            new At(new int[] { 0, 6 }, "A", "beyaz").addtable(hepsi);
            new At(new int[] { 7, 1 }, "A", "siyah").addtable(hepsi);
            new At(new int[] { 7, 6 }, "A", "siyah").addtable(hepsi);

            new Fil(new int[] { 0, 2 }, "F", "beyaz").addtable(hepsi);
            new Fil(new int[] { 0, 5 }, "F", "beyaz").addtable(hepsi);
            new Fil(new int[] { 7, 2 }, "F", "siyah").addtable(hepsi);
            new Fil(new int[] { 7, 5 }, "F", "siyah").addtable(hepsi);

            new Vezir(new int[] { 0, 3 }, "V", "beyaz").addtable(hepsi);
            new Sah(new int[] { 0, 4 }, "S", "beyaz").addtable(hepsi);
            new Vezir(new int[] { 7, 3 }, "V", "siyah").addtable(hepsi);
            new Sah(new int[] { 7, 4 }, "S", "siyah").addtable(hepsi);

            new Piyon(new int[] { 1, 0 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 1, 1 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 1, 2 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 1, 3 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 1, 4 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 1, 5 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 1, 6 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 1, 7 }, "P", "beyaz").addtable(hepsi);
            new Piyon(new int[] { 6, 0 }, "P", "siyah").addtable(hepsi);
            new Piyon(new int[] { 6, 1 }, "P", "siyah").addtable(hepsi);
            new Piyon(new int[] { 6, 2 }, "P", "siyah").addtable(hepsi);
            new Piyon(new int[] { 6, 3 }, "P", "siyah").addtable(hepsi);
            new Piyon(new int[] { 6, 4 }, "P", "siyah").addtable(hepsi);
            new Piyon(new int[] { 6, 5 }, "P", "siyah").addtable(hepsi);
            new Piyon(new int[] { 6, 6 }, "P", "siyah").addtable(hepsi);
            new Piyon(new int[] { 6, 7 }, "P", "siyah").addtable(hepsi);

        }

        
        //ilk çalıştırılan method budur
        //constructor static methodun renk belirlemesi ardından çalışır
        //oyuncuların puan ve isim bilgilerini parametre olarak alır
        public static int maketable(string firstplayer,string secondplayer,int fps, int sps)
        {
            bool sira = true;
            string hamle;
            //oyun notasyon yazımı anlatılır
            Console.ResetColor();
            Console.WriteLine("OYUN BAŞLIYOR...");
            Console.WriteLine("iki olan kazanır");
            Console.WriteLine("ÖRNEK YAZIM:Kaxa5+");
            Console.WriteLine("K:büyük harf taşı simgeler");
            Console.WriteLine("a:iki kale de aynı kareye gidiyorsa ayırt edici olması için yazılır.");
            Console.WriteLine("a5: gidilecek kare");
            Console.WriteLine("x:taş alma +:şah çekme");
            //boş liste önce her alanı boşluk ile doldurdum ki tahtada boş olanlar sorun çıkarmasın
            dynamic[,] tümtaslar = 
            {
                {" "," "," "," "," "," "," "," " },
                {" "," "," "," "," "," "," "," " },
                {" "," "," "," "," "," "," "," " },
                {" "," "," "," "," "," "," "," " },
                {" "," "," "," "," "," "," "," " },
                {" "," "," "," "," "," "," "," " },
                {" "," "," "," "," "," "," "," " },
                {" "," "," "," "," "," "," "," " }
            };
            //taşları oluştur ve listeye ekle
            
            taslari_ekle(tümtaslar);
            while (fps!=2||sps!=2)
            {
                //Tahta burada bastırılır ilk baştaki color değişkeni kullanılır
                Console.Write("                                  ");
                Console.WriteLine($"{firstplayer}:{fps}-{sps}:{secondplayer}");
                writetable(Color,false,tümtaslar);
                //her el oyuncu 1 beyaz olmasın diyeayarlama yapıldı
                if (fps - sps == 0)
                {
                    if (sira)
                    {
                        Console.Write($"{firstplayer} hamleni yap:");
                    }
                    else { Console.Write($"{secondplayer} hamleni yap:"); }
                }
                else
                {
                    if (sira)
                    {
                        Console.Write($"{secondplayer} hamleni yap:");
                    }
                    else { Console.Write($"{firstplayer} hamleni yap:"); }
                }
                hamle = Console.ReadLine();
                //kullanıcıdan hamlesi alınır ve hamle_yap mehodu açğrılır
                if (hamle.Length >= 2 && hamle.Length <= 6)
                {
                    hamle_yap(ref sira, hamle!, tümtaslar, ref fps, ref sps);

                }
                else Console.WriteLine("Hamle en az 2 en fazla 6 karakterden oluşmalıdır");

                //sayfa yenilenir
                yenile();
            }
            //bu fark program.cs dosyasına döner burdan gelen sayı kimin kaç kaç yendiğini gösterir
            return fps-sps;
        }

        /*
╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗
║ K ║ A ║ F ║ V ║ S ║ F ║ A ║ K ║
╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
║ P ║ P ║ P ║ P ║ P ║ P ║ P ║ P ║   
╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
║   ║   ║   ║   ║   ║   ║   ║   ║
╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
║   ║   ║   ║   ║   ║   ║   ║   ║
╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
║   ║   ║   ║   ║   ║   ║   ║   ║
╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
║   ║   ║   ║   ║   ║   ║   ║   ║
╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
║ P ║ P ║ P ║ P ║ P ║ P ║ P ║ P ║
╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
║ K ║ A ║ F ║ V ║ S ║ F ║ A ║ K ║
╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝

 
         */
    }
}
