using System;
using Elements;
using UnityEngine;

namespace DefaultNamespace
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private Element element;
        private ElementPosition slotElementPosition;
        private ElementType _elementType;

        private void Start()
        {
            element = GetComponentInChildren<Element>();
        }

        public void Initialize(ElementPosition elementPosition, ElementType elementType)
        {
            _elementType = elementType;
            slotElementPosition = elementPosition;
            if(_elementType != ElementType.NONE)
                element.Initialize(elementPosition, elementType);
        }

        public void Initialize(ElementType elementType)
        {
            _elementType = elementType;
            if (elementType != ElementType.NONE)
            {
                if (transform.GetChild(0).TryGetComponent(out element))
                {
                    element.MoveElement(_rectTransform, null);
                    element.Initialize(slotElementPosition, _elementType);
                }
            }
            else element = null;
        }

        public void MoveElement(RectTransform rectTransform)
        {
            if(element != null) 
                element.MoveElement(rectTransform, null);
        }

        public ElementPosition SlotElementPosition => slotElementPosition;

        public ElementType ElementType => _elementType;
    }
}