using Main.Scripts.Configs;
using UnityEngine;

namespace Main.Scripts.Logic.GameGrid
{
    public class GameGridZone : MonoBehaviour
    {
        public Rect GameGridRect => _gameGridRect;

        [SerializeField] private Camera _camera;
        [SerializeField] private GridZoneConfig _gridZoneConfig;

        private readonly Vector2 _center = new Vector2(0, 0);
        private Rect _gameGridRect;
        private Rect _screenRect;
        private Resolution _currentResolution;


        private void Start()
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
            Gizmos.color = _gridZoneConfig.RectangleColor;
            Gizmos.DrawWireCube(_gameGridRect.center, _gameGridRect.size);
        }

        private void UpdateAllZones()
        {
            CalculateScreenRect();
            CalculateGameGridRect();
        }

        private void CalculateScreenRect()
        {
            Vector2 screenZoneSize = CalculateScreenSizeInWorldUnits();
            _screenRect.width = screenZoneSize.x;
            _screenRect.height = screenZoneSize.y;
            _screenRect.center = _center;
        }

        private void CalculateGameGridRect()
        {
            _gameGridRect.min = _screenRect.min + _screenRect.size * new Vector2(_gridZoneConfig.SideOffset, 0f);
            _gameGridRect.max = _screenRect.max - _screenRect.size * new Vector2(_gridZoneConfig.SideOffset, _gridZoneConfig.UpperOffset);
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