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
        SetupKey();
        if (isTest)
        {
            SetCurrentZone(level);
        }
        if (Instance == null)
        {
            Instance = this;
            gameData = new GameData();
            if (CPlayerPrefs.HasKey(nameFile))
                LoadData();
        }
        else
            Destroy(gameObject);

        CPlayerPrefs.GenereateNewKey();

        DontDestroyOnLoad(this);
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

    [Button("Add Item")]
    private void AddItem(string name, ItemRare itemRare)
    {
        gameData.AddItem(name, itemRare);
    }
    [Button("Remove Item")]
    private void RemoveItem(string name, ItemRare itemRare)
    {
        gameData.RemoveItem(name, itemRare);
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
    public string CurrentWeapon { get; set; }
    public string CurrentCharater { get; set; }
    public string CurrentKeyReward { get; set; }
    public int CurrentHpUpgrade { get; set; }
    public int CurrentDamageUpgrade { get; set; }
    public int CurrentCoinUpgrade { get; set; }
    public int CurrentReviveTime { get; set; }
    public int CurrentZone { get; set; }
    public int MaxZone { get; set; }
    public int CurrentSlotItem { get; set; }
    public int CountPlay { get; set; }
    public bool IsNoads { get; set; }
    public bool IsTutoLevelUp { get; set; }
    public bool IsTutoCircleFire { get; set; }
    public string IntervalCommonItem { get; set; }
    public string IntervalImmortalItem { get; set; }
    void SetupKey()
    {
        Coins = CPlayerPrefs.GetInt(Helper.Current_Coins_Key, 0);
        Key = CPlayerPrefs.GetInt(Helper.Current_Key_Key, 0);
        //CurrentLevel = CPlayerPrefs.GetInt(Helper.Current_Level_Key, 1);
        CurrentNoadsReward = CPlayerPrefs.GetInt(Helper.Current_Noads_Reward_Key, 0);
        CurrentTheme = CPlayerPrefs.GetInt(Helper.Current_Theme_Key, 1);
        CurrentSoundThemeState = CPlayerPrefs.GetInt(Helper.Current_Sound_Theme_State_Key,1);
        CurrentSoundEffectState = CPlayerPrefs.GetInt(Helper.Current_Sound_Effect_State_Key, 1);
        CurrentVibrateState = CPlayerPrefs.GetInt(Helper.Current_Vibrate_State_Key, 1);
        CurrentWeapon = CPlayerPrefs.GetString(Helper.Current_Weapon_Key, "PISTOL_GUN");
        CurrentCharater = CPlayerPrefs.GetString(Helper.Current_Character_Key, "Player 1");
        CurrentKeyReward = CPlayerPrefs.GetString(Helper.Current_Key_Reward_Key, "Skin 1");
        CurrentCoinUpgrade = CPlayerPrefs.GetInt(Helper.Current_Coin_Upgrade_Key, 0);
        CurrentHpUpgrade = CPlayerPrefs.GetInt(Helper.Current_Hp_Upgrade_Key, 0);
        CurrentDamageUpgrade = CPlayerPrefs.GetInt(Helper.Current_Damage_Upgrade_Key, 0);
        CurrentReviveTime = CPlayerPrefs.GetInt(Helper.Current_Revive_Time_Key, 1);
        CurrentZone = CPlayerPrefs.GetInt(Helper.Current_Zone_Key, 1);
        MaxZone = CPlayerPrefs.GetInt(Helper.Max_Zone_Key, 1);
        CurrentSlotItem = CPlayerPrefs.GetInt(Helper.Current_Slot_Item_Key, 1);
        CountPlay = CPlayerPrefs.GetInt(Helper.Current_Count_Play_Key, 0);
        IsNoads = CPlayerPrefs.GetBool(Helper.Is_No_Ads_Key, false);
        IsTutoLevelUp = CPlayerPrefs.GetBool(Helper.Is_Tut_Level_Up_Key, false);
        IsTutoCircleFire = CPlayerPrefs.GetBool(Helper.Is_Tut_Circle_fire_Key, false);
        IntervalCommonItem = CPlayerPrefs.GetString(Helper.Interval_Common_Item_Key, DateTime.Now.ToString());
        IntervalImmortalItem = CPlayerPrefs.GetString(Helper.Interval_Immortal_Item_Key, DateTime.Now.ToString());

    }

    #region DailyMission
    //public MissionType[] GetDailyMissionType()
    //{
    //    string key = DateTime.Now.Date.ToString();


    //    string currentMission = PlayerPrefs.GetString(key + "_DailyMissionType", "");
    //    //string currentMission = PlayerPrefs.GetString(key + "_DailyMission", randomMission[0] + "_" + randomMission[1] + "_" + randomMission[2]);
    //    if (currentMission == "")
    //    {
    //        string[] missions = Enum.GetNames(typeof(MissionType));
    //        missions = missions.Where(m => m != MissionType.WinTimeAttack.ToString() &&
    //                                    m != MissionType.WacthAds.ToString() &&
    //                                    m != MissionType.CompleteMission.ToString()
    //                                       ).OrderBy(o => UnityEngine.Random.Range(-1f, 1f)).ToArray();

    //        PlayerPrefs.SetString(key + "_DailyMissionType", missions[0] + "_" + missions[1] + "_" + missions[2]);
    //        return new MissionType[] { (MissionType)Enum.Parse(typeof(MissionType), missions[0]),
    //                                (MissionType)Enum.Parse(typeof(MissionType), missions[1]),
    //                                MissionType.WinTimeAttack,
    //                                MissionType.WacthAds,
    //                                MissionType.CompleteMission };
    //    }
    //    else
    //    {
    //        string[] currentMissions = currentMission.Split('_');

    //        return new MissionType[] { (MissionType)Enum.Parse(typeof(MissionType), currentMissions[0]),
    //                                (MissionType)Enum.Parse(typeof(MissionType), currentMissions[1]),
    //                                (MissionType)Enum.Parse(typeof(MissionType), currentMissions[2]),
    //                                MissionType.WinTimeAttack,
    //                                MissionType.WacthAds,
    //                                MissionType.CompleteMission };
    //    }
    //}
    //public void SetDailyMissionKey(MissionType missionType, float value)
    //{
    //    PlayerPrefs.SetFloat(DateTime.Now.Date.ToString() + missionType.ToString(), value);
    //}
    //public float GetDailyMissionKey(MissionType missionType)
    //{
    //    return PlayerPrefs.GetFloat(DateTime.Now.Date.ToString() + missionType.ToString(), 0);
    //}
    //public string GetDailyMissionName()
    //{
    //    string key = DateTime.Now.Date.ToString();

    //    return PlayerPrefs.GetString(key + "_DailyMissionName", "");
    //}
    //public bool CheckDaiLyMissionCollected(MissionType missionType)
    //{
    //    string key = DateTime.Now.Date.ToString();

    //    return PlayerPrefs.HasKey(key + missionType.ToString() + "_Collected");
    //}
    //public void SaveDaiLyMissionCollected(MissionType missionType)
    //{
    //    string key = DateTime.Now.Date.ToString();

    //    PlayerPrefs.SetString(key + missionType.ToString() + "_Collected", "Collected");
    //}
    //public void SetDailyMissionName(string value)
    //{
    //    string key = DateTime.Now.Date.ToString();

    //    PlayerPrefs.SetString(key + "_DailyMissionName", value);
    //}
    #endregion

    public void SetIntervalCommonItem(string day)
    {
        IntervalCommonItem = day;
        CPlayerPrefs.SetString(Helper.Interval_Common_Item_Key, IntervalCommonItem);
    }
    public void SetIntervalImmortalItem(string day)
    {
        IntervalImmortalItem = day;
        CPlayerPrefs.SetString(Helper.Interval_Immortal_Item_Key, IntervalImmortalItem);
    }

    public void SetTutoLevelUp(bool isTutorialed)
    {
        IsTutoLevelUp = isTutorialed;
        CPlayerPrefs.SetBool(Helper.Is_Tut_Level_Up_Key, IsTutoLevelUp);
    }
    public void SetTutoCircleFire(bool isTutorialed)
    {
        IsTutoCircleFire = isTutorialed;
        CPlayerPrefs.SetBool(Helper.Is_Tut_Circle_fire_Key, IsTutoCircleFire);
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
    public void AddCurrentCoinUpgrade(int id)
    {
        CurrentCoinUpgrade += id;
        CPlayerPrefs.SetInt(Helper.Current_Coin_Upgrade_Key, CurrentCoinUpgrade);
    }
    public void AddCurrentHpUpgrade(int id)
    {
        CurrentHpUpgrade += id;
        CPlayerPrefs.SetInt(Helper.Current_Hp_Upgrade_Key, CurrentHpUpgrade);
    }
    public float GetBestTime()
    {
        return CPlayerPrefs.GetFloat("Zone " + CurrentZone, 0);
    }
    public void SetBestTime(float time)
    {
        if (time > CPlayerPrefs.GetFloat("Zone " + CurrentZone, 0))
        {
            CPlayerPrefs.SetFloat("Zone " + CurrentZone, time);
        }
    }
    public void AddCurrentDamageUpgrade(int id)
    {
        CurrentDamageUpgrade += id;
        CPlayerPrefs.SetInt(Helper.Current_Damage_Upgrade_Key, CurrentDamageUpgrade);
    }
    public void SetCurrentWeapon(string id)
    {
        CurrentWeapon = id;
        CPlayerPrefs.SetString(Helper.Current_Weapon_Key, id);
    }
    public void SetCurrentTheme(int id)
    {
        CurrentTheme = id;
        CPlayerPrefs.SetInt(Helper.Current_Theme_Key, id);
    }
    public void SetCurrentZone(int id)
    {
        CurrentZone = id;
        CPlayerPrefs.SetInt(Helper.Current_Zone_Key, id);
    }
    //public void AddCurrentMaxZone(int id)
    //{
    //    if (CurrentZone + id > MaxZone && CurrentZone + id <= EnemyController.Instance.allZone.Length)
    //    {
    //        MaxZone = CurrentZone + id;
    //        CPlayerPrefs.SetInt(Helper.Max_Zone_Key, MaxZone);
    //    }
    //}
    public void AddCurrentZone(int id)
    {
        CurrentZone += id;
        if (CurrentZone > MaxZone)
        {
            CurrentZone = MaxZone;
        }
        if (CurrentZone < 1)
        {
            CurrentZone = 1;
        }
        //UnlockNewFeature();
        CPlayerPrefs.SetInt(Helper.Current_Zone_Key, CurrentZone);
    }
    //private void UnlockNewFeature()
    //{
    //    switch (CurrentZone)
    //    {

    //        case 2:
    //            GetData().AddNewSkillToPool(new string[1] {
    //                                            "TO_LIGHTNING"
    //                                        });
    //            break;
    //        case 3:
    //            GetData().AddNewSkillToPool(new string[1] {
    //                                            "LIGHTNING_CHILD"
    //                                        });
    //            break;
    //        case 5:
    //            GetData().UnlockSlotItem(1);
    //            break;
    //        case 10:
    //            GetData().UnlockSlotItem(2);
    //            break;
    //        case 15:
    //            GetData().UnlockSlotItem(3);
    //            break;
    //    }
    //    SaveData();
    //}

    public void SetCurrentReviveTime(int count)
    {
        CurrentReviveTime = count;
        CPlayerPrefs.SetInt(Helper.Current_Revive_Time_Key, count);
    }
    public void AddCurrentReviveTime(int count)
    {
        CurrentReviveTime += count;
        CPlayerPrefs.SetInt(Helper.Current_Revive_Time_Key, CurrentReviveTime);
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
    public void SetCurrentCharacter(string id)
    {
        CurrentCharater = id;
        CPlayerPrefs.SetString(Helper.Current_Character_Key, id);
    }
    public void SetCurrentKeyReward(string id)
    {
        CurrentKeyReward = id;
        CPlayerPrefs.SetString(Helper.Current_Key_Reward_Key, id);
    }
    public void SetCurrentHpUpgrade(int id)
    {
        CurrentHpUpgrade = id;
        CPlayerPrefs.SetInt(Helper.Current_Hp_Upgrade_Key, id);
    }
    public void SetCurrentDamageUpgrade(int id)
    {
        CurrentDamageUpgrade = id;
        CPlayerPrefs.SetInt(Helper.Current_Damage_Upgrade_Key, id);
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

    #region itemShop
    //public void GetCurrentItems()
    //{
    //    foreach (var item in shopItems)
    //    {
    //        if (item.owned)
    //        {
    //            AddItem(item);
    //            continue;
    //        }

    //        string idItem = item.type.ToString() + "_" + item.id;

    //        if (CPlayerPrefs.HasKey(idItem))
    //            currentItems.Add(idItem, true);
    //        else
    //            currentItems.Add(idItem, false);
    //    }
    //}
    //public void GetAllShopItem()
    //{
    //    shopItems = GameManager.Instance.GetAllShopItem();
    //}

    //public void AddItem(ShopItem item)
    //{
    //    string idItem = item.type.ToString() + "_" + item.id;
    //    CPlayerPrefs.SetInt(idItem, 1);
    //    currentItems[idItem] = true;
    //}
    //public bool CheckOwnedItem(ShopItem item)
    //{
    //    return CPlayerPrefs.HasKey(item.type.ToString() + "_" + item.id);
    //}

    //public ShopItem GetRandomItemShopNotUnlock()
    //{
    //    return shopItems.Where(i => !CheckOwnedItem(i)).OrderBy(o => Random.Range(-1f, 1f)).FirstOrDefault();
    //}

    //public void ClearKey()
    //{
    //    CPlayerPrefs.DeleteAll();
    //    SceneManager.LoadScene(0);
    //    Debug.Log(GetDailyMissionKey(MissionType.TimePlay) * 60f);
    //    DontDestroy.Instance.timePlayed = GetDailyMissionKey(MissionType.TimePlay) * 60f;
    //}

    //void LoadCurrentItems()
    //{
    //    GameManager.Instance.ChangeCharacter(shopItems.Where(i => i.type == ItemType.Character && i.id == CurrentModeling).FirstOrDefault());
    //    GameManager.Instance.ChangeWeapon(shopItems.Where(i => i.type == ItemType.Weapon && i.id == CurrentWeapon).FirstOrDefault());
    //    //GameManager.Instance.ChangePlayerSkin(shopItems.Where(i => i.type == ItemType.CharacterSkin && i.id == CurrentCharater).FirstOrDefault());
    //    //GameManager.Instance.ChangeAddonPlayer(shopItems.Where(i => i.type == ItemType.AddOn && i.id == CurrentAddOn).FirstOrDefault());
    //    //GameManager.Instance.ChangeTrail(shopItems.Where(i => i.type == ItemType.Trail && i.id == CurrentTrail).FirstOrDefault());
    //}
    //public void SetDefaultEachModeling(string idModeling)
    //{
    //    ShopItem defaultSkin = shopItems.Where(i => i.type == ItemType.CharacterSkin && (i.idModeling.Contains(idModeling) || i.idModeling.Contains(""))).FirstOrDefault();
    //    ShopItem defaultAddon = shopItems.Where(i => i.type == ItemType.AddOn && (i.idModeling.Contains(idModeling) || i.idModeling.Contains(""))).FirstOrDefault();

    //    SetCurrentCharacter(defaultSkin.id);
    //    SetCurrentAddOn(defaultAddon.id);

    //    GameManager.Instance.ChangeAddonPlayerPreview(defaultAddon);
    //    GameManager.Instance.ChangePlayerSkin(defaultSkin);
    //}
    #endregion
    #endregion

#if UNITY_EDITOR
    [Button("Delete Data")]
    void DeleteFileDataJson()
    {
        CPlayerPrefs.DeleteAll();
    }
#endif
}