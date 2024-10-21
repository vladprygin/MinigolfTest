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
        Hub.UIManager.OpenSection(ScreenTypeEnum.GameMenuPanel);
        Hub.GameStateManager.SetGameState(GameStateEnum.Game);
    }
    
    private void OnSettingsButtonClicked()
    {
        Hub.UIManager.OpenSection(ScreenTypeEnum.SettingsPanel);
    }

    private void OnMainMenuButtonClicked()
    {
        Hub.UIManager.OpenConfirmationPanel(_mainMenuConfirmationTitle,
            OnMainMenuConfirmed,
            OnMainMenuCanceled);
    }

    private void OnMainMenuConfirmed()
    {
        Hub.GameStateManager.SetGameState(GameStateEnum.MainMenu);
        Hub.UIManager.OpenSection(ScreenTypeEnum.MainMenuPanel);
        Hub.LevelsManager.LoadMainMenu();;
    }

    private void OnMainMenuCanceled()
    {
        Hub.GameStateManager.SetGameState(GameStateEnum.Pause);
        Hub.UIManager.OpenSection(ScreenTypeEnum.PauseMenuPanel);
    }

    private void OnResetLevelButtonClicked()
    {
        Hub.LevelsManager.ResetCurrentLevel();
        Hub.GameStateManager.SetGameState(GameStateEnum.Game);
        Hub.UIManager.OpenSection(ScreenTypeEnum.GameMenuPanel);
    }

}
