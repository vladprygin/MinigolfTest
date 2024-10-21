using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : IManager
{
    private List<LevelData> _levelDatas;
    [SerializeField] private LevelController[] _levels;
    [SerializeField] private int _mainMenuIndex;
    
    private int _currentLevelIndex;
    
    private Action _onLevelLoadedEvent;
    private Action<int> _onLevelCompletedEvent;

    public event Action OnLevelLoadedEvent
    {
        add => _onLevelLoadedEvent += value;
        remove => _onLevelLoadedEvent -= value;
    }
    
    public event Action<int> OnLevelCompletedEvent
    {
        add => _onLevelCompletedEvent += value;
        remove => _onLevelCompletedEvent -= value;
    }

    private void LoadLevelDatas()
    {
        _levelDatas = new List<LevelData>();
        _levelDatas.Add(new LevelData(0, 3, new int[3]{3,4,5}, "MainMenu"));
        _levelDatas.Add(new LevelData(1, 3, new int[3]{3,4,5}, "Level1"));
        _levelDatas.Add(new LevelData(2, 3, new int[3]{4,5,6}, "Level2"));

    }

    private void LoadLevelsFromFile()
    {
        
    }

    public void Init()
    {
        LoadMainMenu();
    }

    public void Release()
    {
    }

    private void LevelLoaded()
    {
        _onLevelLoadedEvent?.Invoke();
    }

    public void CompleteLevel(LevelData data, int attempts)
    {
        _onLevelCompletedEvent?.Invoke(CalculateStars(data, attempts));
    }
    
    private int CalculateStars(LevelData levelData, int hits)
    {
        if ( hits <= levelData.StarsThreshold[0] )
        {
            return 3;
        }

        if ( hits <= levelData.StarsThreshold[1] )
        {
            return 2;
        }
        
        return 1;
    }

    public void LoadLevel(int levelIndex)
    {
        foreach ( var level in _levels )
        {
            level.TryLoadLevel(levelIndex);
        }

        Hub.GameStateManager.SetGameState(levelIndex != _mainMenuIndex
            ? GameStateEnum.Game
            : GameStateEnum.MainMenu);
        
        _currentLevelIndex = levelIndex;
        LevelLoaded();
    }

    public void ResetCurrentLevel()
    {
        foreach ( var level in _levels )
        {
            if ( level.LevelData.ID == _currentLevelIndex )
            {
                level.TryResetLevel(level.LevelData.ID);
            }
        }
        LevelLoaded();
    }
    
    public void LoadMainMenu()
    {
        foreach ( var level in _levels )
        {
            LoadLevel(_levels[_mainMenuIndex].LevelData.ID);
        }
    }

    public LevelData[] GetLevelDatas()
    {
        LevelData[] levelDatas = new LevelData[_levels.Length];

        for ( int i = 0; i < _levels.Length; i++ )
        {
            levelDatas[i] = _levels[i].LevelData;
        }

        return levelDatas;
    }
}
