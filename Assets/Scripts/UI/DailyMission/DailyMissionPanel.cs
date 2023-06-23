using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using DG.Tweening;
using Assets.Scripts.UI.DailyReward;
using Assets.Scripts.UI.Play;

namespace Assets.Scripts.UI.DailyMission
{
    public class DailyMissionPanel : BaseUI
    {
        [SerializeField]
        private Button backBtn;
        [SerializeField]
        private Image mainProcess;
        [SerializeField]
        private List<DailyMissionElement> elements;
        [SerializeField]
        private List<MainDailyMissionGift> giftElements;


        public int currentStar;

        public override void LoadData()
        {
            Time.timeScale = 0;
            backBtn.onClick.AddListener(delegate { BackButton(); });
            currentStar = 0;
            ((PlayPanel)UIManager.Instance.gamePlayPanel).dailyMissionNoti.SetActive(false);

            LoadElement();
            UIManager.Instance.currentcyPanel.Open();
        }

        private void BackButton()
        {
            Close();
        }

        private void LoadElement()
        {
            int length = elements.Count;
            for (int i = 0; i < length; i++)
            {
                elements[i].LoadElement(DataManager.Instance.GetData().dailyMissions[i], this);
            }

            for (int i = 0; i < 3; i++)
            {
                giftElements[i].LoadDailyMissionGift(DataManager.Instance.GetData().dailyMissionsgift[i]);

            }

            mainProcess.fillAmount = currentStar / 100f;
        }
        public void CollectMission(int star)
        {
            int targetStar = currentStar + star;

            DOTween.To(() => currentStar, x => currentStar = x, targetStar, 0.25f)
                    .SetUpdate(true)
                    .OnUpdate(() =>
                    {
                        UpdateVisual();
                    });


        }
        private void UpdateVisual()
        {
            mainProcess.fillAmount = currentStar / 100f;

            if (currentStar >= 30&& giftElements[0].currentData.status == EMissionStatus.Skip)
            {
                giftElements[0].currentData.status = EMissionStatus.Collect;
                giftElements[0].UpdateVisual();
                DataManager.Instance.GetData().UpdateDailyMissionGift(0, EMissionStatus.Collect);
            }

            if (currentStar >= 60 && giftElements[1].currentData.status == EMissionStatus.Skip)
            {
                giftElements[1].currentData.status = EMissionStatus.Collect;
                giftElements[1].UpdateVisual();
                DataManager.Instance.GetData().UpdateDailyMissionGift(1, EMissionStatus.Collect);
            }

            if (currentStar >= 100 && giftElements[2].currentData.status == EMissionStatus.Skip)
            {
                giftElements[2].currentData.status = EMissionStatus.Collect;
                giftElements[2].UpdateVisual();
                DataManager.Instance.GetData().UpdateDailyMissionGift(2, EMissionStatus.Collect);
            }
        }
        public override void SaveData()
        {
            Time.timeScale =1;

            UIManager.Instance.currentcyPanel.Close();

        }
    }
}