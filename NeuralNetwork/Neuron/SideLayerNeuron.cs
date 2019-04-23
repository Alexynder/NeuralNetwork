using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    abstract class SideLayerNeuron:BasicNeuron
    {
        public int WeightCount { get { return Weights.Length; } }
        public SideLayerNeuron(int nextLayerNeuronCount)
        {
            Weights = new Weight[nextLayerNeuronCount];
        }
        public Weight[] Weights { get; set; }

    }
}
