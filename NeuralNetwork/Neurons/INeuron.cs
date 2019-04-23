using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    interface INeuron
    {
        void NormalizeSigmod();
        void NormalizeHyperbola();
        double DerivedNormalizeSigmod(double x);
        double DerivedNormalizeHyperbola(double x);
    }
}
