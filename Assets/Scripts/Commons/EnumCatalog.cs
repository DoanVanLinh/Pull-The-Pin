using System.Collections;
using UnityEngine;

public enum GameState
{
    MainUi,
    Gameplay,
    Pause,
    Win,
    Lose,
    Null
}
public enum GameType
{
    Campaign,
    Tutorial,
    Endless,
}
public enum EAnimationState
{
    None = 0,
    Idle = 1,
    Move = 2,
    START = 3,
    LOOP = 4,
    END = 5,
    Attack = 6,
    Chasing = 7,

}
public enum TypeChest
{
    Common,
    Upgrade
}
public enum UnlockType
{
    Free,
    Coin,
    Key,
}

public enum TypeText
{
    CommonDame,
    CritDame,
    FireDame,
    LeafDame,
    IceDame,
    Health,
    HeadShot,
    LightningDame,
    WaterDame
}

public enum TypeSound
{
    Theme,
    Sfx
}

public enum EBallType
{
    Color,
    Grey
}
public enum EMissionStatus
{
    Skip,
    Collect,
    Collected
}
public enum EItemType
{
   Ball,
   Theme,
   Pin,
   Trail,
   Wall
}
public enum EItemUnlockType
{
    Coins,
    LevelComplete,
    LuckyWheel,
    DailyMission,
    GiftBox,
    Streak
}

public enum EStageType
{
    Lock,
    Playing,
    Complete
}
public enum ELoseType
{
    BomBall,
    BomBuck,
    LoseBall,
    CollectGreyBall
}

public enum EDailyRewardType
{
    Coins,
    Item,
    Random
}