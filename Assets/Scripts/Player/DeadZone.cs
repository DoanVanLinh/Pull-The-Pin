using System.Collections;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Helper.BALL_TAG))
            GameManager.Instance.SetGameState(GameState.Lose);
    }
}
