using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        private void OnEnable()
        {
            GameScore.Instance.OnScoreChanged += OnScoreChanged;
            GameScore.Instance.OnHighScoreChanged += OnHighScoreChanged;
            
            ShowScores(_highScoreText, GameScore.Instance.HighScoreValue);
        }
        
        private void OnDisable()
        {
            GameScore.Instance.OnScoreChanged -= OnScoreChanged;
            GameScore.Instance.OnHighScoreChanged -= OnHighScoreChanged;
        }

        private void OnScoreChanged(int scoreValue)
        {
            ShowScores(_scoreText, scoreValue);
        }
        
        private void OnHighScoreChanged(int highScoreValue)
        {
            ShowScores(_highScoreText, highScoreValue);
        }

        private void ShowScores(TMP_Text scoreText, int value)
        {
            scoreText.text = value.ToString();
        }
    }
}