using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;
using Assets.Scripts.Data;
using Assets.Scripts.UI.ResourceRecive;

namespace Assets.Scripts.UI.DailyMission
{
    public class DailyMissionElement : MonoBehaviour
    {
        [FoldoutGroup("Button"), SerializeField]
        private Button actionBtn;

        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI stars;
        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI description;
        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI value;

        [FoldoutGroup("Component"), SerializeField]
        private GameObject skip;
        [FoldoutGroup("Component"), SerializeField]
        private GameObject collect;
        [FoldoutGroup("Component"), SerializeField]
        private GameObject collected;

        [FoldoutGroup("Component"), SerializeField]
        private GameObject starCollect;
        [FoldoutGroup("Component"), SerializeField]
        private GameObject starCollected;

        [FoldoutGroup("Component"), SerializeField]
        private Color collectColor;
        [FoldoutGroup("Component"), SerializeField]
        private Color collectedColor;

        [FoldoutGroup("Image"), SerializeField]
        private Image processBar;
        [FoldoutGroup("Image"), SerializeField]
        private Image actionVisual;

        public DailyMissions currentDailyMission;

        private DailyMissionData data;
        private DailyMissionPanel owner;


        public void LoadElement(DailyMissions currentDailyMission, DailyMissionPanel owner)
        {
            this.currentDailyMission = currentDailyMission;
            data = GameManager.Instance.dailyMissionsData[currentDailyMission.id];

            if (currentDailyMission.currentStatus == EMissionStatus.Collected)
                owner.currentStar += data.amountStar;

                if (currentDailyMission.currentStatus == EMissionStatus.Skip && currentDailyMission.currentValue >= data.value)
                currentDailyMission.currentStatus = EMissionStatus.Collect;

            data.level = currentDailyMission.currentLevel;
            stars.text = data.amountStar.ToString();
            description.text = data.description;
            actionBtn.onClick.AddListener(delegate { ActionButton(); });
            this.owner = owner;

            UpdateElement();
        }

        private void ActionButton()
        {
            SoundManager.Instance.Play("Button Click");

            switch (currentDailyMission.currentStatus)
            {
                case EMissionStatus.Skip:

                    GameManager.Instance.ShowAdsReward(Helper.Skip_Daily_Mission_Placement, () =>
                    {
                        currentDailyMission.currentValue = data.value;
                        currentDailyMission.currentStatus = EMissionStatus.Collect;
                    });

                    break;
                case EMissionStatus.Collect:

                    ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).StarsRecive(starCollect.transform.position,
                                   delegate
                                   {
                                       owner.CollectMission(data.amountStar);
                                   });

                    currentDailyMission.currentStatus = EMissionStatus.Collected;

                    break;
                case EMissionStatus.Collected:

                    break;
                default:
                    break;
            }
            UpdateElement();
        }

        public void UpdateElement()
        {
            ButtonStatus(currentDailyMission.currentStatus);
            value.text = currentDailyMission.currentValue + "/" + data.value;
            processBar.fillAmount = (float)currentDailyMission.currentValue / data.value;

            void ButtonStatus(EMissionStatus status)
            {
                skip.SetActive(status == EMissionStatus.Skip);
                collect.SetActive(status == EMissionStatus.Collect);
                collected.SetActive(status == EMissionStatus.Collected);
                actionVisual.color = status == EMissionStatus.Collected ? collectedColor : collectColor;

                starCollect.SetActive(status != EMissionStatus.Collected);
                starCollected.SetActive(status == EMissionStatus.Collected);

            }
        }
        public void SetStatus(EMissionStatus status)
        {
            this.currentDailyMission.currentStatus = status;

            UpdateElement();
        }

        private void OnDisable()
        {
            actionBtn.onClick.RemoveAllListeners();
            DataManager.Instance.GetData().UpdateDailyMission(currentDailyMission);
        }
    }
}