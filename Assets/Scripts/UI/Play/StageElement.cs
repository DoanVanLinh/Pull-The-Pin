using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Play
{
    public class StageElement : MonoBehaviour
    {

        public Image visual;
        public Sprite lockSpr;
        public Sprite playingSpr;
        public Sprite completeSpr;

        public void InitStage()
        {
            visual.sprite = lockSpr;
            gameObject.SetActive(false);
        }

        public void SetVisual(EStageType type)
        {
            gameObject.SetActive(true);

            switch (type)
            {
                case EStageType.Lock:
                    visual.sprite = lockSpr;

                    break;
                case EStageType.Playing:
                    visual.sprite = playingSpr;
                    break;
                case EStageType.Complete:
                    visual.sprite = completeSpr;
                    break;
                default:
                    break;
            }
            visual.SetNativeSize();
        }
    }
}