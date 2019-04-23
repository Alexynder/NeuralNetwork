using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.ToolsNN;

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
            layers = LayerBuilder.CreateNewNeuralNetwork(hidenLayersNeuronCount, inputCount, outputCount);
        }
        public void Initialise()
        {
            layers[0] = LayerBuilder.CreateInputLayer(layers[0], layers[1]);
            layers = LayerBuilder.CreateHidenLayer(layers);
            layers[layers.Length - 1] = LayerBuilder.CreateOutputLayer(layers[layers.Length - 1],
                                                                    layers[layers.Length - 2]);            
        }
        public void RandomiseWeights()
        {
            layers = WeightTools.RandomiseWeights(layers);
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
        private string Study(int epochCount,string normaliseFuncType, int logPointCount)
        {
            bool loging = false;
            //forming table header
            string log = "Epoch;";
            for (int i = 0; i < DataSet.Inputs.Count(); i++)
                log += " input"+(i + 1).ToString()+";";
            log += " \n";
            int logCounter = 1;
            if (logPointCount < epochCount)
                logCounter = epochCount / logPointCount;
            int LogIndex = 0;
            for (int i = 0; i < epochCount; i++)
            {
                if (LogIndex == i)
                {
                    log += string.Format("{0} ;", i);
                    loging = true;
                }
                for (int iteration = 0; iteration < DataSet.Inputs.Length; iteration++)
                {
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
                    //loging data from studying here
                    if (loging)
                    {
                        log += string.Format(" {0:0.00000};", CountMSE(iteration));
                    }
                }
                if (loging)
                {
                    LogIndex += logCounter;
                    loging = false;
                    log += "\n";
                }
            }
            return log;
        }
        public string StudyHyperbola(int epochCount, int logPointCount)
        {
            string log = Study(epochCount, "hyperbola", logPointCount);
            return log;
        }
        public string StudySigmoid(int epochCount, int logPointCount)
        {
            string log = Study(epochCount, "sigmoid", logPointCount);
            return log;
        }
    }
}
