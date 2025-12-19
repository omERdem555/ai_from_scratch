using DenseLayer;
internal class Program
{
    private static void Main(string[] args)
    {
        var layer = new Neuron(inputSize: 3, neuronCount: 3);

        double[] x = { 0.8, 0.2, 0.9 };
        double[] y = { 1, 0, 1 };

        for (int epoch = 0; epoch < 1000; epoch++)
        {
            Train(layer, x, y, 0.1);
        }

        double[] result = layer.Predict(x);
    }


    static void Train(Neuron layer, double[] x, double[] y, double learningRate)
    {
        double[] yPred = layer.Predict(x);

        for (int n = 0; n < layer.NeuronCount; n++)
        {
            double error = yPred[n] - y[n];

            for (int i = 0; i < layer.InputSize; i++)
                layer.W[n, i] -= learningRate * error * x[i];

            layer.B[n] -= learningRate * error;
        }
    }

}