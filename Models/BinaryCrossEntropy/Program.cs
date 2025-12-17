using BinaryCrossEntropy;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var model = new Model(0, 0);

        double x = 4;
        double y = 1; // gerçek etiket (0 veya 1)

        for (int epoch = 0; epoch < 100; epoch++)
        {
            Train(model, x, y, 0.1);
            Console.WriteLine($"Epoch {epoch} - W: {model.W}, B: {model.B}");
        }


    }

    private static void Train(Model model, double x, double y, double learningRate)
    {
        double yPred = model.Predict(x);

        double dL_dz = yPred - y;

        model.W -= learningRate * dL_dz * x;
        model.B -= learningRate * dL_dz;
    }

}