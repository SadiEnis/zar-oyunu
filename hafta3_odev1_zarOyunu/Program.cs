using System;
using System.Collections;
// İçinde ArrayList, HashTable, SortedList ,Stack, Queue... gibi koleksiyonları bulunduran System.Collections'ı dizi yerine count kontrolünü daha rahat yapabilmek için ben ekledim.
using System.Threading;

namespace hafta3_odev1_zarOyunu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Oyun oyun = new Oyun();

            menu.Olustur();
            oyun.OyuncuKayıt();
            oyun.Zar();
        }
        class Oyun
        {
            public string Ad;
            public string Soyad;
            public DateTime Date;
            public int Puan = 0;
            public void OyuncuKayıt()
            {
                Console.Write("Oyuncu Adı: ");
                Ad = Console.ReadLine();
                Console.Write("Oyuncu Soyadı: ");
                Soyad = Console.ReadLine();
                Console.Write("Doğum Tarihiniz(örnek:{0}.{1}.{2}): ", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                Date = Convert.ToDateTime(Console.ReadLine());
            }
            public void Zar()
            {
                int oyunHakkı; // 1,6 gelirse hakkı azaltırız; 2,3,4,5 oyun hakkı değişmez
                yeniOyunBaslat:
                string yeniOyun = null;
                this.Puan = 0; // Oyun yeniden başlatıldığında puan skorumuz sıfırlanır.
                oyunHakkı = 10; // Oyun yeniden başlatıldığında oyun hakkımız da yenilenir.

                ArrayList zarKaydi = new ArrayList();
                // Normal dizilerde 2,3,4,5 sayıları geldiğinde dizinin uzunluğunu artıramadığım için
                // ArrayList koleksiyonunun kendi Capacity ve Count'unu artırma özelliğini kullanmak istedim.
                // Böylece daha rahat ve esnek bir ZarKaydı oluşturabildim.
                zarKaydi.Clear();

                int i = 0;
                Random zarRandom = new Random();
                Console.Clear(); // Oyuncu tekrar oynamak isteterse Listemizi boşaltalım.
                Console.WriteLine("Zar atmak için ENTER..."); 
                do
                {
                    Console.ReadLine(); // Entere basınca zar atalım.
                    Console.WriteLine("Zar atılıyor...");
                    Thread.Sleep(1000);
                    // 1000 milisaniye yani 1 saniye bekleterek zar atılıyor hissi kazandırmak istedim. 
                    zarKaydi.Add(zarRandom.Next(1,7)); // Add metodu ArrayList'e yeni değer eklerken kullanırız.

                    Console.WriteLine("Gelen Değer: " + zarKaydi[i]);
                    if (zarKaydi[i].ToString() == "1") // ArrayList object değer kaydediyor.
                                                       // zarKaydi[i] == 1 yazdığımda 'object integer'e eşit olduğu için hata olduğunu' söyledi.
                                                       // Ben de eşitliğin iki tarafını string'e çevirdim.
                        Kaybet();

                    else if (zarKaydi[i].ToString() == "6")
                        Kazan();

                    else
                        i++;

                } while (oyunHakkı != 0); // oyunHakkımız bitene kadar do while döngüsü dönecek.

                Console.Clear();
                Console.WriteLine("{0} {1}", Ad, Soyad);
                Console.WriteLine("\n10 zar atma hakkınız tamamlandı. Sonuca Bakalım!");
                Console.Write("Zarda gelen değerler: ");
                foreach (var item in zarKaydi)
                    Console.Write(" " + item);
                Console.WriteLine(" Puan: " + this.Puan);

                int ayKontrol = 0, isimKontrol = 0;
                if (Date.Month >= 1 && Date.Month <= 6)
                {
                    ayKontrol = 1;
                    this.Puan += 30;
                }
                else if (Date.Month > 6 && Date.Month <= 12)
                {
                    ayKontrol = 2;
                    this.Puan -= 20;
                }


                if (Ad.Length > Soyad.Length)
                {
                    isimKontrol = 1;
                    this.Puan += 50;
                }
                else if (Ad.Length == Soyad.Length)
                {
                    isimKontrol = 2;
                    this.Puan += 25;
                }
                else if (Ad.Length < Soyad.Length)
                {
                    isimKontrol = 3;
                    this.Puan -= 10;
                }

                if (ayKontrol == 1 && isimKontrol == 1)
                {
                    Console.WriteLine("\nİlk altı içinde doğduğunuz için ilave 30 puan, adınız soyadınızdan uzun olduğu için ilave 50 puan kazandınız.");
                    Console.WriteLine("Güncel puan durumu: " + this.Puan);

                    if (Galibiyet())
                        Console.WriteLine("Oyunu kazandınız. Tebrik ederiz.");
                    else
                        Console.WriteLine("Oyunu kaybettiniz.");

                    Console.Write("Tekrar oynamak isterseniz [*] basınız. Çıkmak için ENTER ");
                    yeniOyun = Console.ReadLine();
                    if (yeniOyun == "*")
                        goto yeniOyunBaslat;
                }
                else if (ayKontrol == 1 && isimKontrol == 2)
                {
                    Console.WriteLine("\nİlk altı içinde doğduğunuz için ilave 30 puan, adınız soyadınızla aynı uzunlukta olduğu için ilave 25 puan kazandınız.");
                    Console.WriteLine("Güncel puan durumu: " + this.Puan);

                    if (Galibiyet())
                        Console.WriteLine("Oyunu kazandınız. Tebrik ederiz.");
                    else
                        Console.WriteLine("Oyunu kaybettiniz.");

                    Console.Write("Tekrar oynamak isterseniz [*] basınız. Çıkmak için ENTER ");
                    yeniOyun = Console.ReadLine();
                    if (yeniOyun == "*")
                        goto yeniOyunBaslat;
                }
                else if (ayKontrol == 1 && isimKontrol == 3)
                {
                    Console.WriteLine("\nİlk altı içinde doğduğunuz için ilave 30 puan kazandınız, adınız soyadınızdan kısa olduğu için 10 puan kaybettiniz.");
                    Console.WriteLine("Güncel puan durumu: " + this.Puan);

                    if (Galibiyet())
                        Console.WriteLine("Oyunu kazandınız. Tebrik ederiz.");
                    else
                        Console.WriteLine("Oyunu kaybettiniz.");

                    Console.Write("Tekrar oynamak isterseniz [*] basınız. Çıkmak için ENTER ");
                    yeniOyun = Console.ReadLine();
                    if (yeniOyun == "*")
                        goto yeniOyunBaslat;
                }

                else if (ayKontrol == 2 && isimKontrol == 1)
                {
                    Console.WriteLine("\nSon altı içinde doğduğunuz için 20 puan kaybettiniz, adınız soyadınızdan uzun olduğu için ilave 50 puan kazandınız.");
                    Console.WriteLine("Güncel puan durumu: " + this.Puan);

                    if (Galibiyet())
                        Console.WriteLine("Oyunu kazandınız. Tebrik ederiz.");
                    else
                        Console.WriteLine("Oyunu kaybettiniz.");

                    Console.Write("Tekrar oynamak isterseniz [*] basınız. Çıkmak için ENTER ");
                    yeniOyun = Console.ReadLine();
                    if (yeniOyun == "*")
                        goto yeniOyunBaslat;
                }
                else if (ayKontrol == 2 && isimKontrol == 2)
                {
                    Console.WriteLine("\nSon altı içinde doğduğunuz için 20 puan kaybettiniz, adınız soyadınızla aynı uzunlukta olduğu için olduğu için ilave 25 puan kazandınız.");
                    Console.WriteLine("Güncel puan durumu: " + this.Puan);

                    if (Galibiyet())
                        Console.WriteLine("Oyunu kazandınız. Tebrik ederiz.");
                    else
                        Console.WriteLine("Oyunu kaybettiniz.");

                    Console.Write("Tekrar oynamak isterseniz [*] basınız. Çıkmak için ENTER ");
                    yeniOyun = Console.ReadLine();
                    if (yeniOyun == "*")
                        goto yeniOyunBaslat;
                }
                else if (ayKontrol == 2 && isimKontrol == 3)
                {
                    Console.WriteLine("\nSon altı içinde doğduğunuz için 20 puan, adınız soyadınızdan kısa olduğu için 10 puan kaybettiniz.");
                    Console.WriteLine("Güncel puan durumu: " + this.Puan);

                    if (Galibiyet())
                        Console.WriteLine("Oyunu kazandınız. Tebrik ederiz.");
                    else
                        Console.WriteLine("Oyunu kaybettiniz.");

                    Console.Write("Tekrar oynamak isterseniz [*] basınız. Çıkmak için ENTER ");
                    yeniOyun = Console.ReadLine();
                    if (yeniOyun == "*")
                        goto yeniOyunBaslat;
                }

                Console.ReadKey();

                void Kaybet()
                {
                    this.Puan -= 75;
                    Console.WriteLine("-75 puan / Toplam puan: " + this.Puan);
                    oyunHakkı--;
                    i++;
                }
                void Kazan()
                {
                    this.Puan += 100;
                    Console.WriteLine("+100 puan / Toplam puan: " + this.Puan);
                    oyunHakkı--;
                    i++;;
                }
                bool Galibiyet()
                {
                    return this.Puan > 500;
                }
            }
        }
        class Menu 
        {
            public void Olustur()
            {
                Console.WriteLine("Sadi Enis Güçlüer - \n\n ZarOyunu");

                Console.Write("\n\n\nMENU");

                Cizgi();

                Console.WriteLine("OYUNA HOŞ GELDİNİZ\n");
                Console.WriteLine(" Oyunumuzun kuralı basit, oyunumuz sizin için bir zar atacak. " +
                    "Zar 1 gelirse kaybedeceksiniz ama 6 gelirse kazanırsınız. 2,3,4,5 değerleri gelirse yeniden zar atma hakkı kazanırsınız. " +
                    "10 tane zar hakkınız vardır. 1 ve 6 dışında gelen değerler hakkınızdan eksiltmez.\n " +
                    "\n Puanlama aşamasında kaybetmeniz(1) durumunda -75 puan, kazanmanız(6) durumunda +100 puan alırsınız. " +
                    "*Bunlar dışında ilk altı ay içerisinde doğduysanız 30 puan kazanırsınız, son altı ay içinde doğduysanız 20 puan kaybedersiniz. " +
                    "Adınızın karakter sayısı soyadının karakter sayısından büyük ise 50 puan kazanır, eşit ise 25 puan kazanırsınız; küçük ise 10 puan kaybedersiniz. " +
                    "Oyun sonunda en az 500 puanı olan oyuncu oyunun galibi olur. " +
                    "Not: Lütfen bilgilerinizi doğru giriniz!");

                Cizgi();

                Console.WriteLine("Oyuna başlamak için ENTER\nİyi Şanslar... ");

                Cizgi();
                Console.ReadLine();
                Console.Clear();
            }
            private void Cizgi() // Sadece bu sınıf içinde kullanacağım için sınıfa özel tanımlatan Private olarak kullandım.
                { 
                    Console.WriteLine("\n------------------------------\n"); 
                }
        }
    }
}