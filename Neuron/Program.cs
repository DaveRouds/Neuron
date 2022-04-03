using System;
using System.Threading;

namespace Neuron
{
    public class Neuron
    {
        public double weight = 0.5;

        public double ActualResult { get; private set; }
        public double LastError { get; private set; }
        public double Smoothing { get; set; } = 0.001;

        public double ProcessInputData(double input)
        {
            return Activate(input * weight);
        }

        public double RestoreInputData(double output)
        {
            return output / weight;
        }

        public void Train(double input, double expectedResult)
        {
            ActualResult = Activate(input * weight);
            LastError = expectedResult - ActualResult;
            var correction = (LastError / ActualResult) * Smoothing;
            weight += correction;
        } 

        public double Activate(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            double input = 100;
            double expectedValueActivation = 0.7;

            Neuron neuron = new Neuron();

            int i = 0;
            do
            {
                i++;

                neuron.Train(input, expectedValueActivation);

                //Console.WriteLine($"Iteretion: {i}\t ActualResult: {neuron.ActualResult}\t LastError: {neuron.LastError}\t Weight: {neuron.weight}\t");
                //Thread.Sleep(100);

            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);

            Console.WriteLine();
            Console.WriteLine($"Input: {input}, expected {expectedValueActivation}");
            Console.WriteLine($"Кол-во итераций: {i}, result bias: {neuron.weight}");
            Console.WriteLine($"Result Activation: {neuron.ProcessInputData(input)}");
        }
    }
}
