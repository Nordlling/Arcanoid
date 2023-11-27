using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.UI.Animations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Views
{
	public class EnergyBarUIView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _energyValue;
		[SerializeField] private RectTransform _energyBarProgress;
		[SerializeField] private RectTransform _maxEnergyBarProgress;
		[SerializeField] private float _maxEnergyBarProgressValue;
		[SerializeField] private RectTransform _lightningIcon;
		[SerializeField] private RectTransform _glowIconFocus;
		
		[Header("Timers")]
		[SerializeField] private Button _timersViewButton;
		[SerializeField] private GameObject _timersView;
		[SerializeField] private TextMeshProUGUI _nextChargeValue;
		[SerializeField] private TextMeshProUGUI _fullRechargeValue;
		
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
			_timersViewButton.onClick.AddListener(OpenTimersView);
			_energyService.OnEnergyChanged += RefreshEnergyWithAnimations;
		}

		public void OnClose()
		{
			_timersViewButton.onClick.RemoveListener(OpenTimersView);
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
			_scaleAnimation.Play(_energyBarProgress, new Vector2(energyValue * _maxEnergyBarProgressValue,_energyBarProgress.sizeDelta.y));
			_rotateAnimation.Play(_lightningIcon);
		}

		public void RefreshEnergy()
		{
			RefreshEnergy(_energyService.EnergyCount);
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_timersView.SetActive(false);
			}
			
			if (_energyService.CurrentSecondsToRecharge <= 0f || !_timersView.activeSelf)
			{
				_nextChargeValue.text = "";
				_fullRechargeValue.text = "";
				return;
			}

			FormatTimer(_nextChargeValue, _energyService.CurrentSecondsToRecharge);

			int energyCountToFullRecharge = _energyService.EnergyCapacity - _energyService.EnergyCount - 1;
			float secondsToRechargeAll = energyCountToFullRecharge * _energyService.AllSecondsToRecharge + _energyService.CurrentSecondsToRecharge;
			FormatTimer(_fullRechargeValue, secondsToRechargeAll);
		}

		private void FormatTimer(TextMeshProUGUI value, float allSeconds)
		{
			int totalSeconds = (int)allSeconds;
			int minutes = totalSeconds / 60;
			int remainingSeconds = totalSeconds % 60;

			value.text = $"{minutes:D2}:{remainingSeconds:D2}";
		}

		private void RefreshEnergy(int energyCount)
		{
			_energyValue.text = $"{energyCount}/{_energyService.EnergyCapacity}";
			float energyValue = Mathf.Clamp01(energyCount / (float)_energyService.EnergyCapacity);
			_energyBarProgress.sizeDelta = new Vector2(energyValue * _maxEnergyBarProgressValue, _energyBarProgress.sizeDelta.y);
		}

		private void OpenTimersView()
		{
			if (_energyService.CurrentSecondsToRecharge <= 0f)
			{
				return;
			}
			_timersView.SetActive(true);
		}
	}
}