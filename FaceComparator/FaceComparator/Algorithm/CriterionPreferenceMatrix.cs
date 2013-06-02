using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceComparator.Algorithm
{
    class CriterionPreferenceMatrix : PreferenceMatrix
    {
        private readonly ObservableCollection<CriterionPair> _pairs = new ObservableCollection<CriterionPair>();
        public ObservableCollection<CriterionPair> Decisions
        {
            get { return _pairs; }
        }

        public CriterionPreferenceMatrix(ObservableCollection<Criterion> criteria) : base(criteria.Count)
        {
            for (int row = 0; row < _numDecisions; row++)
            {
                for (int column = 0; column < _numDecisions; column++)
                {
                    if (row >= column) continue;

                    _pairs.Add(
                        new CriterionPair(this, row, column)
                            {
                                First = criteria[row],
                                Second = criteria[column]
                            }
                        );
                }
            }
        }
    }

    class CriterionPair
    {
        private readonly int _firstCritNum;
        private readonly int _secondCritNum;

        private readonly CriterionPreferenceMatrix _parentMatrix;

        public CriterionPair(CriterionPreferenceMatrix parentMatrix, int firstCritNum, int secondCritNum)
        {
            _parentMatrix = parentMatrix;
            this._secondCritNum = secondCritNum;
            this._firstCritNum = firstCritNum;
        }


        public Criterion First { get; set; }
        public Criterion Second { get; set; }

        public double Value
        {
            get { return _parentMatrix.GetValue(_secondCritNum, _firstCritNum); }

            set
            {
                _parentMatrix.SetValue(_secondCritNum, _firstCritNum, value);
            }
        }
    }
}
