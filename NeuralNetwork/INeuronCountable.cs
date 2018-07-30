using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    /// <summary>
    /// This object's must implement method for calculating value of neuron from input weights.
    /// Useful for hiden and output layers.
    /// </summary>
    interface INeuronCountable
    {
        Double GetDelta { get; }
        void CountValueSigmoid();
        void CountValueHyperbola();
        void CountDeltaSigmoid();
        void CountDeltaHyperbola();
    }
}
