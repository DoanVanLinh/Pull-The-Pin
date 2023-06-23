using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using Assets.Scripts.Commons;
using System.Collections.Generic;
using Assets.Scripts.Data;

namespace Assets.Scripts.UI.Shop
{
    public class GroupShopElement : MonoBehaviour
    {
        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI labelTxt;

        [FoldoutGroup("GameObject"), SerializeField]
        private Transform elementParent;

        [FoldoutGroup("Prefabs"), SerializeField]
        private ShopElement element;
        private List<Item> items = new List<Item>();
        private List<ShopElement> itemElements = new List<ShopElement>();

        public void AddItems(List<Item> item)
        {
            items.AddRange(item);
        }
        public void LoadGroup()
        {
            ClearGroup();
            transform.localPosition = Vector3.zero;
            if (items.Count == 0) return;

            labelTxt.text = LoadLabel(items[0].itemUnlockType);
            int length = items.Count;

            for (int i = 0; i < length; i++)
            {
                itemElements.Add(Instantiate(element, Vector3.zero, Quaternion.identity, elementParent));
                itemElements[i].LoadElement(items[i].id);
            }
            UpdateGroup();
        }
        public void UpdateGroup()
        {
            int length = itemElements.Count;
            for (int i = 0; i < length; i++)
            {
                itemElements[i].UpdateElement();
            }
        }

        private string LoadLabel(EItemUnlockType type)
        {
            switch (type)
            {
                case EItemUnlockType.Coins:
                    return "Unlock by purchasing";
                case EItemUnlockType.LevelComplete:
                    return "Unlock by completing levels";
                case EItemUnlockType.LuckyWheel:
                    return "Get from lucky wheel";
                case EItemUnlockType.DailyMission:
                    return "Unlock by completing daily missions";
                case EItemUnlockType.GiftBox:
                    return "Unlock through gift boxes";
                case EItemUnlockType.Streak:
                    return "Unlock by streak";
                default:
                    return "";
            }
        }
        private void ClearGroup()
        {
            int length = elementParent.childCount;
            for (int i = 0; i < length; i++)
            {
                Destroy(elementParent.GetChild(i).gameObject);
            }
        }
    }
}