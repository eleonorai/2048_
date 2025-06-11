using Cube;
using Handlers.Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WinScreen : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private CubeHandler _cubeHandler;
        [SerializeField] private WinHandler _winHandler;
        [SerializeField] private GameOverArea _gameOverArea;
        [SerializeField] private GameObject _bonusButtonsScreen;
        
        [Header("CanvasGroup")]
        [SerializeField] private CanvasGroup _winScreen;
        [SerializeField] private CanvasGroup _scoreScreen;
        
        [Header("Texts")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;

        private void OnWin()
        {
            _cubeHandler.gameObject.SetActive(false);
            _gameOverArea.gameObject.SetActive(false);
            
            EnableCanvasGroup(_scoreScreen, 0f, false, false);
            EnableCanvasGroup(_winScreen, 1f, true, true);
            
            _bonusButtonsScreen.SetActive(false);

            _scoreText.text = $"Your final score: {GameScore.Instance.ScoreValue}";
            _highScoreText.text = $"All-time best: {GameScore.Instance.HighScoreValue}";
        }

        private void EnableCanvasGroup(CanvasGroup canvasGroup, float alpha, bool interactable, bool blockRaycasts)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = interactable;
        }

        private void OnEnable()
        {
            WinHandler.Instance.OnWin += OnWin;
        }

        private void OnDisable()
        {
            WinHandler.Instance.OnWin -= OnWin;
        }
    }
}