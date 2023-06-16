using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Assets.Scripts.Data;

[Serializable]
public class GameData
{

    public List<DailyMissions> dailyMissions;
    public List<string> currentItems = new List<string>();

    public GameData()
    {
        dailyMissions = new List<DailyMissions>();
    }
    public void Init()
    {
        InitDailyMission();
        InitShop();

        void InitDailyMission()
        {
            for (int i = 1; i <= 5; i++)
            {
                if (i < 3)
                    dailyMissions.Add(new DailyMissions(i, 1));
                else if (i < 5)
                    dailyMissions.Add(new DailyMissions(i, 2));
                else
                    dailyMissions.Add(new DailyMissions(i, 3));
            }
        }
        void InitShop()
        {
            AddItem("Ball1");
            AddItem("Theme1");
            AddItem("Pin1");
            AddItem("Trail1");
            AddItem("Wall1");
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