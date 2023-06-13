using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Commons
{
    public class UINotification : MonoBehaviour
    {
        public Animator ani;
        public TextMeshProUGUI content;

        private void OnEnable()
        {
            ani.Play("Open");
        }
        public void PushNotification(string content)
        {
            gameObject.SetActive(false);
            this.content.text = content;
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
        
    }
}