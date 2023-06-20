using Assets.Scripts.UI.Lose;
using System.Collections;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Helper.BALL_TAG))
        {
            ((LosePanel)UIManager.Instance.losePanel).loseType = ELoseType.LoseBall;

            GameManager.Instance.SetGameState(GameState.Lose);
        }
    }
}
