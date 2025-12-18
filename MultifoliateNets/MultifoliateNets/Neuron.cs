using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemXOR
{
    public class XorNetwork
    {
        // Gizli katman
        public double W11, W12, B1;
        public double W21, W22, B2;

        // Çıkış
        public double V1, V2, B3;

        public double Predict(double x1, double x2)
        {
            double h1 = Sigmoid(W11 * x1 + W12 * x2 + B1);
            double h2 = Sigmoid(W21 * x1 + W22 * x2 + B2);

            double y = Sigmoid(V1 * h1 + V2 * h2 + B3);
            return y;
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }
    }

}
