using System;
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
            AddCriterion("Ząby");
            AddCriterion("Futerko");
        }


        private readonly ObservableCollection<Decision> _decisions = new ObservableCollection<Decision>();
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

        public void SaveDecisions(Stream output)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                //TODO: serialization - problem with ObserableCollection @tobikster
                //foreach (var decision in _decisions)
                //{
                //    formatter.Serialize(output, decision);
                //}
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
                //TODO: deserialization @tobikster
            }
            finally
            {
                input.Close();
            }
        }
    }
}
