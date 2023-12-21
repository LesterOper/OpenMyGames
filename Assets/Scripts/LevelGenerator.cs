using System;
using System.Collections.Generic;
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
            
        }

        private void OnDisable()
        {
            
        }

        public void Generate()
        {
            _level = new Level();
            slots = _level.GenerateLevel(slots);
        }

        public void Normalize()
        {
            _level.NormalizeLevel();
        }

        public bool CanSwitch(ElementPosition elementPosition, SwipeDirection swipeDirection) => _level.CanSwitchBetweenElements(elementPosition, swipeDirection);
    }

    public class Level
    {
        private ElementType[][] _elements;

        public List<SlotController> GenerateLevel(List<SlotController> slots)
        {
            _elements = new ElementType[2][];
            _elements = LevelContainer.level;
            ElementPosition elementPosition = new ElementPosition();
            int slotsIndex = 0;
            slots.Reverse();

            for (int i = 0; i < _elements.Length; i++)
            {
                elementPosition.Row = i;
                for (int j = 0; j < _elements[0].Length; j++)
                {
                    elementPosition.Column = j;
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
                    if (elementPosition.Row == 0 || _elements[elementPosition.Row + swipeIncremental][elementPosition.Column] == ElementType.NONE) return false;
                    
                    var elem = _elements[elementPosition.Row][elementPosition.Column];
                    _elements[elementPosition.Row][elementPosition.Column] =
                        _elements[elementPosition.Row + swipeIncremental][elementPosition.Column];
                    _elements[elementPosition.Row + swipeIncremental][elementPosition.Column] = elem;
                    return true;
                    
                }

                if (swipeDirection == SwipeDirection.RIGHT)
                {
                    if (elementPosition.Column == _elements[0].Length-1) return false;
                    
                    var elem = _elements[elementPosition.Row][elementPosition.Column];
                    _elements[elementPosition.Row][elementPosition.Column] =
                        _elements[elementPosition.Row][elementPosition.Column + swipeIncremental];
                    _elements[elementPosition.Row][elementPosition.Column + swipeIncremental] = elem;
                    return true;
                    
                }
            }
            else
            {
                if (swipeDirection == SwipeDirection.DOWN)
                {
                    if (elementPosition.Row == _elements.Length-1) return false;
                    
                    var elem = _elements[elementPosition.Row][elementPosition.Column];
                    _elements[elementPosition.Row][elementPosition.Column] =
                        _elements[elementPosition.Row - swipeIncremental][elementPosition.Column];
                    _elements[elementPosition.Row - swipeIncremental][elementPosition.Column] = elem;
                    return true;
                    
                }

                if (swipeDirection == SwipeDirection.LEFT)
                {
                    if (elementPosition.Column == 0) return false;
                   
                    var elem = _elements[elementPosition.Row][elementPosition.Column];
                    _elements[elementPosition.Row][elementPosition.Column] =
                        _elements[elementPosition.Row][elementPosition.Column - swipeIncremental];
                    _elements[elementPosition.Row][elementPosition.Column - swipeIncremental] = elem;
                    return true;
                    
                }
            }

            return false;
        }
        


    }
}