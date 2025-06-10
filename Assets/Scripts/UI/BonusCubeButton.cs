using Cube;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BonusCubeButton : UIButton
    {
        [SerializeField] private int _bonusCubeCost;
        [SerializeField] private CubeUnit _bonusCubeUnit;
        [SerializeField] private CubeSpawner _cubeSpawner;
        
        protected override void OnButtonClick()
        {
            if (_bonusCubeCost >= GameScore.Instance.MoneyValue)
            {
                _cubeSpawner.SpawnBonusCube(_bonusCubeUnit);
                // GameScore.Instance.MoneyValue -= _bonusCubeCost;
            }
            
        }
    }
}