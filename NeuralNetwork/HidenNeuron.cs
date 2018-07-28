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
