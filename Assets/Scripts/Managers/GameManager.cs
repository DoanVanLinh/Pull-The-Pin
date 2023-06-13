using Assets.Scripts.Commons;
using Assets.Scripts.Data;
//using Assets.Scripts.Player;
//using Assets.Scripts.ScriptsableObjects;
using Assets.Scripts.UI;
//using Dragon.SDK;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        Application.targetFrameRate = 300;
        isStart = false;
        gameState = GameState.MainUi;
    }
    #endregion

    public bool isStart;
    public GameState gameState;
    public Action OnPlayNewLevel;

    #region Upgrade Attribute
    [FoldoutGroup("Upgrade Attribute")]
    public float percentDamePerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float percentCoinsUpgradeDamePerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float baseCoinsUpgradeDamePerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float percentHpPerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float percentCoinsUpgradeHpPerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float baseCoinsUpgradeHpPerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float percentCoinsPerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float percentCoinsUpgradeCoinsPerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float baseCoinsUpgradeCoinsPerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float percentDameGunPerLevel;
    [FoldoutGroup("Upgrade Attribute")]
    public float percentCoinsUpgradeDameGunPerLevel;
    #endregion

    #region ExtraReward
    [FoldoutGroup("ExtraReward")]
    public int extraReward;
    #endregion



    private void Start()
    {
        SetGameState(GameState.MainUi);
    }
    public ScriptableObject GetDataByName(string name)
    {
        return Resources.LoadAll<ScriptableObject>("Prefabs/Data/" + name)[0];
    }
    public void SetGameState(GameState state)
    {
        gameState = state;

        switch (gameState)
        {
            case GameState.MainUi:
                
                break;
            case GameState.Gameplay:
                
                break;
            case GameState.Win:
                
                break;
            case GameState.Lose:
                
                break;
            case GameState.Null:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }
    IEnumerator IEDelay(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    public void ShowAdsReward(string splacenment, Action actionClose)
    {
        //AdStatus adStatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        //{
            actionClose?.Invoke();
            //Analytics.LogRewardedVideoShow(DataManager.Instance.CurrentZone, splacenment);
            DontDestroy.Instance.curentAdsCapping = 0;
        //}, splacenment);
        //switch (adStatus)
        //{
        //    case AdStatus.NoInternet:
        //        {
        //            DebugCustom.Log("NoInternet");
        //            break;
        //        }
        //    case AdStatus.NoVideo:
        //        {
        //            DebugCustom.Log("NoVideo");
        //            break;
        //        }
        //}
    }
    public void ShowAdsInter(string splacenment, Action actionClose)
    {
        //AdStatus adStatus = SDKDGManager.Instance.AdsManager.ShowInterAdsStatus(() =>
        //{
            actionClose?.Invoke();
            //DebugCustom.Log("Show Inter");
            //Analytics.LogInterstitialShow(DataManager.Instance.CurrentZone, Helper.Int_Capping_Placement);
        //}, splacenment);

        //switch (adStatus)
        //{
        //    case AdStatus.NoInternet:
        //        {
        //            DebugCustom.Log("NoInternet");
        //            break;
        //        }
        //}
    }
    public void InAppPurchase(string idPack, Action actionClose)
    {
        //SDKDGManager.Instance.IAPManager.Purchase(idPack, () =>
        //{
            actionClose?.Invoke();
            //Analytics.LogInappPurcahse(idPack,"1");
        //});
    }
}
