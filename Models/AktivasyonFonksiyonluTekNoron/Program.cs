using AktivasyonFonksiyonluTekNoron;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        SigmoidModel model = new SigmoidModel { W = 0.5, B = 0 };

        double[] xs = { 0, 1, 2, 3, 4, 5 };
        double[] ys = { 0, 0, 0, 1, 1, 1 };

        for (int epoch = 0; epoch < 10; epoch++)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Write(epoch + " ");
            }
            Console.WriteLine("\n");
            for (int i = 0; i < xs.Length; i++)
            {
                Console.Write(i + " ");
                Train(model, xs[i], ys[i], 0.01);
                Console.WriteLine($"W: {model.W}, B: {model.B}");
                Console.WriteLine(new string('-', 10));
            }
            Console.WriteLine("\n");
        }
        Console.WriteLine($"Final W: {model.W}, Final B: {model.B}");
    }


    static void Train(SigmoidModel model, double x, double y, double learningRate)
    {
        double prediction = model.Predict(x);
        Console.WriteLine($"Prediction: {prediction}");
        double error = prediction - y;
        Console.WriteLine($"Error: {error}");
        // türevler
        double dZ = error * prediction * (1 - prediction); // Sigmoid türevi
        model.W -= learningRate * dZ * x;
        model.B -= learningRate * dZ;
    }
}