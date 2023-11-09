using Main.Scripts.Configs;
using UnityEngine;

namespace Main.Scripts.Logic.GameGrid
{
    public class ZonesManager : MonoBehaviour
    {
        public Rect GameGridRect => _gameGridRect;

        [SerializeField] private Camera _camera;
        [SerializeField] private ZonesConfig _zonesConfig;

        private Rect _screenRect;
        private Rect _gameGridRect;
        private Rect _livingRect;
        private Resolution _currentResolution;
        
        public bool IsInLivingZone(Vector2 position)
        {
            return _livingRect.Contains(position);
        }
        
        public bool IsInScreenZone(Vector2 position)
        {
            return _screenRect.Contains(position);
        }

        public void Init()
        {
            _currentResolution = Screen.currentResolution;
            UpdateAllZones();
        }

        private void Update()
        {
            if (ResolutionIsChanged())
            {
                UpdateAllZones();
            }
        }

        private void OnValidate()
        {
            UpdateAllZones();
        }

        private bool ResolutionIsChanged()
        {
            return Screen.currentResolution.width != _currentResolution.width ||
                   Screen.currentResolution.height != _currentResolution.height;
        }

        private void OnDrawGizmos()
        {
            UpdateAllZones();
            Gizmos.color = _zonesConfig.GridZoneColor;
            Gizmos.DrawWireCube(_gameGridRect.center, _gameGridRect.size);
            Gizmos.color = _zonesConfig.LivingZoneColor;
            Gizmos.DrawWireCube(_livingRect.center, _livingRect.size);
        }

        private void UpdateAllZones()
        {
            CalculateScreenRect();
            CalculateGameGridRect();
            CalculateLivingRect();
        }

        private void CalculateScreenRect()
        {
            Vector2 screenZoneSize = CalculateScreenSizeInWorldUnits();
            _screenRect.width = screenZoneSize.x;
            _screenRect.height = screenZoneSize.y;
            _screenRect.center = Vector2.zero;
        }

        private void CalculateGameGridRect()
        {
            _gameGridRect.min = _screenRect.min + _screenRect.size * new Vector2(_zonesConfig.SideOffset, 0f);
            _gameGridRect.max = _screenRect.max - _screenRect.size * new Vector2(_zonesConfig.SideOffset, _zonesConfig.UpperOffset);
        }
        
        private void CalculateLivingRect()
        {
            Vector2 livingZoneSize = _screenRect.size + (_screenRect.size * _zonesConfig.LiveOffset);
            _livingRect.width = livingZoneSize.x;
            _livingRect.height = livingZoneSize.y;
            _livingRect.center = Vector2.zero;
        }

        private Vector2 CalculateScreenSizeInWorldUnits()
        {
            if (_camera == null)
            {
                return new Vector2(0, 0);
            }
            float screenHeightInWorldUnits = 2f * _camera.orthographicSize;
            float screenWidthInWorldUnits = screenHeightInWorldUnits * _camera.aspect;
            return new Vector2(screenWidthInWorldUnits, screenHeightInWorldUnits);
        }
        
    }
}