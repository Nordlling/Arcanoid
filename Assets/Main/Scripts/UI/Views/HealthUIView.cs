using System.Collections.Generic;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services.Healths;
using Main.Scripts.UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class HealthUIView : MonoBehaviour, IInitializable
    {
        [SerializeField] private ChangeImageColorAnimation _changeImageColorAnimation;
        [SerializeField] private GameObject _healthPanel;
        [SerializeField] private Image _healthImagePrefab;
        [SerializeField] private Color _fullHealthColor;
        [SerializeField] private Color _emptyHealthColor;

        private int _currentHealthCount;
        private readonly List<Image> _allHealthImages = new();
        
        private IHealthService _healthService;

        public void Construct(IHealthService healthService)
        {
            _healthService = healthService;
            Subscribe();
        }

        public void Init()
        {
            InitHealths();
        }

        private void InitHealths()
        {
            ClearAllChildren();
            for (int i = 0; i < _healthService.LeftHealths; i++)
            {
                Image healthImage = Instantiate(_healthImagePrefab, _healthPanel.transform);

                var emptyColor = healthImage.color;
                emptyColor.a = 0f;
                healthImage.color = emptyColor;
                _changeImageColorAnimation.Play(healthImage, _fullHealthColor);
                _currentHealthCount++;
                _allHealthImages.Add(healthImage);
            }
        }

        private void Subscribe()
        {
            _healthService.OnChanged += RefreshHealth;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _healthService.OnChanged -= RefreshHealth;
        }

        private void DecreaseHealth()
        {
            if (_currentHealthCount <= 0)
            {
                return;
            }
            _currentHealthCount--;
            _changeImageColorAnimation.Play(_allHealthImages[_currentHealthCount],  _emptyHealthColor);
        }

        private void IncreaseHealth()
        {
            _changeImageColorAnimation.Play(_allHealthImages[_currentHealthCount],  _fullHealthColor);
            _currentHealthCount++;
            
        }
        
        private void RefreshHealth()
        {
            _currentHealthCount = _healthService.LeftHealths;
            for (int i = 0; i < _allHealthImages.Count; i++)
            {
                Color color = i < _currentHealthCount ?  _fullHealthColor : _emptyHealthColor;
                _changeImageColorAnimation.Play(_allHealthImages[i], color);
            }
        }

        private void ClearAllChildren()
        {
            int childCount = _healthPanel.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = _healthPanel.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}