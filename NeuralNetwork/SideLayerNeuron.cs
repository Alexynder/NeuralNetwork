using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    abstract class SideLayerNeuron:BasicNeuron,IEnumerable
    {
        public int WeightCount { get { return weights.Length; } }
        public SideLayerNeuron(int nextLayerNeuronCount)
        {
            weights = new Weight[nextLayerNeuronCount];
        }
        Weight[] weights;
        public Weight this[int index]
        {
            get { return weights[index]; }
            set { weights[index] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            return weights.GetEnumerator();
        }
    }
}
