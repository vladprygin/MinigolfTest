using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    [SerializeField] public UIManager UIManager;
    [SerializeField] public LevelsManager LevelsManager;
    [SerializeField] public GameStateManager GameStateManager;
    
    // Start is called before the first frame update
    public static Hub Instance;

    private void Awake()
    {
        if ( Instance == null )
        {
            Instance = this;
        }
        
        InitManagers();
    }

    private void OnApplicationQuit()
    {
        ReleaseManagers();
    }

    private void ReleaseManagers()
    {
        UIManager.Release();
        GameStateManager.Release();
        LevelsManager.Release();
    }

    private void InitManagers()
    {
        UIManager.Init();
        GameStateManager.Init();
        LevelsManager.Init();
    }
}
