using Elements;

namespace DefaultNamespace
{
    public class LevelGenerator
    {
        private Level _level;

        public void Generate()
        {
            _level.GenerateLevel();
        }

        public void Normalize()
        {
            _level.NormalizeLevel();
        }
    }

    public class Level
    {
        private IElement[,] _elements;

        public void GenerateLevel()
        {
            
        }

        public void NormalizeLevel()
        {
            
        }
    }
}