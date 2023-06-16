using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Assets.Scripts.UI.DailyMission
{
    public class DailyMissionPanel : BaseUI
    {
        [SerializeField]
        private List<DailyMissionElement> elements;

        private int currentStar;

        public override void LoadData()
        {
            LoadElement();
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
        }
    }
}