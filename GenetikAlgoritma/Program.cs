using GenetikAlgoritma.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetikAlgoritma
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[1] Tek İterasyon (İterasyon adımları ile birlikte)    ");
            Console.WriteLine("[2] Çoklu İterasyon (Sadece sonuç)                   ");
            int secim = Convert.ToInt32(Console.ReadLine());

            if(secim == 1)
            {
                DetayliTekIterasyon();
            }
            else if(secim == 2)
            {
                Console.WriteLine("İterasyon sayısı ?");
                int iterasyonSayisi = Convert.ToInt32(Console.ReadLine());
                CokluIterasyon(iterasyonSayisi);
            }
        }


        public static void CokluIterasyon(int iterasyonSayisi)
        {
            #region Tanımlamalar

            int urunSayisi = 18;
            int rafSayisi = 8;
            int katSayisi = 3;
            int skor = 0;
            double uzaklik = 0;

            List<Urun> urunler = new List<Urun>()
            {
                new Urun { UrunNo = 1, Agirlik = 865, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 2, Agirlik = 618, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 3, Agirlik = 200, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 4, Agirlik = 701, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 5, Agirlik = 372, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 6, Agirlik = 597, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 7, Agirlik = 225, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 8, Agirlik = 633, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 9, Agirlik = 681, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 10, Agirlik = 751, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 11, Agirlik = 503, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 12, Agirlik = 599, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 13, Agirlik = 360, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 14, Agirlik = 562, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 15, Agirlik = 674, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 16, Agirlik = 646, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 17, Agirlik = 647, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 18, Agirlik = 504, AgirlikSinifi = "Orta" },
            };

            List<Hucre> hucreler = new List<Hucre>(); //1'den 24'e sıralı hücre
            int hucreNo = 1;
            for (int i = 1; i <= rafSayisi; i++)
            {
                for (int j = 1; j <= katSayisi; j++)
                {
                    Hucre hucre = new Hucre();
                    hucre.HucreNo = hucreNo;
                    hucre.Raf = i;
                    hucre.Kat = j;
                    hucreNo++;
                    hucreler.Add(hucre);
                }
            }

            #region Hücrenin kapıya olan uzaklığı
            hucreler[0].KapiyaUzaklik = 4.39;
            hucreler[1].KapiyaUzaklik = 6.17;
            hucreler[2].KapiyaUzaklik = 8.32;
            hucreler[3].KapiyaUzaklik = 3.58;
            hucreler[4].KapiyaUzaklik = 5.62;
            hucreler[5].KapiyaUzaklik = 7.93;
            hucreler[6].KapiyaUzaklik = 3.58;
            hucreler[7].KapiyaUzaklik = 5.62;
            hucreler[8].KapiyaUzaklik = 7.93;
            hucreler[9].KapiyaUzaklik = 4.39;
            hucreler[10].KapiyaUzaklik = 6.17;
            hucreler[11].KapiyaUzaklik = 8.32;
            hucreler[12].KapiyaUzaklik = 8.09;
            hucreler[13].KapiyaUzaklik = 9.17;
            hucreler[14].KapiyaUzaklik = 10.74;
            hucreler[15].KapiyaUzaklik = 7.67;
            hucreler[16].KapiyaUzaklik = 8.81;
            hucreler[17].KapiyaUzaklik = 10.44;
            hucreler[18].KapiyaUzaklik = 7.67;
            hucreler[19].KapiyaUzaklik = 8.81;
            hucreler[20].KapiyaUzaklik = 10.44;
            hucreler[21].KapiyaUzaklik = 8.09;
            hucreler[22].KapiyaUzaklik = 9.17;
            hucreler[23].KapiyaUzaklik = 10.74;

            #endregion

            #endregion

            Console.WriteLine("*-*-*-*-*-*-*- BAŞLANGIÇ POPÜLASYONU -*-*-*-*-*-*-*-*-");

            #region 5 Popülasyon İçin Kromozom Oluşturma ve Fitness Değerleri

            List<Kromozom> kromozomlar = new List<Kromozom>();
            double baslangicEnUygunFitness = double.MaxValue;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("====================================");
                Kromozom kromozom = KromozomOlustur(urunler, hucreler);
                kromozom.KromozomNo = i + 1;
                Console.WriteLine("KROMOZOM NO " + kromozom.KromozomNo);
                Console.WriteLine("Ürün Numarası Kromozomu");
                for (int j = 0; j < 18; j++)
                {
                    Console.Write(kromozom.Urunler[j].UrunNo + " \t");
                }
                Console.WriteLine();
                Console.WriteLine("Hücre Numarası Kromozomu");
                for (int j = 0; j < 18; j++)
                {
                    Console.Write(kromozom.Hucreler[j].HucreNo + " \t");
                }
                Console.WriteLine();
                Console.WriteLine("Fitness Skoru = " + kromozom.Fitness);
                kromozomlar.Add(kromozom);
                Console.WriteLine("====================================");
                if (kromozom.Fitness < baslangicEnUygunFitness)
                {
                    baslangicEnUygunFitness = kromozom.Fitness;
                }
            }

            Console.WriteLine("Başlangıçta en uygun fitness değeri = " + baslangicEnUygunFitness);
            Console.WriteLine("--------------------------------------------");

            #endregion


            Random rnd = new Random();

            int jenerasyonSayisi = iterasyonSayisi;
            List<Kromozom> mevcutPopulasyon = kromozomlar;
            Kromozom enIyiKromozom = null;
            double enIyiFitness = double.MaxValue;


            for (int jenerasyon = 0; jenerasyon < jenerasyonSayisi; jenerasyon++)
            {
                //Console.WriteLine($"\n=== JENERASYON {jenerasyon + 1} ===\n");
                List<Kromozom> yeniNesil = new List<Kromozom>();

                #region Seçim Operatörü (Turnuva Seçimi) 
                for (int i = 0; i < 5; i++)
                {
                    List<Kromozom> karisikKromozom = kromozomlar.OrderBy(x => Guid.NewGuid()).ToList();
                    Kromozom k1 = karisikKromozom[0];
                    Kromozom k2 = karisikKromozom[1];
                    Kromozom kazanan;
                    if (k1.Fitness < k2.Fitness)
                    {
                        kazanan = new Kromozom
                        {
                            KromozomNo = k1.KromozomNo,
                            Urunler = new List<Urun>(k1.Urunler),
                            Hucreler = new List<Hucre>(k1.Hucreler),
                            Fitness = k1.Fitness,
                            RassalDeger = rnd.NextDouble(),
                        };
                        yeniNesil.Add(kazanan);
                    }
                    else
                    {
                        kazanan = new Kromozom
                        {
                            KromozomNo = k2.KromozomNo,
                            Urunler = new List<Urun>(k2.Urunler),
                            Hucreler = new List<Hucre>(k2.Hucreler),
                            Fitness = k2.Fitness,
                            RassalDeger = rnd.NextDouble(),
                        };
                        yeniNesil.Add(kazanan);
                    }
                }
                yeniNesil = yeniNesil.OrderBy(x => x.RassalDeger).ToList();
                #endregion

                #region Kısmi Planlı Çaprazlama
                List<Kromozom> yeniNesilKopya = new List<Kromozom>();

                for (int z = 0; z < yeniNesil.Count - 1; z += 2)
                {
                    List<Kromozom> caprazlanacakKromozomlar = new List<Kromozom>();

                    #region Çaprazlanacak Kromozomları Ayarla

                    Kromozom ilkKromozom = yeniNesil[z];
                    Kromozom ikinciKromozom = yeniNesil[z + 1];

                    if (rnd.NextDouble() <= 0.95) //%95 olasılık
                    {
                        caprazlanacakKromozomlar.Add(new Kromozom
                        {
                            KromozomNo = ilkKromozom.KromozomNo,
                            Urunler = new List<Urun>(ilkKromozom.Urunler),
                            Hucreler = new List<Hucre>(ilkKromozom.Hucreler),
                            Fitness = ilkKromozom.Fitness,
                            RassalDeger = ilkKromozom.RassalDeger
                        });
                        caprazlanacakKromozomlar.Add(new Kromozom
                        {
                            KromozomNo = ikinciKromozom.KromozomNo,
                            Urunler = new List<Urun>(ikinciKromozom.Urunler),
                            Hucreler = new List<Hucre>(ikinciKromozom.Hucreler),
                            Fitness = ikinciKromozom.Fitness,
                            RassalDeger = ikinciKromozom.RassalDeger
                        });
                    }
                    else
                    {
                        caprazlanacakKromozomlar.Add(new Kromozom
                        {
                            KromozomNo = ilkKromozom.KromozomNo,
                            Urunler = new List<Urun>(ilkKromozom.Urunler),
                            Hucreler = new List<Hucre>(ilkKromozom.Hucreler),
                            Fitness = ilkKromozom.Fitness,
                            RassalDeger = ilkKromozom.RassalDeger
                        });
                        caprazlanacakKromozomlar.Add(new Kromozom
                        {
                            KromozomNo = ikinciKromozom.KromozomNo,
                            Urunler = new List<Urun>(ikinciKromozom.Urunler),
                            Hucreler = new List<Hucre>(ikinciKromozom.Hucreler),
                            Fitness = ikinciKromozom.Fitness,
                            RassalDeger = ikinciKromozom.RassalDeger
                        });
                    }

                    #endregion

                    #region Çaprazlama
                    if (caprazlanacakKromozomlar[0] != ilkKromozom)
                    {
                        Kromozom k1 = caprazlanacakKromozomlar[0];
                        Kromozom k2 = caprazlanacakKromozomlar[1];
                        Dictionary<int, int> urunEslestirmeK1toK2 = new Dictionary<int, int>();
                        Dictionary<int, int> urunEslestirmeK2toK1 = new Dictionary<int, int>();
                        Dictionary<int, int> hucreEslestirmeK1toK2 = new Dictionary<int, int>();
                        Dictionary<int, int> hucreEslestirmeK2toK1 = new Dictionary<int, int>();

                        for (int j = 5; j < 11; j++)
                        {
                            if (k1.Urunler[j].UrunNo != k2.Urunler[j].UrunNo)
                            {
                                urunEslestirmeK1toK2.Add(k1.Urunler[j].UrunNo, k2.Urunler[j].UrunNo);
                                urunEslestirmeK2toK1.Add(k2.Urunler[j].UrunNo, k1.Urunler[j].UrunNo);
                            }
                            if (k1.Hucreler[j].HucreNo != k2.Hucreler[j].HucreNo)
                            {
                                hucreEslestirmeK1toK2.Add(k1.Hucreler[j].HucreNo, k2.Hucreler[j].HucreNo);
                                hucreEslestirmeK2toK1.Add(k2.Hucreler[j].HucreNo, k1.Hucreler[j].HucreNo);
                            }

                            Urun tempUrun = k1.Urunler[j];
                            k1.Urunler[j] = k2.Urunler[j];
                            k2.Urunler[j] = tempUrun;
                            
                            Hucre tempHucre = k1.Hucreler[j];
                            k1.Hucreler[j] = k2.Hucreler[j];
                            k2.Hucreler[j] = tempHucre;

                            k1.Hucreler[j].Urun = k1.Urunler[j];
                            k2.Hucreler[j].Urun = k2.Urunler[j];
                        }
                        
                        for (int j = 0; j < k1.Urunler.Count; j++)
                        {
                            if (j >= 5 && j < 11)
                            {
                                continue;
                            }
                            else
                            {
                                int urunNo = k1.Urunler[j].UrunNo;
                                while (urunEslestirmeK2toK1.ContainsKey(urunNo))
                                {
                                    urunNo = urunEslestirmeK2toK1[urunNo];
                                    k1.Urunler[j] = urunler.First(x => x.UrunNo == urunNo);
                                    k1.Hucreler[j].Urun = k1.Urunler[j];
                                }

                                int hNo = k1.Hucreler[j].HucreNo;
                                while (hucreEslestirmeK2toK1.ContainsKey(hNo))
                                {
                                    hNo = hucreEslestirmeK2toK1[hNo];
                                    k1.Hucreler[j] = hucreler.First(x => x.HucreNo == hNo);
                                    k1.Hucreler[j].Urun = k1.Urunler[j];
                                }

                                urunNo = k2.Urunler[j].UrunNo;
                                while (urunEslestirmeK1toK2.ContainsKey(urunNo))
                                {
                                    urunNo = urunEslestirmeK1toK2[urunNo];
                                    k2.Urunler[j] = urunler.First(x => x.UrunNo == urunNo);
                                    k2.Hucreler[j].Urun = k2.Urunler[j];
                                }

                                hNo = k2.Hucreler[j].HucreNo;
                                while (hucreEslestirmeK1toK2.ContainsKey(hNo))
                                {
                                    hNo = hucreEslestirmeK1toK2[hNo];
                                    k2.Hucreler[j] = hucreler.First(x => x.HucreNo == hNo);
                                    k2.Hucreler[j].Urun = k2.Urunler[j];
                                }
                            }
                        }

                        k1.Fitness = FitnessHesapla(k1);
                        k2.Fitness = FitnessHesapla(k2);
                    }

                    yeniNesilKopya.Add(caprazlanacakKromozomlar[0]);
                    yeniNesilKopya.Add(caprazlanacakKromozomlar[1]);
                    #endregion
                }

                if (yeniNesil.Count % 2 == 1)
                {
                    int sonuncuIndex = yeniNesil.Count - 1;
                    yeniNesilKopya.Add(
                        new Kromozom
                        {
                            KromozomNo = yeniNesil[sonuncuIndex].KromozomNo,
                            Urunler = new List<Urun>(yeniNesil[sonuncuIndex].Urunler),
                            Hucreler = new List<Hucre>(yeniNesil[sonuncuIndex].Hucreler),
                            Fitness = yeniNesil[sonuncuIndex].Fitness,
                            RassalDeger = yeniNesil[sonuncuIndex].RassalDeger
                        });
                }

                for (int i = 0; i < yeniNesilKopya.Count; i++)
                {
                    yeniNesilKopya[i].KromozomNo = i + 1;
                }

                yeniNesil = yeniNesilKopya;

                #endregion

                #region Mutasyon İşlemi - Karıştırma (Shuffle) Yaklaşımı

                for (int i = 0; i < yeniNesil.Count; i++)
                {
                    int karistirmaAraligi = 4;
                    int karistirmaBaslangici = rnd.Next(0, urunler.Count - karistirmaAraligi);
                    int karistirmaSonu = karistirmaBaslangici + karistirmaAraligi;

                    List<Hucre> hucreKaristirmaListesi = new List<Hucre>();
                    List<Urun> urunKaristirmaListesi = new List<Urun>();
                    for (int j = karistirmaBaslangici; j < karistirmaSonu; j++)
                    {
                        hucreKaristirmaListesi.Add(yeniNesil[i].Hucreler[j]);
                        urunKaristirmaListesi.Add(yeniNesil[i].Urunler[j]);
                    }

                    List<Hucre> karistirilmisHucreBlogu = hucreKaristirmaListesi.OrderBy(x => Guid.NewGuid()).ToList();
                    List<Urun> karistirilmisUrunBlogu = urunKaristirmaListesi.OrderBy(x => Guid.NewGuid()).ToList();

                    int g = 0;
                    for (int j = karistirmaBaslangici; j < karistirmaSonu; j++)
                    {
                        yeniNesil[i].Hucreler[j] = karistirilmisHucreBlogu[g];
                        yeniNesil[i].Urunler[j] = karistirilmisUrunBlogu[g];
                        yeniNesil[i].Hucreler[j].Urun = karistirilmisUrunBlogu[g];
                        g++;
                    }

                    double eskiFitness = yeniNesil[i].Fitness;
                    yeniNesil[i].Fitness = FitnessHesapla(yeniNesil[i]);
                }

                #endregion

                #region En İyi Kromozomu Kaydet

                foreach (var kromozom in yeniNesil)
                {
                    if (kromozom.Fitness < enIyiFitness)
                    {
                        enIyiFitness = kromozom.Fitness;
                        enIyiKromozom = new Kromozom
                        {
                            KromozomNo = kromozom.KromozomNo,
                            Urunler = new List<Urun>(kromozom.Urunler),
                            Hucreler = new List<Hucre>(kromozom.Hucreler),
                            Fitness = kromozom.Fitness
                        };
                    }
                }

                #endregion

                //Yeni nesli mevcut popülasyona aktar
                mevcutPopulasyon = yeniNesil;
                Console.WriteLine($"\nJenerasyon {jenerasyon + 1} En İyi Fitness: {enIyiFitness}");
            }
            Console.WriteLine("\n\n=== EN İYİ ÇÖZÜM ===\n");
            Console.WriteLine($"Kromozom NO: {enIyiKromozom.KromozomNo}");
            Console.Write("Ürün\t| ");
            foreach (var no in enIyiKromozom.Urunler)
            {
                Console.Write(no.UrunNo + " \t");
            }
            Console.WriteLine();
            Console.Write("Hücre\t| ");
            foreach (var h in enIyiKromozom.Hucreler)
            {
                Console.Write(h.HucreNo + " \t");
            }
            Console.WriteLine();
            Console.WriteLine($"En İyi Fitness Değeri: {enIyiFitness}");
        }

        public static void DetayliTekIterasyon()
        {
            #region Tanımlamalar

            int urunSayisi = 18;
            int rafSayisi = 8;
            int katSayisi = 3;
            int skor = 0;
            double uzaklik = 0;

            List<Urun> urunler = new List<Urun>()
            {
                new Urun { UrunNo = 1, Agirlik = 865, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 2, Agirlik = 618, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 3, Agirlik = 200, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 4, Agirlik = 701, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 5, Agirlik = 372, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 6, Agirlik = 597, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 7, Agirlik = 225, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 8, Agirlik = 633, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 9, Agirlik = 681, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 10, Agirlik = 751, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 11, Agirlik = 503, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 12, Agirlik = 599, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 13, Agirlik = 360, AgirlikSinifi = "Hafif" },
                new Urun { UrunNo = 14, Agirlik = 562, AgirlikSinifi = "Orta" },
                new Urun { UrunNo = 15, Agirlik = 674, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 16, Agirlik = 646, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 17, Agirlik = 647, AgirlikSinifi = "Ağır" },
                new Urun { UrunNo = 18, Agirlik = 504, AgirlikSinifi = "Orta" },
            };

            List<Hucre> hucreler = new List<Hucre>(); //1'den 24'e sıralı hücre
            int hucreNo = 1;
            for (int i = 1; i <= rafSayisi; i++)
            {
                for (int j = 1; j <= katSayisi; j++)
                {
                    Hucre hucre = new Hucre();
                    hucre.HucreNo = hucreNo;
                    hucre.Raf = i;
                    hucre.Kat = j;
                    hucreNo++;
                    hucreler.Add(hucre);
                }
            }

            #region Hücrenin kapıya olan uzaklığı
            hucreler[0].KapiyaUzaklik = 4.39;
            hucreler[1].KapiyaUzaklik = 6.17;
            hucreler[2].KapiyaUzaklik = 8.32;
            hucreler[3].KapiyaUzaklik = 3.58;
            hucreler[4].KapiyaUzaklik = 5.62;
            hucreler[5].KapiyaUzaklik = 7.93;
            hucreler[6].KapiyaUzaklik = 3.58;
            hucreler[7].KapiyaUzaklik = 5.62;
            hucreler[8].KapiyaUzaklik = 7.93;
            hucreler[9].KapiyaUzaklik = 4.39;
            hucreler[10].KapiyaUzaklik = 6.17;
            hucreler[11].KapiyaUzaklik = 8.32;
            hucreler[12].KapiyaUzaklik = 8.09;
            hucreler[13].KapiyaUzaklik = 9.17;
            hucreler[14].KapiyaUzaklik = 10.74;
            hucreler[15].KapiyaUzaklik = 7.67;
            hucreler[16].KapiyaUzaklik = 8.81;
            hucreler[17].KapiyaUzaklik = 10.44;
            hucreler[18].KapiyaUzaklik = 7.67;
            hucreler[19].KapiyaUzaklik = 8.81;
            hucreler[20].KapiyaUzaklik = 10.44;
            hucreler[21].KapiyaUzaklik = 8.09;
            hucreler[22].KapiyaUzaklik = 9.17;
            hucreler[23].KapiyaUzaklik = 10.74;

            #endregion

            #endregion

            #region Tek Seferlik Gösterim İçin Kodlar
            Console.WriteLine("*-*-*-*-*-*-*- TEK SEFERLİK GÖSTERİM -*-*-*-*-*-*-*-*-");


            #region Hücreye karışık bir şekilde ürün ataması ve Skor Hesaplama

            List<Urun> karisikUrunListesi = urunler.OrderBy(x => Guid.NewGuid()).ToList();
            List<Hucre> karisikHucreListesi = hucreler.OrderBy(x => Guid.NewGuid()).ToList();

            for (int i = 0; i < urunSayisi; i++)
            {
                karisikHucreListesi[i].Urun = karisikUrunListesi[i];
            }

            foreach (var item in karisikHucreListesi)
            {
                if (item.Urun != null)
                {
                    Console.WriteLine($"{item.Raf}. Raf - {item.Kat}. Kat - {item.HucreNo} Numaraları Hücreye Atanan Ürün\nÜrün No : {item.Urun.UrunNo}\nAğırlık : {item.Urun.Agirlik}\nSınıf : {item.Urun.AgirlikSinifi}");

                    int hucreSkoru = SkorHesapla(item.Urun.AgirlikSinifi, item.Kat);
                    double hucreUzakligi = item.KapiyaUzaklik * 2; //Gidiş dönüş

                    Console.WriteLine("*-*-*");
                    Console.WriteLine("Skor     = " + hucreSkoru);
                    Console.WriteLine("Uzaklik  = " + hucreUzakligi);
                    skor += hucreSkoru;
                    uzaklik += hucreUzakligi;
                    Console.WriteLine("----------------------");
                }
                else
                {
                    Console.WriteLine($"{item.Raf}. Raf - {item.Kat}. Kat - {item.HucreNo} Numaralı Hücre: Boş");
                }
            }

            Console.WriteLine("*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("Toplam Skor      = " + skor);
            Console.WriteLine("Toplam Uzaklık   = " + uzaklik);

            #endregion

            #region Uygunluk (Fitness) Fonksiyonu

            double skorAgirlik = -0.5;
            double uzaklikAgirlik = 0.5;

            double fitness = skorAgirlik * skor + uzaklikAgirlik * uzaklik;
            Console.WriteLine("Fitness Skoru   = " + fitness);

            #endregion

            #endregion

            Console.WriteLine("\n\n");
            Console.WriteLine("*-*-*-*-*-*-*- TÜM KROMOZOMLAR VE DEĞERLERİ -*-*-*-*-*-*-*-*-");

            #region 5 Popülasyon İçin Kromozom Oluşturma ve Fitness Değerleri
            List<Kromozom> iyilestirmeOncesiKromozomlar = new List<Kromozom>();
            List<Kromozom> kromozomlar = new List<Kromozom>();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("====================================");
                Kromozom kromozom = KromozomOlustur(urunler, hucreler);
                kromozom.KromozomNo = i + 1;
                Console.WriteLine("KROMOZOM NO " + kromozom.KromozomNo);
                Console.WriteLine("Ürün Numarası Kromozomu");
                for (int j = 0; j < 18; j++)
                {
                    Console.Write(kromozom.Urunler[j].UrunNo + " \t");
                }
                Console.WriteLine();
                Console.WriteLine("Hücre Numarası Kromozomu");
                for (int j = 0; j < 18; j++)
                {
                    Console.Write(kromozom.Hucreler[j].HucreNo + " \t");
                }
                Console.WriteLine();
                Console.WriteLine("Fitness Skoru = " + kromozom.Fitness);
                kromozomlar.Add(kromozom);
                iyilestirmeOncesiKromozomlar.Add(kromozom);
                Console.WriteLine("====================================\n\n");
            }

            #endregion

            #region Seçim Operatörü (Turnuva Seçimi) 
            Console.WriteLine("*-*-*-*-*-*-*- TURNUVA SEÇİMİ -*-*-*-*-*-*-*-*-");
            Random rnd = new Random();

            List<Kromozom> yeniNesil = new List<Kromozom>();

            for (int i = 0; i < 5; i++)
            {
                List<Kromozom> karisikKromozom = kromozomlar.OrderBy(x => Guid.NewGuid()).ToList();


                Kromozom k1 = karisikKromozom[0];
                Kromozom k2 = karisikKromozom[1];

                Console.WriteLine("====================================");
                Console.WriteLine("Kromozom " + k1.KromozomNo + " -> Fitness : " + k1.Fitness);
                Console.WriteLine("Kromozom " + k2.KromozomNo + " -> Fitness : " + k2.Fitness);

                Kromozom kazanan;
                if (k1.Fitness < k2.Fitness)
                {
                    Console.WriteLine("Kazanan KROMOZOM " + k1.KromozomNo);
                    kazanan = new Kromozom // Kazanan kromozomun bir kopyasına rassal değer atayarak aynı kromozom noya sahip olanların aynı rassal değeri olmasını engelledik.
                    {
                        KromozomNo = k1.KromozomNo,
                        Urunler = new List<Urun>(k1.Urunler),
                        Hucreler = new List<Hucre>(k1.Hucreler),
                        Fitness = k1.Fitness,
                        RassalDeger = rnd.NextDouble(),
                    };
                    yeniNesil.Add(kazanan);
                }
                else
                {
                    Console.WriteLine("Kazanan KROMOZOM " + k2.KromozomNo);
                    kazanan = new Kromozom
                    {
                        KromozomNo = k2.KromozomNo,
                        Urunler = new List<Urun>(k2.Urunler),
                        Hucreler = new List<Hucre>(k2.Hucreler),
                        Fitness = k2.Fitness,
                        RassalDeger = rnd.NextDouble(),
                    };
                    yeniNesil.Add(kazanan);
                }
                Console.WriteLine("====================================");

            }

            yeniNesil = yeniNesil.OrderBy(x => x.RassalDeger).ToList(); //Rassal değere göre küçükten büyüğe sırala

            foreach (var item in yeniNesil)
            {
                Console.WriteLine("Kromozom no : " + item.KromozomNo + "\nRassal Değer : " + item.RassalDeger);
            }

            #endregion

            #region Kısmi Planlı Çaprazlama
            Console.WriteLine("\n\n*-*-*-*-*-*-*- KISMİ PLANLI ÇAPRAZLAMA -*-*-*-*-*-*-*-*-\n");

            Console.WriteLine("ÇAPRAZLAMA ÖNCESİ");
            List<Kromozom> yeniNesilKopya = new List<Kromozom>(); //Çaprazlama için kopya liste

            for (int z = 0; z < yeniNesil.Count - 1; z += 2)
            {
                List<Kromozom> caprazlanacakKromozomlar = new List<Kromozom>();

                #region Çaprazlanacak Kromozomları Ayarla

                Kromozom ilkKromozom = yeniNesil[z];
                Kromozom ikinciKromozom = yeniNesil[z + 1];

                if (rnd.NextDouble() <= 0.95) //%95 olasılık
                {
                    caprazlanacakKromozomlar.Add(new Kromozom
                    {
                        KromozomNo = ilkKromozom.KromozomNo,
                        Urunler = new List<Urun>(ilkKromozom.Urunler),
                        Hucreler = new List<Hucre>(ilkKromozom.Hucreler),
                        Fitness = ilkKromozom.Fitness,
                        RassalDeger = ilkKromozom.RassalDeger
                    });
                    caprazlanacakKromozomlar.Add(new Kromozom
                    {
                        KromozomNo = ikinciKromozom.KromozomNo,
                        Urunler = new List<Urun>(ikinciKromozom.Urunler),
                        Hucreler = new List<Hucre>(ikinciKromozom.Hucreler),
                        Fitness = ikinciKromozom.Fitness,
                        RassalDeger = ikinciKromozom.RassalDeger
                    });
                }
                else //Çaprazlama olmazsa orijinal kromozomları ekle
                {
                    caprazlanacakKromozomlar.Add(new Kromozom
                    {
                        KromozomNo = ilkKromozom.KromozomNo,
                        Urunler = new List<Urun>(ilkKromozom.Urunler),
                        Hucreler = new List<Hucre>(ilkKromozom.Hucreler),
                        Fitness = ilkKromozom.Fitness,
                        RassalDeger = ilkKromozom.RassalDeger
                    });
                    caprazlanacakKromozomlar.Add(new Kromozom
                    {
                        KromozomNo = ikinciKromozom.KromozomNo,
                        Urunler = new List<Urun>(ikinciKromozom.Urunler),
                        Hucreler = new List<Hucre>(ikinciKromozom.Hucreler),
                        Fitness = ikinciKromozom.Fitness,
                        RassalDeger = ikinciKromozom.RassalDeger
                    });
                }

                // Çaprazlamadan önceki hali
                foreach (var item in caprazlanacakKromozomlar)
                {
                    Console.WriteLine("Kromozom NO : " + item.KromozomNo);
                    Console.Write("Ürün\t| ");
                    foreach (var urunNo in item.Urunler)
                    {
                        Console.Write(urunNo.UrunNo + " \t");
                    }
                    Console.WriteLine();
                    Console.Write("Hücre\t| ");
                    foreach (var h in item.Hucreler)
                    {
                        Console.Write(h.HucreNo + " \t");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Eski fitness değeri = " + item.Fitness);
                }
                Console.WriteLine("---------------------");

                #endregion

                #region Çaprazlama
                if (caprazlanacakKromozomlar[0] != ilkKromozom) //Çaprazlama yapılacaksa
                {
                    Kromozom k1 = caprazlanacakKromozomlar[0];
                    Kromozom k2 = caprazlanacakKromozomlar[1];
                    Dictionary<int, int> urunEslestirmeK1toK2 = new Dictionary<int, int>();
                    Dictionary<int, int> urunEslestirmeK2toK1 = new Dictionary<int, int>();
                    Dictionary<int, int> hucreEslestirmeK1toK2 = new Dictionary<int, int>();
                    Dictionary<int, int> hucreEslestirmeK2toK1 = new Dictionary<int, int>();

                    for (int j = 5; j < 11; j++)
                    {
                        //Ürün eşleştirmeleri
                        if (k1.Urunler[j].UrunNo != k2.Urunler[j].UrunNo) //Aynı ürünler takas edilmez
                        {
                            urunEslestirmeK1toK2.Add(k1.Urunler[j].UrunNo, k2.Urunler[j].UrunNo);
                            urunEslestirmeK2toK1.Add(k2.Urunler[j].UrunNo, k1.Urunler[j].UrunNo);
                        }
                        //Hücre eşleştirmeleri
                        if (k1.Hucreler[j].HucreNo != k2.Hucreler[j].HucreNo)
                        {
                            hucreEslestirmeK1toK2.Add(k1.Hucreler[j].HucreNo, k2.Hucreler[j].HucreNo);
                            hucreEslestirmeK2toK1.Add(k2.Hucreler[j].HucreNo, k1.Hucreler[j].HucreNo);
                        }

                        // Ürün takası
                        Urun tempUrun = k1.Urunler[j];
                        k1.Urunler[j] = k2.Urunler[j];
                        k2.Urunler[j] = tempUrun;

                        // Hücre takası
                        Hucre tempHucre = k1.Hucreler[j];
                        k1.Hucreler[j] = k2.Hucreler[j];
                        k2.Hucreler[j] = tempHucre;

                        // Hücre - Ürün bağlantısını güncelle
                        k1.Hucreler[j].Urun = k1.Urunler[j];
                        k2.Hucreler[j].Urun = k2.Urunler[j];
                    }

                    //Çakışma düzeltmeleri
                    for (int j = 0; j < k1.Urunler.Count; j++)
                    {
                        if (j >= 5 && j < 11) //Transfer işlemi gerçekleşmiş olanlar için bunu yapma (Takas bölgesini atla)
                        {
                            continue;
                        }
                        else
                        {
                            //K1 için ürün çakışma düzeltmesi
                            int urunNo = k1.Urunler[j].UrunNo; //Örneğin, urunNo = 18
                            while (urunEslestirmeK2toK1.ContainsKey(urunNo))
                            {
                                urunNo = urunEslestirmeK2toK1[urunNo]; //Yeni urunNo = 14
                                k1.Urunler[j] = urunler.First(x => x.UrunNo == urunNo);
                                k1.Hucreler[j].Urun = k1.Urunler[j];
                            }

                            //K1 için hücre çakışma düzeltmesi
                            int hNo = k1.Hucreler[j].HucreNo;
                            while (hucreEslestirmeK2toK1.ContainsKey(hNo))
                            {
                                hNo = hucreEslestirmeK2toK1[hNo];
                                k1.Hucreler[j] = hucreler.First(x => x.HucreNo == hNo);
                                k1.Hucreler[j].Urun = k1.Urunler[j];
                            }

                            //K2 için ürün çakışma düzeltmesi
                            urunNo = k2.Urunler[j].UrunNo;
                            while (urunEslestirmeK1toK2.ContainsKey(urunNo))
                            {
                                urunNo = urunEslestirmeK1toK2[urunNo];
                                k2.Urunler[j] = urunler.First(x => x.UrunNo == urunNo);
                                k2.Hucreler[j].Urun = k2.Urunler[j];
                            }

                            //K2 için hücre çakışma düzeltmesi
                            hNo = k2.Hucreler[j].HucreNo;
                            while (hucreEslestirmeK1toK2.ContainsKey(hNo))
                            {
                                hNo = hucreEslestirmeK1toK2[hNo];
                                k2.Hucreler[j] = hucreler.First(x => x.HucreNo == hNo);
                                k2.Hucreler[j].Urun = k2.Urunler[j];
                            }
                        }
                    }

                    //Fitness değerlerini güncelle
                    k1.Fitness = FitnessHesapla(k1);
                    k2.Fitness = FitnessHesapla(k2);
                }

                yeniNesilKopya.Add(caprazlanacakKromozomlar[0]);
                yeniNesilKopya.Add(caprazlanacakKromozomlar[1]);
                #endregion
            }

            //Son kromozomu ekle (Tek sayıda kromozom varsa sonuncusu çiftlenmez)
            if (yeniNesil.Count % 2 == 1)
            {
                int sonuncuIndex = yeniNesil.Count - 1;
                yeniNesilKopya.Add(
                    new Kromozom
                    {
                        KromozomNo = yeniNesil[sonuncuIndex].KromozomNo,
                        Urunler = new List<Urun>(yeniNesil[sonuncuIndex].Urunler),
                        Hucreler = new List<Hucre>(yeniNesil[sonuncuIndex].Hucreler),
                        Fitness = yeniNesil[sonuncuIndex].Fitness,
                        RassalDeger = yeniNesil[sonuncuIndex].RassalDeger
                    });
            }

            //Çaprazlama sonrası gösterimi ve yeni fitness değerleri
            Console.WriteLine("\n\nÇAPRAZLAMA SONRASI\n");
            foreach (var item in yeniNesilKopya)
            {
                Console.WriteLine("Kromozom NO : " + item.KromozomNo);
                Console.Write("Ürün\t| ");
                foreach (var no in item.Urunler)
                {
                    Console.Write(no.UrunNo + " \t");
                }
                Console.WriteLine();
                Console.Write("Hücre\t| ");
                foreach (var h in item.Hucreler)
                {
                    Console.Write(h.HucreNo + " \t");
                }
                Console.WriteLine();
                Console.WriteLine("Yeni fitness değeri = " + item.Fitness);
            }
            Console.WriteLine("---------------------");

            for (int i = 0; i < yeniNesilKopya.Count; i++)
            {
                yeniNesilKopya[i].KromozomNo = i + 1;
            }

            //Yeni nesli güncelle
            yeniNesil = yeniNesilKopya;


            #endregion

            #region Mutasyon İşlemi - Karıştırma (Shuffle) Yaklaşımı
            Console.WriteLine("\n\n*-*-*-*-*-*-*- MUTASYON -*-*-*-*-*-*-*-*-\n");

            for (int i = 0; i < yeniNesil.Count; i++) //Tüm kromozomlar için bu işlemleri yap
            {
                int karistirmaAraligi = 4;
                int karistirmaBaslangici = rnd.Next(0, urunler.Count - karistirmaAraligi);
                int karistirmaSonu = karistirmaBaslangici + karistirmaAraligi;

                Console.WriteLine($"Kromozom {yeniNesil[i].KromozomNo} için mutasyon: {karistirmaBaslangici} - {karistirmaSonu} aralığında");

                List<Hucre> hucreKaristirmaListesi = new List<Hucre>();
                List<Urun> urunKaristirmaListesi = new List<Urun>();
                for (int j = karistirmaBaslangici; j < karistirmaSonu; j++)
                {
                    hucreKaristirmaListesi.Add(yeniNesil[i].Hucreler[j]);
                    urunKaristirmaListesi.Add(yeniNesil[i].Urunler[j]);
                }

                List<Hucre> karistirilmisHucreBlogu = hucreKaristirmaListesi.OrderBy(x => Guid.NewGuid()).ToList();
                List<Urun> karistirilmisUrunBlogu = urunKaristirmaListesi.OrderBy(x => Guid.NewGuid()).ToList();

                int g = 0;
                for (int j = karistirmaBaslangici; j < karistirmaSonu; j++)
                {
                    yeniNesil[i].Hucreler[j] = karistirilmisHucreBlogu[g];
                    yeniNesil[i].Urunler[j] = karistirilmisUrunBlogu[g];
                    yeniNesil[i].Hucreler[j].Urun = karistirilmisUrunBlogu[g];
                    g++;
                }

                //Fitness değerini güncelle
                double eskiFitness = yeniNesil[i].Fitness;
                yeniNesil[i].Fitness = FitnessHesapla(yeniNesil[i]);
                Console.WriteLine($"Kromozom {yeniNesil[i].KromozomNo} mutasyona uğradı. Eski Fitness: {eskiFitness}, Yeni Fitness: {yeniNesil[i].Fitness}");
            }


            Console.WriteLine("\n\nMUTASYON SONRASI\n");
            foreach (var item in yeniNesil)
            {
                Console.WriteLine("Kromozom NO : " + item.KromozomNo);
                Console.Write("Ürün\t| ");
                foreach (var no in item.Urunler)
                {
                    Console.Write(no.UrunNo + " \t");
                }
                Console.WriteLine();
                Console.Write("Hücre\t| ");
                foreach (var h in item.Hucreler)
                {
                    Console.Write(h.HucreNo + " \t");
                }
                Console.WriteLine();
                Console.WriteLine("Yeni fitness değeri = " + item.Fitness);
            }
            Console.WriteLine("---------------------");

            #endregion

            #region Genel Değerlendirme
            Kromozom optimalKromozom = new Kromozom();
            optimalKromozom.Fitness = 1000;
            Console.WriteLine("\n--------------------");
            Console.WriteLine("İyileştirme Öncesi");
            for (int i = 0; i < iyilestirmeOncesiKromozomlar.Count; i++)
            {
                Kromozom k = iyilestirmeOncesiKromozomlar[i];
                Console.WriteLine("Kromozom NO : " + k.KromozomNo);
                Console.WriteLine("Fitness     : " + k.Fitness);
                if (k.Fitness < optimalKromozom.Fitness)
                {
                    optimalKromozom = k;
                }
            }

            Console.WriteLine("\nİyileştirme Sonrası");
            for (int i = 0; i < yeniNesil.Count; i++)
            {
                Kromozom k = yeniNesil[i];
                Console.WriteLine("Kromozom NO : " + k.KromozomNo);
                Console.WriteLine("Fitness     : " + k.Fitness);
                if (k.Fitness < optimalKromozom.Fitness)
                {
                    optimalKromozom = k;
                }
            }

            Console.WriteLine("\nOptimal Kromozom : " + optimalKromozom.KromozomNo);

            Console.Write("Ürün\t| ");
            foreach (var no in optimalKromozom.Urunler)
            {
                Console.Write(no.UrunNo + " \t");
            }
            Console.WriteLine();
            Console.Write("Hücre\t| ");
            foreach (var h in optimalKromozom.Hucreler)
            {
                Console.Write(h.HucreNo + " \t");
            }
            Console.WriteLine();
            Console.WriteLine("Optimal fitness değeri = " + optimalKromozom.Fitness);


            #endregion
        }

        public static int SkorHesapla(string agirlikSinifi, int kat)
        {
            if (agirlikSinifi == "Ağır" && kat == 1) return 20;
            if (agirlikSinifi == "Ağır" && kat == 2) return 14;
            if (agirlikSinifi == "Ağır" && kat == 3) return 2;
            if (agirlikSinifi == "Orta" && kat == 1) return 14;
            if (agirlikSinifi == "Orta" && kat == 2) return 20;
            if (agirlikSinifi == "Orta" && kat == 3) return 14;
            if (agirlikSinifi == "Hafif" && kat == 1) return 2;
            if (agirlikSinifi == "Hafif" && kat == 2) return 14;
            if (agirlikSinifi == "Hafif" && kat == 3) return 20;
            return 0;
        }

        public static Kromozom KromozomOlustur(List<Urun> urunListesi, List<Hucre> hucreListesi)
        {
            //Kromozom oluştur
            Kromozom kromozom = new Kromozom
            {
                Urunler = new List<Urun>(urunListesi.OrderBy(x => Guid.NewGuid())),
                Hucreler = new List<Hucre>(hucreListesi.OrderBy(x => Guid.NewGuid()).Take(18))
            };

            //Hücrelere ürün ataması yap
            for (int i = 0; i < 18; i++)
            {
                kromozom.Hucreler[i].Urun = kromozom.Urunler[i];
            }

            kromozom.Fitness = FitnessHesapla(kromozom);
            return kromozom;
        }

        public static double FitnessHesapla(Kromozom kromozom)
        {
            int skor = 0;
            double uzaklik = 0;
            double skorAgirlik = -0.5;
            double uzaklikAgirlik = 0.5;

            foreach (var hucre in kromozom.Hucreler)
            {
                if (hucre.Urun != null)
                {
                    int hucreSkoru = SkorHesapla(hucre.Urun.AgirlikSinifi, hucre.Kat);
                    double hucreUzakligi = hucre.KapiyaUzaklik * 2; // Gidiş dönüş
                    skor += hucreSkoru;
                    uzaklik += hucreUzakligi;
                }
            }

            return skorAgirlik * skor + uzaklikAgirlik * uzaklik;
        }

    }
}
