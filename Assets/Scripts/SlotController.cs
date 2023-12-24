using Elements;
using Elements.ElementsConfig;
using UnityEngine;

namespace DefaultNamespace
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private ElementPosition slotElementPosition;
        private ElementType _elementType;
        private Element element;
        
        public ElementPosition SlotElementPosition => slotElementPosition;
        public ElementType ElementType => _elementType;

        public void Initialize(ElementPosition elementPosition, ElementData elementData)
        {
            _elementType = elementData.ElementType;
            slotElementPosition = elementPosition;
            if (_elementType != ElementType.NONE)
            {
                element = Instantiate(elementData.Element, transform);
                element.Initialize(elementPosition);
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
                    element.MoveElement(_rectTransform);
                    element.Initialize(slotElementPosition);
                }
            }
            else element = null;
        }

        public void MoveElement(RectTransform rectTransform)
        {
            if(element != null) 
                element.MoveElement(rectTransform);
        }
        public void ClearElement() => element.PlayDestroyAnimation();
    }
}