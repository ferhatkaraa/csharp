using System;
using System.Collections.Generic;

class Program
{
    //soru-7
    //-----------------------------------------------------------------------------------------
    static int calculatesum(int a, int b)
    {
        return a + b;
    }
    static double calculatesum(double a, double b)
    {
        return a + b;
    }
    static int calculatesum(int a, int b, int c)
    {
        return a + b + c;
    }
    //--------------------------------------------------------------------------------------------
    static void Main(string[] args)
    {

        /*
        1. Matematiksel Hesaplama Yapan Parametresiz ve Geriye Değer Döndüren Metot

        Soru: Bir metot yazın; bu metot, kullanıcıdan üçgenin taban uzunluğu ve yüksekliğini alıp, üçgenin alanını hesaplayarak geriye döndürsün. Alanı hesaplamak için Taban * Yükseklik / 2 formülünü kullanın.
        İpucu: double türünde dönen bir metot tanımlamalı ve kullanıcıdan alınan değerlerle hesaplama yapmalısınız.
        */
        //--------------------------------------------------------------------------------------------------------
        double alanhesapla()
        {
            Console.WriteLine("üçgenin taban bilgisini giriniz:");
            double Taban = Double.Parse(Console.ReadLine());
            Console.WriteLine("üçgenin yükseklik bilgisini giriniz:");
            double yükseklik = Convert.ToDouble(Console.ReadLine());
            Console.Write("üçgenin alanı hesaplandı:");
            return Taban * yükseklik * 1 / 2.0;
        }
        Console.WriteLine(alanhesapla());


        //--------------------------------------------------------------------------------------------------------

        /*
         2. Dizideki En Büyük Değeri Bulan Parametreli ve Geriye Değer Döndüren Metot

        Soru: int türünde bir dizi parametresi alan ve bu dizideki en büyük değeri bulan bir metot yazın.
        İpucu: int dönen bir metot tanımlayıp, foreach döngüsü ile diziyi dolaşarak en büyük değeri bulabilirsiniz.

         */
        //--------------------------------------------------------------------------------------------------------
        int maksvalue(int[] mylist)
        {
            int ansver = 0;
            foreach (int i in mylist)
            {
                if (ansver == 0) ansver = i;
                else if (ansver < i) ansver = i;
            }
            return ansver;
        }
        int[] myint = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.Write("Dizinin en büyük elemanı:");
        Console.WriteLine(maksvalue(myint));

        //--------------------------------------------------------------------------------------------------------

        /*
        3. Aşırı Yüklenmiş (Overloaded) Metot ile Farklı Türdeki Verilerin Toplamını Bulma

        Soru: Aynı isimle üç farklı CalculateSum metodu yazın. İlk metot iki int sayıyı toplasın, ikinci metot iki double sayıyı toplasın, üçüncü metot ise üç int sayıyı toplasın.
        İpucu: Metot isimleri aynı olmalı fakat parametre türleri farklı olmalıdır.

        */
        //--------------------------------------------------------------------------------------------------------
        //çözüm-7 en yukarıda main dışında
        Console.WriteLine(calculatesum(1, 2));
        Console.WriteLine(calculatesum(1.0, 2.0));
        Console.WriteLine(calculatesum(0, 1, 2));
        //--------------------------------------------------------------------------------------------------------
        /*
         4. Recursive Metot ile Fibonacci Dizisi Hesaplama

        Soru: Bir sayının Fibonacci dizisindeki karşılığını hesaplayan özyinelemeli (recursive) bir metot yazın. fibonacci(5) çağrısı, dizideki 5. Fibonacci sayısını döndürmelidir.
        İpucu: int dönen ve kendini çağırarak Fibonacci dizisi üreten bir metot yazmalısınız.
        
         */
        //--------------------------------------------------------------------------------------------------------
        int fib(int sor, List<int> fiblist = null)
        {
            if (fiblist == null)
            {
                fiblist = new List<int> { 1, 1 };
            }
            if (fiblist.Contains(sor))
                return fiblist.Count;
            else fiblist.Add(fiblist[fiblist.Count - 2] + fiblist[fiblist.Count - 1]);
            return fib(sor, fiblist);
        }
        Console.Write("Vermiş olduğunuz sayının fibonaççideki sırası:");
        Console.WriteLine(fib(5));
        //--------------------------------------------------------------------------------------------------------
        /*
         5. Params ile Sınırsız Sayıda Parametre Alarak Ortalama Hesaplama

        Soru: params anahtar kelimesini kullanarak sınırsız sayıda double parametre alabilen ve bu sayılar arasındaki ortalamayı hesaplayan bir metot yazın.
        İpucu: params ile dizi gibi parametre alabilir ve döngü kullanarak ortalamayı hesaplayabilirsiniz.

         */

        //--------------------------------------------------------------------------------------------------------
        double ort(params double[] mydouble)
        {
            double toplam = 0.0;
            foreach (double d in mydouble)
            {
                toplam += d;
            }
            return toplam / mydouble.Length;
        }
        Console.Write("Verilen değerlerin ortalaması:");
        Console.WriteLine(ort(1.2,2.3,3.4,4.5,5.6));
        //--------------------------------------------------------------------------------------------------------
        /*
         6. Dizi Elemanlarını Toplayan ve Filtreleme Şartı Ekleyen Metot

        Soru: int türünde bir dizi ve bir filtre değeri (int) alan bir metot yazın. Bu metot, filtre değerinden büyük olan tüm elemanları toplasın ve toplamı döndürsün.
        İpucu: int türünde bir metot tanımlayıp foreach döngüsü ile filtreyi uygulayarak toplamı hesaplayabilirsiniz.

         */
        //--------------------------------------------------------------------------------------------------------
        int filtersum(int[] sumlist, int filterx)
        {
            int toplam = 0;
            foreach (int i in sumlist)
            {
                if (filterx < i) toplam += i;
            }
            return toplam;
        }
        Console.Write("Verilen sayıdan büyük olan sayıların toplamı:");
        Console.WriteLine(filtersum(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 6));

        //--------------------------------------------------------------------------------------------------------
        /*
         7. Seçmeli (Optional) Parametre ile Belirli Yaştan Sonraki Yılları Sayma

        Soru: Bir yaş değeri alan bir metot yazın. Eğer yaş belirtilmezse varsayılan olarak 18 olsun ve metot, verilen yaşın 18’den ne kadar fazla olduğunu döndürsün.
        İpucu: int türünde bir metot tanımlayıp varsayılan parametre kullanarak yaşı hesaplayabilirsiniz.

         */

        //--------------------------------------------------------------------------------------------------------
        int HMO(int yas = 18)
        {
            if (yas <= 18) return 0; else return yas - 18;
        }
        Console.Write("How Mnany Old Test Score:");
        Console.WriteLine(HMO(23));

        //--------------------------------------------------------------------------------------------------------
        /*
        8. Geriye Koleksiyon Döndüren ve Veriyi Filtreleyen Metot

        Soru: string türünde bir dizi alan bir metot yazın. Bu metot, sadece uzunluğu 5 karakterden büyük olan elemanları içeren bir List<string> döndürsün.
        İpucu: List<string> türünde bir metot tanımlayarak döngü ile filtreleme yapabilirsiniz.

        */

        //--------------------------------------------------------------------------------------------------------
        List<string> filterchar(string[] first)
        {
            List<string> mylist1 = new List<string>();
            foreach (string s in first)
            {
                if (s.Length > 5) mylist1.Add(s);
            }
            return mylist1;
        }
        List<string> ansver8 = filterchar(new string[] { "merhaba", "ben", "ferhat", "her", "gün", "kaliteli", "çalışmalısın" });
        foreach (string s in ansver8) Console.WriteLine(s);

        //--------------------------------------------------------------------------------------------------------
    }
}
