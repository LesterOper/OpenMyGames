using System;
using Elements;
using Elements.ElementsConfig;
using UnityEngine;

namespace DefaultNamespace
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private Element element;
        private ElementPosition slotElementPosition;
        private ElementType _elementType;

        private void Awake()
        {
            //element = GetComponentInChildren<Element>();
        }

        public void Initialize(ElementPosition elementPosition, ElementType elementType)
        {
            _elementType = elementType;
            slotElementPosition = elementPosition;
            if(_elementType != ElementType.NONE)
                element.Initialize(elementPosition, elementType);
        }
        
        public void Initialize(ElementPosition elementPosition, ElementData elementData)
        {
            _elementType = elementData.ElementType;
            slotElementPosition = elementPosition;
            if (_elementType != ElementType.NONE)
            {
                element = Instantiate(elementData.Element, transform);
                element.Initialize(elementPosition, _elementType);
            }
        }

        public void Initialize(ElementType elementType)
        {
            _elementType = elementType;
            if (elementType != ElementType.NONE)
            {
                element = transform.GetComponentInChildren<Element>();
                if (element != null)
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

        public void ClearElement() => element.PlayDestroyAnimation();

        public ElementPosition SlotElementPosition => slotElementPosition;

        public ElementType ElementType => _elementType;
    }
}