using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPanelController : PanelUIController
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private TextMeshProUGUI _levelTitleText;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _previousLevelButton;
    [SerializeField] private string _levelTitleFormat;

    private LevelData[] _levelDatas;
    private int _curLevelIndex;

    private void SetLevelDatas()
    {
        if ( Hub.LevelsManager != null )
        {
            _levelDatas = Hub.LevelsManager.GetLevelDatas();
        }
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        SetLevelDatas();
        SetDefaultLevel();
    }

    private void SetDefaultLevel()
    {
        if ( _levelDatas != null && _levelDatas.Length > 0 )
        {
            _curLevelIndex = _levelDatas[1].ID;
        }
    }
    
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
        _nextLevelButton.onClick.AddListener(()=>ChangeLevelSelection(1));
        _previousLevelButton.onClick.AddListener(()=>ChangeLevelSelection(-1));
    }

    private void ReleaseButtons()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _nextLevelButton.onClick.RemoveAllListeners();
        _previousLevelButton.onClick.RemoveAllListeners();
    }

    private void ChangeLevelSelection(int offset)
    {
        if ( _curLevelIndex + offset < 1 )
        {
            _curLevelIndex = _levelDatas.Length - 1;
            return;
        }

        if ( _curLevelIndex + offset > _levelDatas.Length - 1 )
        {
            _curLevelIndex = 1;
        }

        _curLevelIndex += offset;
        _levelTitleText.text = string.Format(_levelTitleFormat, _curLevelIndex);
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
    }

    private void OnStartGameButtonClicked()
    {
        Hub.UIManager.OpenSection(ScreenTypeEnum.GameMenuPanel);
        Hub.LevelsManager.LoadLevel(_levelDatas[_curLevelIndex].ID);
    }
}
