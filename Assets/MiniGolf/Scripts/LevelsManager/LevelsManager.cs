using System;
using UnityEngine;

public class LevelsManager : MonoBehaviour, IManager
{
    [SerializeField] private LevelController[] _levels;
    [SerializeField] private int _mainMenuIndex;
    
    private int _currentLevelIndex;
    
    public Action OnBallHitEvent;
    public Action<LevelData> OnLevelLoadedEvent;
    public Action<LevelData, int> OnLevelCompletedEvent;
    public Action<int> OnBallHitConfirmedEvent;

    public void Init()
    {
        LoadMainMenu();
    }

    public void Release()
    {
        
    }

    public void LoadLevel(int levelIndex)
    {
        foreach ( var level in _levels )
        {
            level.TryLoadLevel(levelIndex);
        }

        Hub.Instance.GameStateManager.SetGameState(levelIndex != _mainMenuIndex
            ? GameStateEnum.Game
            : GameStateEnum.MainMenu);
        
        _currentLevelIndex = levelIndex;
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
