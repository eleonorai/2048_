using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        
        private VisualElement _root;
        private Label _highScoreLabel;
        private Button _playButton;
        private Button _resetScoreButton;

        private void Awake()
        {
            _root = _uiDocument.rootVisualElement;
            _highScoreLabel = _root.Q<Label>("HighScoreLabel");
            _playButton = _root.Q<Button>("PlayButton");
            _resetScoreButton = _root.Q<Button>("ResetScoreButton");
            
            _highScoreLabel.text = $"HIGHSCORE: {GameScore.Instance.HighScoreValue}";
            
            _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
            _resetScoreButton.RegisterCallback<ClickEvent>(OnResetScoreButtonClick);
        }
        
        private void OnPlayButtonClick(ClickEvent clickEvent)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        private void OnResetScoreButtonClick(ClickEvent clickEvent)
        {
            GameScore.Instance.ResetHighScore();
            _highScoreLabel.text = $"HIGHSCORE: {GameScore.Instance.HighScoreValue}";
        }
    }
}