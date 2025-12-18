using Kredi_Onay_Kararı;

internal class Program
{
    static void Main()
    {
        // 3 özellikli basit bir nöron modeli
        var model = new NeuronModel(3);


        // Eğitim verisi: [Gelir, Kredi Skoru, Borç/ Gelir Oranı] -> [Onay (1) / Reddet (0)]
        double[][] X =
        {
        new double[] {0.9, 0.1, 0.8},
        new double[] {0.8, 0.2, 0.7},
        new double[] {0.7, 0.3, 0.6},
        new double[] {0.4, 0.6, 0.4},
        new double[] {0.3, 0.7, 0.3},
        new double[] {0.2, 0.8, 0.2}
    };

        double[] Y = { 1, 1, 1, 0, 0, 0 };// Etiketler

        // Eğitim döngüsü
        for (int epoch = 0; epoch < 1000; epoch++)
        {
            for (int i = 0; i < X.Length; i++)
                Train(model, X[i], Y[i], 0.1);
        }

        Console.WriteLine("Test:");
        foreach (var x in X)
            Console.WriteLine(model.Predict(x).ToString("F3"));
    }



    /// <summary>
    /// Train (BCE + Sigmoid, sade hali)
    /// </summary>
    /// <param name="model"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="learningRate"></param>
    static void Train(NeuronModel model, double[] x, double y, double learningRate)
    {
        double yPred = model.Predict(x);

        // BCE + Sigmoid türevi sadeleşir
        double error = yPred - y;

        for (int i = 0; i < x.Length; i++)
            model.W[i] -= learningRate * error * x[i];

        model.B -= learningRate * error;
    }

}