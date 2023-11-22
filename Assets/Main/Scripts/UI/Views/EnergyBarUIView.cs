using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.UI.Animations;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Views
{
	public class EnergyBarUIView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _energyValue;
		[SerializeField] private RectTransform _energyBarProgress;
		[SerializeField] private RectTransform _maxEnergyBarProgress;
		[SerializeField] private RectTransform _lightningIcon;
		[SerializeField] private RectTransform _glowIconFocus;
		
		[Header("Animations")]
		[SerializeField] private RunningCounterAnimation  _runningCounterAnimation;
		[SerializeField] private ScaleAnimation  _scaleAnimation;
		[SerializeField] private RotateAnimation  _rotateAnimation;
		[SerializeField] private PulseAnimation  _pulseAnimation;

		private IEnergyService _energyService;

		public void Construct(IEnergyService energyService)
		{
			_energyService = energyService;
			RefreshEnergy();
		}
		
		public void OnOpen()
		{
			_energyService.OnEnergyChanged += RefreshEnergyWithAnimations;
		}

		public void OnClose()
		{
			_energyService.OnEnergyChanged -= RefreshEnergyWithAnimations;
		}
		
		public void Focus()
		{
			_pulseAnimation.Play(_glowIconFocus);
		}

		public void RefreshEnergyWithAnimations()
		{
			RefreshEnergy(_energyService.PreviousEnergyCount);
			
			_runningCounterAnimation.Play(_energyValue, _energyService.EnergyCount, $"/{_energyService.EnergyCapacity}");
			float energyValue = Mathf.Clamp01(_energyService.EnergyCount / (float)_energyService.EnergyCapacity);
			_scaleAnimation.Play(_energyBarProgress, new Vector2(energyValue * _maxEnergyBarProgress.sizeDelta.x,_energyBarProgress.sizeDelta.y));
			_rotateAnimation.Play(_lightningIcon);
		}

		public void RefreshEnergy()
		{
			RefreshEnergy(_energyService.EnergyCount);
		}

		private void RefreshEnergy(int energyCount)
		{
			_energyValue.text = $"{energyCount}/{_energyService.EnergyCapacity}";
			float energyValue = Mathf.Clamp01(energyCount / (float)_energyService.EnergyCapacity);
			_energyBarProgress.sizeDelta = new Vector2(energyValue * _maxEnergyBarProgress.sizeDelta.x, _energyBarProgress.sizeDelta.y);
		}
	}
}