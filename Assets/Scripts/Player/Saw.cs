using Assets.Scripts.UI.Lose;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Saw : MonoBehaviour
    {
        public float smooth;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Helper.BALL_TAG))
            {
                Ball ball = other.GetComponent<Ball>();

                ball.meshRender.enabled = false;
                ball.fxGrey.Play();
                ball.PlaySound();

                ((LosePanel)UIManager.Instance.losePanel).loseType = ELoseType.SawBall;
                Destroy(ball.gameObject, 1f);
                GameManager.Instance.SetGameState(GameState.Lose);
            }
        }

        private void FixedUpdate()
        {
            transform.localEulerAngles += Vector3.forward * Time.deltaTime * smooth;
        }
    }
}