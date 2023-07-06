using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.ChallengeLose
{
    public class ChallengeLose : BaseUI
    {
        public Animator ani;
        public Button closeBtn;
        public Button retryBtn;

        public override void LoadData()
        {
            closeBtn.onClick.AddListener(() => CloseButton());
            retryBtn.onClick.AddListener(() => RetryButton());
        }
        public override void Open()
        {
            base.Open();
            ani.Play("Open");
        }
        private void RetryButton()
        {
            SoundManager.Instance.Play("Button Click");

            GameManager.Instance.ShowAdsReward(Helper.Play_Again_Challenge_Placement, () =>
            {
                DataManager.Instance.GetData().SetChallengeStatusById(GameManager.Instance.currentChallenge.id, EChalengeType.Play);
                GameManager.Instance.StartChallenge(GameManager.Instance.currentChallenge.id);
                ani.Play("Close");
            });
        }

        private void CloseButton()
        {
            SoundManager.Instance.Play("Button Click");

            ani.Play("Close");
            GameManager.Instance.SetGameState(GameState.NormalMode);
            UIManager.Instance.challegentPanel.Open();
        }

        public override void SaveData()
        {
            closeBtn.onClick.RemoveAllListeners();
            retryBtn.onClick.RemoveAllListeners();
        }
    }
}