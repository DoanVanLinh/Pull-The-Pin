using Assets.Scripts.UI.ResourceRecive;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI.ChallengeWin
{
    public class ChallengeWin : BaseUI
    {
        public Animator ani;
        public Button continuesBtn;
        public TextMeshProUGUI rewardTxt;
        private int reward;
        public override void LoadData()
        {
            continuesBtn.onClick.AddListener(() => ContinueButton());
            reward = DataManager.Instance.GetData().GetChallengeReward(GameManager.Instance.currentChallenge.id);
            rewardTxt.text = reward.ToString();
            UIManager.Instance.currentcyPanel.Open();

        }
        public override void Open()
        {
            base.Open();
            ani.Play("Open");
        }
        private void ContinueButton()
        {
            SoundManager.Instance.Play("Button Click");


            ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position,
                                   delegate
                                   {
                                       SoundManager.Instance.Play("GetCoins");
                                       DataManager.Instance.AddCoins(reward);
                                       UIManager.Instance.challegentPanel.Open();
                                       GameManager.Instance.SetGameState(GameState.NormalMode);
                                       ani.Play("Close");
                                   });
            //GameManager.Instance.currentChallenge = null;

        }

        public override void SaveData()
        {
            UIManager.Instance.currentcyPanel.Close();

            continuesBtn.onClick.RemoveAllListeners();
        }
    }
}