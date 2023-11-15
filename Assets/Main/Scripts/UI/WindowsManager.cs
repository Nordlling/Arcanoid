using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class WindowsManager : MonoBehaviour, IWindowsManager
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        private ServiceContainer _serviceContainer;
        private WindowsConfig _windowsConfig;
        private readonly Dictionary<string, UIView> _createdWindowsWithKey = new();
        private readonly List<UIView> _createdWindowsList = new();

        public List<string> WindowKeys { get; private set; }

        public void Construct(WindowsConfig windowsConfig, Camera viewCamera)
        {
            _windowsConfig = windowsConfig;
            _canvas.worldCamera = viewCamera;
            WindowKeys = windowsConfig.Windows.Keys.ToList();
        }

        public void SetServiceContainer(ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public UIView GetWindow(string key)
        {
            RefreshCamera();
            if (!_createdWindowsWithKey.TryGetValue(key, out UIView window))
            {
                window = CreateWindow(key);
            }

            if (window != null)
            {
                window.gameObject.SetActive(true);
            }

            return window;
        }

        public T GetWindow<T>() where T : UIView
        {
            RefreshCamera();
            foreach (UIView view in _createdWindowsList)
            {
                if (view is T uiView)
                {
                    return uiView;
                }
            }
            
            return CreateWindow<T>();
        }

        public void EnableRaycast()
        {
            _graphicRaycaster.enabled = true;
        }

        public void DisableRaycast()
        {
            _graphicRaycaster.enabled = false;
        }

        private UIView CreateWindow(string key)
        {
            if (!_windowsConfig.Windows.TryGetValue(key, out UIView windowPrefab))
            {
               Debug.LogError($"Window panel with key {key} not found");
               return null;
            }

            UIView window = CreateWindowBasic(windowPrefab);
            _createdWindowsWithKey[key] = window;
            return window;
        }

        private void RefreshCamera()
        {
            _canvas.worldCamera.enabled = false;
            _canvas.worldCamera.enabled = true;
        }

        private T CreateWindow<T>() where T : UIView
        {
            foreach (var uiView in _windowsConfig.Windows.Values)
            {
                if (uiView as T)
                {
                    T window = (T)CreateWindowBasic(uiView);
                    _createdWindowsList.Add(window);
                    return window;
                }
            }
            
            Debug.LogError($"Window panel with type {typeof(T)} not found");
            return null;
        }

        private T CreateWindowBasic<T>(T prefab) where T : UIView
        {
            T window = Instantiate(prefab, transform);
            window.Construct(
                this, 
                _serviceContainer.Get<IGameStateMachine>(), 
                _serviceContainer.Get<IGameplayStateMachine>());
            return window;
        }
        
    }
}