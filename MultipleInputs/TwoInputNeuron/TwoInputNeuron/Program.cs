using static System.Net.Mime.MediaTypeNames;
using TwoInputNeuron;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //Örnek veri seti (2D) - Çalışma süresi ve devamsızlık oranı
        double[,] xs =
{
    { 1, 0.9 }, //Geçememiş
    { 2, 0.8 }, //Geçememiş
    { 4, 0.2 }, //Geçmiş
    { 5, 0.1 }, //Geçmiş
    { 3, 0.7 }, //Geçememiş
    { 4, 0.4 }  //Geçmiş
};

        double[] ys = { 0, 0, 1, 1, 0, 1 }; //Hedef etiketler (0: başarısız, 1: başarılı)




        //Eğitim döngüsü
        var model = new Neuron
        {
            W1 = 0,
            W2 = 0,
            B = 0
        };

        for (int epoch = 0; epoch < 2000; epoch++)
        {
            Console.WriteLine($"{epoch}. döngü: ");
            for (int i = 0; i < ys.Length; i++)
            {
                Console.WriteLine($"Çalışma Süresi: {xs[i, 0]} devamsızlık oranı: {xs[i, 1]} y: {ys[i]}");
                Train(model, xs[i, 0], xs[i, 1], ys[i], 0.1);
            }
            Console.WriteLine($"\n{new string('-', 30)}\n");
        }



        //Test et
        double pred = model.Predict(4, 0.3); //Çalışma süresi: 4 saat, devamsızlık oranı: %30
        Console.WriteLine("En son tahmin: " + pred);
        Console.WriteLine($"W1: {model.W1}, W2: {model.W2}, B: {model.B}");


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
        Console.WriteLine($"prediction: {yPred}");
        double error = yPred - y;
        Console.WriteLine($"error: {error}");

        model.W1 -= learningRate * error * x1;
        model.W2 -= learningRate * error * x2;
        model.B -= learningRate * error;
    }
}