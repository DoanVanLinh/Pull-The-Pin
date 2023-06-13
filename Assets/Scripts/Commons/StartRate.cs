using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StartRate : MonoBehaviour,IPointerDownHandler
{
    private Transform startParent;
    private Color hideColor;
    private Color showColor;

    private int countStart;
    private List<Image> startImg = new List<Image>();

    void Start()
    {
        countStart = 1;
        hideColor = Color.black;
        showColor = Color.white;
        GetComponent<Image>().color = hideColor;
        startParent = transform.parent;
        startImg.AddRange(startParent.GetComponentsInChildren<Image>());
        RatePanel.countStar = 0;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.Play("Click");
        countStart = 0;
        foreach (Transform child in startParent)
        {
            countStart++;

            if (child == transform)
                break;
        }
        for (int i = 0; i < 5; i++)
        {
            if (i < countStart)
                startImg[i].color = showColor;
            else
                startImg[i].color = hideColor;
        }

        RatePanel.countStar = countStart;
    }
}
