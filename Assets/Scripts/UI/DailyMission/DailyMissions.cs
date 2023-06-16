using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.Data;

[Serializable]
public class DailyMissions
{
    public int id;
    public int currentValue;
    public int currentLevel;
    public EMissionStatus currentStatus;

    public DailyMissions(int id,int level)
    {
        this.id = id;
        this.currentValue = 0;
        this.currentLevel = level;
        this.currentStatus = EMissionStatus.Skip;
    }
}
