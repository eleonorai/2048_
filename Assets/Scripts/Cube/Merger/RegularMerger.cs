using Handlers.Game;
using UI;
using UnityEngine;

namespace Cube.Merger
{
    public class RegularMerger : CubeMerger
    {
        public override void MergeCube(CubeUnit self, CubeUnit other)
        {
            if (self.CubeNumber == other.CubeNumber)
            {
                EnableMergeCube(other, false);
                    
                AddMergeValueToScore(self);
                    
                InvokeCubeMerged(self.CubeNumber * 2);
                    
                TossMergeCube(self);
            }
        }

        private static void AddMergeValueToScore(CubeUnit cubeUnit)
        {
            var mergeValue = cubeUnit.CubeNumber / 2;
            GameScore.Instance.AddScore(mergeValue);
            WinHandler.Instance.AddScore(mergeValue);
        }
    }
}