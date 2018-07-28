using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Weight
    {
        public double Value { get; set; }
        public BasicNeuron Input { get; set; }
        public BasicNeuron Output { get; set; }
    }
}
