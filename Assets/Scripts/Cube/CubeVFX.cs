using System;
using System.Collections;
using UnityEngine;

namespace Cube
{
    public class CubeVFX : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private ParticleSystem _mergeVfx;
        [SerializeField] private ParticleSystem _hitVfx;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged += OnCubeMerged;
            _cubeUnit.CubeMerger.OnCubeHitted += OnCubeHitted;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged -= OnCubeMerged;
            _cubeUnit.CubeMerger.OnCubeHitted -= OnCubeHitted;
        }

        private void OnCubeMerged(int value)
        {
            PlayVFX(_mergeVfx);
        }

        private void OnCubeHitted()
        {
            PlayVFX(_hitVfx);
        }

        private void PlayVFX(ParticleSystem particleSystemPrefab)
        {
            particleSystemPrefab.transform.SetParent(null);
            StartCoroutine(WaitVFX(particleSystemPrefab, particleSystemPrefab.main.duration));
        }

        private IEnumerator WaitVFX(ParticleSystem particleSystemPrefab, float duration)
        {
            particleSystemPrefab.Play();
            yield return new WaitForSeconds(duration);
            particleSystemPrefab.transform.SetParent(transform);
            particleSystemPrefab.transform.localPosition = Vector3.zero;
        }
    }
}