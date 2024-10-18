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
        Hub.Instance.LevelsManager.OnLevelLoadedEvent?.Invoke(_levelData);
    }

    private void LoadLevel()
    {
        _levelGO.SetActive(true);
        TryResetLevel(_levelData.ID);
        Hub.Instance.LevelsManager.OnLevelLoadedEvent?.Invoke(_levelData);
    }

    private void UnloadLevel()
    {
        _levelGO.SetActive(false);
    }
    
    private void Awake()
    {
        _curLevelHole.OnBallInsideEvent += OnBallInsideEventHandler;
        Hub.Instance.LevelsManager.OnBallHitEvent += OnBallHitEventHandler;
    }

    private void OnDestroy()
    {
        _curLevelHole.OnBallInsideEvent -= OnBallInsideEventHandler;
        Hub.Instance.LevelsManager.OnBallHitEvent -= OnBallHitEventHandler;
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

    private void OnBallInsideEventHandler()
    {
        Hub.Instance.LevelsManager.OnLevelCompletedEvent?.Invoke(_levelData, CalculateStars(_levelData, _attemptsCount));
    }

    private void OnBallHitEventHandler()
    {
        _attemptsCount++;
        Hub.Instance.LevelsManager.OnBallHitConfirmedEvent.Invoke(_attemptsCount);
    }
}
