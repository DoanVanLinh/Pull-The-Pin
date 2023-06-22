using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.Data;

[Serializable]
public class DailyMissions
{
    public EDailyMissionID id;
    public int currentValue;
    public int currentLevel;
    public EMissionStatus currentStatus;

    public DailyMissions(EDailyMissionID id,int level)
    {
        this.id = id;
        this.currentValue = 0;
        this.currentLevel = level;
        this.currentStatus = EMissionStatus.Skip;
    }
}
