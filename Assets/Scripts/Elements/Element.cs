using UnityEngine;
using UnityEngine.EventSystems;

namespace Elements
{
    public class Element : MonoBehaviour, IElement, IPointerDownHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private Animation _elementAnimationComponent;
        private ElementType _elementType;
        private PositionPair _positionPair;

        class PositionPair
        {
            private int _indexX;
            private int _indexY;

            public int IndexX => _indexX;

            public int IndexY => _indexY;

            public PositionPair(int indexX, int indexY)
            {
                _indexX = indexX;
                _indexY = indexY;
            }

            public void ChangePosition(int indexX, int indexY)
            {
                _indexX = indexX;
                _indexY = indexY;
            }
        }

        public void Initialize()
        {
            
        }

        public void PlayIdleAnimation()
        {
        }

        public void PlayDestroyAnimation()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }

    public enum ElementType
    {
        NONE = 0,
        FIRE = 1,
        WATER = 2,
    }
}