using System.Collections.Generic;
using Elements;

namespace DefaultNamespace
{
    public class MatchesFinder
    {
        private List<ElementPosition> _elementPositionsToDestroy;
        private List<ElementPosition> _elementPositionsToMatch;
        private ElementType current = ElementType.NONE;
        private ElementType[,] toMatch;

        public List<ElementPosition> ElementPositionsToDestroy => _elementPositionsToDestroy;
        
        public MatchesFinder()
        {
            _elementPositionsToDestroy = new List<ElementPosition>();
            _elementPositionsToMatch = new List<ElementPosition>();
        }

        private void ClearProps()
        {
            _elementPositionsToMatch.Clear();
            current = ElementType.NONE;
        }
        
        public void Match(ElementType[,] elementTypes)
        {
            ClearProps();
            CloneLevelMatrix(elementTypes);
            _elementPositionsToDestroy.Clear();
            
            int rows = toMatch.GetUpperBound(0) + 1;
            int columns = toMatch.GetUpperBound(1) + 1;
            
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    MatchAdjacentElement(i, j, true);
                    CheckResult();
                    MatchAdjacentElement(i, j, false);
                    CheckResult();
                }
            }
        }

        private void CheckResult()
        {
            if (_elementPositionsToMatch.Count >= 3)
            {
                _elementPositionsToDestroy.AddRange(_elementPositionsToMatch);
            }
            ClearProps();
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

        private void MatchAdjacentElement(int x, int y, bool checkRowOrColumn)
        {
            if (toMatch[x,y] == ElementType.NONE)
            {
                return;
            }
            
            if (current == ElementType.NONE)
            {
                current = toMatch[x,y];
                _elementPositionsToMatch.Add(new ElementPosition(x, y));
            }
            
            if (checkRowOrColumn)
            {
                if (y > 0)
                    MatchSlot(x, y - 1);
                if (y < toMatch.GetUpperBound(1))
                    MatchSlot(x, y + 1);
            }
            else
            {
                if (x > 0)
                    MatchSlot(x - 1, y);
                if (x < toMatch.GetUpperBound(0))
                    MatchSlot(x + 1, y);
            }
        }

        private void MatchSlot(int x, int y)
        {
            if (toMatch[x, y] == current)
            {
                _elementPositionsToMatch.Add(new ElementPosition(x, y));
            }
        }
    }

}