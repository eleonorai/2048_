using System;
using UnityEngine;

namespace Handlers.Game
{
    public class WinHandler : MonoBehaviour
    {
        public static WinHandler Instance { get; private set; }

        [SerializeField] private int winningPoints;
        private int _currentScore;

        public event Action OnWin;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void AddScore(int points)
        {
            _currentScore += points;
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (_currentScore >= winningPoints) OnWin?.Invoke();
        }
    }
}