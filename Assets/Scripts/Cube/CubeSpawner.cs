using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cube
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubePrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CubeThrower _cubeThrower;
        
        private List<CubeUnit> _cubeUnits = new List<CubeUnit>();
        
        private CubeUnit _currentCube;
        private Coroutine _waitCubeStopped;
        
        public event Action<CubeUnit> OnCubeSpawned;

        private void OnEnable()
        {
            _cubeThrower.OnCubeThrowed += OnCubeThrowed;
        }
        
        private void Start()
        {
            _cubeUnits.Add(SpawnCube(_cubePrefab));
        }

        private void OnDisable()
        {
            _cubeThrower.OnCubeThrowed -= OnCubeThrowed;
        }
        
        private CubeUnit SpawnCube(CubeUnit cubeUnit)
        {
            var newCube = Instantiate(cubeUnit, _spawnPoint.position, Quaternion.identity, transform);
            _currentCube = newCube;
            
            _currentCube.SetMainCube(true);
            _currentCube.CubeViewer.SetCubeView();
            _currentCube.CubeUnitData.SetCubeLayer(_currentCube, _currentCube.CubeUnitData.MainCubeLayer);
            
            OnCubeSpawned?.Invoke(_currentCube);
            
            return _currentCube;
        }

        private void OnCubeThrowed(CubeUnit throwedCube)
        {
            _currentCube.SetMainCube(false);
            _currentCube = null;
            
            if (_waitCubeStopped != null) StopCoroutine(_waitCubeStopped);
            
            _waitCubeStopped = StartCoroutine(WaitCubeStopped(throwedCube));
        }

        private void ResetCube(CubeUnit cubeUnit)
        {
            cubeUnit.Rigidbody.linearVelocity = Vector3.zero;
            cubeUnit.Rigidbody.angularVelocity = Vector3.zero;
            cubeUnit.transform.position = _spawnPoint.position;
            cubeUnit.transform.rotation = Quaternion.identity;
            cubeUnit.CubeMerger.enabled = false;
        }

        private void TakeCubeFromPool()
        {
            for (int i = 0; i < _cubeUnits.Count; i++)
            {
                var cubeUnit = _cubeUnits[i];

                if (!cubeUnit.gameObject.activeSelf)
                {
                    ResetCube(cubeUnit);
                    
                    cubeUnit.gameObject.SetActive(true);
                    cubeUnit.SetMainCube(true);
                    cubeUnit.CubeViewer.SetCubeView();
                    cubeUnit.CubeUnitData.SetCubeLayer(cubeUnit, cubeUnit.CubeUnitData.MainCubeLayer);
                    
                    _currentCube = cubeUnit;
                    
                    OnCubeSpawned?.Invoke(_currentCube);

                    return;
                }
            }
            
            _cubeUnits.Add(SpawnCube(_cubePrefab));
        }

        private IEnumerator WaitCubeStopped(CubeUnit cube)
        {
            const float threshold = 0.1f;
            const float delay = 0.1f;
            const float timeout = 1f;
            
            var cubeRigidbody = cube.Rigidbody;
            var timer = 0f;

            while (cubeRigidbody != null && cubeRigidbody.linearVelocity.sqrMagnitude > threshold)
            {
                yield return new WaitForSeconds(delay);

                timer += delay;

                if (timer >= timeout) break;
            }

            cube.CubeMerger.enabled = true;
            cube.CubeUnitData.SetCubeLayer(cube, cube.CubeUnitData.OnBoardCubeLayer);

            TakeCubeFromPool();
        }

        public void SpawnBonusCube(CubeUnit bonusCube)
        {
            if (_waitCubeStopped != null) StopCoroutine(_waitCubeStopped);
            
            if (_currentCube != null)
            {
                _currentCube.gameObject.SetActive(false);
                _currentCube = null;
            }
            SpawnCube(bonusCube);
        }
    }
}
