using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoInputNeuron
{
    public class Neuron
    {
        public double W1;
        public double W2;
        public double B;

        public double Predict(double x1, double x2)
        {
            double z = W1 * x1 + W2 * x2 + B;
            return 1.0 / (1.0 + Math.Exp(-z));
        }
    }
}
