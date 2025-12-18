using ProblemXOR;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //Eğitim verisi (XOR problemi)
        double[,] xs =
{
    {0,0},
    {0,1},
    {1,0},
    {1,1}
};

        double[] ys = { 0, 1, 1, 0 }; // Beklenen çıktılar

        var net = new XorNetwork();
        Random r = new Random();

        // Rastgele başlat
        net.W11 = r.NextDouble();
        net.W12 = r.NextDouble();
        net.W21 = r.NextDouble();
        net.W22 = r.NextDouble();
        net.V1 = r.NextDouble();
        net.V2 = r.NextDouble();

        // Eğitim döngüsü
        for (int epoch = 0; epoch < 10000; epoch++)
        {
            for (int i = 0; i < 4; i++)
            {
                Train(net, xs[i, 0], xs[i, 1], ys[i], 0.01);
            }
        }

        // Test
        Console.WriteLine(net.Predict(0, 0));
        Console.WriteLine(net.Predict(0, 1));
        Console.WriteLine(net.Predict(1, 0));
        Console.WriteLine(net.Predict(1, 1));

    }







    /// <summary>
    /// Eğitim (backpropagation – sade hali)
    ///Burada tam türev yazmıyoruz, ama mantığı aynı:
    /// </summary>
    /// <param name="net"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="y"></param>
    /// <param name="lr"></param>
    static void Train(XorNetwork net, double x1, double x2, double y, double lr)
    {
        // Forward
        double z1 = net.W11 * x1 + net.W12 * x2 + net.B1;
        double h1 = 1 / (1 + Math.Exp(-z1));

        double z2 = net.W21 * x1 + net.W22 * x2 + net.B2;
        double h2 = 1 / (1 + Math.Exp(-z2));

        double z3 = net.V1 * h1 + net.V2 * h2 + net.B3;
        double yPred = 1 / (1 + Math.Exp(-z3));

        // Output error (BCE + Sigmoid)
        double d3 = yPred - y;

        // Çıkış ağırlıkları
        net.V1 -= lr * d3 * h1;
        net.V2 -= lr * d3 * h2;
        net.B3 -= lr * d3;

        // Gizli katman hataları
        double d1 = d3 * net.V1 * h1 * (1 - h1);
        double d2 = d3 * net.V2 * h2 * (1 - h2);

        net.W11 -= lr * d1 * x1;
        net.W12 -= lr * d1 * x2;
        net.B1 -= lr * d1;

        net.W21 -= lr * d2 * x1;
        net.W22 -= lr * d2 * x2;
        net.B2 -= lr * d2;
    }

}