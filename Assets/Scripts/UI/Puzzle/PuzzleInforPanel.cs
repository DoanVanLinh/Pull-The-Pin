using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Puzzle
{
    public class PuzzleInforPanel : BaseUI
    {
        public Animator ani;

        public Button closeBtn;
        public TextMeshProUGUI label;
        public TextMeshProUGUI amountReward;
        public List<PuzzlePiece> pieces;
        public GameObject bg;
        public Image completePuzzle;
        public PuzzleData data;

        public void Init(PuzzleData data)
        {
            this.data = data;
            label.text = this.data.id;
            amountReward.text = this.data.amountReward.ToString() ;
            completePuzzle.sprite = data.sprites[9];
            UpdateData();
        }

        public void UpdateData()
        {
            for (int i = 0; i < 9; i++)
            {
                pieces[i].UpdateData(data.id + "_" + i);
            }
            if (DataManager.Instance.GetInt(data.id) == 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    pieces[i].gameObject.SetActive(false);
                }
                bg.SetActive(false);
            }
        }
        public override void LoadData()
        {
            closeBtn.onClick.AddListener(delegate { CloseButton(); });
            ani.Play("Open");
        }

        private void CloseButton()
        {
            ani.Play("Close");
        }

        public override void SaveData()
        {
            closeBtn.onClick.RemoveAllListeners();

        }
    }
}