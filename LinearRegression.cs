using System;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

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
            LeastSquaresCoefficients();
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

        public void LeastSquaresCoefficients()
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
        }

        public double StandardDeviation()
        {
            double numerator = 0;

            for (int i = 0; i < this._n; i++)
            {
                numerator += Math.Pow((this._xData[i] - this._xBar), 2);
            }

            double sd = Math.Sqrt(numerator / (this._n - 1));

            return sd;
        }

        public double ResidualSumofSquares()
        {
            double rss = 0;

            for (int i = 0; i < this._n; i++)
            {
                rss += Math.Pow((this._yData[i] - this.betaZero - (this.betaOne * this._xData[i])), 2);
            }

            return rss;
        }

        private double TotalSumOfSquares()
        {
            double tss = 0;
            for (int i = 0; i < this._n; i++)
            {
                tss += Math.Pow((this._yData[i] - this._yBar), 2);
            }

            return tss;
        }
        public double[] LSEStandardError()  // used for computing the standard error of least squares coefficients
        {
            double betaZeroSE = 0, betaOneSE = 0;
            double denominator = 0;

            // for betaOne (slope)
            for (int i = 0; i < this._n; i++)
            {
                denominator += Math.Pow((this._xData[i] - this._xBar), 2);    
            }
            betaOneSE = Math.Pow(StandardDeviation(), 2) / denominator;

            // for betaZero (y-intercept)
            double equationOne = (1 / this._n) + ((Math.Pow(this._xBar, 2)) / (denominator));
            betaZeroSE = Math.Pow(StandardDeviation(), 2) * equationOne;

            return new double[] { betaOneSE, betaZeroSE };
        }

        public double TStatistic()
        {
            double[] se = LSEStandardError();

            return this.betaOne / se[0];
        }

        public double ResidualStandardError()
        {
            return Math.Sqrt(ResidualSumofSquares() / (this._n - 2));
        }

        public double RSquared()
        {
            reuturn 1 - (ResidualSumofSquares() / TotalSumOfSquares());
        }
    }
}