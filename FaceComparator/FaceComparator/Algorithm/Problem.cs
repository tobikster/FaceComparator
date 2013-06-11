using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FaceComparator.Algorithm
{
    class Problem
    {
        public Problem()
        {
            AddCriterion("Oczy");
            AddCriterion("Usta");
            AddCriterion("Nos");
        }


        private ObservableCollection<Decision> _decisions = new ObservableCollection<Decision>();
        public ObservableCollection<Decision> Decisions
        {
            get { return _decisions; }
        }

        public void AddDecision(BitmapImage image)
        {
            _decisions.Add(new Decision(image));
        }

        public void RemoveDecision(Decision toRemove)
        {
            _decisions.Remove(toRemove);
        }

        public void SortDecisionsByRanking(double[] ranking)
        {
            for (int i = 0; i < ranking.Length; i++)
            {
                Decisions[i].ranking = ranking[i];
            }

            _decisions = new ObservableCollection<Decision>(Decisions.OrderByDescending(e => e.ranking));
        }


        private readonly ObservableCollection<Criterion> _criteria = new ObservableCollection<Criterion>();
        public ObservableCollection<Criterion> Criteria
        {
            get { return _criteria; }
        }

        public void AddCriterion(string name)
        {
            _criteria.Add(new Criterion { Name = name });
        }

        public void RemoveCriterion(Criterion toRemove)
        {
            _criteria.Remove(toRemove);
        }

        private CriterionPreferenceMatrix _criterionPreferenceMatrix;
        public CriterionPreferenceMatrix CriterionPreferenceMatrix
        {
            get
            {
                return _criterionPreferenceMatrix ??
                       (_criterionPreferenceMatrix = new CriterionPreferenceMatrix(_criteria));
            }
        }

        private List<DecisionPreferenceMatrix> _decisionPreferenceMatrices;
        public List<DecisionPreferenceMatrix> DecisionPreferenceMatrices
        {
            get
            {
                return _decisionPreferenceMatrices ??
                       (_decisionPreferenceMatrices = GenerateDecisionPreferenceMatrices());
            }
        }

        private List<DecisionPreferenceMatrix> GenerateDecisionPreferenceMatrices()
        {
            var result = new List<DecisionPreferenceMatrix>();
            foreach (var criterion in Criteria)
            {
                var decisionMatrix = new DecisionPreferenceMatrix(_decisions, criterion);
                result.Add(decisionMatrix);
            }
            return result;
        }

        public void SaveDecisions(Stream output)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                var decisions = (IList<Decision>)(_decisions);
                foreach (var decision in decisions)
                {
                    formatter.Serialize(output, decision);
                }
            }
            finally
            {
                output.Close();
            }
        }

        public void LoadDecisions(Stream input)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                var decisions = (IList<Decision>)(formatter.Deserialize(input));
                _decisions.Clear();
                foreach (var decision in decisions)
                {
                    _decisions.Add(decision);
                }
            }
            finally
            {
                input.Close();
            }
        }
    }
}
