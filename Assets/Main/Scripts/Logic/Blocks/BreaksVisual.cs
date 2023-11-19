using Main.Scripts.Infrastructure.Services.Collision;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BreaksVisual : MonoBehaviour, ICollisionInteractable
    {
        private SpriteRenderer _breakSpriteRenderer;
        private Sprite[] _breakSprites;

        private int _breakSpriteIndex;

        public void Construct(SpriteRenderer breakSpriteRenderer, Sprite[] breakSprites)
        {
            _breakSpriteIndex = 0;
            _breakSprites = breakSprites;
            _breakSpriteRenderer = breakSpriteRenderer;
            _breakSpriteRenderer.sprite = null;
        }

        public void Interact()
        {
            if (_breakSpriteIndex >= _breakSprites.Length)
            {
                return;
            }
            _breakSpriteRenderer.sprite = _breakSprites[_breakSpriteIndex];
            _breakSpriteIndex++;
        }
        
    }
}