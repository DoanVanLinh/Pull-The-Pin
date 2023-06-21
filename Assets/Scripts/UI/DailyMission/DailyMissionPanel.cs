using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.UI.DailyMission
{
    public class DailyMissionPanel : BaseUI
    {
        [SerializeField]
        private Button backBtn;
        [SerializeField]
        private List<DailyMissionElement> elements;

        private int currentStar;

        public override void LoadData()
        {
            backBtn.onClick.AddListener(delegate { BackButton(); });
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

        }
        public void CollectMission(int star)
        {
            currentStar += star;
        }

        public override void SaveData()
        {
            UIManager.Instance.currentcyPanel.Close();

        }
    }
}