using Assets.Scripts.Commons;
using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public static class Helper
{
    public const string BALL_TAG = "Ball";

    public const string COLOR_BALL_LAYER = "ColorBall";
    public const string GREY_BALL_LAYER = "GreyBall";
    public const string BUCK_LAYER = "Buck";
    public const string PIN_LAYER = "Pin";
    public const string BOM_LAYER = "Bom";
    public const string PLAYER_BULLET_LAYER = "PlayerBullet";
    public const string WALL_BULLET_LAYER = "WallBullet";
    public const string ENEMY_IMPACT_ZONE_LAYER = "EnemyImpactZone";
    //Skeleton State
    public const string IDLE_STATE_ANI = "Idle";
    public const string MOVE_STATE_ANI = "Move";
    public const string ATTACK_STATE_ANI = "Attack";
    public const string DEAD_STATE_ANI = "Dead";

    //Sound Name
    public const string SOUND_BUTTON_CLICK = "Button Click";

    public const string ConverStringFormatK = "{0:0.#}K";
    public const string ConverStringFormatM = "{0:0.#}M";
    public const string ConverStringFormatB = "{0:0.#}B";

    #region DataKey
    public static string Current_Coins_Key = "CurrentCoins1";
    public static string Current_Key_Key = "CurrentKey1";
    public static string Current_Ball_Key = "CurrentBall1";
    public static string Current_Theme_Visual_Key = "CurrentKThemeVisual1";
    public static string Current_Pin_Key = "CurrentPin1";
    public static string Current_Trail_Key = "CurrentTrail1";
    public static string Current_Wall_Key = "CurrentWall1";
    public static string Current_Level_Key = "CurrentLevel1";
    public static string Current_Noads_Reward_Key = "CurrentNoadsReward1";
    public static string Current_Theme_Key = "CurrentTheme1";
    public static string Current_Sound_Theme_State_Key = "CurrentSoundThemeState1";
    public static string Current_Sound_Effect_State_Key = "CurrentSoundEffectState1";
    public static string Current_Vibrate_State_Key = "CurrentVibrateState1";
    public static string Current_Stage_Key = "CurrentState1";
    public static string Max_Zone_Key = "MaxZone1";
    public static string Current_Count_Play_Key = "CurrentCountPlay1";
    public static string Current_Count_Daily_Reward_Key = "CurrentCountDailyReward1";
    public static string Is_No_Ads_Key = "NoAds1";
    public static string Gift_Percent_Key = "GiftPercent1";
    public static string Current_Streak_Key = "CurrentStreak1";
    public static string First_Gift_Key = "FristGift1";

    public static string Another_Puzzle_Placement = "AnotherPuzzle";
    public static string Gift_Placement = "Gift";
    public static string Extra_Coins_Win_Placement = "ExtraCoinsWin";
    public static string Skip_Level_Placement = "SkipLevel";
    public static string Resume_Level_Placement = "ResumeLevel";
    public static string Skip_Daily_Mission_Placement = "SkipDailyMission";
    public static string Int_Capping_Placement = "CappingInt";
    public static string Fail_Challenge_Placement = "FailChallenge";
    public static string Play_Again_Challenge_Placement = "PlayAgainChallenge";
    #endregion


    public const string OPEN_LINK_RATE = "market://details?id=" + "com.drag.mushrooms.survival";
    public const string NO_ADS_PACK_ID = "com.drag.mushrooms.survival.noads";
    public const string COMMON_ITEM_PACK_ID = "com.drag.mushrooms.survival.commonitem";
    public const string IMMORTAL_ITEM_PACK_ID = "com.drag.mushrooms.survival.immortaltem";
    public const string ACIENT_ITEM_PACK_ID = "com.drag.mushrooms.survival.acienttem";
    public const string COINS_PACK_ID = "com.drag.mushrooms.survival.coins";

    public static Action OnNewTarget;

    public static string EnumToString<T>(T enumT)
    {
        return enumT.ToString();
    }
    public static T StringToEnum<T>(string value)
    {
        object result;
        if (Enum.TryParse(typeof(T), value, out result))
            return (T)Enum.Parse(typeof(T), value);

        return default(T);
    }
    public static bool IsOverUI(int layer = 5)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Where(r => r.gameObject.layer == layer).ToList().Count > 0;
    }
    public static void PushNotification(string content)
    {
        UIManager.Instance.notification.PushNotification(content);
    }
    public static string ConvertCoins(int value)
    {
        int integeral = value;
        int diveTime = 0;

        while (integeral != 0)
        {
            diveTime++;
            integeral /= 1000;
        }

        switch (diveTime)
        {
            case 0:
            case 1:
                return value.ToString();
            case 2:
                return value / 1000 + " K";
            case 3:
                return value / 1000000 + " M";
            default:
                return value / 1000000000 + " B";
        }
    }
    public static float ParseFloat(string data)
    {
        if (string.IsNullOrEmpty(data))
            return 0;

#if UNITY_ANDROID
        return float.Parse(data);
#else
        float f = float.Parse(data, CultureInfo.InvariantCulture);
        return f;
#endif
    }

    public static bool RandomPiority(float minRange, float totalRange)
    {
        return Random.Range(0, totalRange) < minRange;
    }

    public static DateTime StringToDateTime(string time)
    {
        return DateTime.Parse(time);
    }

    #region Item Equiq
    //public static ItemRare RandomRare()
    //{
    //    Dictionary<ItemRare, float> itemProbability = new Dictionary<ItemRare, float>()
    //    {
    //        { ItemRare.Ancient,0.00004f },
    //        { ItemRare.Arcana,0.0001f},
    //        { ItemRare.Immortal,0.000256f},
    //        { ItemRare.Legendary,0.00064f},
    //        { ItemRare.Mythical,0.0016f},
    //        { ItemRare.Rare,0.004f},
    //        { ItemRare.Common,0.01f},
    //    };

    //    foreach (KeyValuePair<ItemRare, float> item in itemProbability)
    //    {
    //        if (RandomPiority(item.Value, 100))
    //            return item.Key;
    //    }

    //    return ItemRare.Common;
    //}

    //public static ItemStatsLite RandomDropItemEquip()
    //{
    //    ItemRare randomRare = RandomRare();

    //    string nameItem = GameManager.Instance.itemData.items.Where(i => i.Value.HasRare(randomRare))
    //                                        .OrderBy(o => Random.Range(-1f, 1f))
    //                                        .FirstOrDefault().Key;

    //    return new ItemStatsLite(nameItem, randomRare);
    //}
    //public static ItemStatsLite ItemEquipByRare(ItemRare randomRare)
    //{
    //    string nameItem = GameManager.Instance.itemData.items.Where(i => i.Value.HasRare(randomRare))
    //                                        .OrderBy(o => Random.Range(-1f, 1f))
    //                                        .FirstOrDefault().Key;

    //    return new ItemStatsLite(nameItem, randomRare);
    //}
    //public static List<ItemStatsLite> RandomDropItemEquip(int count)
    //{
    //    List<ItemStatsLite> items = new List<ItemStatsLite>();

    //    for (int i = 0; i < count; i++)
    //    {
    //        items.Add(RandomDropItemEquip());
    //    }
    //    return items;
    //}


    #endregion

    #region Skill
    //public static BaseSkill[] AvilablePlayerSkills()
    //{
    //    return BasePlayer.Instance.allSkills.Where(s => s.CanDrop &&
    //                                              BasePlayer.Instance.skillPooling.Any(sp => sp == s.nameSkill) &&
    //                                              (s.playerType == PlayerType.All || BasePlayer.Instance.childs.Any(c => c.PlayerType == s.playerType))).ToArray();
    //}

    //public static T RandomPiority<T>(List<T> groupRandom) where T : BaseSkill
    //{
    //    int lenghtRandomGroup = groupRandom.Count;
    //    groupRandom = groupRandom.OrderBy(o => Random.Range(-1f, 1f)).ToList();
    //    if (lenghtRandomGroup == 0) return null;

    //    #region random
    //    float total = 0;

    //    for (int i = 0; i < groupRandom.Count; i++)
    //    {
    //        total += groupRandom[i].priority;
    //    }

    //    float currentPercentage = 0f;

    //    for (int i = 0; i < lenghtRandomGroup; i++)
    //    {
    //        currentPercentage += groupRandom[i].priority;

    //        if (RandomPiority(currentPercentage, total))
    //            return groupRandom[i];
    //    }
    //    #endregion

    //    return groupRandom[0];
    //}
    //public static List<T> RandomAvilableSkillByPriority<T>(List<T> groupRandom, int takenCount) where T : BaseSkill
    //{
    //    int lenghtRandomGroup = groupRandom.Count;
    //    if (lenghtRandomGroup == 0 || takenCount == 0)
    //        return null;

    //    if (lenghtRandomGroup <= takenCount)
    //        return groupRandom;

    //    List<T> takenSkills = new List<T>();
    //    while (takenSkills.Count != takenCount)
    //    {
    //        T skill = RandomPiority(groupRandom);

    //        if (skill == null)
    //            continue;

    //        takenSkills.Add(skill);
    //        groupRandom.Remove(skill);
    //    }

    //    return takenSkills;
    //}
    #endregion

    #region Enemy
    //public static EnemyInZone RandomEnemyPiority(List<EnemyInZone> groupRandom, float time)
    //{

    //    int lenghtRandomGroup = groupRandom.Count;

    //    if (lenghtRandomGroup == 0) return null;

    //    #region random
    //    float total = 0;

    //    for (int i = 0; i < groupRandom.Count; i++)
    //    {
    //        total += groupRandom[i].priorityOverTime.Evaluate(time);
    //    }

    //    //float randomValue = Random.Range(0f, total);

    //    //if (randomValue == 0) return groupRandom[0];

    //    float currentPercentage = 0f;

    //    for (int i = 0; i < lenghtRandomGroup; i++)
    //    {
    //        currentPercentage += groupRandom[i].priorityOverTime.Evaluate(time);

    //        if (RandomPiority(currentPercentage, total))
    //            return groupRandom[i];
    //    }
    //    #endregion
    //    return groupRandom[0];
    //}
    //public static BaseEnemy GetInRangeTarget(Vector3 pos, float MaxRangeCheck, LayerMask layerEnemy)
    //{
    //    int rangeCast = 1;
    //    BaseEnemy e = null;
    //    while (e == null && rangeCast <= MaxRangeCheck)
    //    {
    //        Collider2D hit = Physics2D.OverlapCircle(pos, rangeCast, layerEnemy);
    //        if (hit != null)
    //        {
    //            e = hit.GetComponent<BaseEnemy>();
    //            if (e != null)
    //            {
    //                return e;
    //            }
    //        }
    //        rangeCast++;
    //    }
    //    return e;
    //}
    //public static BaseEnemy GetInRangeTarget(Vector3 pos, float MaxRangeCheck, LayerMask layerEnemy, BaseEnemy exceptEnemy)
    //{
    //    int rangeCast = 1;
    //    BaseEnemy e = null;
    //    while (e == null && rangeCast <= MaxRangeCheck)
    //    {
    //        Collider2D hit = Physics2D.OverlapCircle(pos, rangeCast, layerEnemy);
    //        if (hit != null)
    //        {
    //            e = hit.GetComponent<BaseEnemy>();
    //            if (e != null && e != exceptEnemy)
    //            {
    //                return e;
    //            }
    //        }
    //        rangeCast++;
    //    }
    //    return e;
    //}
    //public static BaseEnemy SpawnSingleEnemy(string name, Vector2 position, float hp, float damage, float exp, bool isBoss = false, float size = 1)
    //{
    //    BaseEnemy enemy = (BaseEnemy)ObjectPooler.Instance.Spawn(name, position, Quaternion.identity, Vector3.one * size);
    //    if (enemy == null)
    //        return null;

    //    enemy.Init(hp, damage, exp, isBoss);

    //    return enemy;
    //}

    #endregion
    public static Vector3 GetDirectionFromAngle(float angle)
    {
        float horizontal = Cos(angle);
        float vertical = Sin(angle);
        Vector3 dir = new Vector3(horizontal, vertical, 0);
        return dir;
    }
    public static float Sin(float angle)
    {
        return Mathf.Sin(angle * Mathf.Deg2Rad);
    }

    public static float Cos(float angle)
    {
        return Mathf.Cos(angle * Mathf.Deg2Rad);
    }
    public static float getAngle(Vector3 dir)
    {
        return getAngle(dir.x, dir.z);
    }

    public static float getAngle(float x, float y)
    {
        float getAngle_angle = 90f - Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        getAngle_angle = (getAngle_angle + 360f) % 360f;
        return GetFloat2(getAngle_angle);
    }
    public static float GetFloat2(float f)
    {
        return (float)((int)(f * 100f)) / 100f;
    }
    public static Vector3 GetPositionFromAngle(Vector3 startPos, float angle, float distance)
    {
        float horizontal = Cos(angle);
        float vertical = Sin(angle);
        Vector3 dir = new Vector3(horizontal, vertical, 0);
        Vector3 pos = startPos + dir * distance;
        return pos;
    }
    public static string ConvertNumber(int value)
    {
        int integeral = value;
        int diveTime = 0;

        while (integeral != 0)
        {
            diveTime++;
            integeral /= 1000;
        }

        switch (diveTime)
        {
            case 0:
            case 1:
                return value.ToString();
            case 2:
                return string.Format(ConverStringFormatK, value / 1000f);
            case 3:
                return string.Format(ConverStringFormatM, value / 1000000f);
            default:
                return string.Format(ConverStringFormatB, value / 1000000000f);
        }
    }

    public static string ConvertTimer(double time)
    {
        TimeSpan interval = TimeSpan.FromSeconds(time);

        if (interval.Days == 0)
        {
            if (interval.Hours == 0)
            {
                return interval.ToString(@"mm\:ss");
            }
            else
            {
                string timeSpan = interval.ToString(@"hh\:mm");

                timeSpan = timeSpan.Split(':')[0] + "h:" + timeSpan.Split(':')[1] + "m";

                return timeSpan;
            }
        }
        else
        {
            string timeSpan = interval.ToString(@"dd\:hh");

            timeSpan = timeSpan.Split(':')[0] + "d:" + timeSpan.Split(':')[1] + "h";

            return timeSpan;
        }

    }
    public static void SpawnTextPopup(Vector3 position, string content, TypeText type)
    {
        CommonSpriteTextPopup text = ((CommonSpriteTextPopup)ObjectPooler.Instance.Spawn("CommonSpriteTextPopUp", position + (Vector3)Random.insideUnitCircle * 0.2f));
        if (text == null) return;

        text?.LoadText(content, type);
    }
    public static string ParseTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        return timeSpan.ToString(@"mm\:ss");
    }
    public static bool IsOutOfView(Vector2 position)
    {
        Vector2 pointInScreen = Camera.main.WorldToViewportPoint(position);

        return pointInScreen.x < 0 || pointInScreen.x > 1 || pointInScreen.y < 0 || pointInScreen.y > 1;
    }

    public static CommonSound SpawnSound(string soundName, float duration = 0)
    {
        CommonSound sound = (CommonSound)ObjectPooler.Instance.Spawn(soundName);
        if (sound != null)
        {
            sound.timeDispose = duration;
            sound.AutoDisPose();
        }
        return sound;
    }

#if UNITY_EDITOR
    public static IEnumerator IELoadData(string urlData, System.Action<string> actionComplete, bool showAlert = false)
    {
        var www = new WWW(urlData);
        float time = 0;
        //TextAsset fileCsvLevel = null;
        while (!www.isDone)
        {
            time += 0.001f;
            if (time > 10000)
            {
                yield return null;
                Debug.Log("Downloading...");
                time = 0;
            }
        }
        if (!string.IsNullOrEmpty(www.error))
        {
            UnityEditor.EditorUtility.DisplayDialog("Notice", "Load CSV Fail", "OK");
            yield break;
        }
        yield return null;
        actionComplete?.Invoke(www.text);
        yield return null;
        UnityEditor.AssetDatabase.SaveAssets();
        if (showAlert)
            UnityEditor.EditorUtility.DisplayDialog("Notice", "Load Data Success", "OK");
        else
            Debug.Log("<color=yellow>Download Data Complete</color>");
    }
#endif
}

