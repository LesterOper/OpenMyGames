using System.Collections.Generic;
using Elements;

namespace DefaultNamespace
{
    public class Normalizer
    {
        private List<InfoOfElementMoveAfterNormalize> _infoOfElementMoveAfterNormalizes;
        private ElementType[,] _elementTypes;

        public List<InfoOfElementMoveAfterNormalize> InfoOfElementMoveAfterNormalizes => _infoOfElementMoveAfterNormalizes;

        public Normalizer()
        {
            _infoOfElementMoveAfterNormalizes = new List<InfoOfElementMoveAfterNormalize>();
        }
        
        public ElementType[,] Normalize(ElementType[,] elementTypes)
        {
            _infoOfElementMoveAfterNormalizes.Clear();
            _elementTypes = elementTypes;
            int rows = _elementTypes.GetUpperBound(0) + 1;
            int columns = _elementTypes.GetUpperBound(1) + 1;
            for (int i = rows-1; i >= 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    if(_elementTypes[i,j] == ElementType.NONE) continue;
                    var increment = CountIncrementToMoveDownElement(rows, i, j);
                    MoveDownElementInLevelMatrix(i, j, increment);
                }
            }
            return _elementTypes;
        }
        private int CountIncrementToMoveDownElement(int rows, int currentRow, int currentColumn)
        {
            int increment = 0;
            for (int i = currentRow; i < rows; i++)
            {
                if (_elementTypes[i,currentColumn] == ElementType.NONE) increment++;
            }
            
            return increment;
        }

        private void MoveDownElementInLevelMatrix(int currentRow, int currentColumn, int increment)
        {
            ElementPosition needToMove = new ElementPosition(currentRow, currentColumn);
            ElementPosition targetPosition = new ElementPosition(currentRow + increment, currentColumn);
            var buf = _elementTypes[currentRow, currentColumn];
            _elementTypes[currentRow, currentColumn] = _elementTypes[currentRow + increment, currentColumn];
            _elementTypes[currentRow + increment, currentColumn] = buf;
            _infoOfElementMoveAfterNormalizes.Add(
                new InfoOfElementMoveAfterNormalize(needToMove, targetPosition));
        }
    }
    public class InfoOfElementMoveAfterNormalize
    {
        private ElementPosition needToMove;
        private ElementPosition targetPosition;

        public InfoOfElementMoveAfterNormalize(ElementPosition needToMove, ElementPosition targetPosition)
        {
            this.needToMove = needToMove;
            this.targetPosition = targetPosition;
        }

        public ElementPosition NeedToMove => needToMove;

        public ElementPosition TargetPosition => targetPosition;
    }
}