namespace Sahte_Gercek_Kullanici_Tespiti
{
    /// <summary>
    /// 2 girişli → 2 gizli nöron → 1 çıkışlı
    /// Sigmoid aktivasyonlu sinir ağı
    /// </summary>
    public class IkiKatmanliSinirAgi
    {


        // === GİZLİ KATMAN ===
        // [gizliNöronIndex, girişIndex]
        public double[,] GizliAgirliklar = new double[2, 2];
        public double[] GizliBias = new double[2];



        // === ÇIKIŞ KATMANI ===
        public double[] CikisAgirliklari = new double[2];
        public double CikisBias;




        public IkiKatmanliSinirAgi()
        {
            var rastgele = new Random();

            // Küçük rastgele başlangıç değerleri
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    GizliAgirliklar[i, j] =
                        rastgele.NextDouble() - 0.5;

            for (int i = 0; i < 2; i++)
                CikisAgirliklari[i] =
                    rastgele.NextDouble() - 0.5;
        }



        /// <summary>
        /// Verilen giriş için ağın tahminini döndürür
        /// </summary>
        public double TahminEt(double[] giris)
        {
            double[] gizliCikis = new double[2];



            // Gizli katman
            for (int i = 0; i < 2; i++)
            {
                double toplam = GizliBias[i];

                for (int j = 0; j < 2; j++)
                    toplam += GizliAgirliklar[i, j] * giris[j];

                gizliCikis[i] = Sigmoid(toplam);
            }



            // Çıkış katmanı
            double cikisToplam = CikisBias;
            for (int i = 0; i < 2; i++)
                cikisToplam += CikisAgirliklari[i] * gizliCikis[i];

            return Sigmoid(cikisToplam);
        }

        private double Sigmoid(double deger)
        {
            return 1.0 / (1.0 + Math.Exp(-deger));
        }
    }
}
