using System.Reflection;
using MSE_Kullanimi;

internal class Program
{

    private static void Main(string[] args)
    {


        var model = new LinearModel { W = 0, B = 0 };

        double[] xs = { 1, 2, 3, 4 };
        double[] ys = { 2, 4, 6, 8 };


        for (int epoch = 0; epoch < 10; epoch++)
        {
            Console.WriteLine($"Epoch: {epoch}");
            for (int i = 0; i < xs.Length; i++)
            {
                Train(model, xs[i], ys[i], 0.01);
                Console.WriteLine($"W: {model.W}, B: {model.B}");
                Console.WriteLine(new string('-', 10));
            }

            Console.WriteLine(new string('$', 20));

            if (epoch % 10 == 0)
            {
                double mse = CalculateMSE(model, xs, ys);
                Console.WriteLine($"Epoch {epoch} - MSE: {mse}");
            }
        }
    }




    static void Train(LinearModel model, double x, double y, double learningRate)
    {
        double prediction = model.Predict(x);
        Console.WriteLine($"prediction: {prediction}");
        //hata = tahmin - gerçek
        double error = prediction - y;
        Console.WriteLine($"error: {error}");


        // türevler
        model.W -= learningRate * error * x;
        model.B -= learningRate * error;
    }



    static double CalculateMSE(LinearModel model, double[] xs, double[] ys)
    {
        double totalError = 0;

        for (int i = 0; i < xs.Length; i++)
        {
            double prediction = model.Predict(xs[i]);
            totalError += Math.Pow(prediction - ys[i], 2);
        }

        return totalError / xs.Length;
        //MSE = (1/N) * Σ (tahmin - gerçek)²
    }
}