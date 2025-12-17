using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktivasyonFonksiyonluTekNoron
{
    public class SigmoidModel
    {
        public double W; // ağırlık
        public double B; // bias

        public double Predict(double x)
        {
            double z = W * x + B;
            return 1 / (1 + Math.Exp(-z)); // Sigmoid aktivasyon fonksiyonu
        }
    }
}
