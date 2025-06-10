using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameScore : MonoBehaviour
    {
        public static GameScore Instance;

        [SerializeField] private int _moneyThreshold = 10;
        
        private int _scoreValue;
        private int _highScoreValue;
        private int _moneyValue;
        
        public int ScoreValue => _scoreValue;
        public int HighScoreValue => _highScoreValue;
        public int MoneyValue => _moneyValue;

        public event Action<int> OnScoreChanged; 
        public event Action<int> OnHighScoreChanged;

        public void Initialize()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
            
            _highScoreValue = PlayerPrefs.GetInt("HighScore", 0);
            OnHighScoreChanged?.Invoke(_highScoreValue);
            
            DontDestroyOnLoad(gameObject);
        }

        public void AddScore(int value)
        {
            if (value < 0) return;
            _scoreValue += value;

            if (_scoreValue >= _moneyThreshold)
            {
                _moneyValue++;
                _moneyThreshold += 10;
            }

            if (_scoreValue > _highScoreValue)
            {
                _highScoreValue = _scoreValue;
                PlayerPrefs.SetInt("HighScore", _scoreValue);
                OnHighScoreChanged?.Invoke(_highScoreValue);
            }
            
            OnScoreChanged?.Invoke(_scoreValue);
        }

        public void ResetHighScore()
        {
            PlayerPrefs.DeleteKey("HighScore");
            _highScoreValue = 0;
            OnHighScoreChanged?.Invoke(_highScoreValue);
        }
    }
}