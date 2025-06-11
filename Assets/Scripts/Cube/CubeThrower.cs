using System;
using UnityEngine;

namespace Cube
{
    public class CubeThrower : CubeHandler
    {
        [SerializeField] private float _throwForce = 10f;

        public event Action<CubeUnit> OnCubeThrowed;

        protected override void OnPressCanceled()
        {
            if (CubeUnit == null) return;
            if (!CubeUnit.IsMainCube || _inputHandler.ClickedUI) return;

            ThrowCube(CubeUnit);
            CubeUnit = null;

            base.OnPressCanceled();
        }

        private void ThrowCube(CubeUnit cube)
        {
            cube.Rigidbody.linearVelocity = Vector3.forward * _throwForce;
            cube.CubeUnitData.SetCubeLayer(cube, cube.CubeUnitData.OnBoardCubeLayer);
            OnCubeThrowed?.Invoke(cube);
        }
    }
}