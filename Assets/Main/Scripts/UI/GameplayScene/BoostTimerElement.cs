using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.GameplayScene
{
    public class BoostTimerElement : MonoBehaviour
    {
        [SerializeField] private Image _boostIcon;
        [SerializeField] private TextMeshProUGUI _timerValue;
 
        public void RefreshInfo(Sprite icon, float seconds)
        {
            _boostIcon.sprite = icon;
            
            int totalSeconds = (int)seconds;
            int minutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;

            _timerValue.text = $"{minutes:D2}:{remainingSeconds:D2}";
        }
        
    }
}