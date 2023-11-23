using Main.Scripts.Configs;
using UnityEngine;

namespace Main.Scripts.Logic.Zones
{
    public class ZonesManager : MonoBehaviour
    {
        public Rect ScreenRect => _screenRect;
        public Rect GameGridRect => _gameGridRect;
        public Rect LivingRect => _livingRect;
        public Rect InputRect => _inputRect;

        [SerializeField] private Camera _camera;
        [SerializeField] private ZonesConfig _zonesConfig;

        private Rect _screenRect;
        private Rect _gameGridRect;
        private Rect _livingRect;
        private Rect _inputRect;
        
        private Resolution _currentResolution;

        public bool IsInLivingZone(Vector2 position)
        {
            return _livingRect.Contains(position);
        }
        
        public bool IsInScreenZone(Vector2 position)
        {
            return _screenRect.Contains(position);
        }
        
        public bool IsInGameGridZone(Vector2 position)
        {
            return _gameGridRect.Contains(position);
        }
        
        public bool IsInInputZone(Vector2 position)
        {
            return _inputRect.Contains(position);
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
            DrawRect(_gameGridRect, _zonesConfig.GridZone.Color);
            DrawRect(_livingRect, _zonesConfig.LivingZone.Color);
            DrawRect(_inputRect, _zonesConfig.InputZone.Color);
        }

        private void UpdateAllZones()
        {
            CalculateScreenRect();
            CalculateRect(out _gameGridRect, _zonesConfig.GridZone);
            CalculateRect(out _livingRect, _zonesConfig.LivingZone);
            CalculateRect(out _inputRect, _zonesConfig.InputZone);
        }

        private void CalculateScreenRect()
        {
            Vector2 screenZoneSize = CalculateScreenSizeInWorldUnits();
            _screenRect.width = screenZoneSize.x;
            _screenRect.height = screenZoneSize.y;
            _screenRect.center = Vector2.zero;
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

        private void CalculateRect(out Rect rect, ZoneSettings zoneSettings)
        {
            rect = default;
            rect.min = _screenRect.min + _screenRect.size * new Vector2(zoneSettings.LeftOffset, zoneSettings.BottomOffset);
            rect.max = _screenRect.max - _screenRect.size * new Vector2(zoneSettings.RightOffset, zoneSettings.UpperOffset);
        }

        private void DrawRect(Rect rect, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(rect.center, rect.size);
        }
        
    }
}