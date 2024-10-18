using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : PanelUIController
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMPro.TextMeshProUGUI _parText;
    [SerializeField] private TMPro.TextMeshProUGUI _currentHitsText;

    [SerializeField] private string _parTextFormat;
    [SerializeField] private string _hitTextFormat;

    public override void Init()
    {
        base.Init();
        SubscribeToEvents();
        SetupButtons();
    }

    public override void Release()
    {
        base.Release();
        UnsubscribeFromEvents();
        ReleaseButtons();
    }

    private void SubscribeToEvents()
    {
        Hub.Instance.LevelsManager.OnLevelLoadedEvent += OnLevelLoaded;
        Hub.Instance.LevelsManager.OnBallHitConfirmedEvent += OnBallHit;
    }

    private void UnsubscribeFromEvents()
    {
        Hub.Instance.LevelsManager.OnLevelLoadedEvent -= OnLevelLoaded;
        Hub.Instance.LevelsManager.OnBallHitConfirmedEvent -= OnBallHit;
    }

    private void SetupButtons()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void ReleaseButtons()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
    }

    private void OnLevelLoaded(LevelData levelData)
    {
        _parText.text = string.Format(_parTextFormat, levelData.ParNumber);
        _currentHitsText.text = string.Format(_hitTextFormat, 0);
    }
    
    private void OnBallHit(int hitCount)
    {
        _currentHitsText.text = string.Format(_hitTextFormat, hitCount);
    }

    private void OnPauseButtonClicked()
    {
        Hub.Instance.GameStateManager.SetGameState(GameStateEnum.Pause);
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.PauseMenuPanel);
    }
}
