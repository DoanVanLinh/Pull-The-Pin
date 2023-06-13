using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Commons
{
    public class CommonSpriteTextPopup : PoolingObject
    {
        public float spacing;

        public SpriteRenderer[] texts;
        public Dictionary<char, Sprite> alphabet;
        public string content = "";
        public float lengthContent;
        public List<TextPopup> listTypeText;

        public GameObject contentObj;
        public SpriteRenderer back;
        public Vector2 newPosition;
        public float timeAnimation;
        public TypeText type;
        public override void Init()
        {

        }
        [Button()]
        public void LoadText(string text, TypeText type)
        {
            if (text == "0") Dispose();

            back.gameObject.SetActive(type == TypeText.HeadShot);

            if (!content.Equals(text))
            {
                content = text;
                content = content.ToLower();
                lengthContent = 0;

                //Deactive all text
                for (int i = 1; i < texts.Length; i++)
                {
                    texts[i].gameObject.SetActive(false);
                }

                //GetType
                Color color = new Color();
                for (int i = 0; i < listTypeText.Count; i++)
                {
                    if (listTypeText[i].typeText == type)
                    {
                        color = listTypeText[i].color;
                        break;
                    }
                }
                int contentLength = content.Length;
                for (int i = 0; i < contentLength; i++)
                {
                    if (type == TypeText.HeadShot)
                        texts[i].sortingOrder = 2;
                    else
                        texts[i].sortingOrder = 0;

                    texts[i].gameObject.SetActive(true);

                    if (alphabet.ContainsKey(content[i]))
                        texts[i].sprite = alphabet[content[i]];
                    else
                        texts[i].sprite = alphabet['.'];

                    texts[i].color = color;

                    float lenghtText = texts[i].size.x + spacing;

                    if (i != 0)
                    {
                        texts[i].transform.localPosition = new Vector2(lenghtText, 0);
                        lengthContent += lenghtText;
                    }
                }

                contentObj.transform.localPosition = Vector3.left * lengthContent / 2f;

            }

            if (Helper.IsOutOfView(transform.position))
            {
                Dispose();
                return;
            }

            contentObj.transform.DOLocalMove(newPosition, timeAnimation).SetEase(Ease.InOutSine).OnComplete(() =>
            contentObj.transform.DOLocalMove(Vector2.zero, timeAnimation).SetEase(Ease.InOutSine).OnComplete(() =>
                Dispose()
            ));

        }
        public override void Dispose()
        {
            contentObj.transform.DOKill();

            base.Dispose();
        }
    }
}