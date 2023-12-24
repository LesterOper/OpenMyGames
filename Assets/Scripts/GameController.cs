using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Utils;
using Elements;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform levelParent;
    [SerializeField] private LevelGenerator levelGeneratorPrefab;
    [SerializeField] private LoadLevelViewController _loadLevelViewController;
    private LevelGenerator _levelGenerator;
    private LevelsParser _levelsParser;
    private int _currentLevel;

    private void OnEnable() => EventsInvoker.StartListening(EventsKeys.LOAD_NEXT_LEVEL, NextLevel);

    private void OnDisable() => EventsInvoker.StopListening(EventsKeys.LOAD_NEXT_LEVEL, NextLevel);

    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt(DataKeys.CURRENT_LEVEL);
        if (_currentLevel == 0)
        {
            _currentLevel = 1;
            PlayerPrefs.SetInt(DataKeys.CURRENT_LEVEL, _currentLevel);
        }
        
        ParseXmlWithLevelsData();
        CreateLevel();
    }

    private void ParseXmlWithLevelsData()
    {
        _levelsParser = new LevelsParser();
        _levelsParser.ParseXml();
    }
    
    private void CreateLevel()
    {
        ElementType[,] level = _levelsParser.GetLevel(_currentLevel);
        if(_levelGenerator != null) Destroy(_levelGenerator.gameObject);
        _levelGenerator = Instantiate(levelGeneratorPrefab, levelParent);
        _levelGenerator.Generate(level);
        _loadLevelViewController.LoadLevelViewDeactivate(0.5f);
    }

    private void NextLevel(Dictionary<string, object> arg)
    {
        if (_currentLevel == _levelsParser.Levels.LevelDatas.Length)
        {
            _currentLevel = 1;
        }
        else _currentLevel++;
        PlayerPrefs.SetInt(DataKeys.CURRENT_LEVEL, _currentLevel);
        _loadLevelViewController.LoadLevelViewActivate(0.5f, CreateLevel);
    }
}
