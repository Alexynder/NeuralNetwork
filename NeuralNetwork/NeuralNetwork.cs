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
            //Creating neurons in input layer, creating weights for them and saving ref in weghts for this and next layer.
            for (int i=0; i<layers[0].NeuronCount;i++)
            {
                layers[0][i] = new InputNeuron(layers[1].NeuronCount);
                for (int j=0;j<((InputNeuron)layers[0][i]).WeightCount;j++)
                {
                    (layers[0][i] as InputNeuron)[j].Output = layers[1][j];
                }
            }
            //creating neurons in hiden layer and making seting ref to weight for prev layer, and creating new weight for next layer.
            for (int i=1; i<layers.Length-1;i++)
            {
                for (int j=0; j<layers[i].NeuronCount;j++)
                {
                    layers[i][j] = new HidenNeuron(layers[i-1].NeuronCount,layers[i+1].NeuronCount);
                    //input weights adjustment, input weight created in previos layer neuron constructor
                    for (int k = 0; k < ((HidenNeuron)layers[i][j]).InWeightsCount; k++)
                    {
                        if (layers[i-1][j] as InputNeuron!=null)
                        {
                            (layers[i][j] as HidenNeuron)["in", k] = (layers[i - 1][j] as InputNeuron)[k];
                            (layers[i][j] as HidenNeuron)["in", k].Output = layers[i][j];
                        }
                        else
                        {
                            (layers[i][j] as HidenNeuron)["in", k] = (layers[i - 1][j] as HidenNeuron)["out",k];
                            (layers[i][j] as HidenNeuron)["in", k].Output = layers[i][j];
                        }
                    }
                    //output weights adjustment, output weight created in HidenNeuron constructor
                    for (int k = 0; k < ((HidenNeuron)layers[i][j]).OutWeightsCount; k++)
                    {                        
                        (layers[i][j] as HidenNeuron)["out", k].Output = layers[i+1][j];
                    }
                }
            }
            // initialising last layer
            for (int i = 0; i < layers[layers.Length-1].NeuronCount; i++)
            {
                layers[layers.Length - 1][i] = new OutputNeuron(layers[layers.Length-2].NeuronCount);
                for (int j = 0; j < ((OutputNeuron)layers[layers.Length - 1][i]).WeightCount; j++)
                {
                    (layers[layers.Length - 1][i] as OutputNeuron)[j] = (layers[layers.Length - 2][i] as HidenNeuron)["out",j];
                }
            }
        }
    }
}
