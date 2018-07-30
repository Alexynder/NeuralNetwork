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
            NeuralDataSet dataForLearning = CreateNewData(30);            
            NeuralNetwork.NeuralNetwork neuralNetwork = new NeuralNetwork.NeuralNetwork(new int[] { 12,13,14,15 }, 2, 1);
            DateTime time = DateTime.Now;
            neuralNetwork.Initialise();
            neuralNetwork.StudySpeed = 0.2;
            neuralNetwork.studyMoment = 0.05;
            neuralNetwork.RandomiseWeights();
            neuralNetwork.SetDataSet(dataForLearning);

            string log = neuralNetwork.Study(500);



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
            Console.WriteLine("input 0,0. Result:{0:0.}", neuralNetwork.Output[0]);
            neuralNetwork.SetInputValues(new double[] { 0, 1 });
            neuralNetwork.PoolInputsToOutputSigmoid();
            Console.WriteLine("input 0,1. Result:{0:0.}", neuralNetwork.Output[0]);
            neuralNetwork.SetInputValues(new double[] { 1, 0 });
            neuralNetwork.PoolInputsToOutputSigmoid();
            Console.WriteLine("input 1,0. Result:{0:0.}", neuralNetwork.Output[0]);
            neuralNetwork.SetInputValues(new double[] { 1, 1 });
            neuralNetwork.PoolInputsToOutputSigmoid();
            Console.WriteLine("input 1,1. Result:{0:0.}", neuralNetwork.Output[0]);
        }
        static NeuralDataSet CreateNewData(int repitTimes)
        {
            Double[] d1 = new double[] { 0, 0 };
            double[] do1 = new double[] { 0 };

            Double[] d2 = new double[] { 0, 1 };
            double[] do2 = new double[] { 1 };

            Double[] d3 = new double[] { 1, 0 };
            double[] do3 = new double[] { 1 };

            Double[] d4 = new double[] { 1, 1 };
            double[] do4 = new double[] { 1 };
            NeuralDataSet data = new NeuralDataSet()
            {
                Inputs = new double[repitTimes * 4][],
                ExpectedResult = new double[repitTimes*4][]
            };
            for (int i=0;i<(repitTimes);i++)
            {
                data.Inputs[i] = d1;
                data.ExpectedResult[i] = do1;
            }

            for (int i = repitTimes; i < (repitTimes*2); i++)
            {
                data.Inputs[i] = d2;
                data.ExpectedResult[i] = do2;
            }

            for (int i = repitTimes*2; i < (repitTimes * 3); i++)
            {
                data.Inputs[i] = d3;
                data.ExpectedResult[i] = do3;
            }
            for (int i = repitTimes * 3; i < (repitTimes * 4); i++)
            {
                data.Inputs[i] = d4;
                data.ExpectedResult[i] = do4;
            }
            return data;
        }
    }
}
