using Elements;
using Elements.ElementsConfig;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelGenerator : MonoBehaviour
    {
        private Level _level;

        public void Generate()
        {
            _level = new Level();
            _level.GenerateLevel();
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

        public void GenerateLevel()
        {
            _elements = new ElementType[3][];
            _elements = LevelContainer.level;
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