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
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using System.Linq;

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


    #region ExtraReward
    [FoldoutGroup("ExtraReward")]
    public int extraReward;
    #endregion

    #region Data
    public Dictionary<int, DailyMissionData> dailyMissionsData;
    public Dictionary<string, Item> itemsData;
    public Dictionary<string, Item> ballData;
    public Dictionary<string, Item> themeData;
    public Dictionary<string, Item> pinData;
    public Dictionary<string, Item> trailData;
    public Dictionary<string, Item> wallData;
    #endregion

    public Camera mainCam;
    public LayerMask pinLayer;
    public List<State> state;

    private State currentState;
    public void Init()
    {
        currentState = state[DataManager.Instance.CurrentState];
        GetDailyMissionData();
        GetItemsData();
    }
    private void GetItemsData()
    {
        itemsData = Resources.LoadAll<Item>("ScriptableObject/Items/").ToDictionary(m => m.id, m => m);

        ballData = Resources.LoadAll<Item>("ScriptableObject/Items/Balls/").ToDictionary(m => m.id, m => m);
        themeData = Resources.LoadAll<Item>("ScriptableObject/Items/Themes/").ToDictionary(m => m.id, m => m);
        pinData = Resources.LoadAll<Item>("ScriptableObject/Items/Pins/").ToDictionary(m => m.id, m => m);
        trailData = Resources.LoadAll<Item>("ScriptableObject/Items/Trails/").ToDictionary(m => m.id, m => m);
        wallData = Resources.LoadAll<Item>("ScriptableObject/Items/Walls/").ToDictionary(m => m.id, m => m);

    }
    private void GetDailyMissionData()
    {
        dailyMissionsData = Resources.LoadAll<DailyMissionData>("ScriptableObject/DailyMission/").ToDictionary(m => m.id, m => m);
    }

    public ScriptableObject GetDataByName(string name)
    {
        return Resources.LoadAll<ScriptableObject>("Prefabs/Data/" + name)[0];
    }
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        switch (this.gameState)
        {
            case GameState.MainUi:

                break;
            case GameState.Gameplay:
                currentState.StartState(DataManager.Instance.CurrentLevel);
                break;
            case GameState.Win:
                if (!this.currentState.NextLevel())
                    StartCoroutine(IEDelay(1f, delegate
                    {
                        UIManager.Instance.winPanel.Open();
                    }));

                break;
            case GameState.Lose:
                UIManager.Instance.losePanel.Open();

                StartCoroutine(IEDelay(1f, delegate
                {
                    Replay();
                    UIManager.Instance.losePanel.Close();

                })); break;
            case GameState.Null:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }
    public void Replay()
    {
        currentState.ResumeLevel();
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

    public Pin DetectPin()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, pinLayer))
        {
            return hit.collider.GetComponent<Pin>();
        }
        return null;
    }
}
