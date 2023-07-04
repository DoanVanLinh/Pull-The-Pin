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
using Assets.Scripts.UI.Streak;
using DG.Tweening;
using Cinemachine;

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
        currentGameState = GameState.MainUi;
    }
    #endregion

    public bool isStart;
    public GameState currentGameState;
    public Action OnPlayNewLevel;


    #region ExtraReward
    [FoldoutGroup("ExtraReward")]
    public int extraReward;
    #endregion

    #region Data
    public Dictionary<EDailyMissionID, DailyMissionData> dailyMissionsData;
    public Dictionary<string, Item> itemsData;
    public Dictionary<string, Item> ballData;
    public Dictionary<string, Item> themeData;
    public Dictionary<string, Item> pinData;
    public Dictionary<string, Item> trailData;
    public Dictionary<string, Item> wallData;
    public List<PuzzleGroupData> puzzleGroupData;
    public List<PuzzleData> puzzleData;
    public Dictionary<string, ChallengeLevel> challengeLevels;

    #endregion

    public Camera mainCam;
    public Transform cinemachineTarget;
    public CinemachineVirtualCamera cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin mainCinemachineBasicMultiChannelPerlin;

    private bool folowBall;
    public LayerMask pinLayer;
    public List<State> stage;

    public State currentStage;
    public ChallengeLevel currentChallenge;

    public GameObject victory1;
    public GameObject victory2;
    private int countStageTutorial;
    private int stageIndex => DataManager.Instance.CurrentStage < stage.Count ?
                                DataManager.Instance.CurrentStage :
                                DataManager.Instance.CurrentStage - (stage.Count - countStageTutorial) * Math.DivRem((DataManager.Instance.CurrentStage - countStageTutorial), (stage.Count - countStageTutorial), out int a);

    public Item currentBall => ballData[DataManager.Instance.CurrentBall];
    public Item currentTheme => themeData[DataManager.Instance.CurrentThemeVisual];
    public Item currentPin => pinData[DataManager.Instance.CurrentPin];
    public Item currentTrail => trailData[DataManager.Instance.CurrentTrail];
    public Item currentWall => wallData[DataManager.Instance.CurrentWall];

    public void Init()
    {
        countStageTutorial = 2;
        currentStage = stage[stageIndex];
        mainCinemachineBasicMultiChannelPerlin = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GetDailyMissionData();
        GetItemsData();
        GetPuzzleGroupData();
        GetPuzzleData();
        ReloadDailyMisson();
        GetChallengeLevelData();
    }

    private void GetChallengeLevelData()
    {
        challengeLevels = Resources.LoadAll<ChallengeLevel>("Prefabs/ChallengeLevels/").ToDictionary(m => m.id, m => m);
    }

    private void ReloadDailyMisson()
    {
        if (!CPlayerPrefs.GetBool(DateTime.Now.ToString("d"), false))
        {
            EDailyMissionID[] ids = dailyMissionsData.Keys.OrderBy(o => Random.Range(-1f, 1f)).Take(5).ToArray();
            DataManager.Instance.GetData().RandomDailyMisson(ids);
            DataManager.Instance.GetData().ResetDailyMissionGift();
        }
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
    private void GetPuzzleGroupData()
    {
        puzzleGroupData = Resources.LoadAll<PuzzleGroupData>("ScriptableObject/Puzzle/Group/").ToList();
    }
    private void GetPuzzleData()
    {
        puzzleData = Resources.LoadAll<PuzzleData>("ScriptableObject/Puzzle/").ToList();
    }
    public void UpdateCamChallegeLoc()
    {
        if (folowBall)
            if (cinemachineTarget.position.y > Ball.hieghtestBall.transform.position.y && Ball.hieghtestBall.transform.position.y > 0)
            {
                cinemachineTarget.position = new Vector3(0, Ball.hieghtestBall.transform.position.y, 0);
            }
    }
    public void SetCamLoc(Vector2 loc)
    {
        cinemachineTarget.position = loc;
    }
    public ScriptableObject GetDataByName(string name)
    {
        return Resources.LoadAll<ScriptableObject>("Prefabs/Data/" + name)[0];
    }

    public void SetGameState(GameState gameState, float delay = 1)
    {
        if (this.currentGameState == gameState)
            return;


        switch (gameState)
        {
            case GameState.MainUi:

                break;
            case GameState.NormalMode:
                CamrraShake(0);
                if (currentChallenge != null)
                {
                    Destroy(currentChallenge.gameObject);
                }

                SetCamLoc(Vector2.zero);
                UIManager.Instance.gamePlayPanel.Open();
                UIManager.Instance.challengePlayPanel.Close();

                currentStage.StartState(DataManager.Instance.CurrentLevel);
                break;
            case GameState.Win:
                victory1.SetActive(true);
                victory2.SetActive(true);

                if (currentGameState == GameState.NormalMode)
                    StartCoroutine(IEDelay(delay, delegate
                    {
                        if (!this.currentStage.NextLevel())
                        {
                            if (DataManager.Instance.CurrentStage > 4)
                                ((StreakPanel)UIManager.Instance.streakPanel).Open(true);
                            UIManager.Instance.winPanel.Open();
                        }
                        else
                            this.currentGameState = GameState.NormalMode;
                    }));
                else if (currentGameState == GameState.ChallengeMode)
                {
                    DataManager.Instance.GetData().AddDailyMissionValue(EDailyMissionID.CompleteChallenge, 1);
                    DataManager.Instance.GetData().SetChallengeStatusById(currentChallenge.id, EChalengeType.Win);
                    StartCoroutine(IEDelay(delay, delegate
                    {
                        UIManager.Instance.challengeWinPanel.Open();

                    }));
                }
                break;
            case GameState.Lose:
                if (currentGameState == GameState.NormalMode)
                    StartCoroutine(IEDelay(delay, delegate
                    {
                        if (DataManager.Instance.CurrentStreak != 0)
                            ((StreakPanel)UIManager.Instance.streakPanel).Open(false);
                        UIManager.Instance.losePanel.Open();
                    }));
                else if (currentGameState == GameState.ChallengeMode)
                {
                    DataManager.Instance.GetData().SetChallengeStatusById(currentChallenge.id, EChalengeType.Failed);

                    StartCoroutine(IEDelay(delay, delegate
                    {
                        UIManager.Instance.challengeLosePanel.Open();
                    }));
                }
                break;
            case GameState.Null:
                Time.timeScale = 0;
                break;
            case GameState.Pause:
                break;
            case GameState.ChallengeMode:
                CamrraShake(0);
                folowBall = false;
                if (currentStage != null)
                    currentStage.ClearStage();

                SetCameraSize(currentChallenge.cameraSize);
                SetCamLoc(Vector2.zero);
                UIManager.Instance.gamePlayPanel.Close();
                UIManager.Instance.challengePlayPanel.Open();

                Vector2 screentPoint = mainCam.WorldToViewportPoint(Ball.hieghtestBall.transform.position);

                if (screentPoint.y > 1 || screentPoint.y < 0)
                    folowBall = true;
                if (folowBall)
                    StartCoroutine(IEDelay(1, delegate
                    {
                        cinemachineTarget.DOMove(new Vector3(0, Ball.hieghtestBall.transform.position.y, 0), 2f)
                                        .SetEase(Ease.Linear);
                    }));

                break;
            default:
                break;
        }

        this.currentGameState = gameState;

    }
    public void SetCameraSize(float size)
    {
        cinemachineCamera.m_Lens.FieldOfView = size;
    }
    public void StartChallenge(string id)
    {
        if (currentChallenge != null)
        {
            Destroy(currentChallenge.gameObject);
            currentChallenge = null;
        }
        currentChallenge = Instantiate(challengeLevels[id], Vector3.zero, Quaternion.identity);
        currentChallenge.Init(false);
        SetGameState(GameState.ChallengeMode);
    }
    [Button()]
    public void NextStage()
    {
        currentStage.ClearStage();
        DataManager.Instance.AddCurrentStage(1);
        currentStage = stage[stageIndex];
        SetGameState(GameState.NormalMode);
    }
    public void NextLevel()
    {
        currentGameState = GameState.NormalMode;
        if (!this.currentStage.NextLevel(false))
        {
            currentGameState = GameState.Null;
            NextStage();
        }
    }
    public void ReplayStage()
    {
        DataManager.Instance.SetCurrentLevel(0);
        SetGameState(GameState.NormalMode);
    }
    public void ReplayLevel()
    {
        currentGameState = GameState.Null;
        SetGameState(GameState.NormalMode);
    }
    public void CamrraShake(float force = 1, float time = 1)
    {
        mainCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = force;
        StartCoroutine(IEDelay(time, () =>
        {
            mainCinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }));
    }
    IEnumerator IEDelay(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
    public bool AnyPiecePuzzle()
    {
        int length = puzzleData.Count;

        for (int i = 0; i < length; i++)
        {
            int currentAmount = DataManager.Instance.GetInt(puzzleData[i].id);
            if (currentAmount < 9)
            {
                return true;
            }
        }
        return false;
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
    public string GetRandomItemByType(EItemUnlockType typeUnlock)
    {
        List<Item> items = itemsData.Values.Where(i => i.itemUnlockType == typeUnlock && !DataManager.Instance.GetData().HasItem(i.id)).OrderBy(o => Random.Range(-1f, 1f)).ToList();

        return items.Count != 0 ? items[0].id : "";
    }
}
