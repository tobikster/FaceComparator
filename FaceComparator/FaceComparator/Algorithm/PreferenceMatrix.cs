using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceComparator.Algorithm
{
    class PreferenceMatrix
    {
        public static const double[] RI = 
        {
            0, 0, 0.52, 0.89, 1.11, 1.25, 1.35, 1.4, 1.45, 1.49, 1.51, 1.54, 1.56, 1.57,1.58, 
        };

        private readonly double[,] _internalMatrix;
        private readonly int _numDecisions;

        public PreferenceMatrix(int decisions)
        {
            _internalMatrix = new double[decisions, decisions];
            _numDecisions = decisions;


            for (int i = 0; i < decisions; i++)
            {
                for (int j = 0; j < decisions; j++)
                {
                    _internalMatrix[i, j] = 1;
                }
            }
        }

        public void SetValue(int row, int column, double value)
        {
            if (row >= column) throw new Exception("Można ustawiać tylko wartości z górnej połowy macierzy!");

            _internalMatrix[row, column] = value;
            _internalMatrix[column, row] = 1 / value;
        }

        private double[] SumColumns()
        {
            var result = new double[_numDecisions];

            for (int i = 0; i < _numDecisions; i++)
            {
                for (int j = 0; j < _numDecisions; j++)
                {
                    result[i] += _internalMatrix[j, i];
                }
            }

            return result;
        }

        public double[] GetPreferenceVector()
        {
            var result = new double[_numDecisions];

            var sumColumns = SumColumns();

            for (int i = 0; i < _numDecisions; i++)
            {
                for (int j = 0; j < _numDecisions; j++)
                {
                    result[i] += (_internalMatrix[i, j] / sumColumns[j]);
                }
            }

            for (int i = 0; i < _numDecisions; i++)
            {
                result[i] /= _numDecisions;
            }

            return result;
        }

        public int GetDecisionsCount()
        {
            return _numDecisions;
        }

        public bool GetCoherence()
        {
            var result = true;
            if (_numDecisions < RI.Length)
            {
                var lambda = 0.0;
                var sumsVector = SumColumns();
                var preferenceVector = GetPreferenceVector();
                for (int i = 0; i < _numDecisions; ++i)
                {
                    lambda += sumsVector[i] * preferenceVector[i];
                }
                var ci = (lambda - _numDecisions) / (_numDecisions - 1);
                var cr = ci / RI[_numDecisions];
                result = cr < 0.1;
            }
            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _numDecisions; i++)
            {
                for (int j = 0; j < _numDecisions; j++)
                {
                    sb.Append(String.Format("{0:0.00}", _internalMatrix[i, j]) + "\t");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
