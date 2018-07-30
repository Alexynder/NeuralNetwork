using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class OutputNeuron : SideLayerNeuron, INeuronCountable
    {
        public double GetDelta
        {
            get
            {
                return Delta;
            }
        }
        public Double idealResult { get; set; }
        public OutputNeuron(int PrevLayerNeuronCount):base(PrevLayerNeuronCount)
        {
        }
        public Double Delta { get; set; }

        public void CountDeltaHyperbola()
        {
            Delta = (idealResult - Value) * DerivedNormalizeHyperbola(Value);
        }

        public void CountDeltaSigmoid()
        {
            Delta = (idealResult - Value) * DerivedNormalizeSigmod(Value);
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
