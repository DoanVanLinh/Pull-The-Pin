using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.PSD;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "PuzzleData", menuName = "We/PuzzleData", order = 0)]
    public class PuzzleData : ScriptableObject
    {
        public string id;
        public int amountReward;
        public List<Sprite> sprites = new List<Sprite>();

#if UNITY_EDITOR
        [Button()]
        private void GetData()
        {
            var psdPath = "Assets/Resources/Sprites/Puzzles/" + id + ".psb";
            foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(psdPath))
            {
                if (obj is Sprite sprite)
                {
                    sprites.Add(obj as Sprite);
                }
            }
        }
#endif
    }
}