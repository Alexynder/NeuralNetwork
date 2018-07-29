﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Layer:IEnumerable
    {
        private BasicNeuron[] neurons;
        public int NeuronCount { get { return neurons.Count(); } }
        public Layer(int neuronCount)
        {
            neurons = new BasicNeuron[neuronCount];
        }

        public BasicNeuron this[int index]
        {
            get { return neurons[index]; }
            set { neurons[index] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            return neurons.GetEnumerator();
        }
    }
}
