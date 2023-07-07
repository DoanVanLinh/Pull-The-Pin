using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class SizeEffectZone : MonoBehaviour
    {
        public float muiltiSize;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Helper.BALL_TAG))
            {
                other.transform.localScale = Vector3.one * muiltiSize;
                other.GetComponent<Ball>().PlaySound();
            }
        }
    }
}