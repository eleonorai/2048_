using UnityEngine;

namespace Cube
{
    public class CubeMover : CubeHandler
    {
        [SerializeField] private float _clampX;
        protected override void OnPerformedPointer()
        {
            base.OnPerformedPointer();
            
            MoveCube();
        }

        private void MoveCube()
        {
            if (!CubeUnit.IsMainCube) return;
            
            var clampedPosition = Mathf.Clamp(TouchPosition.x, -_clampX, _clampX);
            var newCubePosition = new Vector3(clampedPosition, CubeUnit.transform.position.y, CubeUnit.transform.position.z);
            
            CubeUnit.transform.position = newCubePosition;
        }
    }
}