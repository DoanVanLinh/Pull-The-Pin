using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.DailyReward
{
    public class DailyRewardPanel : BaseUI
    {
        public Animator ani;
        public override void LoadData()
        {
            UIManager.Instance.currentcyPanel.Open();

        }
        public void CloseButton()
        {
            ani.Play("Close");
        }

        public override void SaveData()
        {
            UIManager.Instance.currentcyPanel.Close();
        }
    }
}