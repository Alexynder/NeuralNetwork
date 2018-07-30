using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Weight
    {
        // This formula need's prev itertion value, for first use it's always 0.
        private double delta=0;

        public Double Delta { get => delta; set => delta = value; }
        public double Value { get; set; }
        public BasicNeuron Input { get; set; }
        public BasicNeuron Output { get; set; }
        public void CountDelta(double studySpeed, double moment)
        {
            Delta = studySpeed* Input.Value * (Output as INeuronCountable).GetDelta+moment*Delta;
        }
        public void ChangeWeight()
        {
            Value += Delta;
        }
    }
}
