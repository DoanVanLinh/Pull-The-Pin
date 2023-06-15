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
        defaultLoc = transform.localPosition;
        targetLoc = transform.localPosition + transform.up * 50;
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
        transform.localPosition = defaultLoc;
    }
    private void OnPlayerTouchOn()
    {
        canTouch = false;
        SoundManager.Instance.Play("Pin");
        transform.DOLocalMove(targetLoc, timeMove)
                 .SetEase(Ease.Linear)
                 .SetSpeedBased(true)
                 .OnUpdate(()=> { 
                    if(GameManager.Instance.gameState!=GameState.Gameplay)
                         gameObject.SetActive(false);
                 })
                 .OnComplete(()=>{
                     gameObject.SetActive(false);
                 });
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
