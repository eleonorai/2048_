using System;
using Cube;
using Handlers.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameOverArea _gameOverArea;
        [SerializeField] private CubeHandler _cubeHandler;
        [SerializeField] private GameObject _bonusButtonsScreen;
        
        [Header("CanvasGroup")]
        [SerializeField] private CanvasGroup _gameOverScreen;
        [SerializeField] private CanvasGroup _scoreScreen;
        [SerializeField] private CanvasGroup _timerScreen;
        
        [Header("Texts")]
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;

        private void OnGameOver()
        {
            _cubeHandler.gameObject.SetActive(false);
            _gameOverArea.gameObject.SetActive(false);
            
            EnableCanvasGroup(_gameOverScreen, 1f, true, true);
            EnableCanvasGroup(_scoreScreen, 0f, false, false);
            EnableCanvasGroup(_timerScreen, 0f, false, false);
            
            _bonusButtonsScreen.SetActive(false);
            
            _scoreText.text = $"SCORE: {GameScore.Instance.ScoreValue}";
            _highScoreText.text = $"HIGHSCORE: {GameScore.Instance.HighScoreValue}";
        }

        private void EnableCanvasGroup(CanvasGroup canvasGroup, float alpha, bool interactable, bool blockRaycasts)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = blockRaycasts;
        }
        
        private void OnTimeToLoseChanged(float time)
        {
            _timerText.text = $"Time to lose: {Mathf.CeilToInt(time)}";
        }
        
        private void OnTimerStarted()
        { 
            EnableCanvasGroup(_timerScreen, 1f, true, false);
        }

        private void OnTimerStopped()
        {
            EnableCanvasGroup(_timerScreen, 0f, false, true);
        }

        private void OnEnable()
        {
            _gameOverArea.OnGameOver += OnGameOver;
            _gameOverArea.OnTimeToLoseChanged += OnTimeToLoseChanged;
            _gameOverArea.OnTimerStarted += OnTimerStarted;
            _gameOverArea.OnTimerStopped += OnTimerStopped;
        }
        
        private void OnDisable()
        {
            _gameOverArea.OnGameOver -= OnGameOver;
            _gameOverArea.OnTimeToLoseChanged -= OnTimeToLoseChanged;
            _gameOverArea.OnTimerStarted -= OnTimerStarted;
            _gameOverArea.OnTimerStopped -= OnTimerStopped;
        }
    }
}