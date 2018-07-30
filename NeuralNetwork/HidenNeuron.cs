using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class HidenNeuron : BasicNeuron, INeuronCountable
    {
        public double GetDelta
        {   get
            {
                return Delta;
            }
        }
        public double Delta { get; set; }
        public HidenNeuron (int inWeightsCount, int outWeightsCount)
        {
            InWeights = new Weight[inWeightsCount];
            OutWeights = new Weight[outWeightsCount];
            for (int i=0;i<outWeightsCount;i++)
            {
                OutWeights[i] = new Weight() { Input = this };
            }
        }
        public int InWeightsCount { get{ return InWeights.Length; } }
        public int OutWeightsCount { get { return OutWeights.Length; } }
        public Weight[] InWeights { get; set; }
        public Weight[] OutWeights { get; set; }        

        public void CountDeltaHyperbola()
        {
            double sum = 0;
            foreach (Weight w in OutWeights)
            {
                sum += w.Value * (w.Output as INeuronCountable).GetDelta;
            }
            Delta = sum * DerivedNormalizeHyperbola(Value);
        }

        public void CountDeltaSigmoid()
        {
            double sum = 0;
            foreach (Weight w in OutWeights)
            {
                sum += w.Value * (w.Output as INeuronCountable).GetDelta;
            }
            Delta = sum * DerivedNormalizeSigmod(Value);
        }

        public void CountValue()
        {
            Double sum = 0;
            foreach (Weight w in InWeights)
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
