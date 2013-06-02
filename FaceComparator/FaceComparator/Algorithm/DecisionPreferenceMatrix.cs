using System.Collections.ObjectModel;

namespace FaceComparator.Algorithm
{
    class DecisionPreferenceMatrix : PreferenceMatrix
    {
        public Criterion Criterion { get; set; }


        private readonly ObservableCollection<DecisionPair> _pairs = new ObservableCollection<DecisionPair>();
        public ObservableCollection<DecisionPair> Decisions
        {
            get { return _pairs; }
        }

        public DecisionPreferenceMatrix(ObservableCollection<Decision> decisions, Criterion criterion) : base(decisions.Count)
        {
            Criterion = criterion;

            for (int row = 0; row < _numDecisions; row++)
            {
                for (int column = 0; column < _numDecisions; column++)
                {
                    if (row >= column) continue;

                    _pairs.Add(
                        new DecisionPair(this, row, column)
                            {
                                First = decisions[row],
                                Second = decisions[column]
                            }
                        );
                }
            }
        }
    }

    class DecisionPair
    {
        private readonly int _firstDecNum;
        private readonly int _secondDecNum;

        private readonly DecisionPreferenceMatrix _parentMatrix;

        public DecisionPair(DecisionPreferenceMatrix parentMatrix, int firstDecNum, int secondDecNum)
        {
            _parentMatrix = parentMatrix;
            this._firstDecNum = firstDecNum;
            this._secondDecNum = secondDecNum;
        }


        public Decision First { get; set; }
        public Decision Second { get; set; }

        public double Value
        {
            get { return _parentMatrix.GetValue(_firstDecNum, _secondDecNum); }

            set
            {
                _parentMatrix.SetValue(_firstDecNum, _secondDecNum, value);
            }
        }
    }
}