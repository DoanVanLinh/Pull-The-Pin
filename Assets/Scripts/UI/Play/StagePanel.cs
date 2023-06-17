using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Play
{
    public class StagePanel : MonoBehaviour
    {
        public List<StageElement> elements;

        private int currentLevel;
        private int countLevel;
        public void Init(int countLevel)
        {
            ResetElement();

            if (countLevel < 2)
            {
                gameObject.SetActive(false);
                return;
            }
            else
                gameObject.SetActive(true);

            this.countLevel = countLevel;
            currentLevel = DataManager.Instance.CurrentLevel;
            UpdateStage();
        }

        public void NextLevel()
        {
            currentLevel++;

            UpdateStage();

        }
        private void UpdateStage()
        {
            for (int i = 0; i < countLevel; i++)
            {
                if (i < currentLevel)
                    elements[i].SetVisual(EStageType.Complete);
                else if (i > currentLevel)
                    elements[i].SetVisual(EStageType.Lock);
                else
                    elements[i].SetVisual(EStageType.Playing);
            }
        }
        public void ResetStage()
        {
            currentLevel = 0;
            UpdateStage();
        }
        private void ResetElement()
        {
            int length = elements.Count;
            for (int i = 0; i < length; i++)
            {
                elements[i].InitStage();
            }
        }
    }
}