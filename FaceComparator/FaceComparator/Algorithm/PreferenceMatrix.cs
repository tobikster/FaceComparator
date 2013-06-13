using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceComparator.Algorithm
{
    class PreferenceMatrix : INotifyPropertyChanged
    {
        public readonly double[] RI = 
        {
            double.MaxValue, double.MaxValue, 0.52, 0.89, 1.11, 1.25, 1.35, 1.4, 1.45, 1.49, 1.51, 1.54, 1.56, 1.57,1.58, 
        };

        private readonly double[,] _internalMatrix;
        protected readonly int _numDecisions;

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

            IsConsistent = true;
        }

        public double GetValue(int row, int column)
        {
            return _internalMatrix[row, column];
        }

        public void SetValue(int row, int column, double value)
        {
            //if (row >= column) throw new Exception("Można ustawiać tylko wartości z górnej połowy macierzy!");

            _internalMatrix[row, column] = value;
            _internalMatrix[column, row] = 1 / value;

            IsConsistent = GetConsistency();
        }

        private double[] SumColumns()
        {
            var result = new double[_numDecisions];

            for (int row = 0; row < _numDecisions; row++)
            {
                for (int col = 0; col < _numDecisions; col++)
                {
                    result[col] += _internalMatrix[row, col];
                }
            }

            return result;
        }

        public double[] GetPreferenceVector()
        {
            var result = new double[_numDecisions];

            var sumColumns = SumColumns();

            for (int row = 0; row < _numDecisions; row++)
            {
                result[row] = 0.0;
                for (int col = 0; col < _numDecisions; col++)
                {
                    result[row] += (_internalMatrix[row, col] / sumColumns[col]);
                }
                result[row] /= _numDecisions;
            }

            return result;
        }

        public int GetDecisionsCount()
        {
            return _numDecisions;
        }

        public bool GetConsistency()
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
                var cr = ci / RI[_numDecisions - 1];
                result = cr < 0.1;
            }
            return result;
        }

        private bool _isConsistent;
        public bool IsConsistent
        {
            get
            {
                return _isConsistent;
            }
            set 
            { 
                _isConsistent = value;
                OnPropertyChanged();
            }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
