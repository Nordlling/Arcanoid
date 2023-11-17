using System.Threading.Tasks;
using Main.Scripts.Infrastructure;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Packs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
    public class WinUIView : UIView
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _packProgressValue;
        [SerializeField] private Image _mapImage;
        
        private void OnEnable()
        {
            _nextButton.onClick.AddListener(NextGame);
            SetMapInfo();
        }

        private void SetMapInfo()
        {
            IPackService packService = ProjectContext.Instance.ServiceContainer.Get<IPackService>();
            
            string currentLevelIndex = packService.PackProgresses[packService.WonPackIndex].CurrentLevelIndex.ToString();
            string allLevels = packService.PackInfos[packService.WonPackIndex].LevelsCount.ToString();
            
            _packProgressValue.text =$"{currentLevelIndex}/{allLevels}";
            _mapImage.sprite = packService.PackInfos[packService.WonPackIndex].MapImage;
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(NextGame);
        }

        private async void NextGame()
        {
            _gameplayStateMachine.Enter<RestartState>();
            Close();
            await Task.Yield();
            _gameplayStateMachine.Enter<PrePlayState>();
        }
    }
}