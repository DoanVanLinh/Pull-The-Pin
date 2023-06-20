using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Puzzle
{
    public class PuzzleGroupPanel : BaseUI
    {
        public PuzzleGroupElement groupPrefab;
        public Button closeBtn;
        public Transform groupParent;
        private void Awake()
        {
            int length = GameManager.Instance.puzzleGroupData.Count;
            for (int i = 0; i < length; i++)
            {
                Instantiate(groupPrefab, Vector3.zero, Quaternion.identity, groupParent).LoadGroup(GameManager.Instance.puzzleGroupData[i]);
            }
        }
        public override void LoadData()
        {
            
        }

        public override void SaveData()
        {

        }

    }
}