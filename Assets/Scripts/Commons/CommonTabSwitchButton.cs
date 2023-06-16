using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Commons
{
    public class CommonTabSwitchButton : CommonSwitchButtonVisual
    {
        public static int id;
        public static Action OnSelect;
        public static Action OnSelectDone;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnSelect += OnSelected;
        }
        protected virtual void OnSelected()
        {
            SetStatus(id == gameObject.GetInstanceID());
        }

        protected override void OnClick()
        {
            if (status) return;

            SetIDChose();
            base.OnClick();
            OnSelect?.Invoke();
            OnSelectDone?.Invoke();
        }
        protected virtual void SetIDChose()
        {
            id = gameObject.GetInstanceID();
        }
    }
}