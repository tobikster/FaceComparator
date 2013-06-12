using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

                            _problem.SortDecisionsByRanking(ranking);

                            var rankingControl = new RankingControl();
                            rankingControl.DataContext = _problem;

                            rankingControl.BackButton.Click += (o1, args1) =>
                                {
                                    ActiveCriterionIndex--;
                                    decisionComparisionControl.DataContext = _problem.DecisionPreferenceMatrices[ActiveCriterionIndex];
                                    var scrollViewer = VisualTreeHelper.GetChild(decisionComparisionControl.DecisionPairsList, 0) as ScrollViewer;
                                    scrollViewer.ScrollToHome();
                                    _frame.Content = decisionComparisionControl;
                                };

                            rankingControl.RestartButton.Click += (o1, args1) =>
                                {
                                    var problemDefinitionControl = new ProblemDefinitionControl();
                                    ActiveCriterionIndex = 0;

                                    problemDefinitionControl.DataContext = _problem;
                                    problemDefinitionControl.ContinueButton.Click += ProblemDefinitionControlOnDone;

                                    _frame.Content = problemDefinitionControl;
                                };

                            _frame.Content = rankingControl;
                        }
                        else
                        {
                            decisionComparisionControl.DataContext = _problem.DecisionPreferenceMatrices[ActiveCriterionIndex];
                            var scrollViewer = VisualTreeHelper.GetChild(decisionComparisionControl.DecisionPairsList, 0) as ScrollViewer;
                            scrollViewer.ScrollToHome();
                        }
                    };

                decisionComparisionControl.BackButton.Click += (sender1, routedEventArgs) =>
                    {
                        if (ActiveCriterionIndex == 0)
                        {
                            criterionComparisonControl.DataContext = _problem.CriterionPreferenceMatrix;
                            _frame.Content = criterionComparisonControl;
                        }
                        else
                        {
                            ActiveCriterionIndex--;
                            decisionComparisionControl.DataContext = _problem.DecisionPreferenceMatrices[ActiveCriterionIndex];
                            var scrollViewer = VisualTreeHelper.GetChild(decisionComparisionControl.DecisionPairsList, 0) as ScrollViewer;
                            scrollViewer.ScrollToHome();
                        }
                    };

                _frame.Content = decisionComparisionControl;
            };
        }
    }
}
