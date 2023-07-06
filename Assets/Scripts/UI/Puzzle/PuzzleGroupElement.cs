using Assets.Scripts.Data;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Assets.Scripts.UI.Puzzle
{
    public class PuzzleGroupElement : MonoBehaviour
    {
        public Button actionBtn;
        public TextMeshProUGUI nameGroup;
        public Image thumbnail;
        public PuzzleGroupData data;

        private void OnEnable()
        {
            actionBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); OpenGroup(); });
        }

        private void OpenGroup()
        {
            ((PuzzlePanel)UIManager.Instance.puzzlePanel).data = data;
            UIManager.Instance.puzzlePanel.Open();
        }

        public void LoadGroup(PuzzleGroupData data)
        {
            this.data = data;
            nameGroup.text = data.id;
            thumbnail.sprite = data.thumbnail;

        }
        private void OnDisable()
        {
            actionBtn.onClick.RemoveAllListeners();

        }
    }
}