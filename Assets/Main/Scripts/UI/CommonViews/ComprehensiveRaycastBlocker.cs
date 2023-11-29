using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.CommonViews
{
    public class ComprehensiveRaycastBlocker : MonoBehaviour
    {
        public Image Image => _image;
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _image;

        public void Enable()
        {
            _canvas.enabled = true;
        }
        
        public void Disable()
        {
            _canvas.enabled = false;
        }
    }
}