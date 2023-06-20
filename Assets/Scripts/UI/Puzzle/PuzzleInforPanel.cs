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
        public List<PuzzlePiece> pieces;

        public PuzzleData data;

        public void Init(PuzzleData data)
        {
            this.data = data;
            label.text = this.data.id;
            UpdateData();
        }

        public void UpdateData()
        {
            for (int i = 0; i < 9; i++)
            {
                pieces[i].UpdateData(data.id + "_" + i);
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