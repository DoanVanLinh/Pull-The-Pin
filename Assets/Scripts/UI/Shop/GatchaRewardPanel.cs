using Assets.Scripts.UI.ResourceRecive;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Shop
{
    public class GatchaRewardPanel : BaseUI
    {
        public Animator ani;

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

        public void Init(ERewardType type, int amount,string id="")
        {
            this.type = type;
            this.amount = amount;

            coins.SetActive(type == ERewardType.Coins);
            items.SetActive(type == ERewardType.Item);

            coinsTxt.text = amount.ToString();
            if (!string.IsNullOrEmpty(id))
                itemImg.sprite = GameManager.Instance.itemsData[id].icon;
            Open();

        }

        public override void LoadData()
        {
            closeBtn.interactable = true;
            closeBtn.onClick.AddListener(() => OpenButton());
            ani.Play("Open");
        }

        private void OpenButton()
        {
            SoundManager.Instance.Play("Button Click"); closeBtn.interactable = false;
            switch (type)
            {
                case ERewardType.Coins:
                    UIManager.Instance.currentcyPanel.Open();
                    ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position, () =>
                    {
                        SoundManager.Instance.Play("GetCoins");
                        DataManager.Instance.AddCoins(amount);
                        CloseButton();
                    });
                    break;
                case ERewardType.Item:
                    CloseButton();
                    break;
                case ERewardType.Random:
                    break;
                default:
                    break;
            }
        }

        private void CloseButton()
        {
            ani.Play("Close");
        }

        public override void SaveData()
        {
            closeBtn.onClick.RemoveAllListeners();
            ((ShopPanel)UIManager.Instance.shopPanel).bask.Init();
        }


    }
}