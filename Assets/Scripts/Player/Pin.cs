using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Vector3 defaultLoc;
    private Vector3 targetLoc;
    public float timeMove;

    private bool canTouch;
    private void Awake()
    {
        defaultLoc = transform.position;
        targetLoc = transform.position + transform.up * 50;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (GameManager.Instance.DetectPin() == this)
                OnPlayerTouchOn();
    }
    public void Init()
    {
        canTouch = true;
        transform.position = defaultLoc;
    }
    private void OnPlayerTouchOn()
    {
        canTouch = false;

        transform.DOMove(targetLoc, timeMove)
                 .SetEase(Ease.Linear)
                 .SetSpeedBased(true);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
