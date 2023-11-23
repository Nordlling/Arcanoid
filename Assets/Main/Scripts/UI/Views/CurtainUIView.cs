using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Scripts.UI.Views
{
    public class CurtainUIView : MonoBehaviour
    {
        [Header("Curtain")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeInDuration;
        [SerializeField] private float _fadeOutDuration;
        
        [Header("Ball")]
        [SerializeField] private Transform _movedObject;
        [SerializeField] private float _movedObjectDuration;
        [SerializeField] private Transform[] _targets;

        private Sequence _sequence;

        public void Construct(Camera viewCamera)
        {
            _canvas.worldCamera = viewCamera;
            InitCurtain();
        }

        private void InitCurtain()
        {
            _sequence = DOTween.Sequence();

            if (_targets.Length == 0)
            {
                Debug.Log("No targets");
                return;
            }
            
            for (int i = 1; i < _targets.Length; i++)
            {
                _sequence.Append(_movedObject.transform.DOMove(_targets[i].position, _movedObjectDuration));
            }
            _sequence.Append(_movedObject.transform.DOMove(_targets[0].position, _movedObjectDuration));

            _sequence.SetLoops(-1, LoopType.Restart);
        }
        
        public async Task Enable()
        {
            _sequence.Play();
            TaskCompletionSource<bool> taskCompleted = new();
            DoFade(1f, _fadeInDuration, taskCompleted);
            await taskCompleted.Task;
        }

        public async Task Disable()
        {
            TaskCompletionSource<bool> taskCompleted = new();
            DoFade(0f, _fadeOutDuration, taskCompleted);
            await taskCompleted.Task;
            _sequence.Pause();
        }

        private void DoFade(float targetFade, float duration, TaskCompletionSource<bool> taskCompleted = null)
        {
            _canvasGroup.DOFade(targetFade, duration).OnKill(() =>
            {
                taskCompleted?.SetResult(true);
            });
        }
    }
}