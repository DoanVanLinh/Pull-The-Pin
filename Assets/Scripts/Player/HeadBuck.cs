using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HeadBuck : MonoBehaviour
    {
        Dictionary<Ball, float> balls = new Dictionary<Ball, float>();
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                Ball ball = other.GetComponent<Ball>();
                if (balls.ContainsKey(ball))
                {
                    balls[ball] += Time.deltaTime;
                    if (balls[ball] >= 0.5f)
                    {
                        balls.Remove(ball);
                        ball.gameObject.SetActive(false);
                    }
                }
                else
                    balls.Add(ball, 0);
            }
        }
    }
}