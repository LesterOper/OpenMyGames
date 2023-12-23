using System.Collections.Generic;
using Elements;
using Unity.VisualScripting;

namespace DefaultNamespace
{
    public class MatchesFinder
    {
        private List<ElementPosition> _elementPositionsToDestroy;

        public List<ElementPosition> ElementPositionsToDestroy => _elementPositionsToDestroy;

        private List<ElementPosition> _elementPositionsToMatch;
        private ElementType current = ElementType.NONE;
        private ElementType[,] toMatch;

        public MatchesFinder()
        {
            _elementPositionsToDestroy = new List<ElementPosition>();
            _elementPositionsToMatch = new List<ElementPosition>();
        }

        public void Match(ElementType[,] elementTypes)
        {
            _elementPositionsToDestroy.Clear();
            _elementPositionsToMatch.Clear();
            current = ElementType.NONE;
            CloneLevelMatrix(elementTypes);
            int rows = toMatch.GetUpperBound(0) + 1;
            int columns = toMatch.GetUpperBound(1) + 1;
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    MatchAdjacentElement(i, j);
                    if (_elementPositionsToMatch.Count >= 3)
                    {
                        _elementPositionsToDestroy.AddRange(_elementPositionsToMatch);
                            //_elementPositionsToMatch.Clear();
                    }
                    current = ElementType.NONE;
                    _elementPositionsToMatch.Clear();
                }
            }
        }

        private void CloneLevelMatrix(ElementType[,] level)
        {
            int rows = level.GetUpperBound(0) + 1;
            int columns = level.GetUpperBound(1) + 1;
            toMatch = new ElementType[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    toMatch[i,j] = level[i,j];
                }
            }
        }

        private void MatchAdjacentElement(int x, int y)
        {
            if (toMatch[x,y] == ElementType.NONE)
            {
                return;
            }
            
            if (current == ElementType.NONE)
            {
                current = toMatch[x,y];
                toMatch[x,y] = ElementType.NONE;
                _elementPositionsToMatch.Add(new ElementPosition(x, y));
            }
            else if (toMatch[x,y] != current) return;
            else
            {
                _elementPositionsToMatch.Add(new ElementPosition(x, y));
                toMatch[x,y] = ElementType.NONE;
            }
            
            if (x > 0)
                MatchAdjacentElement(x - 1, y);
            if (y > 0)
                MatchAdjacentElement(x, y - 1);
            if (x < toMatch.GetUpperBound(0))
                MatchAdjacentElement(x + 1, y);
            if (y < toMatch.GetUpperBound(1))
                MatchAdjacentElement(x, y + 1);
        }
    }

    public class MatchInfo
    {
        private ElementPosition elementPositionToDestroy;

        public ElementPosition ElementPositionToDestroy => elementPositionToDestroy;

        public MatchInfo(ElementPosition position)
        {
            elementPositionToDestroy = position;
        }
    }
}