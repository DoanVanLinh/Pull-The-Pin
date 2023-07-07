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
        public GameObject coinObj;
        public Image itemObj;

        public override void LoadData()
        {
            openBtn.gameObject.SetActive(true);
            closeBtn.gameObject.SetActive(true);
            coinObj.SetActive(true);
            itemObj.gameObject.SetActive(false);

            amountCoins = Random.Range(600, 1000);

            adsIcon.gameObject.SetActive(DataManager.Instance.HasKey(Helper.First_Gift_Key));
            closeBtn.gameObject.SetActive(DataManager.Instance.HasKey(Helper.First_Gift_Key));

            openBtn.interactable = true;
            open2Btn.interactable = true;
            closeBtn.interactable = true;

            coinsTxt.text = amountCoins.ToString();

            //openBtn.onClick.AddListener(() => OpenButton());
            open2Btn.onClick.AddListener(() => OpenButton());
            closeBtn.onClick.AddListener(() => CloseButton());
        }

        private void OpenButton()
        {
            SoundManager.Instance.Play("Button Click");

            if (!DataManager.Instance.HasKey(Helper.First_Gift_Key))
            {
                OpenGift(true);
                DataManager.Instance.SetInt(Helper.First_Gift_Key, 1);
            }
            else
            {
                GameManager.Instance.ShowAdsReward(Helper.Gift_Placement, delegate
                 {
                     OpenGift();
                 });
            }

            void OpenGift(bool isFirstTime = false)
            {

                openBtn.interactable = false;
                open2Btn.interactable = false;
                closeBtn.interactable = false;
                openBtn.gameObject.SetActive(false);
                closeBtn.gameObject.SetActive(false);

                giftAni.Play("Open");
                if (!isFirstTime)
                    StartCoroutine(IEOpen());
                else
                    StartCoroutine(IEFirstOpen());

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
            float randomGift = Random.Range(-1f, 1f);
            coinObj.SetActive(randomGift > 0);
            itemObj.gameObject.SetActive(randomGift <= 0);
            string id = GameManager.Instance.GetRandomItemByType(EItemUnlockType.GiftBox);

            if (randomGift > 0)//coins
            {
                DataManager.Instance.AddCoins(1000);
            }
            else
            {

                if (!string.IsNullOrEmpty(id))
                {
                    itemObj.sprite = GameManager.Instance.itemsData[id].icon;
                    DataManager.Instance.GetData().AddItem(id);
                }
                else
                {
                    UIManager.Instance.currentcyPanel.Open();
                    DataManager.Instance.AddCoins(1000);
                }
            }

            yield return new WaitForSeconds(1.5f);


            if (randomGift > 0)//coins
            {
                UIManager.Instance.currentcyPanel.Open();
                ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position, () => { SoundManager.Instance.Play("GetCoins"); panelAni.Play("Close"); });
            }
            else
            {
                if (!string.IsNullOrEmpty(id))
                {
                    yield return new WaitForSeconds(1f);
                    panelAni.Play("Close");
                }
                else
                {
                    UIManager.Instance.currentcyPanel.Open();
                    ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position, () => { SoundManager.Instance.Play("GetCoins"); panelAni.Play("Close"); });
                }
            }


        }
        IEnumerator IEFirstOpen()
        {
            coinObj.SetActive(false);
            itemObj.gameObject.SetActive(true);
            itemObj.sprite = GameManager.Instance.themeData["Theme9"].icon;
            DataManager.Instance.GetData().AddItem("Theme9");
            DataManager.Instance.SetCurrentThemeVisual("Theme9");
            yield return new WaitForSeconds(2.5f);
            panelAni.Play("Close");
        }

        public void CloseButton()
        {
            SoundManager.Instance.Play("Button Click");

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