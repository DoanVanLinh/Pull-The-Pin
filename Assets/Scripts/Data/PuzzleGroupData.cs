using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "PuzzleGroupData", menuName = "We/PuzzleGroupData", order = 0)]
    public class PuzzleGroupData : ScriptableObject
    {
        public string id;
        public Sprite thumbnail => datas[0].sprites[9];
        public List<PuzzleData> datas;
    }
}