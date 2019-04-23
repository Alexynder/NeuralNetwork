using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ToolsNN
{
    public static class LayerBuilder
    {
        public static Layer[] CreateNewNeuralNetwork(int[] HLNeuronCount, int inCount, int outCount)
        {
            Layer[]  layers = new Layer[HLNeuronCount.Length + 2]; //+input and output layer
            layers[0] = new Layer(inCount);
            layers[layers.Length - 1] = new Layer(outCount);
            for (int i = 1; i < layers.Length - 1; i++)
            {
                layers[i] = new Layer(HLNeuronCount[i - 1]);
            }
            return layers;
        }
        public static Layer CreateInputLayer(Layer inputLayer, Layer secondLayer)
        {
            for (int i = 0; i < inputLayer.NeuronCount; i++)
            {
                inputLayer.Neurons[i] = new InputNeuron(secondLayer.NeuronCount);
                for (int j = 0; j < ((InputNeuron)inputLayer.Neurons[i]).WeightCount; j++)
                {
                    (inputLayer.Neurons[i] as InputNeuron).Weights[j].Output = secondLayer.Neurons[j];
                }
            }
            return inputLayer;
        }
        public static Layer[] CreateHidenLayer(Layer[] layers)
        {
            for (int i = 1; i < layers.Length - 1; i++)
            {
                for (int j = 0; j < layers[i].NeuronCount; j++)
                {
                    layers[i].Neurons[j] = new HidenNeuron(layers[i - 1].NeuronCount, layers[i + 1].NeuronCount);
                    //input weights adjustment, input weight created in previos layer neuron constructor
                    for (int k = 0; k < ((HidenNeuron)layers[i].Neurons[j]).InWeightsCount; k++)
                    {
                        if (layers[i - 1].Neurons[k] as InputNeuron != null)
                        {
                            (layers[i].Neurons[j] as HidenNeuron).InWeights[k] = (layers[i - 1].Neurons[k] as InputNeuron).Weights[j];
                            (layers[i].Neurons[j] as HidenNeuron).InWeights[k].Output = layers[i].Neurons[j];
                        }
                        else
                        {
                            (layers[i].Neurons[j] as HidenNeuron).InWeights[k] = (layers[i - 1].Neurons[k] as HidenNeuron).OutWeights[j];
                            (layers[i].Neurons[j] as HidenNeuron).InWeights[k].Output = layers[i].Neurons[j];
                        }
                    }
                    //output weights adjustment, output weight created in HidenNeuron constructor
                    for (int k = 0; k < ((HidenNeuron)layers[i].Neurons[j]).OutWeightsCount; k++)
                    {
                        (layers[i].Neurons[j] as HidenNeuron).OutWeights[k].Output = layers[i + 1].Neurons[k];
                    }
                }
            }
            return layers;
        }
        public static Layer CreateOutputLayer(Layer outputLayer,Layer lastHidenLayer)
        {
            for (int i = 0; i < outputLayer.NeuronCount; i++)
            {
                outputLayer.Neurons[i] = new OutputNeuron(lastHidenLayer.NeuronCount);
                for (int j = 0; j < ((OutputNeuron)outputLayer.Neurons[i]).WeightCount; j++)
                {
                    (outputLayer.Neurons[i] as OutputNeuron).Weights[j] =
                        (lastHidenLayer.Neurons[j] as HidenNeuron).OutWeights[i];
                    (outputLayer.Neurons[i] as OutputNeuron).Weights[j].Output =
                        outputLayer.Neurons[i];
                }
            }
            return outputLayer;
        }
    }
}
