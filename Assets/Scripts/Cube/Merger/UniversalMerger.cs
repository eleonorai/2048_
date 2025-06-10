using UnityEngine;

namespace Cube.Merger
{
    public class UniversalMerger : CubeMerger
    {
        public override void MergeCube(CubeUnit self, CubeUnit other)
        {
            self.CubeMerger.InvokeCubeMerged(self.CubeNumber * 2);
            
            EnableMergeCube(self, false);   
            
            AddMergeValueToScore(other);
            
            TossMergeCube(other);
            
            other.CubeMerger.InvokeCubeMerged(other.CubeNumber * 2);
        }
    }
}