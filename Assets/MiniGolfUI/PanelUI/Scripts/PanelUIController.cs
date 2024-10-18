using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PanelUIController : MonoBehaviour
{
    [SerializeField] private ScreenTypeEnum _screenType;
    [SerializeField] private GameObject _panelGO;

    public virtual void Init()
    {
        _panelGO = gameObject;
    }

    public virtual void Release()
    {
        _panelGO = null;
    }

    public virtual void OpenPanel()
    {
        _panelGO.SetActive(true);
    }

    public void TryOpenPanel(ScreenTypeEnum screenType)
    {
        if ( _screenType == screenType )
        {
            OpenPanel();
        }
        else
        {
            ClosePanel();
        }
    }

    public void ClosePanel()
    {
        _panelGO.SetActive(false);
    }
}
