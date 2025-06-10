using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Handlers.Game
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private GameScore _gameScore;
        private void Awake()
        {
            _gameScore.Initialize();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}