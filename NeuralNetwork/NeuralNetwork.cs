using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        Layer[] layers;
        /// <summary>
        /// Basic initialising neural network, before initialising fully use Initialise() method
        /// </summary>
        /// <param name="hidenLayersNeuronCount">int array that contains number of neurons for each hiden layer</param>
        /// <param name="inputCount">number of input neurons</param>
        /// <param name="outputCount">number of output neurons</param>
        public NeuralNetwork(int[] hidenLayersNeuronCount,int inputCount, int outputCount)
        {
            layers = new Layer[hidenLayersNeuronCount.Length + 2]; //+input and output layer
            layers[0] = new Layer(inputCount);
            layers[layers.Length - 1] = new Layer(outputCount);
            for (int i=1; i<layers.Length-2;i++)
            {
                layers[i] = new Layer(hidenLayersNeuronCount[i - 1]);
            }
        }
        public void Initialise()
        {

        }
    }
}
