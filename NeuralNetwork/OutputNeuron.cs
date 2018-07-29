using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class OutputNeuron : SideLayerNeuron, INeuronCountable
    {
        public OutputNeuron(int PrevLayerNeuronCount):base(PrevLayerNeuronCount)
        {
        }
        public void CountValue()
        {
            throw new NotImplementedException();
        }
    }
}
