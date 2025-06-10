using System.Collections.Generic;
using Cube;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New CubeUnit Data", menuName = "CubeUnit Data", order = 0)]
    public class CubeUnitSO : ScriptableObject
    {
        [SerializeField] private List<Color> _colors;
        [SerializeField] private List<int> _chances;
        [SerializeField] private int _mainCubeLayer;
        [SerializeField] private int _onBoardCubeLayer;
        
        public int MainCubeLayer => _mainCubeLayer;
        public int OnBoardCubeLayer => _onBoardCubeLayer;

        public int CubeNumber()
        {
            var roll = Random.Range(0, 100);
            var cumulative = 0;

            for (int i = 0; i < _chances.Count; i++)
            {
                cumulative += _chances[i];
                if (roll < cumulative) return (int)Mathf.Pow(2, i + 1);
            }
            
            return (int)Mathf.Pow(2, _chances.Count);
        }

        public Color CubeColor(int cubeNumber)
        {
            var colorIndex = (int)Mathf.Log(cubeNumber, 2) - 1;
            return _colors[colorIndex];
        }

        public void SetCubeLayer(CubeUnit cubeUnit, int layer)
        {
            if (cubeUnit == null) return;
            cubeUnit.gameObject.layer = layer;
        }
    }
}