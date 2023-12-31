﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Commons
{
    public class CommonSwitchButtonVisual : MonoBehaviour
    {
        public bool status;

        public GameObject onVisual;
        public GameObject offVisual;

        public Button button;

        public Action OnClickDone;

        protected virtual void OnEnable()
        {
            button.onClick.AddListener(delegate { OnClick(); });
        }
        protected virtual void OnClick()
        {
            status = !status;

            onVisual?.SetActive(status);
            offVisual?.SetActive(!status);
            OnClickDone?.Invoke();
        }

        public virtual void SetStatus(bool status)
        {
            this.status = status;
            onVisual?.SetActive(status);
            offVisual?.SetActive(!status);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}