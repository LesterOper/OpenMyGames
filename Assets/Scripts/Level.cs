using System.Collections.Generic;
using DefaultNamespace.Utils;
using Elements;
using Elements.ElementsConfig;
using UnityEngine;

namespace DefaultNamespace
{
    public class Level
    {
        private ElementPosition targetElementPosition;
        private MatchesFinder _matchesFinder;
        private ElementType[,] _elements;
        private Normalizer _normalizer;
        public ElementPosition TargetElementPosition => targetElementPosition;

        public List<SlotController> GenerateLevel(ElementType[,] level, Transform parent, SlotController slotControllerPrefab, ElementsConfig elementsConfig)
        {
            InitializeProps(level);
            List<SlotController> slotControllers = new List<SlotController>();
            
            int rows = _elements.GetUpperBound(0) + 1;
            int columns = _elements.GetUpperBound(1) + 1;

            for (int i = rows-1; i >=0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    var positionInLevelMatrix = new ElementPosition(i, j);
                    SlotController instantiatedSlot = Object.Instantiate(slotControllerPrefab, parent);
                    ElementData currentElement = elementsConfig.GetElementGraphicByElementType(_elements[i, j]);
                    instantiatedSlot.Initialize(positionInLevelMatrix, currentElement);
                    slotControllers.Add(instantiatedSlot);
                }
            }
            
            return slotControllers;
        }
        
        public List<ElementPosition> Match()
        {
            _matchesFinder.Match(_elements);
            ClearLevelMatrixAccordingMatchedElements(_matchesFinder.ElementPositionsToDestroy);
            return _matchesFinder.ElementPositionsToDestroy;
        }

        private void ClearLevelMatrixAccordingMatchedElements(List<ElementPosition> matched)
        {
            foreach (var elem in matched)
            {
                _elements[elem.Row, elem.Column] = ElementType.NONE;
            }
        }

        private void InitializeProps(ElementType[,] level)
        {
            _matchesFinder = new MatchesFinder();
            _normalizer = new Normalizer();
            _elements = level;
        }
        
        public List<InfoOfElementMoveAfterNormalize> NormalizeLevel()
        {
            _elements = _normalizer.Normalize(_elements);
            return _normalizer.InfoOfElementMoveAfterNormalizes;
        }

        public bool CanSwitchBetweenElements(ElementPosition elementPosition, SwipeDirection swipeDirection)
        {
            int swipeIncremental =
                swipeDirection == SwipeDirection.DOWN || swipeDirection == SwipeDirection.LEFT ? -1 : 1;

            if (swipeIncremental > 0)
            {
                if (swipeDirection == SwipeDirection.UP)
                {
                    if (elementPosition.Row == 0 || _elements[elementPosition.Row - swipeIncremental,elementPosition.Column] == ElementType.NONE) return false;
                    ChangeElements(-1,0, elementPosition.Row, elementPosition.Column);
                    return true;
                }

                if (swipeDirection == SwipeDirection.RIGHT)
                {
                    if (elementPosition.Column == _elements.GetUpperBound(1)) return false;
                    ChangeElements(0,1, elementPosition.Row, elementPosition.Column);
                    return true;
                }
            }
            else
            {
                if (swipeDirection == SwipeDirection.DOWN)
                {
                    if (elementPosition.Row == _elements.GetUpperBound(0)) return false;
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
            var elem = _elements[posRow,posColumn];
            _elements[posRow,posColumn] =
                _elements[posRow + indexIncrementalX,posColumn + indexIncrementalY];
            _elements[posRow + indexIncrementalX,posColumn + indexIncrementalY] = elem;
            targetElementPosition = new ElementPosition(posRow+indexIncrementalX, posColumn + indexIncrementalY);
        }

        public void CheckLevelProgress()
        {
            int rows = _elements.GetUpperBound(0) + 1;
            int columns = _elements.GetUpperBound(1) + 1;

            int noneElementsCount = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (_elements[i, j] == ElementType.NONE) noneElementsCount++;
                }
            }

            if (noneElementsCount >= rows * columns)
            {
                EventsInvoker.TriggerEvent(EventsKeys.LOAD_NEXT_LEVEL, null);
                return;
            }
            EventsInvoker.TriggerEvent(EventsKeys.SWIPE_BLOCK, new Dictionary<string, object>(){{EventsKeys.SWIPE_BLOCK, true}});
        }
    }
}