using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryCrossEntropy
{
    internal class Model
    {
        public double W;
        public double B;

        public Model(double w, double b)
        {
            W = w;
            B = b;
        }

        public double Predict(double x)
        {
            double z = W * x + B;
            return 1 / (1 + Math.Exp(-z)); // Sigmoid aktivasyon fonksiyonu
        }
    }
}
