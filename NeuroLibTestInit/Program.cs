using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork;


namespace NeuroLibTestInit
{
    class Program
    {
        static void Main(string[] args)
        {
            /*basic XOR test in few updates
             * O - O \
             *   X    O
             * O - O /
             * */
            NeuralDataSet dataForLearning = new NeuralDataSet
            {
                Inputs = new double[][] { new Double[] { 0, 0 },
                                          new Double[] { 0, 1 },
                                          new Double[] { 1, 0 },
                                          new Double[] { 1, 1 }},
                ExpectedResult = new Double[][] { new double[] { 0 },
                                                  new double[] { 1 },
                                                  new double[] { 1 },
                                                  new double[] { 0 }}
            };
            NeuralNetwork.NeuralNetwork neuralNetwork = new NeuralNetwork.NeuralNetwork(new int[] { 2 }, 2, 1);
            DateTime time = DateTime.Now;
            neuralNetwork.Initialise();
            neuralNetwork.RandomiseWeights();
            double[] inp={ 1,0};
            neuralNetwork.SetInputValues(inp);
            neuralNetwork.PoolInputsToOutputSigmoid();
            TimeSpan dif = DateTime.Now - time;

            Console.WriteLine("Initialising successful!, time spent: {0}",dif.ToString());
            Console.WriteLine("Result from first run: {0}",neuralNetwork.Output[0]);
            Console.ReadKey();
        }
    }
}
