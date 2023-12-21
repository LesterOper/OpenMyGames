using UnityEngine;
using UnityEngine.EventSystems;

namespace Elements
{
    public class Element : MonoBehaviour
    {
        [SerializeField] private Animator _elementAnimator;
        [SerializeField] private RectTransform rect;
        [SerializeField] private SwipeElementController _swipeElementController;
        private ElementType _elementType;
        private ElementPosition _positionOnField; 
        

        public void Initialize(ElementPosition elementPosition, ElementType elementType)
        {
            _positionOnField = elementPosition;
            _elementType = elementType;
            _swipeElementController.Initialize(_positionOnField);
        }

        public void MoveElement(Transform newParent)
        {
            transform.SetParent(newParent);
        }

        public void PlayIdleAnimation()
        {
        }

        public void PlayDestroyAnimation()
        {
        }
    }

    public class ElementPosition
    {
        private int row;
        private int column;

        public int Row
        {
            get => row;
            set => row = value;
        }

        public int Column
        {
            get => column;
            set => column = value;
        }
    }

    public enum ElementType
    {
        NONE = 0,
        FIRE = 1,
        WATER = 2,
    }
}