using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Elements
{
    public class SwipeElementController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
    {
        private ElementPosition _elementPosition;
        private bool _canSwipe = true;

        private void OnEnable() => EventsInvoker.StartListening(EventsKeys.SWIPE_BLOCK, BlockSwipe);

        private void OnDisable() => EventsInvoker.StopListening(EventsKeys.SWIPE_BLOCK, BlockSwipe);

        public void Initialize(ElementPosition elementPosition) => _elementPosition = elementPosition;

        public void OnPointerDown(PointerEventData eventData) { }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!_canSwipe) return;
            SwipeDirection swipeDirection;
            Vector2 delta = eventData.delta;

            if (Math.Abs(delta.x) > Math.Abs(delta.y))
            {
                swipeDirection = delta.x > 0 ? SwipeDirection.RIGHT : SwipeDirection.LEFT;
            }
            else
            {
                swipeDirection = delta.y > 0 ? SwipeDirection.UP : SwipeDirection.DOWN;
            }

            var swipeEventsArgs = new SwipeEventsArgs(_elementPosition, swipeDirection);
            EventsInvoker.TriggerEvent(EventsKeys.SWIPE, new Dictionary<string, object>(){{EventsKeys.SWIPE, swipeEventsArgs}});
        }

        public void OnDrag(PointerEventData eventData) { }
        
        private void BlockSwipe(Dictionary<string, object> arg) => _canSwipe = (bool) arg[EventsKeys.SWIPE_BLOCK];
    }

    public class SwipeEventsArgs
    {
        public ElementPosition ElementPosition;
        public SwipeDirection SwipeDirection;

        public SwipeEventsArgs(ElementPosition elementPosition, SwipeDirection swipeDirection)
        {
            ElementPosition = elementPosition;
            SwipeDirection = swipeDirection;
        }
    }
    
    public enum SwipeDirection
    {
        NONE = 0,
        LEFT = 1,
        RIGHT = 2,
        UP = 3,
        DOWN = 4,
    }
}