using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Data;

namespace Assets.Scripts.UI.Puzzle
{
    public class PuzzlePanel : BaseUI
    {
        public PuzzleGroupData data;
        public Button backBtn;
        public List<PuzzleElement> elements = new List<PuzzleElement>();
        public PuzzleElement prefabs;
        public TextMeshProUGUI label;
        public Transform parentContent;

        public override void LoadData()
        {
            backBtn.onClick.AddListener(delegate { Close(); });
            

        }
        private void SpawnElement()
        {
            int length = data.datas.Count;
            for (int i = 0; i < length; i++)
            {
                elements.Add(Instantiate(prefabs, Vector3.zero, Quaternion.identity, parentContent));
                elements[i].Init(data.datas[i]);
            }
        }
        private void ClearElement()
        {
            int length = elements.Count;
            for (int i = 0; i < length; i++)
            {
                Destroy(elements[i].gameObject);
            }
        }
        private void UpdateElement()
        {
            ClearElement();
            SpawnElement();
            int length = data.datas.Count;
            for (int i = 0; i < length; i++)
            {
                elements[i].UpdateData();
            }
        }
        public override void Open()
        {
            UpdateElement();
            base.Open();
            transform.position = UIManager.Instance.right;
            transform.DOKill();
            transform.DOMove(UIManager.Instance.center, 0.5f)
                .SetEase(Ease.Linear);
        }
        public override void Close()
        {
            transform.DOKill();
            transform.DOMove(UIManager.Instance.right, 0.5f)
                .SetEase(Ease.Linear);
        }

        public override void SaveData()
        {
            backBtn.onClick.RemoveAllListeners();

        }
    }
}