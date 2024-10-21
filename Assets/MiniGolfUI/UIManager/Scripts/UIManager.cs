using UnityEngine;

public class UIManager : IManager
{
    private PanelUIController[] _screenPanels;
    private ConfirmationPanelController _confirmationPanel;
    
    public void Init()
    {
        InitPanels();
        
        OpenSection(ScreenTypeEnum.MainMenuPanel);
        _confirmationPanel.ClosePanel();
    }

    public void Release()
    {
        ReleasePanels();
    }
    
    private void InitPanels()
    {
        foreach ( var panel in _screenPanels )
        {
            panel.Init();
        }
    }

    private void ReleasePanels()
    {
        foreach ( var panel in _screenPanels )
        {
            panel.Release();
        }
    }

    public void OpenConfirmationPanel(string titleText, System.Action onConfirmAction, System.Action onCancelAction)
    {
        foreach ( var panel in _screenPanels )
        {
            panel.ClosePanel();
        }
        
        _confirmationPanel.OpenConfirmationPanel(titleText, onConfirmAction, onCancelAction);
    }

    public void OpenSection(ScreenTypeEnum screenType)
    {
        foreach ( var _panel in _screenPanels )
        {
            _panel.TryOpenPanel(screenType);
        }
    }
}
