using System;

namespace Statistical_Modelling
{
    public class LinearRegression
    {
        private double[] _xData;
        private double[] _yData;
        private double _xBar, _yBar; // sample means
        private double betaZero, betaOne; // least squares coefficients
        private int _n;

        public LinearRegression(double[] _xData, double[] _yData) 
        {
            this._xData = _xData;
            this._yData = _yData;
            this._n = _xData.Length;
            SampleMeans();
        }

        private void SampleMeans()
        {
            double ySum = 0;
            double xSum = 0;
            for (int i = 0; i < _n; i++)
            {
                xSum += _xData[i];
                ySum += _yData[i];
            }

            this._xBar = xSum / this._n;
            this._yBar = ySum / this._n;
        }

        public double[] LeastSquaresCoefficients()
        {
            double numerator = 0;
            double denominator = 0;

            for (int i = 0; i < _n; i++)
            {
                numerator += (this._xData[i] - this._xBar) * (this._yData[i] - this._yBar);
                denominator += Math.Pow((this._xData[i] - this._xBar), 2); 
            }

            this.betaOne = numerator / denominator;
            this.betaZero = this._yBar - (this.betaOne * this._xBar);

            return new double[] { this.betaOne, this.betaOne };
        }
    }
}