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

        public void Start()
        {
            _problem = new Problem();

            var problemDefinitionControl = new ProblemDefinitionControl();

            problemDefinitionControl.DataContext = _problem;
            problemDefinitionControl.Done += ProblemDefinitionControlOnDone;

            _frame.Content = problemDefinitionControl;
        }

        private void ProblemDefinitionControlOnDone(object sender, EventArgs eventArgs)
        {
            var criterionComparisonControl = new CriterionComparisonControl();

            criterionComparisonControl.DataContext = _problem.CriterionPreferenceMatrix;

            _frame.Content = criterionComparisonControl;
        }
    }
}
