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
public enum SpawmType
{
    Single,
    Group,
    Sequentially
}
public enum GameType
{
    Campaign,
    Tutorial,
    Endless,
}
public enum EffectType
{
    Buff,
    DeBuff
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
public enum SkillType
{
    Active_Skill,
    Weapon_Skill,
    Bullet_Effect_Skill,
    Passive_Skill,
}
public enum EActiveSkill
{
    SUMMON_DRONE_TYPE_A,
    SUMMON_DRONE_TYPE_B,
    PISTOL_GUN,
    SHOT_GUN,
    ROCKET_GUN,
    LAZER_GUN,
    CANNON_GUN,
    FIRE_BALL_GUN,
    BLADE_SAW_GUN,
    MACHINE_GUN,
    MOLOTOV,
    MOON_SLASH,
    DROP_A_BOMB,
    ROCKET_RAIN,
    SHARIKAN,
    FREZING_FIELD,
    POWER_KICK,
    HOLY_FIELD,
    FIRE_STEP,
    SUMMON_FROG,
    INVISIBLE_CLOAK,
    BULLET_KING,
    SAW,
    DEATH_LAZER,
    LIGHTNING,
    ASAYO,
    TOXIC_ZONE,
    ROCKET,
    CIRCLE_FIRE,
    LEAF_CHILD,
    ICE_CHILD,
    FIRE_CHILD,
    LIGHTNING_CHILD,
    WATER_CHILD,
}
public enum EPassiveSkill
{
    ATTACK_PLUS,
    CRIT_CHANCE_PLUS,
    SPEED_PLUS,
    CRIT_MASTER,
    MORE_GUN,
    KRAZE,
    REBORN,
    THICKER,
    LUCKY_MAN,
    DCD,
    INCREASE_AREA,
    FREE_CHESS,
    INCREASE_GUN,
    TO_FIRE,
    TO_LEAF,
    TO_ICE,
    TO_LIGHTNING,
    TO_WATER,
}
public enum EBulletEffectSkill
{
    SHADOW_CLONE,
    RICOCHET,
    BOLT,
    PIERCING_SHOT,
    BLOODY_THIRST,
    HEAD_SHOT,
    FREEZING_FIELD,
    DEATH_BOOMB,
    DEATH_NOVA,
    DEATH_BLACKHOLE,
    BOUNCING_BULLET,
    SHATTER_SHOT,
    LIGHTNING_BALL,
    DEATH_WATER,
}
public enum EWeaponSkill
{
    MULTI_SHOT,
    FRONT_BULLET_x2,
    DIAGONAL_BULLET,
    SIDE_BULLET,
    BACK_SHOT,
    DIAGONAL_BACK_BULLET,
}
public enum TypeChest
{
    Common,
    Upgrade
}
public enum GolobalUpgrade
{
    Hp_Increase,
    Attack_Increase,
    Reward_Increase,
}

public enum EIconOutOfView
{
    Boss,
    Chest,
    Room
}
public enum EItemInGame
{
    Null,
    Small_Coin,
    Normal_Coin,
    Big_Coin,
    Magnetic,
    Bomb,
    Heal,
    Exp,
    Boss_Chest,
    EndlessKey,
    Child,
    Skill
}
public enum ItemRare
{
    Ancient,
    Arcana,
    Immortal,
    Legendary,
    Mythical,
    Rare,
    Common,
}

public enum InventoryStatus
{
    Equiq,
    Merge
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

public enum SkillChoseElementType
{
    LevelUp,
    Gift,
    RoomSkill
}

public enum PlayerType
{
    All,
    COMMON,
    LEAF,
    FIRE,
    ICE,
    LIGHTNING,
    EARTH,
    WATER,
    WIND,
    MYSTERY
}

public enum ShopElementType
{
    Noads,
    CommonItem,
    ImmortalItem,
    AncientItem,
    Coins
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