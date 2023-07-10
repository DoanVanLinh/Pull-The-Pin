using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.UI.ResourceRecive;

namespace Assets.Scripts.UI.RewardRecive
{
    public class RewardRecivePanel : BaseUI
    {
        [FoldoutGroup("Ani")]
        public Animator panelAni;
        [FoldoutGroup("Ani")]
        public Animator giftAni;

        [FoldoutGroup("Button")]
        public Button openBtn;
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
        private string id;

        Action onOpenDone;
        public void Init(ERewardType type, int amount,Action onOpenDone, string id = "")
        {
            this.onOpenDone = null;
            this.type = type;
            this.amount = amount;
            this.id = id;

            this.onOpenDone = onOpenDone;

            coins.SetActive(type == ERewardType.Coins);
            items.SetActive(type == ERewardType.Item);

            coinsTxt.text = amount.ToString();
            if (!string.IsNullOrEmpty(id))
                itemImg.sprite = GameManager.Instance.itemsData[id].icon;
            Open();

        }

        public override void LoadData()
        {
            openBtn.onClick.AddListener(() => { SoundManager.Instance.Play("Button Click"); OpenButton(); });
            closeBtn.onClick.AddListener(() => { SoundManager.Instance.Play("Button Click"); panelAni.Play("Close"); });
            closeBtn.gameObject.SetActive(false);
            openBtn.gameObject.SetActive(true);
            panelAni.Play("Open");
            giftAni.Play("Idle");
        }

        private void OpenButton()
        {
            openBtn.gameObject.SetActive(false);
            giftAni.Play("Open");

            switch (type)
            {
                case ERewardType.Coins:
                    StartCoroutine(IEOpen(() =>
                    {
                        UIManager.Instance.currentcyPanel.Open();
                        ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position, () =>
                        {
                            SoundManager.Instance.Play("GetCoins");
                            DataManager.Instance.AddCoins(amount);

                            closeBtn.gameObject.SetActive(true);
                            onOpenDone?.Invoke();

                        });

                    }));
                    break;
                case ERewardType.Item:
                    onOpenDone?.Invoke();

                    StartCoroutine(IEOpen(() =>
                    {
                        closeBtn.gameObject.SetActive(true);
                    }));
                    break;
                case ERewardType.Random:
                    break;
                default:
                    break;
            }
        }
        IEnumerator IEOpen(Action action)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            SoundManager.Instance.Play("OpenGift");
            yield return new WaitForSecondsRealtime(1.0f);

            action?.Invoke();

        }
        public override void Close()
        {
            base.Close();
            UIManager.Instance.currentcyPanel.Close();
        }
        public override void SaveData()
        {

            openBtn.onClick.RemoveAllListeners();
            closeBtn.onClick.RemoveAllListeners();
        }
    }
}