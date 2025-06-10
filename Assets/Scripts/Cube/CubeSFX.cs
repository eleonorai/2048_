using UnityEngine;

namespace Cube
{
    public class CubeSFX : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private AudioSource _mergeSfx;
        [SerializeField] private AudioSource _hitSfx;

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
            PlaySFX(_mergeSfx);
        }

        private void OnCubeHitted()
        {
            PlaySFX(_hitSfx);
        }

        private void PlaySFX(AudioSource audioSource)
        {
            audioSource.Play();
        }
    }
}