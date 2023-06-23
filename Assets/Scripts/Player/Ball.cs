using Assets.Scripts.UI.Lose;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public static Action OnUpdateVisual;

    public EBallType type;
    public Texture greyTexture;
    public List<Texture> colorTexture;
    public Material material;
    public MeshRenderer meshRender;
    public Rigidbody rb;

    public ParticleSystem fxColor;
    public ParticleSystem fxGrey;

    [FoldoutGroup("Visual")]
    public MeshFilter meshFilter;
    [FoldoutGroup("Visual")]
    public List<Trail> trails;

    private void Start()
    {
        material = meshRender.sharedMaterial;
        meshRender.enabled = true;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ;
        UpdateVisual();
        OnUpdateVisual += UpdateVisual;
    }

    public void UpdateVisual()
    {
        meshFilter.mesh = GameManager.Instance.currentBall.ballMesh;
        colorTexture.Clear();
        colorTexture.AddRange(GameManager.Instance.currentBall.ballTexture);
        SetColor();

        //trails
        int length = trails.Count;
        for (int i = 0; i < length; i++)
        {
            trails[i].gameObject.SetActive(trails[i].id == GameManager.Instance.currentTrail.id);
        }

    }

    [Button()]
    public virtual void Init(EBallType type, float size = 1)
    {
        this.type = type;
        gameObject.layer = LayerMask.NameToLayer(type == EBallType.Color ? Helper.COLOR_BALL_LAYER : Helper.GREY_BALL_LAYER);
        transform.localScale = Vector3.one * size;
    }

    private void SetColor()
    {
        material = meshRender.material;
        switch (type)
        {
            case EBallType.Color:
                material.SetTexture("_MainTex", colorTexture[Random.Range(0, colorTexture.Count)]);
                break;
            case EBallType.Grey:
                material.SetTexture("_MainTex", greyTexture);
                break;
        }
    }
    public void ToColor()
    {
        StartCoroutine(IEToColor());
    }
    IEnumerator IEToColor()
    {
        yield return null;
        if (type == EBallType.Color) yield break;

        DataManager.Instance.GetData().AddDailyMissionValue(EDailyMissionID.ColorBalls, 1);
        type = EBallType.Color;
        SetColor();
        gameObject.layer = LayerMask.NameToLayer(Helper.COLOR_BALL_LAYER);
        fxColor.Play();

        PlaySound();
    }
    private void PlaySound()
    {
        string[] nameSounds = new string[3] { "Bubble", "Bubble2", "Bubble3" };
        SoundManager.Instance.Play(nameSounds[Random.Range(0, 3)]);
    }

    private void OnTriggerStay(Collider other)
    {
        if (type == EBallType.Grey) return;

        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.GREY_BALL_LAYER))
        {
            other.GetComponent<Ball>().ToColor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (type)
        {
            case EBallType.Color:
                if (other.gameObject.layer == LayerMask.NameToLayer(Helper.BUCK_LAYER))
                {
                    rb.constraints = RigidbodyConstraints.None;
                    fxColor.Play();
                    PlaySound();

                }
                break;
            case EBallType.Grey:
                if (other.gameObject.layer == LayerMask.NameToLayer(Helper.BUCK_LAYER))
                {
                    meshRender.enabled = false;
                    fxGrey.Play();
                    PlaySound();

                    ((LosePanel)UIManager.Instance.losePanel).loseType = ELoseType.CollectGreyBall;
                    GameManager.Instance.SetGameState(GameState.Lose);
                }
                break;
            default:
                break;
        }

    }

    public void AddForce(Vector3 force, bool isOutOfScreen = false)
    {
        rb.AddForce(force, ForceMode.Impulse);

        if (isOutOfScreen)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(((GameManager.Instance.mainCam.transform.position + Random.insideUnitSphere.normalized * 3f) - transform.position).normalized * 40f, ForceMode.Impulse);
            ((LosePanel)UIManager.Instance.losePanel).loseType = ELoseType.BomBall;
            if (GameManager.Instance.gameState == GameState.Gameplay)
                GameManager.Instance.SetGameState(GameState.Lose);
        }

    }

    private void OnDestroy()
    {
        OnUpdateVisual -= UpdateVisual;

    }
}
