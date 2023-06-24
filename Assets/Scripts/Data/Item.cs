using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "We/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [ShowInInspector, ReadOnly]
        public string id => itemType.ToString() + name;
        public EItemType itemType;
        public Sprite icon;
        public EItemUnlockType itemUnlockType;

        public int value;

        [ShowIf("itemType", EItemType.Ball)]
        public Mesh ballMesh;
        [ShowIf("itemType", EItemType.Ball)]
        public List<Texture> ballTexture;

        [ShowIf("itemType", EItemType.Pin)]
        public Mesh headPin;
        [ShowIf("itemType", EItemType.Pin)]
        public Texture headPinTexture;
        [ShowIf("itemType", EItemType.Pin)]
        public Mesh bodyPin;
        [ShowIf("itemType", EItemType.Pin)]
        public Texture bodyPinTexture;

        [ShowIf("itemType", EItemType.Theme)]
        public Sprite themeBG;
        [ShowIf("itemType", EItemType.Theme)]
        public Color wallColor;

        [ShowIf("itemType", EItemType.Wall)]
        public Sprite wallTexture;
    }
}