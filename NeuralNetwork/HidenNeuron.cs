using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class HidenNeuron : BasicNeuron, INeuronCountable
    {
        private Weight[] inWeights;
        private Weight[] outWeights;
        /// <summary>
        /// Indexer to get values from 2 arrays in way like this: hiden["in",3].value...
        /// </summary>
        /// <param name="dir">string index to achieve in-array or out-array, use "in" or "out" values</param>
        /// <param name="index">basic index of array</param>
        /// <returns></returns>
        public Weight this[string dir, int index]
        {
            get
            {
                if (dir.Equals("in"))
                    return inWeights[index];
                else if (dir.Equals("out"))
                    return outWeights[index];
                else throw new IndexOutOfRangeException("Wrong array pointer, use \"in\" or \"out\" as first index.");
            }
            set
            {
                if (dir.Equals("in"))
                    inWeights[index] = value;
                else if (dir.Equals("out"))
                    outWeights[index] = value;
                else throw new IndexOutOfRangeException("Wrong array pointer, use \"in\" or \"out\" as first index.");
            }
        }
        public void CountValue()
        {
            throw new NotImplementedException();
        }
    }
}
