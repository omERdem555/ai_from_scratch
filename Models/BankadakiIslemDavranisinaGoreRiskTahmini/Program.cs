

using System.Runtime.Serialization.Formatters;

internal class Program
{


    private static void Main(string[] args)
    {


        double[,] veriSeti = new double[,]
    {
        { 0.05 , 0 },
{ 0.10 , 0 },
{ 0.20 , 0},
{ 0.45 , 1},
{ 0.50 , 1},
{ 0.55 , 1},
{ 0.80 , 1},
{ 0.90 , 1 }

    };

        Neuron neuron = new Neuron();

        double OgrenmeOrani = 0.1;
        for (int epoch = 0; epoch < 3000; epoch++)
        {
            for (int i = 0; i < veriSeti.GetLength(0); i++)
            {
                Egit(veriSeti[i, 0], veriSeti[i, 1], neuron, OgrenmeOrani);
            }
        }

        Console.WriteLine("ağırlık: " + neuron.weight +
            "\n" + "bias: " + neuron.bias);

        Console.WriteLine("0.22 işlem sıklığına sahip birinin riskli olma oranı: " + neuron.Tahmin(0.22));

        Console.WriteLine("0.67 işlem sıklığına sahip birinin riskli olma oranı: " + neuron.Tahmin(0.67));
        Console.WriteLine("\n\nFakat şöyle bir sorunumuz var ki.\n" +
            "Banka sistemlerinde kişinin işlem sıklığına göre rikli bir kullanıcı olup olmadığını tespit etmek\n" +
            "tek bir nöron kullanılar çözülecek bir sorun değildir. Bunun için iki nörona ihtiyaç vardır. \n" +
            "Biz şuan ne yaptık? Tek nöron, tek input kullandık. " +
            "Yani bu karara varmak için tek bir özelliği tek bir yerden ayırdık. \n" +
            "Sadece belli bir sayıdan fazla işlem yapan kullanıcılar değil. \n" +
            "Aynı zamanda belli bir sayıdan az işlem yapan kullanıcılar da risk altındadır. \n" +
            "Bu yüzden bu inputumuzu en 2 nöron kullanarak tasarlamamız lazımdı.");
    }

    private static void Egit(double v1, double v2, Neuron neuron, double ogrenmeOrani)
    {
        double tahmin = neuron.Tahmin(v1);
        double hata = tahmin - v2;
        neuron.weight -= ogrenmeOrani * hata * v1;
        neuron.bias -= ogrenmeOrani * hata;
    }

    class Neuron
    {
        public double weight = 0.1;
        public double bias = 0.1;


        public double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }
        public double Tahmin(double x)
        {
            double z = weight * x + bias;

            return Sigmoid(z);
        }
    }

}