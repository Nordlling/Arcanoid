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
            int milliseconds = (int)((seconds - totalSeconds) * 100);

            _timerValue.text = $"{totalSeconds:D2}.{milliseconds:D2}";
        }
        
    }
}