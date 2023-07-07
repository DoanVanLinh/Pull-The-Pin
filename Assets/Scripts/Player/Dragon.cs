using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Dragon : MonoBehaviour
    {
        public Animator ani;
        public void SetTrigger(string trigger = "Idle")
        {
            if (!gameObject.activeSelf)
                return;

            ani.SetTrigger(trigger);
        }

    }
}