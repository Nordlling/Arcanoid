using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.UI.Animations;
using Main.Scripts.UI.CommonViews;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.PopUps
{
    public class NoEnergyUIView : UIView
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _closeButton;
        
        [Header("Animation")]
        [SerializeField] private Vector3 _showTargetScale;
        [SerializeField] private float _showDuration;
        [SerializeField] private float _hideDuration;

        private IEnergyService _energyService;
        private ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;

        private readonly TransformAnimations _transformAnimations = new();

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _energyService = _serviceContainer.Get<IEnergyService>();
            _comprehensiveRaycastBlocker = _serviceContainer.Get<ComprehensiveRaycastBlocker>();
        }
        
        protected override void OnOpen()
        {
            base.OnOpen();
            
            _buyButton.onClick.AddListener(BuyEnergy);
            _closeButton.onClick.AddListener(ClosePopUp);
            
            transform.localScale = Vector3.zero;
            PlayShowAnimation();
        }

        protected override void OnClose()
        {
            base.OnClose();
            _buyButton.onClick.RemoveListener(BuyEnergy);
            _closeButton.onClick.RemoveListener(ClosePopUp);
        }

        private async void PlayShowAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            await _transformAnimations.ScaleTo(transform, _showTargetScale, _showDuration);
            _comprehensiveRaycastBlocker.Disable();
        }
        
        private async Task PlayHideAnimation()
        {
            _comprehensiveRaycastBlocker.Enable();
            await _transformAnimations.ScaleTo(transform, Vector3.zero, _hideDuration);
            _comprehensiveRaycastBlocker.Disable();
        }

        private void BuyEnergy()
        {
            _energyService.RewardEnergy(_energyService.RewardForBuy);
        }

        private async void ClosePopUp()
        {
            await PlayHideAnimation();
            Close();
        }
    }
}