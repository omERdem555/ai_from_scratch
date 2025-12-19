using Sahte_Gercek_Kullanici_Tespiti;

internal class Program
{
    private static void Main(string[] args)
    {
        /*
         * GİRİŞ VERİLERİ
         * x[0] = Günlük işlem yoğunluğu (0–1)
         * x[1] = Saat düzensizliği (0–1)
         * y    = 1 → sahte kullanıcı
         * y    = 0 → gerçek kullanıcı
         */

        double[][] girisVerileri =
        {
            new double[] {0.1, 0.1},   // sahte (çok düşük aktivite)
            new double[] {0.9, 0.9},   // sahte (çok yüksek aktivite)
            new double[] {0.15, 0.8},  // sahte
            new double[] {0.8, 0.2},   // sahte

            new double[] {0.4, 0.5},   // gerçek
            new double[] {0.5, 0.4},   // gerçek
            new double[] {0.6, 0.6},   // gerçek
            new double[] {0.45, 0.55}  // gerçek
        };

        double[] etiketler =
        {
            1, 1, 1, 1,
            0, 0, 0, 0
        };

        // İki katmanlı sinir ağı oluşturuluyor
        var ag = new IkiKatmanliSinirAgi();

        int epochSayisi = 5000;
        double ogrenmeOrani = 0.1;

        // Eğitim döngüsü
        for (int epoch = 0; epoch < epochSayisi; epoch++)
        {
            for (int i = 0; i < girisVerileri.Length; i++)
            {
                Egit(ag, girisVerileri[i], etiketler[i], ogrenmeOrani);
            }
        }

        // Test sonuçları
        Console.WriteLine("TEST SONUÇLARI");
        Console.WriteLine("----------------");

        for (int i = 0; i < girisVerileri.Length; i++)
        {
            double tahmin = ag.TahminEt(girisVerileri[i]);

            Console.WriteLine(
                $"Giriş: ({girisVerileri[i][0]}, {girisVerileri[i][1]}) " +
                $"→ Tahmin: {tahmin:F3} | Gerçek: {etiketler[i]}"
            );
        }
    }

    /// <summary>
    /// Tek örnek üzerinden eğitim yapar
    /// (Binary Cross Entropy + Backpropagation)
    /// </summary>
    static void Egit(IkiKatmanliSinirAgi ag,double[] giris, double hedef, double ogrenmeOrani)
    {
        // === FORWARD PASS ===

        double[] gizliKatmanCikisi = new double[2];
        double[] gizliKatmanToplam = new double[2];

        // Gizli katman hesaplaması
        for (int i = 0; i < 2; i++)
        {
            gizliKatmanToplam[i] = ag.GizliBias[i];

            for (int j = 0; j < 2; j++)
                gizliKatmanToplam[i] += ag.GizliAgirliklar[i, j] * giris[j];

            gizliKatmanCikisi[i] =
                1.0 / (1.0 + Math.Exp(-gizliKatmanToplam[i]));
        }

        // Çıkış nöronu
        double cikisToplam = ag.CikisBias;
        for (int i = 0; i < 2; i++)
            cikisToplam += ag.CikisAgirliklari[i] * gizliKatmanCikisi[i];

        double tahmin =
            1.0 / (1.0 + Math.Exp(-cikisToplam));

        // === BACKPROPAGATION ===

        // Çıkış katmanı hatası
        double cikisHatasi = tahmin - hedef;

        // Çıkış katmanı ağırlıkları güncelleme
        for (int i = 0; i < 2; i++)
            ag.CikisAgirliklari[i] -=
                ogrenmeOrani * cikisHatasi * gizliKatmanCikisi[i];

        ag.CikisBias -= ogrenmeOrani * cikisHatasi;

        // Gizli katman güncellemesi
        for (int i = 0; i < 2; i++)
        {
            double gizliHata =
                cikisHatasi *
                ag.CikisAgirliklari[i] *
                gizliKatmanCikisi[i] *
                (1 - gizliKatmanCikisi[i]);

            for (int j = 0; j < 2; j++)
                ag.GizliAgirliklar[i, j] -=
                    ogrenmeOrani * gizliHata * giris[j];

            ag.GizliBias[i] -= ogrenmeOrani * gizliHata;
        }
    }
}
