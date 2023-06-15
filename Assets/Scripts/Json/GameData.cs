using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Assets.Scripts.Data;

[Serializable]
public class GameData
{

    public List<string> playerSkillPooling;
    
    public GameData()
    {
        
    }

    public GameData(Dictionary<string, int> listCurrentGun)
    {
    }
    public void Init()
    {

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