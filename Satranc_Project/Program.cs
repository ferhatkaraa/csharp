
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ferhat_Kara_Proje_231041008
{
    public class Program
    {
        

        
        static void Main(string[] args)
        {
            //projenin türkçe yazılabilmesi için gerekli
            Console.OutputEncoding = Encoding.UTF8;

            string oyuncu1;
            string oyuncu2;
            int puan1;
            int puan2;
            string menu = "-1";

            void süsle()
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.Write("♟");
                }
                Console.WriteLine();
            }
            // eğer oyuncu isimleri boş girilirse default isim girilir
            Console.WriteLine("HOŞGELDİNİZ");
            süsle();
            Console.Write("Oyuncu1:");
            oyuncu1 = Console.ReadLine();
            Console.WriteLine();
            if (oyuncu1 == "")
            {
                oyuncu1 = "Oyuncu1";
            }
            Console.Write("OYUNCU2:");
            oyuncu2 = Console.ReadLine();
            if (oyuncu2 == "")
            {
                oyuncu2 = "Oyuncu2";
            }
            süsle();
            // 1 oyuna girer 2 oyundan çıkar 
            //oyun bitince döngü buraya dmber 
            while (menu == "-1") {
                puan1 = 0;
                puan2 = 0;
                Console.WriteLine("1-oyuna başla\n2-çıkış");
                menu = Console.ReadLine();
                switch(menu){
                    case "-1":break;
                    case "1":break;
                    case "2":break;
                    default:
                        Console.WriteLine("Hata, lütfen size belirtilen rakamlardan birini giriniz.");
                        menu = "-1";
                        break;
                }
                //OYUN
                if (menu == "1") {
                    while (menu != "-1") {
                        //oyun Tahta.cs dosyasındaki fonksiyonlar ile oynanır 
                        //maketable puan1 değişkenine analiz edilecek sayiyi döner
                        // static constrauctor method olduğu için önce o çalışır sonra maketable çalışır
                        puan1 = Tahta.maketable(oyuncu1, oyuncu2, puan1, puan2);
                        süsle();
                        if (puan1 > 0)
                        {
                            Console.WriteLine($"{oyuncu1} kazandı.");
                            puan2 = 2 - puan1;
                            puan1 = 2;
                        }
                        else if (puan1 < 0)
                        {
                            Console.WriteLine($"{oyuncu2} kazandı.");
                            puan2 = 2;
                            puan1 = 2 + puan1;
                        }
                        else Console.WriteLine("Beklenmedik bir hata oluştu");
                        Console.WriteLine($"OYUN BİTTİ\n{oyuncu1}:{puan1}-{puan2}:{oyuncu2}");
                        Console.WriteLine("Tekrar oynamak ister misin?");
                        menu = "-1";
                        süsle();
                    }//while menu
                }//if menu
           }//while menu
        }//Main
    }//program class

}//namespace
//version 1.1
