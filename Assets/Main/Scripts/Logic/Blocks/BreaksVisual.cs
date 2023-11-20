using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BreaksVisual : MonoBehaviour
    {
        private SpriteRenderer _breakSpriteRenderer;
        private Sprite[] _breakSprites;

        private int _breakSpriteIndex;

        public void Construct(SpriteRenderer breakSpriteRenderer, Sprite[] breakSprites)
        {
            _breakSpriteIndex = -1;
            _breakSprites = breakSprites;
            _breakSpriteRenderer = breakSpriteRenderer;
            _breakSpriteRenderer.sprite = null;
        }

        public void Refresh(int count)
        {
            _breakSpriteIndex += count;
            if (_breakSpriteIndex >= _breakSprites.Length)
            {
                _breakSpriteIndex = _breakSprites.Length - 1;
            }
            _breakSpriteRenderer.sprite = _breakSprites[_breakSpriteIndex];
        }
        
    }
}