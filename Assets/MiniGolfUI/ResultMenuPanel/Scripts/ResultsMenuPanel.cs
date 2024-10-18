using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenuPanel : PanelUIController
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _starsText;
    [SerializeField] private string _starsTextFormat;

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
        Hub.Instance.LevelsManager.OnLevelCompletedEvent += OnLevelCompletedEventHandler;
    }

    private void UnsubscribeFromEvents()
    {
        Hub.Instance.LevelsManager.OnLevelCompletedEvent -= OnLevelCompletedEventHandler;
    }

    private void SetupButtons()
    {
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void ReleaseButtons()
    {
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    private void OnLevelCompletedEventHandler(LevelData levelData, int stars)
    {
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.ResultsPanel);
        _starsText.text = string.Format(_starsTextFormat, stars);
    }

    private void OnRestartButtonClicked()
    {
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.GameMenuPanel);
        Hub.Instance.LevelsManager.ResetCurrentLevel();
    }

    private void OnMainMenuButtonClicked()
    {
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.MainMenuPanel);
        Hub.Instance.LevelsManager.LoadMainMenu();
    }
}
