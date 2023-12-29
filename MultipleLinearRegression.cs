using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
;

namespace StatisticalModelling
{
    public class MultipleLinearRegression
    {
        Matrix<double> _xData;
        Matrix<double> _yData;

        public MultipleLinearRegression(Matrix<double> _xData, Matrix<double> _yData)
        {
            this._xData = _xData;
            this._yData = _yData;
        }

        public double[,] RegressionCoefficients()
        {
            double[,] beta = (((this._xData.Transpose() * this._xData).Inverse()) * (this._xData.Transpose() * this._yData)).ToArray();

            return beta;
        }
    }
}
