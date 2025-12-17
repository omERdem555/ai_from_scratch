using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelNedir
{
    class LinearModel
    {
        public double W; // ağırlık
        public double B; // bias

        public double Predict(double x)
        {
            return W * x + B;
        }
    }
}
