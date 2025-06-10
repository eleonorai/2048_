using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cube
{
    public class CubeViewer : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private List<TMP_Text> _numbersTexts;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged += SetCubeView;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged -= SetCubeView;
        }

        public void SetCubeView()
        {
            var cubeNumber = _cubeUnit.CubeUnitData.CubeNumber();
            
            _cubeUnit.SetCubeNumber(cubeNumber);

            foreach (var numberText in _numbersTexts)
            {
                numberText.text = cubeNumber.ToString();
            }
            
            var cubeColor = _cubeUnit.CubeUnitData.CubeColor(cubeNumber);
            _meshRenderer.material.color = cubeColor;
        }
        
        private void SetCubeView(int number)
        {
            var cubeNumber = number;
            
            _cubeUnit.SetCubeNumber(cubeNumber);

            foreach (var numberText in _numbersTexts)
            {
                numberText.text = cubeNumber.ToString();
            }
            
            var cubeColor = _cubeUnit.CubeUnitData.CubeColor(cubeNumber);
            _meshRenderer.material.color = cubeColor;
        }
    }
}