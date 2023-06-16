using Assets.Scripts.Commons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public void Init()
    {
        homePanel.Open();
        gamePlayPanel.Open();
    }

    public BaseUI gamePlayPanel;
    public BaseUI dailyMissionPanel;
    public BaseUI collectionPanel;
    public BaseUI challegentPanel;
    public BaseUI losePanel;
    public BaseUI winPanel;
    public BaseUI chessPanel;
    public BaseUI homePanel;
    public BaseUI shopPanel;
    public BaseUI settingPanel;
    public BaseUI ratePanel;
    public UINotification notification;
}
