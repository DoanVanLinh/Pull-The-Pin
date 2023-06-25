using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public static Action OnVisualUpdate;

    protected  Vector3 defaultLoc;
    protected  Vector3 targetLoc;
    public float timeMove;

    public bool canTouch;

    [FoldoutGroup("Head"), SerializeField]
    protected  MeshFilter headMesh;
    [FoldoutGroup("Body"), SerializeField]
    protected  MeshFilter bodyMesh;
    [FoldoutGroup("Material"), SerializeField]
    protected  Material headMaterial;
    [FoldoutGroup("Material"), SerializeField]
    protected  Material bodyMaterial;

    protected virtual  void Awake()
    {
        defaultLoc = transform.localPosition;
        targetLoc = transform.localPosition + transform.up * 50;
        UpdateVisual();
        OnVisualUpdate += UpdateVisual;
    }

    public void UpdateVisual()
    {
        headMesh.mesh = GameManager.Instance.currentPin.headPin;
        headMaterial.SetTexture("_MainTex", GameManager.Instance.currentPin.headPinTexture);

        bodyMesh.mesh = GameManager.Instance.currentPin.bodyPin;
        bodyMaterial.SetTexture("_MainTex", GameManager.Instance.currentPin.bodyPinTexture);
    }
    protected virtual  void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Helper.IsOverUI())
            if (GameManager.Instance.DetectPin() == this)
                OnPlayerTouchOn();
    }
    
    public void Init()
    {
        canTouch = true;
        transform.localPosition = defaultLoc;
    }
    protected virtual  void OnPlayerTouchOn()
    {
        canTouch = false;
        SoundManager.Instance.Play("Pin");
        transform.DOLocalMove(targetLoc, timeMove)
                 .SetEase(Ease.Linear)
                 .SetSpeedBased(true)
                 .OnUpdate(() =>
                 {
                     if (GameManager.Instance.currentGameState != GameState.NormalMode)
                         gameObject.SetActive(false);
                 })
                 .OnComplete(() =>
                 {
                     gameObject.SetActive(false);
                 });
    }

    protected virtual  void OnDestroy()
    {
        transform.DOKill();
        OnVisualUpdate -= UpdateVisual;

    }
}
