using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Vector3 defaultLoc;
    private Vector3 targetLoc;
    public float timeMove;
    private void Start()
    {
        defaultLoc = transform.position;
        targetLoc = transform.position + transform.up * 10;
    }
    private void OnMouseDown()
    {
        Debug.Log("Touch");
        OnPlayerTouchOn();
    }
    public void Init()
    {
        transform.position = defaultLoc;
    }
    private void OnPlayerTouchOn()
    {
        transform.DOMove(targetLoc, timeMove)
                 .SetEase(Ease.Linear)
                 .SetSpeedBased(true);
    }
}
