using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kredi_Onay_Kararı
{
    public class NeuronModel
    {
        public double[] W; // ağırlıklar
        public double B;   // bias

        public NeuronModel(int inputSize)
        {
            W = new double[inputSize];
            B = 0;
        }

        public double Predict(double[] x)
        {
            double z = B;
            for (int i = 0; i < x.Length; i++)
                z += W[i] * x[i];

            return Sigmoid(z);
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }
    }

}
