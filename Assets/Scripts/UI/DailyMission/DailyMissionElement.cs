using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;
using Assets.Scripts.Data;

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
            data.level = currentDailyMission.currentLevel;
            stars.text = data.amountStar.ToString();
            description.text = data.description;
            actionBtn.onClick.AddListener(delegate { ActionButton(); });
            this.owner = owner;

            UpdateElement();
        }

        private void ActionButton()
        {

            switch (currentDailyMission.currentStatus)
            {
                case EMissionStatus.Skip:
                    //add
                    currentDailyMission.currentValue = data.value;
                    currentDailyMission.currentStatus = EMissionStatus.Collect;

                    break;
                case EMissionStatus.Collect:
                    owner.CollectMission(data.amountStar);
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

        }
    }
}