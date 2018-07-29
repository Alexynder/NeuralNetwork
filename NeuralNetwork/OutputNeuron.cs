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
            Double sum = 0;
            foreach (Weight w in Weights)
            {
                sum += w.Value * w.Input.Value;
            }
            Value = sum;
        }

        public void CountValueHyperbola()
        {
            CountValue();
            NormalizeHyperbola();
        }

        public void CountValueSigmoid()
        {
            CountValue();
            NormalizeSigmod();
        }
    }
}
