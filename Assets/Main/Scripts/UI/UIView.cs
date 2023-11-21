using System.Collections.Generic;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Main.Scripts.UI
{
    public class UIView : MonoBehaviour
    {
        protected ServiceContainer _serviceContainer;

        [SerializeField] private ScenesConfig _scenesConfig;
        
        public PanelMessage PanelMessage { get; set; }

        public void SetSceneServiceContainer(ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public void Init()
        {
            OnInitialize();
        }

        public void Open()
        {
            gameObject.SetActive(true);
            OnOpen();
        }

        public void Close()
        {
            OnClose();
            gameObject.SetActive(false);
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }

        protected List<string> GetSceneNames()
        {
            return _scenesConfig.SceneNames;
        }
    }
}