using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubePrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CubeThrower _cubeThrower;

        private readonly List<CubeUnit> _cubeUnits = new();
        private CubeUnit _currentCube;
        private Coroutine _waitCubeStopped;

        public event Action<CubeUnit> OnCubeSpawned;

        private void OnEnable()
        {
            _cubeThrower.OnCubeThrowed += OnCubeThrowed;
        }

        private void Start()
        {
            AddNewCubeToPool();
        }

        private void OnDisable()
        {
            _cubeThrower.OnCubeThrowed -= OnCubeThrowed;
        }

        private void AddNewCubeToPool()
        {
            var cube = SpawnCube(_cubePrefab);
            _cubeUnits.Add(cube);
        }

        private CubeUnit SpawnCube(CubeUnit prefab)
        {
            var newCube = Instantiate(prefab, _spawnPoint.position, Quaternion.identity, transform);
            _currentCube = newCube;

            SetupCubeAsMain(_currentCube);

            OnCubeSpawned?.Invoke(_currentCube);
            return _currentCube;
        }

        private void SetupCubeAsMain(CubeUnit cube)
        {
            cube.SetMainCube(true);
            cube.CubeViewer.SetCubeView();
            cube.CubeUnitData.SetCubeLayer(cube, cube.CubeUnitData.MainCubeLayer);
        }

        private void OnCubeThrowed(CubeUnit thrownCube)
        {
            if (_currentCube != null)
                _currentCube.SetMainCube(false);

            _currentCube = null;

            if (_waitCubeStopped != null)
                StopCoroutine(_waitCubeStopped);

            _waitCubeStopped = StartCoroutine(WaitCubeStopped(thrownCube));
        }

        private void ResetCube(CubeUnit cube)
        {
            cube.Rigidbody.linearVelocity = Vector3.zero;
            cube.Rigidbody.angularVelocity = Vector3.zero;
            cube.transform.SetPositionAndRotation(_spawnPoint.position, Quaternion.identity);
            cube.CubeMerger.enabled = false;
        }

        private void TakeCubeFromPool()
        {
            foreach (var cube in _cubeUnits)
            {
                if (!cube.gameObject.activeSelf)
                {
                    ResetCube(cube);

                    cube.gameObject.SetActive(true);
                    SetupCubeAsMain(cube);

                    _currentCube = cube;
                    OnCubeSpawned?.Invoke(_currentCube);
                    return;
                }
            }

            AddNewCubeToPool();
        }

        private IEnumerator WaitCubeStopped(CubeUnit cube)
        {
            const float threshold = 0.1f;
            const float delay = 0.1f;
            const float timeout = 1f;

            var rb = cube.Rigidbody;
            float timer = 0f;

            while (rb != null && rb.linearVelocity.sqrMagnitude > threshold)
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
            if (_waitCubeStopped != null)
                StopCoroutine(_waitCubeStopped);

            if (_currentCube != null)
            {
                _currentCube.gameObject.SetActive(false);
                _currentCube = null;
            }

            SpawnCube(bonusCube);
        }
    }
}
