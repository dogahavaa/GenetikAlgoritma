# GenetikAlgoritma
Bu proje, bir depodaki ürünlerin raflara optimal şekilde yerleştirilmesini sağlamak için genetik algoritma (GA) tabanlı bir optimizasyon çözümü sunar.
Amaç, ağır ürünlerin alt katlara, hafif ürünlerin üst katlara yerleştirilmesi ve kapıya yakınlık gibi faktörleri dikkate alarak toplam maliyeti (fitness) minimize etmektir.

Problem Tanımı: 18 ürünün 8 raf ve 3 kat içeren 24 hücreye optimal yerleştirilmesi.

Fitness Fonksiyonu: Ürünlerin ağırlık sınıflarına (Ağır, Orta, Hafif) ve kat numaralarına göre bir skor hesaplar, ayrıca hücrelerin kapıya uzaklığını dikkate alır.

Genetik Algoritma Operatörleri:
- Seçim: Turnuva seçimi ile en iyi kromozomlar seçilir.
- Çaprazlama: Kısmi Planlı Çaprazlama (PMX) ile kromozomlar arasında gen takası yapılır.
- Mutasyon: Rastgele 4 genlik bir bloğun karıştırılmasıyla çeşitlilik sağlanır.
- Jenerasyonlar: Birden fazla jenerasyon boyunca popülasyon evriltilerek en iyi çözüm aranır.

Gelecek İyileştirmeler:
- Şu anda sabit olarak kodlanmış olan ürün sayısı, hücre sayısı (raf ve kat sayısı), kapıya uzaklık verileri, başlangıç popülasyonu büyüklüğü ve turnuva büyüklüğü gibi parametreler, kullanıcının ihtiyaçlarına göre dinamik olarak yapılandırılabilir hale getirilecek. Bu, farklı depo düzenleri ve optimizasyon senaryolarına kolayca uyarlanabilir bir sistem sağlayacak.
