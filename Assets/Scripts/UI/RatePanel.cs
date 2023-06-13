using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RatePanel : BaseUI
    {
        [FoldoutGroup("Button")]
        public Button rateBtn;
        [FoldoutGroup("Button")]
        public Button closeBtn;

        public static int countStar;
        public override void LoadData()
        {
            rateBtn.onClick.AddListener(delegate { RateButton(); });
            closeBtn.onClick.AddListener(delegate {SoundManager.Instance.Play("Click"); Close(); } );
        }
        private void RateButton()
        {
            SoundManager.Instance.Play("Click");

            if (countStar == 5)
            {
                Application.OpenURL(Helper.OPEN_LINK_RATE);
            }
            Close();
        }
        public override void SaveData()
        {
            rateBtn.onClick.RemoveAllListeners();
            closeBtn.onClick.RemoveAllListeners();
        }
    }
}