using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.UI.ResourceRecive;

namespace Assets.Scripts.UI.Streak
{
    public class StreakElement : MonoBehaviour
    {
        public Image visual;
        public TextMeshProUGUI amountTxt;
        public List<Sprite> coinSpr;
        public StreakReward reward;
        public void LoadElement(StreakReward reward)
        {
            gameObject.SetActive(true);
            this.reward = reward;
            if (DataManager.Instance.GetData().HasItem(reward.id))
            {
                this.reward.rewardType = ERewardType.Coins;
            }

            switch (reward.rewardType)
            {
                case ERewardType.Coins:
                    amountTxt.gameObject.SetActive(true);
                    amountTxt.text = "+" + reward.amount;
                    visual.sprite = reward.amount > 1000 ? coinSpr[2] : (reward.amount > 500 ? coinSpr[1] : coinSpr[0]);
                    break;
                case ERewardType.Item:
                    amountTxt.gameObject.SetActive(false);
                    visual.sprite = GameManager.Instance.itemsData[reward.id].icon;
                    break;
                case ERewardType.Random:
                    break;
                case ERewardType.None:
                    gameObject.SetActive(false);
                    return;
                default:
                    break;
            }
            visual.SetNativeSize();
        }
        public void Collect()
        {
            if (reward.amount == 0) return;

            ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(visual.transform.position, () =>
            {
                SoundManager.Instance.Play("GetCoins");
                DataManager.Instance.AddCoins(reward.amount);
            });
        }
    }


}