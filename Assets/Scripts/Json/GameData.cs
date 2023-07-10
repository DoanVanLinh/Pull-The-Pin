using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Assets.Scripts.Data;
using Assets.Scripts.UI.DailyMission;
using Assets.Scripts.UI.Play;

[Serializable]
public class GameData
{

    public List<DailyMissions> dailyMissions;
    public List<MainDailyMissionGiftData> dailyMissionsgift;
    public List<string> currentItems = new List<string>();
    public List<ChallengeData> challenges;
    public GameData()
    {
        dailyMissions = new List<DailyMissions>();
        dailyMissionsgift = new List<MainDailyMissionGiftData>();
        challenges = new List<ChallengeData>();
    }
    public void Init()
    {
        InitShop();
        InitDailyMissionGift();
        InitChallenges();

        void InitShop()
        {
            AddItem("Ball1");
            AddItem("Theme0");
            AddItem("Pin1");
            AddItem("Trail1");
            AddItem("Wall1");

            DataManager.Instance.SetInt("Ball1", 1);
            DataManager.Instance.SetInt("Theme0", 1);
            DataManager.Instance.SetInt("Pin1", 1);
            DataManager.Instance.SetInt("Trail1", 1);
            DataManager.Instance.SetInt("Wall1", 1);
        }
        void InitDailyMissionGift()
        {
            ResetDailyMissionGift();
        }
        void InitChallenges()
        {
            for (int i = 0; i < 6; i++)
            {
                challenges.Add(new ChallengeData((i + 1).ToString(), 200 * (i * 3 + 1), EChalengeType.Lock));
            }
        }
    }

    public void AddItem(string id)
    {
        if (!HasItem(id))
            currentItems.Add(id);
    }
    public bool HasItem(string id)
    {
        return currentItems.Contains(id);
    }

    public void AddDailyMissionValue(EDailyMissionID id, int value)
    {
        int length = dailyMissions.Count;
        for (int i = 0; i < length; i++)
        {
            if (dailyMissions[i].id == id)
            {
                Debug.Log(dailyMissions[i].currentValue + dailyMissions[i].id.ToString() + value);

                dailyMissions[i].currentValue += value;
                if (CanCollected(dailyMissions[i]))
                    ((PlayPanel)UIManager.Instance.gamePlayPanel).dailyMissionNoti.SetActive(true);
            }
        }
    }
    private bool CanCollected(DailyMissions currentDailyMission)
    {
        DailyMissionData data = GameManager.Instance.dailyMissionsData[currentDailyMission.id];
        return (currentDailyMission.currentStatus == EMissionStatus.Skip && currentDailyMission.currentValue >= data.value);
    }
    public void UpdateDailyMission(DailyMissions mission)
    {
        int length = dailyMissions.Count;
        for (int i = 0; i < length; i++)
        {
            if (dailyMissions[i].id == mission.id)
                dailyMissions[i] = mission;
        }
    }
    public void RandomDailyMisson(EDailyMissionID[] id)
    {
        dailyMissions.Clear();
        for (int i = 0; i < 5; i++)
        {
            if (i < 1)
                dailyMissions.Add(new DailyMissions(id[i], 1));
            else if (i < 4)
                dailyMissions.Add(new DailyMissions(id[i], 2));
            else
                dailyMissions.Add(new DailyMissions(id[i], 3));
        }
        DataManager.Instance.SaveData();
    }

    public void ResetDailyMissionGift()
    {
        dailyMissionsgift.Clear();
        for (int i = 0; i < 3; i++)
        {
            dailyMissionsgift.Add(new MainDailyMissionGiftData(i, EMissionStatus.Skip));
        }
    }
    public void UpdateDailyMissionGift(int id, EMissionStatus status)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == id)
                dailyMissionsgift[i].status = status;
        }
    }
    public void SetChallengeStatusById(ChallengeData data)
    {
        SetChallengeStatusById(data.id, data.type);
    }
    public void SetChallengeStatusById(string id, EChalengeType type)
    {
        int length = challenges.Count;
        for (int i = 0; i < length; i++)
        {
            if (challenges[i].id == id)
                challenges[i].type = type;
        }
        DataManager.Instance.SaveData();
    }

    public int GetChallengeReward(string id)
    {
        int length = challenges.Count;
        for (int i = 0; i < length; i++)
        {
            if (challenges[i].id == id)
                return challenges[i].reward;
        }
        return 0;
    }

    public bool HasNewItemInShop()
    {
        int length = currentItems.Count;
        for (int i = 0; i < length; i++)
        {
            if (!DataManager.Instance.HasKey(currentItems[i]))
                return true;
        }
        return false;
    }

    public bool HasNewChallenge()
    {
        int length = challenges.Count;
        for (int i = 0; i < length; i++)
        {
            if (challenges[i].type == EChalengeType.Lock && DataManager.Instance.Coins >= challenges[i].amountUnlock)
                return true;
        }
        return false;
    }
}

[Serializable]
public class CurrentGun
{
    public string gunName;
    public int currentLevel;

    public CurrentGun()
    {
        currentLevel = 0;
    }
}

public class ZoneInfo
{
    public int level;
    public bool isUnlock;
    public float bestTimePlay;

    public ZoneInfo(int level, bool isUnlock, float bestTimePlay)
    {
        this.level = level;
        this.isUnlock = isUnlock;
        this.bestTimePlay = bestTimePlay;
    }
}