using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI.Win
{
    public class Extra : MonoBehaviour
    {
        public TextMeshProUGUI coinsTxt;

        private int currentIndex;
        public bool running;
        private int baseCoins;

        public void Init(int baseCoin)
        {
            running = true;
            this.baseCoins = baseCoin;
            currentIndex = 0;
            StartCoroutine(IEAni());
        }
        IEnumerator IEAni()
        {
            while (running)
            {
                ExtraElement.currentId = currentIndex % 5;
                ExtraElement.onIndexChange?.Invoke();
                if (ExtraElement.currentElement != null)
                    coinsTxt.text = (ExtraElement.currentElement.extra * baseCoins).ToString();
                yield return new WaitForSeconds(.2f);
                currentIndex++;
            }
        }
    }
}