using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Assets.Scripts.Data;

[Serializable]
public class GameData
{
    [ShowInInspector]
    public Dictionary<string, int> listCurrentGun;
    [ShowInInspector]
    public Dictionary<ItemRare, List<string>> listCurrentItems;
    //[ShowInInspector]
    //public Dictionary<int, ItemStatsLite> listEqipedItems;

    public List<string> playerSkillPooling;

    public GameData()
    {
        this.listCurrentGun = new Dictionary<string, int>();
        listCurrentGun.Add("PISTOL_GUN", 0);

        //Skill
        playerSkillPooling = new List<string>();
        playerSkillPooling.AddRange(
            new string[]
            {
                "ATTACK_PLUS",
                "CIRCLE_FIRE",
                "TO_FIRE",
                "TO_LEAF",
                "TO_WATER",
                "FIRE_CHILD",
                "LEAF_CHILD",
                "WATER_CHILD",
            }
        );

        listCurrentItems = new Dictionary<ItemRare, List<string>>();
        string[] itemsRare = Enum.GetNames(typeof(ItemRare));
        int length = itemsRare.Length;
        for (int i = 0; i < length; i++)
        {
            listCurrentItems.Add(Helper.StringToEnum<ItemRare>(itemsRare[i]), new List<string>());
        }

        //listEqipedItems = new Dictionary<int, ItemStatsLite>();
        //listEqipedItems.Add(0, null);
    }

    public GameData(Dictionary<string, int> listCurrentGun)
    {
        this.listCurrentGun = listCurrentGun;
    }

    public void UnlockOrUpgradeGun(string gunName)
    {
        if (IsGunUnlock(gunName))
            listCurrentGun[gunName]++;
        else
            listCurrentGun.Add(gunName, 0);
        DataManager.Instance.SaveData();
    }
    public bool IsGunUnlock(string gunName)
    {
        return listCurrentGun.ContainsKey(gunName);
    }
    public int GetCurrentLevelGun(string gunName)
    {
        return IsGunUnlock(gunName) ? listCurrentGun[gunName] : -1;
    }

    public void AddItem(string itemName, ItemRare itemRare, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            listCurrentItems[itemRare].Add(itemName);
        }
    }
    //public void AddItem(ItemStatsLite item, int amount = 1)
    //{
    //    string itemName = item.nameItems;
    //    ItemRare itemRare = item.rare;
    //    AddItem(itemName, itemRare, amount);
    //}
    public void RemoveItem(string itemName, ItemRare itemRare, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            listCurrentItems[itemRare].Remove(itemName);
        }
    }

    //public bool EqiqItem(ItemStatsLite item, out string notification)
    //{
    //    int length = listEqipedItems.Count;
    //    int slot = 0;
    //    bool hasEmptySlot = false;
    //    notification = "";
    //    //for (int i = 0; i < length; i++)
    //    //{
    //    //    if (listEqipedItems[i] != null)
    //    //    {
    //    //    }
    //    //}


    //    for (int i = 0; i < length; i++)
    //    {
    //        if (listEqipedItems[i] != null)
    //        {
    //            if (listEqipedItems[i].nameItems == item.nameItems)//already has one 
    //            {
    //                notification = "You already has one";
    //                return false;
    //            }
    //            if (i == length - 1 && !hasEmptySlot)// out of slot
    //            {
    //                notification = "Full slot";
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            if (hasEmptySlot) continue;

    //            slot = i;
    //            hasEmptySlot = true;
    //        }
    //    }


    //    listEqipedItems[slot] = item;
    //    RemoveItem(item.nameItems, item.rare);
    //    return true;
    //}
    //public void UnEqiqItem(int slot, ItemStatsLite item)
    //{
    //    listEqipedItems[slot] = null;
    //    AddItem(item.nameItems, item.rare);
    //}

    //public void UnlockSlotItem()
    //{
    //    listEqipedItems.Add(listEqipedItems.Count, null);
    //}
    //public void UnlockSlotItem(int slot)
    //{
    //    if (listEqipedItems.ContainsKey(slot)) return;

    //    listEqipedItems.Add(slot, null);
    //}
    public void AddNewSkillToPool(string nameSkill)
    {
        if (!playerSkillPooling.Contains(nameSkill))
            playerSkillPooling.Add(nameSkill);
    }
    public void AddNewSkillToPool(string[] nameSkill)
    {
        int length = nameSkill.Length;
        for (int i = 0; i < length; i++)
        {
            AddNewSkillToPool(nameSkill[i]);
        }
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