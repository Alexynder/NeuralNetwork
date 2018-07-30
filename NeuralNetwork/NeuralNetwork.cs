using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public double StudySpeed = 0.5;
        public Double studyMoment = 0.6;
        public double[] Output { get
            {
                double[] result = new double[layers[layers.Length-1].NeuronCount];
                for (int i = 0; i < layers[layers.Length - 1].NeuronCount; i++)
                    result[i] = layers[layers.Length - 1].Neurons[i].Value;
                return result;
            }
        }
        public NeuralDataSet DataSet { get; set; }
        Layer[] layers;
        /// <summary>
        /// Basic initialising neural network, before initialising fully use Initialise() method
        /// </summary>
        /// <param name="hidenLayersNeuronCount">int array that contains number of neurons for each hiden layer</param>
        /// <param name="inputCount">number of input neurons</param>
        /// <param name="outputCount">number of output neurons</param>
        public NeuralNetwork(int[] hidenLayersNeuronCount,int inputCount, int outputCount)
        {
            layers = new Layer[hidenLayersNeuronCount.Length + 2]; //+input and output layer
            layers[0] = new Layer(inputCount);
            layers[layers.Length - 1] = new Layer(outputCount);
            for (int i=1; i<layers.Length-1;i++)
            {
                layers[i] = new Layer(hidenLayersNeuronCount[i - 1]);
            }
        }
        public void Initialise()
        {
            //Creating neurons in input layer, creating weights for them and saving ref in weghts for this and next layer.
            for (int i=0; i<layers[0].NeuronCount;i++)
            {
                layers[0].Neurons[i] = new InputNeuron(layers[1].NeuronCount);
                for (int j=0;j<((InputNeuron)layers[0].Neurons[i]).WeightCount;j++)
                {
                    (layers[0].Neurons[i] as InputNeuron).Weights[j].Output = layers[1].Neurons[j];
                }
            }
            //creating neurons in hiden layer and seting ref to weight for prev layer, and creating new weight for next layer.
            for (int i=1; i<layers.Length-1;i++)
            {
                for (int j=0; j<layers[i].NeuronCount;j++)
                {
                    layers[i].Neurons[j] = new HidenNeuron(layers[i-1].NeuronCount,layers[i+1].NeuronCount);
                    //input weights adjustment, input weight created in previos layer neuron constructor
                    for (int k = 0; k < ((HidenNeuron)layers[i].Neurons[j]).InWeightsCount; k++)
                    {
                        if (layers[i-1].Neurons[k] as InputNeuron!=null)
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
                        (layers[i].Neurons[j] as HidenNeuron).OutWeights[k].Output = layers[i+1].Neurons[k];
                    }
                }
            }
            // initialising last layer
            for (int i = 0; i < layers[layers.Length-1].NeuronCount; i++)
            {
                layers[layers.Length - 1].Neurons[i] = new OutputNeuron(layers[layers.Length-2].NeuronCount);
                for (int j = 0; j < ((OutputNeuron)layers[layers.Length - 1].Neurons[i]).WeightCount; j++)
                {
                    (layers[layers.Length - 1].Neurons[i] as OutputNeuron).Weights[j] = 
                        (layers[layers.Length - 2].Neurons[j] as HidenNeuron).OutWeights[i];
                    (layers[layers.Length - 1].Neurons[i] as OutputNeuron).Weights[j].Output = 
                        layers[layers.Length - 1].Neurons[i];
                }
            }
        }
        public void RandomiseWeights()
        {
            Random rnd = new Random();
            for (int i=0;i<layers[0].NeuronCount;i++)
            {
                foreach (Weight w in (layers[0].Neurons[i] as InputNeuron).Weights)
                {
                    w.Value = rnd.NextDouble();
                }
            }
            for (int i=1; i<layers.Length-1;i++)
            {
                for (int j =0; j<layers[i].NeuronCount;j++)
                foreach (Weight w in (layers[i].Neurons[j] as HidenNeuron).OutWeights)
                {
                    w.Value = rnd.NextDouble();
                }
            }
        }
        public void SetInputValues(double[] input)
        {
            if (input.Length!=layers[0].NeuronCount)
            {
                throw new IndexOutOfRangeException("Input neurons count is not the same as data input count.");
            }
            else
            {
                for (int i=0;i<input.Length;i++)
                {
                    layers[0].Neurons[i].Value = input[i];
                }
            }
        }
        public void NormaliseInputSigmoid()
        {
            foreach (InputNeuron n in layers[0].Neurons)
                n.NormalizeSigmod();
        }
        public void NormaliseInputHyperbola()
        {
            foreach (InputNeuron n in layers[0].Neurons)
                n.NormalizeHyperbola();
        }
        public void PoolInputsToOutputSigmoid()
        {
            for(int i=1;i<layers.Length;i++)
            {
                foreach(INeuronCountable n in layers[i].Neurons)
                {
                    n.CountValueSigmoid();
                }
            }
        }
        public void PoolInputsToOutputHyperbola()
        {
            for (int i = 1; i < layers.Length; i++)
            {
                foreach (INeuronCountable n in layers[i].Neurons)
                {
                    n.CountValueHyperbola();
                }
            }
        }
        public double CountMSE(int iteration)
        {
            double result = 0;
            for (int i=0;i<Output.Length;i++)
            {
                result += Math.Pow(DataSet.ExpectedResult[iteration][i] - Output[i],2);
            }
            result = result / Output.Length;
            return result;
        }
        private void ChangeWeights()
        {
            for (int i = 0; i < layers[0].NeuronCount; i++)
            {
                foreach (Weight w in (layers[0].Neurons[i] as InputNeuron).Weights)
                {
                    w.CountDelta(StudySpeed, studyMoment);
                    w.ChangeWeight();
                }
            }
            for (int i = 1; i < layers.Length - 1; i++)
            {
                for (int j = 0; j < layers[i].NeuronCount; j++)
                    foreach (Weight w in (layers[i].Neurons[j] as HidenNeuron).OutWeights)
                    {
                        w.CountDelta(StudySpeed, studyMoment);
                        w.ChangeWeight();
                    }
            }
        }
        private void PushErrorBackSigmoid()
        {
            foreach (OutputNeuron n in layers[layers.Length - 1].Neurons)
            {
                n.CountDeltaSigmoid();
            }
            for (int i = 1; i < layers.Length - 1; i++)
            {
                foreach (HidenNeuron n in layers[i].Neurons)
                {
                    n.CountDeltaSigmoid();
                }
            }
            ChangeWeights();
        }
        private void PushErrorBackHyperbola()
        {
            foreach (OutputNeuron n in layers[layers.Length - 1].Neurons)
            {
                n.CountDeltaHyperbola();
            }
            for (int i = 1; i < layers.Length - 1; i++)
            {
                foreach (HidenNeuron n in layers[i].Neurons)
                {
                    n.CountDeltaHyperbola();
                }
            }
            ChangeWeights();
        }
        public void setIdealResultToOutput(double[] idealResult)
        {
            for(int i=0;i<idealResult.Length;i++)
            {
                (layers[layers.Length - 1].Neurons[i] as OutputNeuron).idealResult = idealResult[i];
            }
        }
        public  void SetDataSet(NeuralDataSet data)
        {
            this.DataSet = data;
        }
        private string Study(int epochCount,string normaliseFuncType)
        {
            string log = "Epoch; 0 0; 0 1; 1 0; 1 1; \n";
            for (int i = 0; i < epochCount; i++)
            {
                if (i > 150 && i < 152)
                {
                    Console.WriteLine();
                }
                log += string.Format("{0} ;", i);
                for (int iteration = 0; iteration < DataSet.Inputs.Length; iteration++)
                {
                    if (iteration == DataSet.Inputs.Length / 4 - 1 ||
                        iteration == DataSet.Inputs.Length / 4 * 2 - 1 ||
                        iteration == DataSet.Inputs.Length / 4 * 3 - 1 ||
                        iteration == DataSet.Inputs.Length - 1)
                    {
                        log += string.Format(" {0:0.00000};", CountMSE(iteration));
                    }
                    SetInputValues(DataSet.Inputs[iteration]);
                    setIdealResultToOutput(DataSet.ExpectedResult[iteration]);
                    //TODO: add enum for this:
                    if (normaliseFuncType.ToLower() == "sigmoid")
                    {
                        PoolInputsToOutputSigmoid();
                        PushErrorBackSigmoid();
                    }
                    else
                    {
                        PoolInputsToOutputHyperbola();
                        PushErrorBackHyperbola();
                    }
                }
                log += "\n";
            }
            return log;
        }
        public string StudyHyperbola(int epochCount)
        {
            string log = Study(epochCount, "hyperbola");
            return log;
        }
        public string StudySigmoid(int epochCount)
        {
            string log = Study(epochCount, "sigmoid");
            return log;
        }
    }
}
