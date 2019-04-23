using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.ToolsNN
{
    public static class WeightTools
    {
        public static Layer[] RandomiseWeights(Layer[] layers)
        {
            Random rnd = new Random();
            for (int i = 0; i < layers[0].NeuronCount; i++)
            {
                foreach (Weight w in (layers[0].Neurons[i] as InputNeuron).Weights)
                {
                    w.Value = rnd.NextDouble();
                }
            }
            for (int i = 1; i < layers.Length - 1; i++)
            {
                for (int j = 0; j < layers[i].NeuronCount; j++)
                    foreach (Weight w in (layers[i].Neurons[j] as HidenNeuron).OutWeights)
                    {
                        w.Value = rnd.NextDouble();
                    }
            }
            return layers;
        }
    }
}
