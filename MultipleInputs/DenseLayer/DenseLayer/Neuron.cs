using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenseLayer
{
    public class Neuron
    {
        public int InputSize;
        public int NeuronCount;

        public double[,] W; // [neuron, input]
        public double[] B;  // [neuron]

        public Neuron(int inputSize, int neuronCount)
        {
            InputSize = inputSize;
            NeuronCount = neuronCount;

            W = new double[neuronCount, inputSize];
            B = new double[neuronCount];

            // Küçük rastgele başlatma
            var rnd = new Random();
            for (int n = 0; n < neuronCount; n++)
                for (int i = 0; i < inputSize; i++)
                    W[n, i] = rnd.NextDouble() * 0.1;
        }

        public double[] Predict(double[] x)
        {
            double[] outputs = new double[NeuronCount];

            for (int n = 0; n < NeuronCount; n++)
            {
                double z = B[n];
                for (int i = 0; i < InputSize; i++)
                    z += W[n, i] * x[i];

                outputs[n] = Sigmoid(z);
            }

            return outputs;
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }
    }

}
