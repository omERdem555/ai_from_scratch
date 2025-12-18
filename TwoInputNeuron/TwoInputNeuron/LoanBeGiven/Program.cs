using System.Reflection;
using TwoInputNeuron;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //Örnek veri seti (2D) - Özellikler (Gelir oranı ve kredi skoru)
        double[,] xs =
{
    {0.2, 0.8},
    {0.3, 0.7},
    {0.6, 0.4},
    {0.8, 0.2},
    {0.4, 0.6},
    {0.7, 0.3}
};
        double[] ys =
            { 0, 0, 1, 1, 0, 1 }; //Hedef etiketler (0: kredi verilmez, 1: kredi verilir)



        //Modeli başlat
        Neuron model = new Neuron
        {
            W1 = 0,
            W2 = 0,
            B = 0
        };

        //Eğitim döngüsü
        for (int epoch = 0; epoch < 3000; epoch++)
        {
            for (int i = 0; i < ys.Length; i++)
            {
                Train(model, xs[i, 0], xs[i, 1], ys[i], 0.1);
            }
        }


        Console.WriteLine($"Final W1(Gelir oranı ağırlığı): {model.W1}\n" +
            $"Final W2(kredi skoru): {model.W2}\n");


        Console.Write("Gelir oranı: 0.75 ve kredi skoru: 0.25 için\n");
        Console.WriteLine(model.Predict(0.75, 0.25)); // yüksek olmalı

        Console.WriteLine();

        Console.Write("Gelir oranı: 0.3 ve kredi skoru: 0.8 için\n");
        Console.WriteLine(model.Predict(0.3, 0.8));   // düşük olmalı

    }

    /// <summary>
    /// Train fonksiyonu (BCE + Sigmoid)
    /// </summary>
    /// <param name="model"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="y"></param>
    /// <param name="learningRate"></param>
    static void Train(Neuron model, double x1, double x2, double y, double learningRate)
    {
        double yPred = model.Predict(x1, x2);
        //Console.WriteLine($"prediction: {yPred}");
        double error = yPred - y;
        //Console.WriteLine($"error: {error}");

        model.W1 -= learningRate * error * x1;
        model.W2 -= learningRate * error * x2;
        model.B -= learningRate * error;
    }
}