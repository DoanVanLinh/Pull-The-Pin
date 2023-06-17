using Assets.Scripts.Commons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
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
        center = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        center = new Vector3(center.x,center.y,mainCanvas.transform.position.z);
        left = mainCam.ViewportToWorldPoint(new Vector3(-0.5f, 0.5f, 0));
        left = new Vector3(left.x, left.y,mainCanvas.transform.position.z);
        right = mainCam.ViewportToWorldPoint(new Vector3(1.5f, 0.5f, 0));
        right = new Vector3(right.x, right.y,mainCanvas.transform.position.z);
        top = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 1.5f, 0));
        top = new Vector3(top.x, top.y,mainCanvas.transform.position.z);
        bottom = mainCam.ViewportToWorldPoint(new Vector3(0.5f, -0.5f, 0));
        bottom = new Vector3(bottom.x, bottom.y, mainCanvas.transform.position.z);

        homePanel.Open();
        gamePlayPanel.Open();
    }

    [HideInInspector]
    public Vector3 left;
    [HideInInspector]
    public Vector3 right;
    [HideInInspector]
    public Vector3 center;
    [HideInInspector]
    public Vector3 top;
    [HideInInspector]
    public Vector3 bottom;
    [FoldoutGroup("Position")]
    public Camera mainCam;
    [FoldoutGroup("Position")]
    public Canvas mainCanvas;

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
