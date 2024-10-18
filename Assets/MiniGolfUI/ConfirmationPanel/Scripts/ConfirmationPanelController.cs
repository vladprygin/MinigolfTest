using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ConfirmationPanelController : PanelUIController
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private TextMeshProUGUI _panelTitle;
    
    public void OpenConfirmationPanel(string titleText, Action onConfirmAction, Action onCancelAction)
    {
        _panelTitle.text = titleText;
        
        _confirmButton.onClick.AddListener(()=>
        {
            onConfirmAction.Invoke();
            OnCloseConfirmationPanel();
        });
        
        _cancelButton.onClick.AddListener(() =>
        {
            onCancelAction.Invoke();
            OnCloseConfirmationPanel();
        });
        
        OpenPanel();
    }

    private void OnCloseConfirmationPanel()
    {
        _panelTitle.text = String.Empty;
        _confirmButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();
        ClosePanel();
    }
}
