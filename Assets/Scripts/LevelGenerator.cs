using System;
using System.Collections.Generic;
using DefaultNamespace.Utils;
using Elements;
using Elements.ElementsConfig;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private List<SlotController> slots;
        private Level _level;

        private void OnEnable()
        {
            EventsInvoker.StartListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);
        }

        private void OnDisable()
        {
            EventsInvoker.StopListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);
        }

        public void Generate()
        {
            _level = new Level();
            slots = _level.GenerateLevel(slots);
        }

        private void Normalize()
        {
            _level.NormalizeLevel();
        }

        private void CheckSwitchBetweenElements(Dictionary<string, object> parameters)
        {
            SwipeEventsArgs swipeEventsArgs = (SwipeEventsArgs) parameters[EventsKeys.SWIPE];
            bool isCan = CanSwitch(swipeEventsArgs.ElementPosition, swipeEventsArgs.SwipeDirection);
            if (isCan)
            {
                ElementPosition targetSlot = _level.TargetElement;
                SlotController swiped = slots.Find(slot => slot.SlotElementPosition.Equals(swipeEventsArgs.ElementPosition));
                SlotController target = slots.Find(slot => slot.SlotElementPosition.Equals(targetSlot));
                SwitchElements(swiped, target); 
            }
        }

        private void SwitchElements(SlotController swipedSlotElement, SlotController targetSlot)
        {
            ElementType swipedElementType = swipedSlotElement.ElementType;
            ElementType targetElementType = targetSlot.ElementType;
            swipedSlotElement.MoveElement(targetSlot.GetComponent<RectTransform>());
            targetSlot.MoveElement(swipedSlotElement.GetComponent<RectTransform>());
            swipedSlotElement.Initialize(targetElementType);
            targetSlot.Initialize(swipedElementType);
        }
        
        private bool CanSwitch(ElementPosition elementPosition, SwipeDirection swipeDirection) => _level.CanSwitchBetweenElements(elementPosition, swipeDirection);
    }

    public class Level
    {
        private ElementType[][] _elements;
        private ElementPosition targetElement;

        public ElementPosition TargetElement => targetElement;

        public List<SlotController> GenerateLevel(List<SlotController> slots)
        {
            _elements = LevelContainer.level;
            int slotsIndex = 0;
            slots.Reverse();

            for (int i = 0; i < _elements.Length; i++)
            {
                for (int j = 0; j < _elements[0].Length; j++)
                {
                    var elementPosition = new ElementPosition
                    {
                        Row = i,
                        Column = j
                    };
                    slots[slotsIndex].Initialize(elementPosition, _elements[i][j]);
                    slotsIndex++;
                }
            }

            return slots;
        }

        public void NormalizeLevel()
        {
            
        }

        public bool CanSwitchBetweenElements(ElementPosition elementPosition, SwipeDirection swipeDirection)
        {
            int swipeIncremental =
                swipeDirection == SwipeDirection.DOWN || swipeDirection == SwipeDirection.LEFT ? -1 : 1;

            if (swipeIncremental > 0)
            {
                if (swipeDirection == SwipeDirection.UP)
                {
                    if (elementPosition.Row == 0 || _elements[elementPosition.Row - swipeIncremental][elementPosition.Column] == ElementType.NONE) return false;
                    ChangeElements(-1,0, elementPosition.Row, elementPosition.Column);
                    return true;
                }

                if (swipeDirection == SwipeDirection.RIGHT)
                {
                    if (elementPosition.Column == _elements[0].Length-1) return false;
                    ChangeElements(0,1, elementPosition.Row, elementPosition.Column);
                    return true;
                }
            }
            else
            {
                if (swipeDirection == SwipeDirection.DOWN)
                {
                    if (elementPosition.Row == _elements.Length-1) return false;
                    ChangeElements(1,0, elementPosition.Row, elementPosition.Column);
                    return true;
                }

                if (swipeDirection == SwipeDirection.LEFT)
                {
                    if (elementPosition.Column == 0) return false;
                    ChangeElements(0,-1, elementPosition.Row, elementPosition.Column);
                    return true;
                }
            }

            return false;
        }

        private void ChangeElements(int indexIncrementalX, int indexIncrementalY, int posRow, int posColumn)
        {
            var elem = _elements[posRow][posColumn];
            _elements[posRow][posColumn] =
                _elements[posRow + indexIncrementalX][posColumn + indexIncrementalY];
            _elements[posRow + indexIncrementalX][posColumn + indexIncrementalY] = elem;
            targetElement = new ElementPosition()
            {
                Row = posRow+indexIncrementalX,
                Column = posColumn + indexIncrementalY
            };
        }
    }
}