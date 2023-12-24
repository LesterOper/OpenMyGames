using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Elements;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelsParser
    {
        private Levels _levels;
        private TextAsset _levelTextAsset;
        public Levels Levels => _levels;

        public void ParseXml()
        {
            _levelTextAsset = Resources.Load<TextAsset>("XmlFiles/Levels");
            XmlSerializer serializer = new XmlSerializer(typeof(Levels));
            StringReader reader = new StringReader(_levelTextAsset.text);
            _levels = serializer.Deserialize(reader) as Levels;
        }
        
        private int[,] ParseLevelToIntMatrix(int levelNumber)
        {
            LevelData currentLevelData = _levels.GetCurrentLevel(levelNumber);
            RowData[] matrixRows = currentLevelData.LevelMatrix.RowDatas;
            int rowCount = matrixRows.Length;
            int columnCount = matrixRows[0].Row.Split(" ").Length;
            int[,] intMatrix = new int[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                string[] row = matrixRows[i].Row.Split(" ");

                for (int j = 0; j < columnCount; j++)
                {
                    intMatrix[i,j] = int.Parse(row[j]);
                }
            }

            return intMatrix;
        }

        public ElementType[,] GetLevel(int levelNumber = 1)
        {
            int[,] levelIntMatrix = ParseLevelToIntMatrix(levelNumber);
            int rows = levelIntMatrix.GetUpperBound(0) + 1;
            int columns = levelIntMatrix.GetUpperBound(1) + 1;
            ElementType[,] level = new ElementType[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    level[i, j] = (ElementType) levelIntMatrix[i, j];
                }
            }

            return level;
        }
    }

    [XmlRoot("Levels")]
    [Serializable]
    public class Levels
    {
        [XmlElement("LevelData")]
        public LevelData[] LevelDatas { get; set; }

        public LevelData GetCurrentLevel(int levelId) => 
            LevelDatas.FirstOrDefault(levelLocal => levelLocal.Id == levelId);
    }

    [Serializable]
    public class LevelData
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("needSteps")]
        public int NeedSteps { get; set; }
        [XmlElement("levelMatrix")]
        public LevelMatrix LevelMatrix { get; set; }
    }

    [Serializable]
    public class LevelMatrix
    {
        [XmlElement("rowData")]
        public RowData[] RowDatas { get; set; }
    }

    [Serializable]
    public class RowData
    {
        [XmlAttribute("row")]
        public string Row { get; set; }
    }
}