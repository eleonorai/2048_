using Cube.Merger;
using SO;
using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeUnit : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CubeUnitSO _cubeUnitData;
        [SerializeField] private CubeViewer _cubeViewer;
        [SerializeField] private CubeMerger _cubeMerger;

        private bool _isMainCube;
        private int _cubeNumber;
        public Rigidbody Rigidbody => _rigidbody;
        public CubeUnitSO CubeUnitData => _cubeUnitData;
        public CubeViewer CubeViewer => _cubeViewer;
        public CubeMerger CubeMerger => _cubeMerger;
        public bool IsMainCube => _isMainCube;
        public int CubeNumber => _cubeNumber;
        
        public void SetMainCube(bool isMainCube)
        {
            _isMainCube = isMainCube;
        }

        public void SetCubeNumber(int cubeNumber)
        {
            if (cubeNumber % 2 != 0) return;
            _cubeNumber = cubeNumber;
        }
    }
}