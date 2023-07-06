using Assets.Scripts.Data;
using Assets.Scripts.UI.ResourceRecive;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Puzzle
{
    public class NewPuzzlePiecePanel : BasePopupUI
    {
        public Animator ani;

        public Button closeBtn;
        public Button anotherPuzzleBtn;
        public TextMeshProUGUI label;
        public List<PuzzlePiece> pieces;
        public GameObject bg;
        public Image completePuzzle;

        public PuzzleData data;
        private string newPiece;
        private int currentAmount;
        public void Init()
        {
            currentAmount = 0;
            GetRandomPuzzlePiece(out data, out newPiece);
            label.text = this.data.id;
            bg.SetActive(true);
            completePuzzle.sprite = data.sprites[9];
            UpdateData();
        }

        public void UpdateData()
        {
            for (int i = 0; i < 9; i++)
            {
                pieces[i].UpdateData(data.id + "_" + i, newPiece, () => ShowAll());
                pieces[i].render.sprite = data.sprites[i];
            }
        }
        private void ShowAll()
        {
            if (currentAmount == 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    pieces[i].gameObject.SetActive(false);
                }

                ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position,
                                   delegate
                                   {
                                       SoundManager.Instance.Play("GetCoins");
                                       DataManager.Instance.AddCoins(data.amountReward);
                                   });

                UIManager.Instance.currentcyPanel.Open();
                anotherPuzzleBtn.gameObject.SetActive(false);
                closeBtn.gameObject.SetActive(false);
                bg.SetActive(false);
            }
        }
        public override void LoadData()
        {
            closeBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); CloseButton(); });
            anotherPuzzleBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); AnotherPuzzleButton(); });
            ani.Play("Open");
        }

        private void AnotherPuzzleButton()
        {
            GameManager.Instance.ShowAdsReward(Helper.Another_Puzzle_Placement, delegate
            {
                ani.Play("Open");

            });
        }

        public bool GetRandomPuzzlePiece(out PuzzleData parent, out string newPiece)
        {
            parent = null;
            newPiece = "";
            int length = GameManager.Instance.puzzleData.Count;

            for (int i = 0; i < length; i++)
            {
                currentAmount = DataManager.Instance.GetInt(GameManager.Instance.puzzleData[i].id);
                if (currentAmount < 9)
                {
                    do
                    {
                        newPiece = GameManager.Instance.puzzleData[i].id + "_" + Random.Range(0, 9);
                    } while (DataManager.Instance.HasKey(newPiece));
                    currentAmount++;
                    DataManager.Instance.SetInt(GameManager.Instance.puzzleData[i].id, currentAmount);
                    DataManager.Instance.SetInt(newPiece, currentAmount);
                    parent = GameManager.Instance.puzzleData[i];
                    return true;
                }
            }
            return false;
        }

        private void CloseButton()
        {
            UIManager.Instance.currentcyPanel.Close();
            ani.Play("Close");
        }

        public override void SaveData()
        {
            closeBtn.onClick.RemoveAllListeners();
            anotherPuzzleBtn.onClick.RemoveAllListeners();

        }
    }
}