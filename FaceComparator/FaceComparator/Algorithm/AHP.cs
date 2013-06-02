using System;
using System.Collections.Generic;
using System.Linq;

namespace FaceComparator.Algorithm
{
    class AHP
    {
        private readonly PreferenceMatrix _criteriaMatrix;
        private readonly List<PreferenceMatrix> _decisionsMatrixes;

        public AHP(PreferenceMatrix criteriaMatrix, List<PreferenceMatrix> decisionsMatrixes)
        {
            _criteriaMatrix = criteriaMatrix;
            _decisionsMatrixes = decisionsMatrixes;
        }

        public bool CalculateConsistent()
        {
            var result = _criteriaMatrix.GetConsistency();
            _decisionsMatrixes.Select(dm => result &= dm.GetConsistency());
            return result;
        }

        public double[] GetRanking()
        {
            var result = new double[_decisionsMatrixes.First().GetDecisionsCount()];

            var criterionPreferenceVector = _criteriaMatrix.GetPreferenceVector();
            var decisionsPreferenceVectors = _decisionsMatrixes.Select(dm => dm.GetPreferenceVector());

            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = 0.0;
                foreach (double[] vector in decisionsPreferenceVectors)
                {
                    result[i] += vector[i];
                }
                result[i] *= criterionPreferenceVector[i];
            }

            return result;
        }
    }
}
