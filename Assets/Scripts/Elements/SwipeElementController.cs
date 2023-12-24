using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Elements
{
    public class SwipeElementController : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        private bool _canSwipe = true;
        private ElementPosition _elementPosition;

        private void OnEnable()
        {
            EventsInvoker.StartListening(EventsKeys.SWIPE_BLOCK, BlockSwipe);
        }

        private void OnDisable()
        {
            EventsInvoker.StopListening(EventsKeys.SWIPE_BLOCK, BlockSwipe);
        }

        private void BlockSwipe(Dictionary<string, object> arg)
        {
            _canSwipe = (bool) arg[EventsKeys.SWIPE_BLOCK];
        }

        public void Initialize(ElementPosition elementPosition)
        {
            _elementPosition = elementPosition;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!_canSwipe) return;
            SwipeEventsArgs swipeEventsArgs;
            Vector2 delta = eventData.delta;

            if (Math.Abs(delta.x) > Math.Abs(delta.y))
            {
                if (delta.x > 0)
                {
                    swipeEventsArgs = new SwipeEventsArgs()
                        {SwipeDirection = SwipeDirection.RIGHT, ElementPosition = _elementPosition};
                    EventsInvoker.TriggerEvent(EventsKeys.SWIPE, new Dictionary<string, object>(){{EventsKeys.SWIPE, swipeEventsArgs}});
                }
                else
                {
                    swipeEventsArgs = new SwipeEventsArgs()
                        {SwipeDirection = SwipeDirection.LEFT, ElementPosition = _elementPosition};
                    EventsInvoker.TriggerEvent(EventsKeys.SWIPE, new Dictionary<string, object>(){{EventsKeys.SWIPE, swipeEventsArgs}});
                }
            }
            else
            {
                if (delta.y > 0)
                {
                    swipeEventsArgs = new SwipeEventsArgs()
                        {SwipeDirection = SwipeDirection.UP, ElementPosition = _elementPosition};
                    EventsInvoker.TriggerEvent(EventsKeys.SWIPE, new Dictionary<string, object>(){{EventsKeys.SWIPE, swipeEventsArgs}});
                }
                else
                {
                    swipeEventsArgs = new SwipeEventsArgs()
                        {SwipeDirection = SwipeDirection.DOWN, ElementPosition = _elementPosition};
                    EventsInvoker.TriggerEvent(EventsKeys.SWIPE, new Dictionary<string, object>(){{EventsKeys.SWIPE, swipeEventsArgs}});
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }

    public class SwipeEventsArgs
    {
        public ElementPosition ElementPosition;
        public SwipeDirection SwipeDirection;
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