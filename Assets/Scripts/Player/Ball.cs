using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallType type;
    public Texture greyTexture;
    public List<Texture> colorTexture;
    public Material material;
    public MeshRenderer meshRender;
    public Rigidbody rb;

    public ParticleSystem fxColor;
    public ParticleSystem fxGrey;
    private void Start()
    {
        material = meshRender.sharedMaterial;
        meshRender.enabled = true;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ;
        SetColor();

    }

    [Button()]
    public virtual void Init(BallType type, float size = 1)
    {
        this.type = type;
        gameObject.layer = LayerMask.NameToLayer(type == BallType.Color ? Helper.COLOR_BALL_LAYER : Helper.GREY_BALL_LAYER);
        transform.localScale = Vector3.one * size;
    }

    private void SetColor()
    {
        material = meshRender.material;
        switch (type)
        {
            case BallType.Color:
                material.SetTexture("_MainTex", colorTexture[Random.Range(0, colorTexture.Count)]);
                break;
            case BallType.Grey:
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
        type = BallType.Color;
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
        if (type == BallType.Grey) return;

        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.GREY_BALL_LAYER))
        {
            other.GetComponent<Ball>().ToColor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (type)
        {
            case BallType.Color:
                if (other.gameObject.layer == LayerMask.NameToLayer(Helper.BUCK_LAYER))
                {
                    rb.constraints = RigidbodyConstraints.None;
                    fxColor.Play();
                    PlaySound();

                }
                break;
            case BallType.Grey:
                if (other.gameObject.layer == LayerMask.NameToLayer(Helper.BUCK_LAYER))
                {
                    meshRender.enabled = false;
                    fxGrey.Play();
                    PlaySound();

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
            GameManager.Instance.SetGameState(GameState.Lose);
        }

    }
}
