using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI.Challenge
{
    public class ChallengeElement : MonoBehaviour
    {
        public string id;
        public List<GameObject> contents;
        public List<GameObject> buttons;
        public ChallengeData data;
        public Button actionBtn;

        public List<TextMeshProUGUI> rewardCoins;
        public TextMeshProUGUI unlockCoins;
        public TextMeshProUGUI levelTxt;
        public void LoadElement(ChallengeData data)
        {
            this.data = data;
            actionBtn.onClick.AddListener(() => ActionButton());

            int length = rewardCoins.Count;
            for (int i = 0; i < length; i++)
            {
                rewardCoins[i].text = data.reward.ToString();
            }
            unlockCoins.text = data.amountUnlock.ToString();
            levelTxt.text = data.id;

            LoadVisual();
        }
        public void LoadVisual()
        {
            int length = contents.Count;
            for (int i = 0; i < length; i++)
            {
                contents[i].SetActive(i == (int)data.type);
                buttons[i].SetActive(i == (int)data.type);
            }
        }
        private void ActionButton()
        {
            switch (data.type)
            {
                case EChalengeType.Lock:
                    if (DataManager.Instance.Coins >= data.amountUnlock)
                    {
                        DataManager.Instance.AddCoins(-data.amountUnlock);
                        data.type = EChalengeType.Play;
                    }//else
                    //    Helper.PushNotification("Not Enought Coins");
                    break;
                case EChalengeType.Play:
                    GameManager.Instance.StartChallenge(data.id);
                    break;
                case EChalengeType.Failed:
                    GameManager.Instance.ShowAdsReward(Helper.Fail_Challenge_Placement, () =>
                    {
                        data.type = EChalengeType.Play;
                    });
                    break;
                case EChalengeType.Win:
                    GameManager.Instance.ShowAdsReward(Helper.Play_Again_Challenge_Placement, () =>
                    {
                        data.type = EChalengeType.Play;
                    }); break;
                default:
                    break;
            }

            DataManager.Instance.GetData().SetChallengeStatusById(data);
            LoadVisual();
        }
    }

}
[Serializable]
public class ChallengeData
{
    public string id;
    public int amountUnlock;
    public int reward => amountUnlock * 2;

    public EChalengeType type;

    public ChallengeData()
    {
    }

    public ChallengeData(string id, int amountUnlock)
    {
        this.id = id;
        this.amountUnlock = amountUnlock;
        this.type = EChalengeType.Lock;
    }

    public ChallengeData(string id, int amountUnlock, EChalengeType type) : this(id, amountUnlock)
    {
        this.type = type;
    }
}