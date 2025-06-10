using System;
using UnityEngine;

namespace Cube.Merger
{
    public class BombMerger : CubeMerger
    {
        [SerializeField] private float _explosionRadius;
        
        public override void MergeCube(CubeUnit self, CubeUnit other)
        {
            var cubesInOverlapSphere = Physics.OverlapSphere(transform.position, _explosionRadius);
            
            self.CubeMerger.InvokeCubeMerged(self.CubeNumber * 2);
            
            foreach (var cube in cubesInOverlapSphere)
            {
                if (cube.TryGetComponent(out CubeUnit cubeUnit))
                {
                    EnableMergeCube(cubeUnit, false);
                    AddMergeValueToScore(cubeUnit);
                }
            }
            
            EnableMergeCube(self, false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}