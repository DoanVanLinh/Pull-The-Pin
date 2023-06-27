using Assets.Scripts.UI.ResourceRecive;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Streak
{
    public class StreakRewardPanel : BaseUI
    {
        [FoldoutGroup("Ani")]
        public Animator panelAni;

        [FoldoutGroup("Button")]
        public Button closeBtn;

        [FoldoutGroup("Gameobject")]
        public GameObject coins;
        [FoldoutGroup("Gameobject")]
        public GameObject items;

        [FoldoutGroup("Text")]
        public TextMeshProUGUI coinsTxt;

        [FoldoutGroup("Image")]
        public Image itemImg;

        public ERewardType type;
        private int amount;

        public void Init(ERewardType type, int amount, string id = "")
        {
            this.type = type;
            this.amount = amount;

            coins.SetActive(type == ERewardType.Coins);
            items.SetActive(type == ERewardType.Item);

            coinsTxt.text = amount.ToString();
            if (!string.IsNullOrEmpty(id))
            {
                itemImg.sprite = GameManager.Instance.itemsData[id].icon;
                DataManager.Instance.GetData().AddItem(id);
            }
            Open();

        }

        public override void LoadData()
        {
            closeBtn.onClick.AddListener(() => CloseButton());
            panelAni.Play("Open");
        }

        private void CloseButton()
        {
            switch (type)
            {
                case ERewardType.Coins:
                    UIManager.Instance.currentcyPanel.Open();
                    ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position, () =>
                    {
                        DataManager.Instance.AddCoins(amount);
                        UIManager.Instance.streakPanel.LoadData();
                        panelAni.Play("Close");
                    });
                    break;
                case ERewardType.Item:
                    UIManager.Instance.streakPanel.LoadData();
                    panelAni.Play("Close");

                    break;
                case ERewardType.Random:
                    break;
                default:
                    break;
            }
        }
        public override void Close()
        {
            base.Close();
            UIManager.Instance.currentcyPanel.Close();
        }
        public override void SaveData()
        {
            closeBtn.onClick.RemoveAllListeners();
        }
    }
}