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
using System.Linq;

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
            DataManager.Instance.GetData().dailyMissions = DataManager.Instance.GetData().dailyMissions.OrderBy(o => o.currentStatus).ToList();
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
            currentStar += star;

            int initStar = currentStar - star;

            DOTween.To(() => initStar, x => initStar = x, currentStar, 0.25f)
                    .SetUpdate(true)
                    .OnUpdate(() =>
                    {
                        UpdateVisual(initStar);
                    });


        }
        private void UpdateVisual(int process)
        {
            mainProcess.fillAmount = process / 100f;

            if (process >= 30 && giftElements[0].currentData.status == EMissionStatus.Skip)
            {
                giftElements[0].currentData.status = EMissionStatus.Collect;
                giftElements[0].UpdateVisual();
                DataManager.Instance.GetData().UpdateDailyMissionGift(0, EMissionStatus.Collect);
            }

            if (process >= 60 && giftElements[1].currentData.status == EMissionStatus.Skip)
            {
                giftElements[1].currentData.status = EMissionStatus.Collect;
                giftElements[1].UpdateVisual();
                DataManager.Instance.GetData().UpdateDailyMissionGift(1, EMissionStatus.Collect);
            }

            if (process >= 100 && giftElements[2].currentData.status == EMissionStatus.Skip)
            {
                giftElements[2].currentData.status = EMissionStatus.Collect;
                giftElements[2].UpdateVisual();
                DataManager.Instance.GetData().UpdateDailyMissionGift(2, EMissionStatus.Collect);
            }
        }
        public override void SaveData()
        {
            Time.timeScale = 1;

            UIManager.Instance.currentcyPanel.Close();

        }
    }
}