using UnityEngine;

namespace Cube.Merger
{
    public class LivesMerger : CubeMerger
    {
        private const int _lives = 5;
        private int _livesCounter = _lives;
        
        public override void MergeCube(CubeUnit self, CubeUnit other)
        {
            if (_livesCounter > 1)
            {
                EnableMergeCube(other, false);
                
                AddMergeValueToScore(other);
                    
                TossMergeCube(self);

                _livesCounter--;
            }
            else
            {
                EnableMergeCube(self, false);
                    
                AddMergeValueToScore(other);
                    
                other.CubeMerger.InvokeCubeMerged(other.CubeNumber * (int)Mathf.Pow(2, _lives));
                    
                TossMergeCube(other);
            }
        }
    }
}