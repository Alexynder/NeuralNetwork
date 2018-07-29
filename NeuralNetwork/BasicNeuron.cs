using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    abstract class BasicNeuron : INeuron
    {
        public double Value { get; set; }

        public double DerivedNormalizeSigmod(double x)
        {
            return (Math.Pow(Math.E, -x)) / (Math.Pow((1+Math.Pow(Math.E,-x)), 2));
        }

        public double DerivedNormalizeHyperbola(double x)
        {
            double ePow2x = Math.Pow(Math.E, 2 * x);
            return -((2*(ePow2x-1)*ePow2x)/Math.Pow((ePow2x+1),2))+2*(ePow2x/(ePow2x+1));
        }

        public void NormalizeHyperbola()
        {
            Value = ((Math.Pow(Math.E, 2 * Value) - 1) / (Math.Pow(Math.E, 2 * Value) + 1));
        }

        public void NormalizeSigmod()
        {
            Value = 1 / (1+Math.Pow(Math.E,-Value));
        }
    }
}
