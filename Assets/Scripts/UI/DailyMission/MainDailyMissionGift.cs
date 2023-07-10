using Assets.Scripts.UI.ResourceRecive;
using Assets.Scripts.UI.RewardRecive;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.DailyMission
{
    public class MainDailyMissionGift : MonoBehaviour
    {
        public int id;
        public Button collectBtn;
        public ERewardType type;
        public int amountReward;

        public GameObject amount;
        public GameObject collectNoti;
        public GameObject collectedNoti;

        public MainDailyMissionGiftData currentData;
        private void OnEnable()
        {
            collectBtn.onClick.AddListener(() => CollectButton());
        }

        private void CollectButton()
        {
            SoundManager.Instance.Play("Button Click");

            string idItem = "";
            switch (currentData.status)
            {
                case EMissionStatus.Skip:

                    break;
                case EMissionStatus.Collect:
                    switch (type)
                    {
                        case ERewardType.Coins:

                            break;
                        case ERewardType.Item:
                            idItem = GameManager.Instance.GetRandomItemByType(EItemUnlockType.DailyMission);
                            if (!string.IsNullOrEmpty(idItem))
                                DataManager.Instance.GetData().AddItem(idItem);
                            else
                            {
                                type = ERewardType.Coins;
                            }
                            break;
                        case ERewardType.Random:
                            break;
                        default:
                            break;
                    }

                    currentData.status = EMissionStatus.Collected;
                    DataManager.Instance.GetData().UpdateDailyMissionGift(currentData.id, EMissionStatus.Collected);
                    ((RewardRecivePanel)UIManager.Instance.rewardRecivePanel).Init(type,
                                                                                    amountReward,
                                                                                     null,
                                                                                    idItem);

                    break;
                case EMissionStatus.Collected:
                    break;
                default:
                    break;
            }
            UpdateVisual();
        }

        public void LoadDailyMissionGift(MainDailyMissionGiftData data)
        {
            currentData = data;
            UpdateVisual();
        }
        public void UpdateVisual()
        {
            amount.SetActive(currentData.status == EMissionStatus.Skip);
            collectNoti.SetActive(currentData.status == EMissionStatus.Collect);
            collectedNoti.SetActive(currentData.status == EMissionStatus.Collected);
        }
        private void OnDisable()
        {

            collectBtn.onClick.RemoveAllListeners();
        }
    }

    [Serializable]
    public class MainDailyMissionGiftData
    {
        public int id;
        public EMissionStatus status;

        public MainDailyMissionGiftData(int id, EMissionStatus status)
        {
            this.id = id;
            this.status = status;
        }
    }
}