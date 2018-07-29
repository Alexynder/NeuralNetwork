using System;
using System.Collections.Generic;
using System.Linq;
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
            NeuralNetwork.NeuralNetwork neuralNetwork = new NeuralNetwork.NeuralNetwork(new int[] { 2 }, 2, 1);
            DateTime time = DateTime.Now;
            neuralNetwork.Initialise();
            TimeSpan dif = DateTime.Now - time;

            Console.WriteLine("Initialising successful!, time spent: {0}",dif.ToString());

            Console.ReadKey();
        }
    }
}
