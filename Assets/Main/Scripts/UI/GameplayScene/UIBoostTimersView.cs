using System.Collections.Generic;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services.BoostTimers;
using Main.Scripts.Logic.Balls.BallSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.GameplayScene
{
    public class UIBoostTimersView : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private VerticalLayoutGroup _boostTimersGroup;
        [SerializeField] private BoostTimerElement _boostTimerElementPrefab;
        
        private IBoostTimersService _boostTimersService;
        private readonly List<BoostTimerElement> _boostTimerElements = new();

        public void Construct(IBoostTimersService boostTimersService)
        {
            _boostTimersService = boostTimersService;
        }

        public void Init()
        {
            ClearAllChildren();

            for (int i = 0; i < _boostTimersService.TimerBoosts.Count; i++)
            {
                BoostTimerElement _boostTimerElement = Instantiate(_boostTimerElementPrefab, _boostTimersGroup.transform);
                _boostTimerElement.gameObject.SetActive(false);
                _boostTimerElements.Add(_boostTimerElement);
            }
        }

        public void Tick()
        {
            if (_boostTimerElements.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < _boostTimersService.TimerBoosts.Count; i++)
            {
                ITimerBoost timerBoost = _boostTimersService.TimerBoosts[i];
                
                if (timerBoost.IsActive)
                {
                    _boostTimerElements[i].gameObject.SetActive(true);
                    _boostTimerElements[i].RefreshInfo(_boostTimersService.Icons[timerBoost.BoostId], timerBoost.BoostTime);
                    continue;
                }
                _boostTimerElements[i].gameObject.SetActive(false);
            }
        }
        
        private void ClearAllChildren()
        {
            int childCount = _boostTimersGroup.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = _boostTimersGroup.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
        
    }
}