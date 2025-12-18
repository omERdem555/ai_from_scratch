using AktivasyonFonksiyonluTekNoron;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        SigmoidModel model = new SigmoidModel { W = 0.5, B = 0 };

        double[] xs = { 0, 1, 2, 3, 4, 5 };
        double[] ys = { 0, 0, 0, 1, 1, 1 };

        for (int epoch = 0; epoch < 1000; epoch++)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Write(epoch + " ");
            }
            Console.WriteLine("\n");
            for (int i = 0; i < xs.Length; i++)
            {
                Console.Write(i + " ");
                Train(model, xs[i], ys[i], 0.1);
                Console.WriteLine($"W: {model.W}, B: {model.B}");
                Console.WriteLine(new string('-', 10));
            }
            Console.WriteLine("\n");
        }
        Console.WriteLine($"Final W: {model.W}, Final B: {model.B}");

        Console.WriteLine("\n");
        Console.WriteLine("\n");
        Console.WriteLine("\n");

        for (int i = 0; i < xs.Length; i++)
        {
            double pred = model.Predict(xs[i]);
            Console.WriteLine($"x={xs[i]} -> {pred:F3}");
        }

    }


    static void Train(SigmoidModel model, double x, double y, double learningRate)
    {
        double prediction = model.Predict(x);
        double error = prediction - y;   // BCE + Sigmoid için bu yeterli

        model.W -= learningRate * error * x;
        model.B -= learningRate * error;
    }

}