using UnityEngine;
using System.IO;
//using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Assets.Scripts.UI;
using System;

public class DataManager : MonoBehaviour
{
    #region JSON
    public static DataManager Instance;
    [SerializeField] private GameData gameData;
    [SerializeField] private string nameFile;

    private string path;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
    public void Init()
    {
        SetupKey();
        gameData = new GameData();
        if (CPlayerPrefs.HasKey(nameFile))
            LoadData();
        else
            gameData.Init();
        CPlayerPrefs.GenereateNewKey();
    }

    public void SaveData()
    {
        SaveLoadNewJson.Save(nameFile, gameData);
    }
    public void LoadData()
    {
        gameData = SaveLoadNewJson.Load<GameData>(nameFile);
    }

    public GameData GetData()
    {
        return gameData;
    }

    #endregion


    #region PlayerPrefs

    public bool isTest;
    public int level;

    //public List<ShopItem> shopItems = new List<ShopItem>();
    public Dictionary<string, bool> currentItems = new Dictionary<string, bool>();

    public int Coins { get; set; }
    public int Key { get; set; }
    public int CurrentNoadsReward { get; set; }
    public int CurrentSoundThemeState { get; set; }
    public int CurrentSoundEffectState { get; set; }
    public int CurrentVibrateState { get; set; }
    public int CurrentTheme { get; set; }
    public int CurrentState { get; set; }
    public int CurrentLevel { get; set; }
    public int CountPlay { get; set; }
    public bool IsNoads { get; set; }
    void SetupKey()
    {
        Coins = CPlayerPrefs.GetInt(Helper.Current_Coins_Key, 0);
        Key = CPlayerPrefs.GetInt(Helper.Current_Key_Key, 0);
        CurrentNoadsReward = CPlayerPrefs.GetInt(Helper.Current_Noads_Reward_Key, 0);
        CurrentTheme = CPlayerPrefs.GetInt(Helper.Current_Theme_Key, 1);
        CurrentSoundThemeState = CPlayerPrefs.GetInt(Helper.Current_Sound_Theme_State_Key,1);
        CurrentSoundEffectState = CPlayerPrefs.GetInt(Helper.Current_Sound_Effect_State_Key, 1);
        CurrentVibrateState = CPlayerPrefs.GetInt(Helper.Current_Vibrate_State_Key, 1);
        CurrentState = CPlayerPrefs.GetInt(Helper.Current_State_Key, 0);
        CurrentLevel = CPlayerPrefs.GetInt(Helper.Current_Level_Key, 0);
        CountPlay = CPlayerPrefs.GetInt(Helper.Current_Count_Play_Key, 0);
        IsNoads = CPlayerPrefs.GetBool(Helper.Is_No_Ads_Key, false);
    }

    public void AddCountPlay()
    {
        CountPlay++;
        CPlayerPrefs.SetInt(Helper.Current_Count_Play_Key, CountPlay);
    }
    public void SetNoAds(bool noads)
    {
        IsNoads = noads;
        CPlayerPrefs.SetBool(Helper.Is_No_Ads_Key, noads);
    }
    public void SetCurrentTheme(int id)
    {
        CurrentTheme = id;
        CPlayerPrefs.SetInt(Helper.Current_Theme_Key, id);
    }
    public void SetCurrentState(int id)
    {
        CurrentState = id;
        CPlayerPrefs.SetInt(Helper.Current_State_Key, id);
    }
    public void AddCurrentState(int id)
    {
        CurrentState += id;
        CPlayerPrefs.SetInt(Helper.Current_State_Key, CurrentState);
    }
    public void SetCurrentLevel(int id)
    {
        CurrentLevel = id;
        CPlayerPrefs.SetInt(Helper.Current_Level_Key, id);
    }
    public void AddCurrentLevel(int id)
    {
        CurrentLevel += id;
        CPlayerPrefs.SetInt(Helper.Current_Level_Key, CurrentLevel);
    }
    public void SetSoundThemeState(int state)
    {
        CurrentSoundThemeState = state;
        CPlayerPrefs.SetInt(Helper.Current_Sound_Theme_State_Key, CurrentSoundThemeState);
    }
    public void SetSoundEffectState(int state)
    {
        CurrentSoundEffectState = state;
        CPlayerPrefs.SetInt(Helper.Current_Sound_Effect_State_Key, CurrentSoundEffectState);
    }
    public void SetVibrateState(int state)
    {
        CurrentVibrateState = state;
        CPlayerPrefs.SetInt(Helper.Current_Vibrate_State_Key, CurrentVibrateState);
    }
    public void AddCoins(int coins)
    {
        Coins += coins;

        CPlayerPrefs.SetInt(Helper.Current_Coins_Key, Coins);
        //((HomePanel)UIManager.Instance.homePanel).UpdateCoin();
    }

    public void AddKey(int key)
    {
        Key += key;
        CPlayerPrefs.SetInt(Helper.Current_Key_Key, Key);
        //((HomePanel)UIManager.Instance.homePanel).UpdateKey();
    }
    public void SetCurrentNoadsReward(int currentNoadsReward)
    {
        this.CurrentNoadsReward = currentNoadsReward;
        CPlayerPrefs.SetInt(Helper.Current_Noads_Reward_Key, CurrentNoadsReward);
    }
    public int AddCurrentNoadsReward(int currentNoadsReward)
    {
        this.CurrentNoadsReward += currentNoadsReward;
        this.CurrentNoadsReward = this.CurrentNoadsReward > 5 ? 5 : this.CurrentNoadsReward;
        CPlayerPrefs.SetInt(Helper.Current_Noads_Reward_Key, CurrentNoadsReward);
        return this.CurrentNoadsReward;
    }
    #endregion

#if UNITY_EDITOR
    [Button("Delete Data")]
    void DeleteFileDataJson()
    {
        CPlayerPrefs.DeleteAll();
    }
#endif
}