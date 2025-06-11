using System;
using System.Collections;
using Handlers;
using UnityEngine;

namespace Cube
{
    public class CubeHandler : MonoBehaviour
    {
        [SerializeField] protected InputHandler _inputHandler;
        [SerializeField] private CubeSpawner _cubeSpawner;

        protected CubeUnit CubeUnit;
        protected Vector3 TouchPosition;

        private void OnEnable()
        {
            // Подписка на события
            _cubeSpawner.OnCubeSpawned += OnCubeSpawned;
            _inputHandler.OnPressStarted += OnPressStarted;
            _inputHandler.OnPressCanceled += OnPressCanceled;
        }

        private void OnDisable()
        {
            // Отписка от событий
            _cubeSpawner.OnCubeSpawned -= OnCubeSpawned;
            _inputHandler.OnPressStarted -= OnPressStarted;
            _inputHandler.OnPressCanceled -= OnPressCanceled;
        }

        private void OnCubeSpawned(CubeUnit newCube)
        {
            // Сохраняем ссылку на новый куб
            CubeUnit = newCube;
        }

        protected virtual void OnPressStarted()
        {
            if (CubeUnit == null) return;

            // Запускаем отложенную обработку
            StartCoroutine(DelayedPressStart());
        }

        protected virtual void OnPerformedPointer()
        {
            if (CubeUnit == null) return;

            // Получаем позицию касания
            TouchPosition = _inputHandler.GetTouchPosition(CubeUnit.transform);
        }

        protected virtual void OnPressCanceled()
        {
            // Отписываемся от события движения
            _inputHandler.OnPerformedPointer -= OnPerformedPointer;
        }

        private IEnumerator DelayedPressStart()
        {
            yield return null;

            // Проверка клика по UI
            if (_inputHandler.ClickedUI)
                yield break;

            _inputHandler.OnPerformedPointer += OnPerformedPointer;
        }
    }
}