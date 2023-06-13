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

    public BaseUI gamePlayPanel;
    public BaseUI losePanel;
    public BaseUI winPanel;
    public BaseUI chessPanel;
    public BaseUI homePanel;
    public BaseUI shopPanel;
    public BaseUI levelUpPanel;
    public BaseUI revivePanel;
    public BaseUI pausePanel;
    public BaseUI settingPanel;
    public BaseUI weaponPanel;
    public BaseUI receviedSkillPanel;
    public BaseUI warningPanel;
    public BaseUI giftPanel;
    public BaseUI ratePanel;
    public BaseUI inventoryPanel;
    public BaseUI roomSkillPanel;
    public BaseUI itemShowPanel;
    public BaseUI itemInforPanel;
    public BaseUI circleFireTutPanel;
    public UINotification notification;
}
