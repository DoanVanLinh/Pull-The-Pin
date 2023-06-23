using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public static Action OnVisualUpdate;

    private Vector3 defaultLoc;
    private Vector3 targetLoc;
    public float timeMove;

    public bool canTouch;

    [FoldoutGroup("Head"), SerializeField]
    private MeshFilter headMesh;
    [FoldoutGroup("Body"), SerializeField]
    private MeshFilter bodyMesh;
    [FoldoutGroup("Material"), SerializeField]
    private Material headMaterial;
    [FoldoutGroup("Material"), SerializeField]
    private Material bodyMaterial;
    private void Awake()
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
    private void Update()
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
    private void OnPlayerTouchOn()
    {
        canTouch = false;
        SoundManager.Instance.Play("Pin");
        transform.DOLocalMove(targetLoc, timeMove)
                 .SetEase(Ease.Linear)
                 .SetSpeedBased(true)
                 .OnUpdate(() =>
                 {
                     if (GameManager.Instance.gameState != GameState.Gameplay)
                         gameObject.SetActive(false);
                 })
                 .OnComplete(() =>
                 {
                     gameObject.SetActive(false);
                 });
    }

    private void OnDestroy()
    {
        transform.DOKill();
        OnVisualUpdate -= UpdateVisual;

    }
}
