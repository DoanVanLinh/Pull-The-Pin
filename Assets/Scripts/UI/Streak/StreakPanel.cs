using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Sirenix.OdinInspector;
using Assets.Scripts.UI.RewardRecive;

namespace Assets.Scripts.UI.Streak
{
    public class StreakPanel : BasePopupUI
    {
        [FoldoutGroup("Component")]
        public Animator ani;
        [FoldoutGroup("Component")]
        public GameObject arrow;
        [FoldoutGroup("Text")]
        public TextMeshProUGUI currentStreakTxt;
        [FoldoutGroup("Component")]
        public List<GameObject> locStreak;
        [FoldoutGroup("Component")]
        public List<StreakElement> streakElement;

        [FoldoutGroup("Component"), ListDrawerSettings(NumberOfItemsPerPage = 5)]
        public List<StreakReward> listStreak;

        [FoldoutGroup("Image")]
        public Image visual;

        [FoldoutGroup("Component")]
        public List<Sprite> visuals;

        [FoldoutGroup("Button")]
        public Button keepStreakBtn;
        [FoldoutGroup("Button")]
        public Button loseStreakBtn;

        bool isWin;
        public void Open(bool isWin)
        {
            this.isWin = isWin;
            Open();
        }
        public override void Open()
        {
            base.Open();
            ani.Play("Open");
        }
        public override void LoadData()
        {
            keepStreakBtn.onClick.AddListener(() => KeepStreakButton());
            loseStreakBtn.onClick.AddListener(() => LoseStreakButton());
            LoadCurrentStreak();
        }

        private void LoseStreakButton()
        {
            ani.Play("Close");
            DataManager.Instance.SetStreak(0);
        }

        private void LoadCurrentStreak()
        {
            int currentStreak = DataManager.Instance.CurrentStreak;
            currentStreakTxt.text = currentStreak.ToString();
            int currentLoc = currentStreak % 5;
            int currentStreakGroup = currentStreak / 5;

            //Load Streak Group
            for (int i = 0; i < 5; i++)
            {
                streakElement[i].LoadElement(listStreak[currentStreakGroup * 5 + i]);
            }


            arrow.transform.eulerAngles = currentLoc == 0 ? Vector3.forward * 180 : Vector3.forward * Vector2.SignedAngle(Vector2.right, (locStreak[currentLoc - 1].transform.position - arrow.transform.position).normalized);

            if (isWin)
            {
                keepStreakBtn.gameObject.SetActive(false);
                loseStreakBtn.gameObject.SetActive(false);

                visual.sprite = visuals[currentStreakGroup + 1];
                float newRotation = Vector2.SignedAngle(Vector2.right, (locStreak[currentLoc].transform.position - arrow.transform.position).normalized);

                arrow.transform.DORotate(Vector3.forward * newRotation, 0.5f)
                    .SetDelay(1.5f)
                    .SetEase(Ease.OutBack)
                    .SetUpdate(true)
                    .OnComplete(() =>
                    {
                        currentStreakTxt.text = (currentStreak + 1).ToString();
                        if (currentLoc == 4)
                        {
                            StartCoroutine(IECollectStreak());
                        }
                        else
                            ani.Play("Close");
                    });

                DataManager.Instance.AddStreak(1);
            }
            else
            {
                visual.sprite = visuals[0];

                keepStreakBtn.gameObject.SetActive(true);
                loseStreakBtn.gameObject.SetActive(true);


            }
        }
        private IEnumerator IECollectStreak()
        {
            UIManager.Instance.currentcyPanel.Open();
            for (int i = 0; i < 4; i++)
            {
                streakElement[i].Collect();
                yield return new WaitForSeconds(0.2f);
            }

            ((StreakRewardPanel)UIManager.Instance.streakRewardPanel).Init(streakElement[4].reward.rewardType, streakElement[4].reward.amount, streakElement[4].reward.id);
        }
        private void KeepStreakButton()
        {
            visual.sprite = visuals[DataManager.Instance.CurrentStreak / 5 + 1];

            ani.Play("Close");
        }

        public override void SaveData()
        {
            keepStreakBtn.onClick.RemoveAllListeners();
            loseStreakBtn.onClick.RemoveAllListeners();
        }
    }
    [Serializable]
    public class StreakReward
    {
        public ERewardType rewardType;
        public int amount;
        [ShowIf("rewardType", ERewardType.Item)]
        public string id;
    }
}