using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceComparator.Algorithm;

namespace FaceComparator
{
    class Controller
    {
        private MainWindow _frame;

        public Controller(MainWindow wnd)
        {
            _frame = wnd;
        }

        private Problem _problem;

        private int ActiveCriterionIndex = 0;

        public void Start()
        {
            _problem = new Problem();

            var problemDefinitionControl = new ProblemDefinitionControl();

            problemDefinitionControl.DataContext = _problem;
            problemDefinitionControl.ContinueButton.Click += ProblemDefinitionControlOnDone;

            _frame.Content = problemDefinitionControl;
        }

        private void ProblemDefinitionControlOnDone(object sender, EventArgs eventArgs)
        {
            var criterionComparisonControl = new CriterionComparisonControl();

            criterionComparisonControl.DataContext = _problem.CriterionPreferenceMatrix;

            _frame.Content = criterionComparisonControl;

            criterionComparisonControl.ContinueButton.Click += (o, args) =>
                {
                    var decisionComparisionControl = new DecisionComparisonControl();

                    decisionComparisionControl.DataContext = _problem.DecisionPreferenceMatrices[ActiveCriterionIndex];

                    decisionComparisionControl.ContinueButton.Click += (sender1, routedEventArgs) =>
                        {
                            
                            ActiveCriterionIndex++;
                            if (ActiveCriterionIndex == _problem.Criteria.Count)
                            {
                                var AHP = new AHP(_problem.CriterionPreferenceMatrix,
                                                  _problem.DecisionPreferenceMatrices.ConvertAll<PreferenceMatrix>(x => x));
                                var ranking = AHP.GetRanking();
                                foreach (var d in ranking)
                                {
                                    Console.Out.WriteLine(d);
                                }
                            }
                            else decisionComparisionControl.DataContext = _problem.DecisionPreferenceMatrices[ActiveCriterionIndex];
                        };

                    _frame.Content = decisionComparisionControl;
                };
        }
    }
}
