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
            neuralNetwork.SetDataSet(dataForLearning);

            TestNN(neuralNetwork);
            Console.ReadKey();
            string log = neuralNetwork.Study(100000);
            TimeSpan dif = DateTime.Now - time;

            Console.WriteLine("Initialising and studiing for 20 epoches succesful, time spent: {0}", dif.ToString());
            Console.WriteLine("Error list from first run: \n{0}", log);

            TestNN(neuralNetwork);
            Console.ReadKey();
        }

        private static void TestNN(NeuralNetwork.NeuralNetwork neuralNetwork)
        {
            neuralNetwork.SetInputValues(new double[] { 0, 0 });
            neuralNetwork.PoolInputsToOutputSigmoid();
            Console.WriteLine("input 0,0. Result:{0}", neuralNetwork.Output[0]);
            neuralNetwork.SetInputValues(new double[] { 0, 1 });
            neuralNetwork.PoolInputsToOutputSigmoid();
            Console.WriteLine("input 0,1. Result:{0}", neuralNetwork.Output[0]);
            neuralNetwork.SetInputValues(new double[] { 1, 0 });
            neuralNetwork.PoolInputsToOutputSigmoid();
            Console.WriteLine("input 1,0. Result:{0}", neuralNetwork.Output[0]);
            neuralNetwork.SetInputValues(new double[] { 1, 1 });
            neuralNetwork.PoolInputsToOutputSigmoid();
            Console.WriteLine("input 1,1. Result:{0}", neuralNetwork.Output[0]);
        }
    }
}
