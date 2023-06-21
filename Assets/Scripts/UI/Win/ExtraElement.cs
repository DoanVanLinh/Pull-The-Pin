using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Win
{
    public class ExtraElement : MonoBehaviour
    {
        public int id;
        public int extra;

        public static int currentId;
        public static Action onIndexChange;
        public static ExtraElement currentElement;

        public Image image;
        private void Awake()
        {
            currentId = 0;
            onIndexChange += OnIndexChange;

        }

        private void OnIndexChange()
        {
            image.color = currentId == id ? Color.white : new Color(0.85f, 0.85f, 0.85f);
            if (currentId == id)
                currentElement = this;
        }
        private void OnDisable()
        {
            onIndexChange -= OnIndexChange;

        }
    }
}