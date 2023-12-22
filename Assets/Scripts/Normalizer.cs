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
        
        public ElementType[][] Normalize(ElementType[][] elementTypes)
        {
            _infoOfElementMoveAfterNormalizes.Clear();

            for (int i = elementTypes.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < elementTypes[i].Length; j++)
                {
                    if(elementTypes[i][j] == ElementType.NONE) continue;
                    ElementType[] column = new ElementType[elementTypes.Length - i];
                    for (int k = i; k < column.Length; k++)
                    {
                        column[k] = elementTypes[k][j];
                    }

                    int increment = 0;
                    for (int t = 1; t < column.Length; t++)
                    {
                        if (column[t] == ElementType.NONE) increment++;
                    }

                    ElementPosition needToMove = new ElementPosition(i, j);
                    ElementPosition targetPosition = new ElementPosition(i+increment, j);
                    var buf = elementTypes[i][j];
                    elementTypes[i][j] = elementTypes[i + increment][j];
                    elementTypes[i + increment][j] = buf;
                    _infoOfElementMoveAfterNormalizes.Add(new InfoOfElementMoveAfterNormalize(needToMove, targetPosition));
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