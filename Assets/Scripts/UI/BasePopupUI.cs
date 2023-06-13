using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public class BasePopupUI : BaseUI
    {
        public static BaseUI currentPopupUI;
        public static Queue<BaseUI> waittingPopupUI = new Queue<BaseUI>();

        public override void Open()
        {
            if (currentPopupUI != null)
                waittingPopupUI.Enqueue(this);
            else
            {
                base.Open();
                currentPopupUI = this;
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
            currentPopupUI = null;
            if (waittingPopupUI.Count != 0)
                waittingPopupUI.Dequeue().Open();
        }

        public override void LoadData()
        {

        }

        public override void SaveData()
        {

        }
    }
}