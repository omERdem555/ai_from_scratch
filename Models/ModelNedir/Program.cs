using ModelNedir;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var model = new LinearModel { W = 0, B = 0 };

        double[] xs = { 1, 2, 3, 4 };
        double[] ys = { 2, 4, 6, 8 };

        for (int epoch = 0; epoch < 100; epoch++)
        {
            for (int i = 0; i < xs.Length; i++)
            {
                Train(model, xs[i], ys[i], 0.01);
                Console.WriteLine($"W: {model.W}, B: {model.B}");
                Console.WriteLine(new string('-', 10));
            }
            Console.WriteLine("\n");
        }
        Console.WriteLine($"W: {model.W}, B: {model.B}");
    }
    static void Train(LinearModel model, double x, double y, double learningRate)
    {
        double prediction = model.Predict(x);
        double error = prediction - y;

        // türevler
        model.W -= learningRate * error * x;
        model.B -= learningRate * error;
    }
    static double MeanSquaredError(double predicted, double actual)
    {
        return Math.Pow(predicted - actual, 2);
    }
}