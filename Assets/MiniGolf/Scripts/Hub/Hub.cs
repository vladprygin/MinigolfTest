using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    public static UIManager UIManager
    {
        get;
        private set;
    }
    
    public static LevelsManager LevelsManager
    {
        get;
        private set;
    }
    
    public static GameStateManager GameStateManager
    {
        get;
        private set;
    }

    public static BallManager BallManager
    {
        get;
        private set;
    }
    
    // Start is called before the first frame update
    private void Awake()
    {
        InitManagers();
    }

    private void OnApplicationQuit()
    {
        ReleaseManagers();
    }

    private void ReleaseManagers()
    {
        if ( UIManager != null )
        {
            UIManager.Release();
        }

        if ( GameStateManager != null )
        {
            GameStateManager.Release();
        }

        if ( LevelsManager != null )
        {
            LevelsManager.Release();
        }
    }

    private void InitManagers()
    {
        UIManager = new UIManager();
        UIManager.Init();
        
        LevelsManager = new LevelsManager();
        LevelsManager.Init();
        
        GameStateManager = new GameStateManager();
        GameStateManager.Init();

        BallManager = new BallManager();
        BallManager.Init();
    }
}
