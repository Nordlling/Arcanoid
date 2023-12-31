using Main.Scripts.Infrastructure.Installers;
using UnityEngine;

namespace Main.Scripts.Logic.Bounds
{
    public class BoundsVisualizer : MonoBehaviour, ITickable
    {
        [SerializeField] private Bounder _bounder;
        [SerializeField] private Camera _camera;
        
        private Resolution _currentResolution;

        public void Init()
        {
            _currentResolution = Screen.currentResolution;
            UpdatePosition();
        }

        public void Tick()
        {
            if (ResolutionIsChanged())
            {
                UpdatePosition();
            }
        }

        private void OnDrawGizmos()
        {
            foreach (BoundInfo spawnInfo in _bounder.BoundInfos)
            {
                SetPosition(spawnInfo);
                _bounder.RelocateBounders();
                DrawSpawnField(spawnInfo);
            }
        }

        private bool ResolutionIsChanged()
        {
            return Screen.currentResolution.width != _currentResolution.width ||
                   Screen.currentResolution.height != _currentResolution.height;
        }

        private void UpdatePosition()
        {
            foreach (BoundInfo spawnInfo in _bounder.BoundInfos)
            {
                SetPosition(spawnInfo);
                _bounder.RelocateBounders();
            }
        }

        private void SetPosition(BoundInfo bounderInfo)
        {
            float currentScreenWidth = _camera.pixelWidth;
            float currentScreenHeight = _camera.pixelHeight;

            float centerX = currentScreenWidth * bounderInfo.CenterX;
            float centerY = currentScreenHeight * bounderInfo.CenterY;
            bounderInfo.CenterPoint = _camera.ScreenToWorldPoint(new Vector2(centerX, centerY));
            
            Vector2 worldSize = _camera.ScreenToWorldPoint(new Vector2(currentScreenWidth, currentScreenHeight)) * 2f;
            bounderInfo.Size = worldSize * new Vector2(bounderInfo.Width, bounderInfo.Height);
        }

        private void DrawSpawnField(BoundInfo bounderInfo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bounderInfo.CenterPoint, bounderInfo.Size);
        }
    }
}