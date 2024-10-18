using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanelController : PanelUIController
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _quitGameButton;

    [SerializeField] private string _quitGameConfirmationTitle;

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
        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        _quitGameButton.onClick.AddListener(OnQuitGameButtonClicked);
    }

    private void ReleaseButtons()
    {
        _startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
        _quitGameButton.onClick.RemoveListener(OnQuitGameButtonClicked);
    }

    private void OnStartGameButtonClicked()
    {
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.LevelSelectionPanel);
    }

    private void OnQuitGameButtonClicked()
    {
        Hub.Instance.UIManager.OpenConfirmationPanel(_quitGameConfirmationTitle,
            OnQuitGameConfirmed,
            OnQuitGameCanceled);
    }

    private void OnQuitGameConfirmed()
    {
        Application.Quit();
    }

    private void OnQuitGameCanceled()
    {
        Hub.Instance.UIManager.OpenSection(ScreenTypeEnum.MainMenuPanel);
    }
}
