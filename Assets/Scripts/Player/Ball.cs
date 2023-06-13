using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallType type;

    public virtual void Init(BallType type)
    {
        this.type = type;
        gameObject.layer = LayerMask.NameToLayer(type ==BallType.Color? Helper.COLOR_BALL_LAYER:Helper.GREY_BALL_LAYER);
        transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
    }
    public void ToColor()
    {
        type = BallType.Color;
        gameObject.layer = LayerMask.NameToLayer(Helper.COLOR_BALL_LAYER);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.GREY_BALL_LAYER))
        {
            other.GetComponent<Ball>().ToColor();
        }
    }
}
