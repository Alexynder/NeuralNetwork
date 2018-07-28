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
