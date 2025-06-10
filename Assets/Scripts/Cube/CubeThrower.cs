using System;
using UnityEngine;

namespace Cube
{
    public class CubeThrower : CubeHandler
    {
        [SerializeField] private float _throwForce;
        
        public event Action<CubeUnit> OnCubeThrowed;

        protected override void OnPressCanceled()
        {
            if (CubeUnit == null || !CubeUnit.IsMainCube || _inputHandler.ClickedUI) return;
            
            CubeUnit.Rigidbody.linearVelocity = Vector3.forward * _throwForce;
            
            OnCubeThrowed?.Invoke(CubeUnit);
            
            CubeUnit.CubeUnitData.SetCubeLayer(CubeUnit, CubeUnit.CubeUnitData.OnBoardCubeLayer);
            CubeUnit = null;
                        
            base.OnPressCanceled();
        }
    }
}