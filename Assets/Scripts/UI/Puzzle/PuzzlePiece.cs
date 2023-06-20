using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Puzzle
{
    public class PuzzlePiece : MonoBehaviour
    {
        public string id;

        public void UpdateData(string id)
        {
            this.id = id;
            gameObject.SetActive(DataManager.Instance.HasKey(id));
        }
    }
}