using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private HoleController _curLevelHole;
    [SerializeField] private Transform _initialBallPosition;
    [SerializeField] private Transform _ballTransform;
    [SerializeField] private GameObject _levelGO;

    private int _attemptsCount;

    public LevelData LevelData => _levelData;

    public void TryLoadLevel(int index)
    {
        if ( index == _levelData.ID )
        {
            LoadLevel();
        }
        else
        {
            UnloadLevel();
        }
    }
    
    public void TryResetLevel(int index)
    {
        if ( index == _levelData.ID )
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        _ballTransform.position = _initialBallPosition.position;
        _ballTransform.rotation = _initialBallPosition.rotation;
        _attemptsCount = 0;
    }

    private void LoadLevel()
    {
        _levelGO.SetActive(true);
        TryResetLevel(_levelData.ID);
    }

    private void UnloadLevel()
    {
        _levelGO.SetActive(false);
    }
    
    private void Start()
    {
        _curLevelHole.OnBallInsideEvent += OnBallInsideEventHandler;
    }

    private void OnDestroy()
    {
        _curLevelHole.OnBallInsideEvent -= OnBallInsideEventHandler;
    }
    


    private void OnBallInsideEventHandler()
    {
        Hub.LevelsManager.CompleteLevel(_levelData, _attemptsCount);
        Hub.GameStateManager.SetGameState(GameStateEnum.LevelComplete);
    }

    private void OnBallHitEventHandler()
    {
        _attemptsCount++;
        //Hub.LevelsManager.BallHitConfirmed(_attemptsCount);
    }
}
