using System;
using System.Collections;
using Cube;
using UnityEngine;

namespace Handlers.Game
{
    public class GameOverArea : MonoBehaviour
    {
        [SerializeField] private float _timeToLose;
        private Coroutine _gameOverCoroutine;
        private float _timer;
        
        public event Action OnGameOver;
        public event Action<float> OnTimeToLoseChanged;
        public event Action OnTimerStarted;
        public event Action OnTimerStopped;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CubeUnit cubeUnit) && !cubeUnit.IsMainCube)
            {
                if (_gameOverCoroutine == null)
                {
                    _gameOverCoroutine = StartCoroutine(GameOverCoroutine());
                    OnTimerStarted?.Invoke();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CubeUnit cubeUnit) && !cubeUnit.IsMainCube)
            {
                if (_gameOverCoroutine != null)
                {
                    StopCoroutine(_gameOverCoroutine);
                    _gameOverCoroutine = null;
                    _timer = 0f;
                    OnTimerStopped?.Invoke();
                }
            }
        }

        private IEnumerator GameOverCoroutine()
        {
            while (_timeToLose > _timer)
            {
                OnTimeToLoseChanged?.Invoke(_timeToLose - _timer);
                yield return new WaitForSeconds(1f);
                _timer++;
            }
            
            OnTimerStopped?.Invoke();
            OnGameOver?.Invoke();
        }
    }
}
