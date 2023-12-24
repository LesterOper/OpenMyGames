using Elements;

namespace DefaultNamespace
{
    public class LevelContainer
    {
        public static ElementType[,] level =
        {
            {ElementType.WATER, ElementType.NONE, ElementType.FIRE, ElementType.NONE, ElementType.NONE}, 
            {ElementType.WATER, ElementType.NONE, ElementType.WATER, ElementType.FIRE, ElementType.FIRE}
        };
        
        public static ElementType[,] level1 =
        {
            {ElementType.NONE, ElementType.WATER, ElementType.WATER, ElementType.NONE, ElementType.NONE}, 
            {ElementType.NONE, ElementType.WATER, ElementType.FIRE, ElementType.WATER, ElementType.WATER}, 
            {ElementType.NONE, ElementType.FIRE, ElementType.WATER, ElementType.FIRE, ElementType.FIRE}, 
            {ElementType.NONE, ElementType.WATER, ElementType.FIRE, ElementType.WATER, ElementType.WATER}, 
            {ElementType.NONE, ElementType.WATER, ElementType.FIRE, ElementType.WATER, ElementType.WATER}
        };
        
        public static ElementType[,] level2 =
        {
            {ElementType.NONE, ElementType.NONE, ElementType.WATER, ElementType.NONE, ElementType.NONE}, 
            {ElementType.NONE, ElementType.WATER, ElementType.FIRE, ElementType.NONE, ElementType.NONE}, 
            {ElementType.NONE, ElementType.WATER, ElementType.WATER, ElementType.NONE, ElementType.WATER}, 
            {ElementType.NONE, ElementType.FIRE, ElementType.WATER, ElementType.FIRE, ElementType.FIRE}, 
            {ElementType.NONE, ElementType.WATER, ElementType.FIRE, ElementType.WATER, ElementType.WATER}, 
            {ElementType.NONE, ElementType.WATER, ElementType.FIRE, ElementType.WATER, ElementType.WATER}
        };
    }
}