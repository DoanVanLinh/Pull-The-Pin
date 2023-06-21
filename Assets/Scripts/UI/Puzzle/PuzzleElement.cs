using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Data;
using System;

namespace Assets.Scripts.UI.Puzzle
{
    public class PuzzleElement : MonoBehaviour
    {
        public Button actionBtn;
        public TextMeshProUGUI label;
        public List<PuzzlePiece> pieces;
        public GameObject bg;
        public Image completePuzzle;

        public PuzzleData data;

        private void OnEnable()
        {
            actionBtn.onClick.AddListener(delegate { ActionButton(); });
        }

        private void ActionButton()
        {
            ((PuzzleInforPanel)UIManager.Instance.puzzleInforPanel).Init(data);
            UIManager.Instance.puzzleInforPanel.Open();
        }

        public void Init(PuzzleData data)
        {
            this.data = data;
            label.text = this.data.id;
            completePuzzle.sprite = data.sprites[9];

            UpdateData();
        }
        public void UpdateData()
        {
            for (int i = 0; i < 9; i++)
            {
                pieces[i].UpdateData(data.id + "_" + i);
            }
            if(DataManager.Instance.GetInt(data.id)==9)
            {
                for (int i = 0; i < 9; i++)
                {
                    pieces[i].gameObject.SetActive(false);
                }
                bg.SetActive(false);
            }
        }

        private void OnDisable()
        {
            actionBtn.onClick.RemoveAllListeners();
        }
    }
}