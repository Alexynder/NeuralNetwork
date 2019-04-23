using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Layer
    {
        public BasicNeuron[] Neurons { get; set; }
        public int NeuronCount { get { return Neurons.Count(); } }
        public Layer(int neuronCount)
        {
            Neurons = new BasicNeuron[neuronCount];
        }        

    }
}
