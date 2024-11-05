/*

Soru 1 - Foreach Döngüsü ile Liste Elemanlarını Yazdırma
Bir ArrayList oluşturun ve içerisine birkaç tamsayı ekleyin. foreach döngüsü kullanarak listedeki her bir sayıyı ekrana yazdıran bir program yazınız.
Döngü tamamlandığında sayıların toplamını da ekrana yazdırınız.

Soru 2 - Hazır Metod Kullanımı 
Kullanıcıdan bir cümle girmesini isteyin ve bu cümledeki kelime sayısını hesaplayıp ekrana yazdıran bir program yazınız. Kelime sayısını hesaplamak için Split() metodunu kullanabilirsiniz.

Soru 3 - ArrayList ile Alfabetik Sıralama 
Kullanıcıdan 5 adet isim alarak bir ArrayList içinde saklayın. Daha sonra bu isimleri alfabetik olarak sıralayarak ekrana yazdırın. (Not: ArrayList.Sort() metodunu kullanabilirsiniz.)

Soru 4 - For Döngüsü ile Fibonacci Serisi 
Kullanıcıdan bir sayı girmesini isteyin. Bu sayı kadar Fibonacci serisindeki sayıları hesaplayıp ekrana yazdıran bir program yazınız. for döngüsü kullanarak seriyi oluşturun.
Örnek: Eğer kullanıcı 5 girerse, ekranda "0 1 1 2 3" yazmalıdır.

Soru 5 - While Döngüsü ile Tahmin Oyunu 
Kullanıcıdan bir sayı tahmin etmesini isteyen bir program yazınız. Program, doğru tahmin yapılana kadar kullanıcıdan yeni tahminler almaya devam etmelidir. Doğru tahmin yapıldığında ekrana bir tebrik mesajı yazdırılmalıdır.
Not: Sabit bir sayı belirleyebilir veya Random sınıfı ile rastgele bir sayı oluşturabilirsiniz.

Soru 6 - Do While Döngüsü ile Basit Hesap Makinesi
Kullanıcıdan işlem türünü (+, -, *, /) ve iki sayı alarak işlemi gerçekleştiren bir program yazınız. Kullanıcı "çıkış" yazana kadar işlemleri tekrarlayan bir do while döngüsü kurunuz.
Ekstra: Bölme işlemi sırasında sıfıra bölme hatasını kontrol ediniz.

Soru 7 - For ve While Döngüsü ile Sayı Toplama Oyunu 
Kullanıcıdan pozitif tam sayılar girmesini isteyen bir program yazınız. Kullanıcı "0" girdiğinde girişi sonlandırın ve girilen pozitif sayıların toplamını bir while döngüsü kullanarak hesaplayın.
Toplamı bulduktan sonra tüm pozitif sayıları tek tek ekrana yazdırmak için bir for döngüsü kullanınız.

 
 

 */






//Soru 1 - Foreach Döngüsü ile Liste Elemanlarını Yazdırma
//------------------------------------------------------------------------

int top = 0;
int[] sayilar = new int[5];
sayilar[0] = 10;
sayilar[1] = 20;
sayilar[2] = 30;
sayilar[3] = 40;
Console.WriteLine("dizinin son integer elemanını giriniz:");
sayilar[4] = int.Parse(Console.ReadLine());
foreach (int i in sayilar)
{
    Console.WriteLine(i);
    top+=i;
}
Console.WriteLine($"sayıların toplamı: {top}");



//Soru 2 - Hazır Metod Kullanımı 
//------------------------------------------------------------------------

Console.WriteLine("bir cümle yazın");
string cumle = Console.ReadLine();
string[] pars = cumle.Split(" ");
Console.WriteLine($"Bu cümle {pars.Length} adet kelime içermektedir.");



//Soru 3 - ArrayList ile Alfabetik Sıralama 
//------------------------------------------------------------------------

string[] sirala = new string[5];
for (int i = 0; i < sirala.Length; i++)
{
    Console.WriteLine("lütfen bir string deger girin.");
    sirala[i] = Console.ReadLine();
}
Array.Sort( sirala );
Console.WriteLine("sıralı hali\n\n\n");
foreach (string s in sirala)
{
    Console.WriteLine(s);
}




