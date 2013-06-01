using System;
using System.Linq;

namespace FaceComparator.Algorithm
{
    class AHP
    {
        private readonly PreferenceMatrix _criterionsMatrix;
        private readonly PreferenceMatrix[] _decisionsMatrixes;
        
        public AHP(PreferenceMatrix criterionsMatrix, PreferenceMatrix[] decisionsMatrixes)
        {
            _criterionsMatrix = criterionsMatrix;
            _decisionsMatrixes = decisionsMatrixes;
        }

        public bool CalculateCoherence()
        {
            var result = _criterionsMatrix.GetCoherence();
            _decisionsMatrixes.Select(dm => result &= dm.GetCoherence());
            return result;
        }

        public double[] GetRanking()
        {
            var result = new double[_decisionsMatrixes.First().GetDecisionsCount()];

            var criterionPreferenceVector = _criterionsMatrix.GetPreferenceVector();
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
