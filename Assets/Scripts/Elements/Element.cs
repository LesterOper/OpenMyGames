using UnityEngine;
using UnityEngine.EventSystems;

namespace Elements
{
    public class Element : MonoBehaviour, IElement
    {
        [SerializeField] private Animator _elementAnimator;
        [SerializeField] private SwipeElementController _swipeElementController;
        private ElementType _elementType;
        private ElementPosition _positionOnField; 
        

        public void Initialize()
        {
            _swipeElementController.Initialize(_positionOnField);
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