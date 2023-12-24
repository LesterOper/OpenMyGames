using System.Collections.Generic;
using Elements;

namespace DefaultNamespace
{
    public class Normalizer
    {
        private List<InfoOfElementMoveAfterNormalize> _infoOfElementMoveAfterNormalizes;

        public List<InfoOfElementMoveAfterNormalize> InfoOfElementMoveAfterNormalizes => _infoOfElementMoveAfterNormalizes;

        public Normalizer()
        {
            _infoOfElementMoveAfterNormalizes = new List<InfoOfElementMoveAfterNormalize>();
        }
        
        public ElementType[,] Normalize(ElementType[,] elementTypes)
        {
            _infoOfElementMoveAfterNormalizes.Clear();
            int rows = elementTypes.GetUpperBound(0) + 1;
            int columns = elementTypes.GetUpperBound(1) + 1;
            for (int i = rows-1; i >= 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    if(elementTypes[i,j] == ElementType.NONE) continue;

                    int increment = 0;
                    for (int t = i; t < rows; t++)
                    {
                        if (elementTypes[t,j] == ElementType.NONE) increment++;
                    }
                    
                    ElementPosition needToMove = new ElementPosition(i, j);
                    ElementPosition targetPosition = new ElementPosition(i + increment, j);
                    var buf = elementTypes[i, j];
                    elementTypes[i, j] = elementTypes[i + increment, j];
                    elementTypes[i + increment, j] = buf;
                    _infoOfElementMoveAfterNormalizes.Add(
                        new InfoOfElementMoveAfterNormalize(needToMove, targetPosition));
                    
                }
            }
            return elementTypes;
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