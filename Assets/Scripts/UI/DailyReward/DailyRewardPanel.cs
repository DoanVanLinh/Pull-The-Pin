using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.DailyReward
{
    public class DailyRewardPanel : BaseUI
    {

        public override void LoadData()
        {
            UIManager.Instance.currentcyPanel.Open();

        }

        public override void SaveData()
        {
            UIManager.Instance.currentcyPanel.Close();

        }
    }
}