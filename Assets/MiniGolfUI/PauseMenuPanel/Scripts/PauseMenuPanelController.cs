using UnityEngine;
using UnityEngine.UI;

public class PauseMenuPanelController : PanelUIController
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resetLevelButton;

    [SerializeField] private string _mainMenuConfirmationTitle;

    public override void Init()
    {
        base.Init();
        SetupButtons();
    }

    public override void Release()
    {
        base.Release();
        ReleaseButtons();
    }

    private void SetupButtons()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        _resetLevelButton.onClick.AddListener(OnResetLevelButtonClicked);
    }

    private void ReleaseButtons()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        _resetLevelButton.onClick.RemoveListener(OnResetLevelButtonClicked);
    }
    

    private void OnResumeButtonClicked()
    {
        Hub.Instance.LevelsManager.OnBallHitEvent?.Invoke();
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.GameMenuPanel);
        Hub.Instance.GameStateManager.SetGameState(GameStateEnum.Game);
    }
    
    private void OnSettingsButtonClicked()
    {
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.SettingsPanel);
    }

    private void OnMainMenuButtonClicked()
    {
        Hub.Instance.UIManager.OpenConfirmationPanel(_mainMenuConfirmationTitle,
            OnMainMenuConfirmed,
            OnMainMenuCanceled);
    }

    private void OnMainMenuConfirmed()
    {
        Hub.Instance.GameStateManager.SetGameState(GameStateEnum.MainMenu);
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.MainMenuPanel);
        Hub.Instance.LevelsManager.LoadMainMenu();;
    }

    private void OnMainMenuCanceled()
    {
        Hub.Instance.GameStateManager.SetGameState(GameStateEnum.Pause);
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.PauseMenuPanel);
    }

    private void OnResetLevelButtonClicked()
    {
        Hub.Instance.LevelsManager.ResetCurrentLevel();
        Hub.Instance.GameStateManager.SetGameState(GameStateEnum.Game);
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.GameMenuPanel);
    }

}
