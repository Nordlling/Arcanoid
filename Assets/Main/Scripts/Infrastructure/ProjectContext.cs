using System.Collections.Generic;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Main.Scripts.Infrastructure
{
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers = new();
        
        public ServiceContainer ServiceContainer { get; private set; }
        private static ProjectContext _instance;


        public static ProjectContext Instance
        {
            get
            {
                if (_instance is null)
                {
                    ProjectContext prefab = Resources.Load(nameof(ProjectContext)).GetComponent<ProjectContext>();
                    ProjectContext projectContext = Instantiate(prefab);
                    projectContext.Init();
                    DontDestroyOnLoad(projectContext);
                    _instance = projectContext;
                }

                return _instance;
            }
        }

        private void Init()
        {
            ServiceContainer = new ServiceContainer();
            foreach (var installer in _installers)
            {
                installer.InstallBindings(ServiceContainer);
            }
        }

        public ServiceContainer GetServices()
        {
            return ServiceContainer;
        }
    }
}