using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.UI.ResourceRecive;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI.Gift
{
    public class GiftPanel : BaseUI
    {
        public float amountCoins;
        public ERewardType type;
        public Animator giftAni;
        public Animator panelAni;

        public TextMeshProUGUI coinsTxt;


        public Button openBtn;
        public Button open2Btn;
        public Button closeBtn;

        public Image adsIcon;
        public override void LoadData()
        {
            openBtn.gameObject.SetActive(true);
            closeBtn.gameObject.SetActive(true);

            amountCoins = Random.Range(600, 1000);

            adsIcon.gameObject.SetActive(DataManager.Instance.HasKey(Helper.First_Gift_Key));
            closeBtn.gameObject.SetActive(DataManager.Instance.HasKey(Helper.First_Gift_Key));

            openBtn.interactable = true;
            open2Btn.interactable = true;
            closeBtn.interactable = true;

            coinsTxt.text = amountCoins.ToString();

            openBtn.onClick.AddListener(() => OpenButton());
            open2Btn.onClick.AddListener(() => OpenButton());
            closeBtn.onClick.AddListener(() => CloseButton());
        }

        private void OpenButton()
        {
            if (!DataManager.Instance.HasKey(Helper.First_Gift_Key))
            {
                DataManager.Instance.SetInt(Helper.First_Gift_Key, 1);
                OpenGift();
            }
            else
            {
                GameManager.Instance.ShowAdsReward(Helper.Gift_Placement, delegate
                 {
                     OpenGift();
                 });
            }

            void OpenGift()
            {
                openBtn.interactable = false;
                open2Btn.interactable = false;
                closeBtn.interactable = false;
                openBtn.gameObject.SetActive(false);
                closeBtn.gameObject.SetActive(false);

                giftAni.Play("Open");
                StartCoroutine(IEOpen());
            }
        }
        public override void Open()
        {
            base.Open();
            panelAni.Play("Open");
            giftAni.Play("Idle");

        }
        IEnumerator IEOpen()
        {
            yield return new WaitForSeconds(1.5f);
            UIManager.Instance.currentcyPanel.Open();
            ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position, () => panelAni.Play("Close"));
        }

        public void CloseButton()
        {
            UIManager.Instance.currentcyPanel.Close();

            openBtn.interactable = false;
            open2Btn.interactable = false;
            closeBtn.interactable = false;

            openBtn.gameObject.SetActive(false);
            closeBtn.gameObject.SetActive(false);

            panelAni.Play("Close");
            giftAni.Play("Close");
        }
        public override void SaveData()
        {
            DataManager.Instance.GiftPercent = 0;
            openBtn.onClick.RemoveAllListeners();
            open2Btn.onClick.RemoveAllListeners();
            closeBtn.onClick.RemoveAllListeners();
        }
    }
}