using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Utils;
using DG.Tweening;
using EventsArgs;
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

        private void OnEnable()
        {
            //EventsInvoker.StartListening(EventsKeys.MOVE_ELEMENT, MoveElement);
        }

        private void OnDisable()
        {
            //EventsInvoker.StopListening(EventsKeys.MOVE_ELEMENT, MoveElement);
        }

        public void Initialize(ElementPosition elementPosition, ElementType elementType)
        {
            _positionOnField = elementPosition;
            _elementType = elementType;
            _swipeElementController.Initialize(_positionOnField);
            
        }

        public void MoveElement(Transform newParent, Action callback = null)
        {
            transform.SetParent(newParent);
            rect.DOAnchorPos(new Vector2(0, 0), 0.4f)
                .SetEase(Ease.InSine);
            //.OnComplete(callback.Invoke);
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

        public ElementPosition(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

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

        public bool Equals(ElementPosition obj)
        {
            return row == obj.row && column == obj.column;
        }
    }

    public enum ElementType
    {
        NONE = 0,
        FIRE = 1,
        WATER = 2,
    }
}