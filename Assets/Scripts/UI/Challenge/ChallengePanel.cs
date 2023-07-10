using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Assets.Scripts.UI.Play;

namespace Assets.Scripts.UI.Challenge
{
    public class ChallengePanel : BaseUI
    {
        public Transform parentContent;
        public ChallengeElement element;
        public List<ChallengeElement> elements;
        public Button closeBtn;
        private void Awake()
        {
            int length = DataManager.Instance.GetData().challenges.Count;
            for (int i = 0; i < length; i++)
            {
                elements.Add(Instantiate(element, Vector3.zero, Quaternion.identity, parentContent));
                elements[i].LoadElement(DataManager.Instance.GetData().challenges[i]);
            }
        }

        public override void LoadData()
        {
            Time.timeScale = 0;
            closeBtn.onClick.AddListener(() =>
            {
                Close();
                SoundManager.Instance.Play("Button Click");

            });
            UpdateVisual();
        }
        public void UpdateVisual()
        {
            int length = elements.Count;
            for (int i = 0; i < length; i++)
            {
                elements[i].LoadVisual();
            }
        }
        public override void SaveData()
        {
            closeBtn.onClick.RemoveAllListeners();
            ((PlayPanel)UIManager.Instance.gamePlayPanel).UpdateChallengeNoti();
            Time.timeScale = 1;

        }
    }
}