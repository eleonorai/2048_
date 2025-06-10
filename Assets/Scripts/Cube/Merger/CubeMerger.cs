using System;
using Handlers.Game;
using Interface;
using UI;
using UnityEngine;

namespace Cube.Merger
{
    public abstract class CubeMerger : MonoBehaviour, ICubeMergeHandler
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private float _minImpulseValueForMerge;
        [SerializeField] private float _tossForse;
        [SerializeField] private WinHandler _winHandler;

        public event Action<int> OnCubeMerged;
        public event Action OnCubeHitted;

        protected void TossMergeCube(CubeUnit cubeUnit)
        {
            var tossVector = new Vector3(0f, 1f, 1f);
            cubeUnit.Rigidbody.AddForce(tossVector * _tossForse, ForceMode.Impulse);
        }

        public void InvokeCubeMerged(int cubeNumber)
        {
            OnCubeMerged?.Invoke(cubeNumber);
        }

        protected void EnableMergeCube(CubeUnit cubeUnit, bool enable)
        {
            cubeUnit.gameObject.SetActive(enable);
            cubeUnit.CubeMerger.enabled = enable;
        }
        
        protected static void AddMergeValueToScore(CubeUnit cubeUnit)
        {
            var mergeValue = cubeUnit.CubeNumber / 2;
            GameScore.Instance.AddScore(mergeValue);
            WinHandler.Instance.AddScore(mergeValue);
        }

        private void OnCollisionEnter(Collision other)
        {
            var impulseValue = _cubeUnit.Rigidbody.linearVelocity.sqrMagnitude;
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit))
            {
                if (impulseValue > _minImpulseValueForMerge)
                {
                    MergeCube(_cubeUnit, cubeUnit);
                }
                
                OnCubeHitted?.Invoke();
            }
        }

        public abstract void MergeCube(CubeUnit self, CubeUnit other);
    }
}