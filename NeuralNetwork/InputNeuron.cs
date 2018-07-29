using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class InputNeuron:SideLayerNeuron
    {
        public InputNeuron(int nextLayerNeuronCount):base(nextLayerNeuronCount)
        {
            for (int i=0;i<WeightCount;i++)
            {
                this[i] = new Weight
                {
                    Input = this
                };
            }
        }
    }
}
