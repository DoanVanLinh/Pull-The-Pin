using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buck : MonoBehaviour
{
    Level owner;
    public float currentPercent;
    public void Init(Level owner)
    {
        this.owner = owner;
        currentPercent = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.COLOR_BALL_LAYER))
        {
            currentPercent += owner.percentPerBall;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.GREY_BALL_LAYER))
        {
            GameManager.Instance.SetGameState(GameState.Lose);
        }
    }
}
