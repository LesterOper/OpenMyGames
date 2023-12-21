using Elements;
using UnityEngine;

namespace DefaultNamespace
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField] private Element element;
        private ElementPosition slotElementPosition;
        private ElementType _elementType;

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
            element.Initialize(slotElementPosition, _elementType);
        }
    }
}