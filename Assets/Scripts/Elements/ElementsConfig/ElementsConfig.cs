using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements.ElementsConfig
{
    [CreateAssetMenu(menuName = "App/" + nameof(ElementsConfig), fileName = nameof(ElementsConfig))]
    public class ElementsConfig : ScriptableObject
    {
        [SerializeField] private List<ElementData> elementDatas;

        public IElement GetElementGraphicByElementType(ElementType elementType) =>
            elementDatas.FirstOrDefault(elem => elem.ElementType == elementType)?.Element;
    }

    [Serializable]
    public class ElementData
    {
        [SerializeField] private ElementType elementType;
        [SerializeField] private Element element;

        public ElementType ElementType => elementType;

        public Element Element => element;
    }
}