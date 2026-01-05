internal class Program
{
    private static void Main(string[] args)
    {

    #region Veriler
        // x1: sıklık, x2: tutar, x3: gece oranı
        // y: 1 = riskli, 0 = güvenli
        double[][] veriSeti =
        {
    new double[] {0.05, 0.40, 0.10}, // çok düşük → riskli
    new double[] {0.10, 0.30, 0.20}, // riskli
    new double[] {0.20, 0.50, 0.30}, // riskli

    new double[] {0.45, 0.50, 0.10}, // güvenli
    new double[] {0.50, 0.45, 0.15}, // güvenli
    new double[] {0.60, 0.55, 0.20}, // güvenli

    new double[] {0.80, 0.70, 0.40}, // çok yüksek → riskli
    new double[] {0.90, 0.85, 0.60}, // riskli
};

        double[] etiketler =
        {
    1, 1, 1,
    0, 0, 0,
    1, 1
};

        #endregion

        IkiKatmanliAg ag = new IkiKatmanliAg();

        // Eğitim
        int epokSayisi = 5000;
        double ogrenmeOrani = 0.5;
        for (int epok = 0; epok < epokSayisi; epok++)
        {
            for (int i = 0; i < veriSeti.Length; i++)
            {
                Egit(ag, veriSeti[i], etiketler[i], ogrenmeOrani);
            }
        }

        Console.WriteLine("TEST:");
        for (int i = 0; i < veriSeti.Length; i++)
        {
            double tahmin = ag.Tahmin(veriSeti[i]);
            Console.WriteLine($"Girdi: [{veriSeti[i][0]:F2}, {veriSeti[i][1]:F2}, {veriSeti[i][2]:F2}] => Tahmin: {tahmin:F5} (Gerçek: {etiketler[i]})");
        }




        Console.WriteLine("Hello, World!");
    }



    static void Egit(IkiKatmanliAg ag, double[] x, double y, double ogrenmeOrani)
    {
        // İleri besleme
        double[] zG = new double[2];
        double[] h = new double[2];

        for (int i = 0; i < 2; i++)
        {
            zG[i] = ag.biasGizli[i];
            for (int j = 0; j < 3; j++)
                zG[i] += ag.agirlikGizli[i, j] * x[j];

            h[i] = 1 / (1 + Math.Exp(-zG[i]));
        }

        double zC = ag.biasCikis + ag.agirlikCikis[0] * h[0] + ag.agirlikCikis[1] * h[1];
        double yTahmin = 1 / (1 + Math.Exp(-zC));

        // Çıkış hatası
        double hataCikis = yTahmin - y;

        // Çıkış güncelleme
        for (int i = 0; i < 2; i++)
            ag.agirlikCikis[i] -= ogrenmeOrani * hataCikis * h[i];

        ag.biasCikis -= ogrenmeOrani * hataCikis;

        // Gizli katman
        for (int i = 0; i < 2; i++)
        {
            double hataGizli = hataCikis * ag.agirlikCikis[i] * h[i] * (1 - h[i]);

            for (int j = 0; j < 3; j++)
                ag.agirlikGizli[i, j] -= ogrenmeOrani * hataGizli * x[j];

            ag.biasGizli[i] -= ogrenmeOrani * hataGizli;
        }
    }


    class IkiKatmanliAg
    {
        // Gizli katman: 2 nöron, 3 giriş
        public double[,] agirlikGizli = new double[2, 3];
        public double[] biasGizli = new double[2];

        // Çıkış katmanı
        public double[] agirlikCikis = new double[2];
        public double biasCikis;

        Random rnd = new Random();

        public IkiKatmanliAg()
        {
            for (int i = 0; i < 2; i++)
            {
                biasGizli[i] = rnd.NextDouble() - 0.5;
                agirlikCikis[i] = rnd.NextDouble() - 0.5;

                for (int j = 0; j < 3; j++)
                    agirlikGizli[i, j] = rnd.NextDouble() - 0.5;
            }

            biasCikis = rnd.NextDouble() - 0.5;
        }

        double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }

        public double Tahmin(double[] x)
        {
            double[] gizli = new double[2];

            for (int i = 0; i < 2; i++)
            {
                double z = biasGizli[i];
                for (int j = 0; j < 3; j++)
                    z += agirlikGizli[i, j] * x[j];

                gizli[i] = Sigmoid(z);
            }

            double cikis = biasCikis;
            for (int i = 0; i < 2; i++)
                cikis += agirlikCikis[i] * gizli[i];

            return Sigmoid(cikis);
        }
    }

}