//Soru 4 - For Döngüsü ile Fibonacci Serisi 
//------------------------------------------------------------------------

Console.WriteLine("fibonacci serisinden ilk kaç değeri görmek istersiniz?");
int fib = int.Parse(Console.ReadLine());
int f1 = 0;
int f2 = 1;
for (int i = 0;i < fib; i++)
{
    Console.WriteLine(f1);
    f2 += f1;
    f1 = f2 - f1;
}




//Soru 5 - While Döngüsü ile Tahmin Oyunu 
//------------------------------------------------------------------------

Random random = new Random();
int tut = random.Next(1,101);
int[] tahmin= {0,0 };
while (tut != tahmin[0])
{
    tahmin[1]++;
    Console.WriteLine("sayiyi tahmin et:");
    tahmin[0] = int.Parse(Console.ReadLine());
    if (tahmin[0] > tut)
    {
        Console.WriteLine("daha küçük bir sayi dene.");
    }
    else if (tahmin[0] < tut)
    {
        Console.WriteLine("daha büyük bir sayi dene.");
    }

}
Console.WriteLine($"tebrikler {tahmin[1] } denemede bildin sayi: {tut}");





//Soru 6 - Do While Döngüsü ile Basit Hesap Makinesi
//------------------------------------------------------------------------

string giris = "1";
int sa1;
int sa2;
string islem;
do {
    
    if (giris == "1" || giris == "basla")
    {
        Console.WriteLine("ilk sayıyı gir:");
        sa1 = int.Parse(Console.ReadLine());

        Console.WriteLine("ikinci sayıyı gir:");
        sa2 = int.Parse(Console.ReadLine());

        Console.WriteLine("hangi işlemi yapacaksınız:+ - % / *");
        islem = Console.ReadLine();


        if (sa2 == 0 && (islem == "/"|| islem == "%"))
        {
            Console.WriteLine("bir sayıyı sıfıra bölemezsin lütfen sayıları tekrar gir:");
            continue;
        }
        switch (islem)
        {
            case "+": Console.WriteLine($"toplamın sonucu {sa1} + {sa2} = {sa1 + sa2}");break;
            case "-": Console.WriteLine($"çıkartma sonucu {sa1} - {sa2} = {sa1 - sa2}"); break;
            case "*": Console.WriteLine($"çarpım sonucu {sa1} * {sa2} = {sa1 * sa2}"); break;
            case "/": Console.WriteLine($"bölme sonucu {sa1} / {sa2} = {sa1 / (float)sa2}"); break;
            case "%": Console.WriteLine($"mod sonucu {sa1} % {sa2} = {sa1 % sa2}"); break;
            default:
                {
                    Console.WriteLine("lütfen tekrar deneyin.\nişlem olarak girebileceğiniz operatörler:- + * / %");
                    break;
                }
                
        }

    }
    else Console.WriteLine("lütfen doğru bir komut girin.");



    Console.WriteLine("1-basla\n2-çıkış");
    giris = Console.ReadLine();
} while (giris != "2" && giris != "çıkış") ;




//Soru 7 - For ve While Döngüsü ile Sayı Toplama Oyunu 
//------------------------------------------------------------------------

int sayigir = 1;
string strsayi = "0";
int toplam = 0;
while (sayigir != 0)
{
    Console.WriteLine("pozitif sayı girin.\nçıkmak için 0 girin.");
    sayigir = int.Parse(Console.ReadLine());
    if (sayigir < 0)
    {
        Console.WriteLine("Bir hata oluştu.(negatif sayı girmeyin)");
        continue;
    }
    else if (sayigir > 0)
    {
        strsayi += $"-{sayigir}";
        toplam += sayigir;
    }

}Console.WriteLine($"sayıların toplamı: {toplam}");
string[] sayilarim = strsayi.Split('-');

for (int i = 1;i< sayilarim.Length;i++)
{
    Console.WriteLine(sayilarim[i]);
}


