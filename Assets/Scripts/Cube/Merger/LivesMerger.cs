using UnityEngine;

namespace Cube.Merger
{
    public class LivesMerger : CubeMerger
    {
        private const int _lives = 5;
        private int _livesCounter = _lives;

        public override void MergeCube(CubeUnit self, CubeUnit other)
        {
            if (HasExtraLives())
            {
                ProcessPartialMerge(self, other);
            }
            else
            {
                ProcessFinalMerge(self, other);
            }
        }

        private bool HasExtraLives()
        {
            return _livesCounter > 1;
        }

        private void ProcessPartialMerge(CubeUnit self, CubeUnit other)
        {
            EnableMergeCube(other, false);
            AddMergeValueToScore(other);
            TossMergeCube(self);
            _livesCounter--;
        }

        private void ProcessFinalMerge(CubeUnit self, CubeUnit other)
        {
            EnableMergeCube(self, false);
            AddMergeValueToScore(other);

            int finalValue = other.CubeNumber * (int)Mathf.Pow(2, _lives);
            other.CubeMerger.InvokeCubeMerged(finalValue);

            TossMergeCube(other);
        }
    }
}