using UnityEngine;

namespace Main.Scripts.UI.Views
{
    public class ComprehensiveRaycastBlocker : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

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