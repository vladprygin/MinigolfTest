using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IManager
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
        
    }

    public void Release()
    {
        
    }
}
