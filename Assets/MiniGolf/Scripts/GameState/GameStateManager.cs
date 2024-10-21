using System;
using UnityEngine;

public class GameStateManager : IManager
{
    private GameStateEnum _currentGameState;
    
    public GameStateEnum GetCurrentGameState()
    {
        return _currentGameState;
    }

    public void SetGameState(GameStateEnum gameState)
    {
        _currentGameState = gameState;
    }

    public void Init()
    {
        SetGameState(GameStateEnum.MainMenu);
    }

    public void Release()
    {
        SetGameState(GameStateEnum.MainMenu);
    }
}
