internal class Program
{
    private static void Main(string[] args)
    {
        #region Veri Seti

        /*
Girdi	Açıklama
x₁	Günlük işlem sıklığı
x₂	Ortalama işlem tutarı
x₃	Hesap yaşı (normalize)
 */
        double[,] OrnekVeriSeti =
        {
//x1   x2   x3   y
//----------------
{ 0.1, 0.1, 0.2, 0},
{ 0.2, 0.2, 0.3, 0},
{ 0.3, 0.4, 0.3, 0},

{ 0.6, 0.6, 0.7, 1},
{ 0.7, 0.8, 0.6, 1},
{ 0.8, 0.7, 0.9, 1}
        }; 
        #endregion


        #region Ayrıştırma
        // Ayrıştırma
        double[][] X = new double[OrnekVeriSeti.GetLength(0)][];
        double[] Y = new double[OrnekVeriSeti.GetLength(0)];
        for (int i = 0; i < OrnekVeriSeti.GetLength(0); i++)
        {
            X[i] = new double[OrnekVeriSeti.GetLength(1) - 1];
            for (int j = 0; j < OrnekVeriSeti.GetLength(1) - 1; j++)
                X[i][j] = OrnekVeriSeti[i, j];
            Y[i] = OrnekVeriSeti[i, OrnekVeriSeti.GetLength(1) - 1];
        } 
        #endregion

        TekNoron noron = new TekNoron(3);
        double lr = 0.1;

        for (int epoch = 0; epoch < 4000; epoch++)
        {
            for (int i = 0; i < X.Length ; i++)
                Egit(noron, X[i], Y[i], lr);
        }

        Console.WriteLine("TEST:");
        Console.WriteLine("0.2, 0.2, 0.3: " + (noron.Tahmin(new double[] { 0.2, 0.2, 0.3 }).ToString("F3")));
        Console.WriteLine("0.7, 0.7, 0.8: " + (noron.Tahmin(new double[] { 0.7, 0.7, 0.8 }).ToString("F3")));

        Console.WriteLine("Hello, World!");
    }



    class TekNoron
    {
        public double[] Agirliklar;
        public double Bias;

        public TekNoron(int girisSayisi)
        {
            Agirliklar = new double[girisSayisi];
            Random rnd = new Random();

            for (int i = 0; i < girisSayisi; i++)
                Agirliklar[i] = rnd.NextDouble() * 0.5;

            Bias = 0.0;
        }

        public double Tahmin(double[] x)
        {
            double z = Bias;

            for (int i = 0; i < x.Length; i++)
                z += Agirliklar[i] * x[i];

            return Sigmoid(z);
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }
    }


    static void Egit(TekNoron noron, double[] x, double y, double ogrenmeOrani)
    {
        double tahmin = noron.Tahmin(x);
        double hata = tahmin - y;

        for (int i = 0; i < x.Length; i++)
            noron.Agirliklar[i] -= ogrenmeOrani * hata * x[i];

        noron.Bias -= ogrenmeOrani * hata;
    }

}