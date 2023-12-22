using System.Collections.Generic;
using Elements;
using UnityEngine;

namespace DefaultNamespace
{
    public class Level
    {
        private ElementType[][] _elements;
        private ElementPosition targetElement;
        private Normalizer _normalizer;

        public Normalizer Normalizer => _normalizer;

        public ElementPosition TargetElement => targetElement;

        public List<SlotController> GenerateLevel(List<SlotController> slots)
        {
            _normalizer = new Normalizer();
            _elements = LevelContainer.level;
            Print("Start");
            int slotsIndex = 0;
            slots.Reverse();

            for (int i = 0; i < _elements.Length; i++)
            {
                for (int j = 0; j < _elements[0].Length; j++)
                {
                    var elementPosition = new ElementPosition(i, j);
                    slots[slotsIndex].Initialize(elementPosition, _elements[i][j]);
                    slotsIndex++;
                }
            }

            return slots;
        }

        public List<InfoOfElementMoveAfterNormalize> NormalizeLevel()
        {
            _elements = _normalizer.Normalize(_elements);
            Print("Normalize");
            return _normalizer.InfoOfElementMoveAfterNormalizes;
        }

        private void Print(string title)
        {
            Debug.Log(title + "\n");
            string print = "";
            for (int i = 0; i < _elements.Length; i++)
            {
                for (int j = 0; j < _elements[i].Length; j++)
                {
                    print += _elements[i][j] + "    ";
                }

                Debug.Log(print + "\n");
                print = "";
            }
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
            targetElement = new ElementPosition(posRow+indexIncrementalX, posColumn + indexIncrementalY);
            Print("Swipe");
        }
    }
}