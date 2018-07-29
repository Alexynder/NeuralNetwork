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
            NeuralNetwork.NeuralNetwork neuralNetwork = new NeuralNetwork.NeuralNetwork(new int[] { 2, 2 }, 2, 2);
            neuralNetwork.Initialise();

            Console.WriteLine("Initialising successful!");

            Console.ReadKey();
        }
    }
}
