using Assets.Scripts.UI.Shop;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Assets.Scripts.Commons
{
    public class HackPC : MonoBehaviour
    {
        public int[] timeScaleList;
        private int currentTimeScale;
#if UNITY_EDITOR
        public InputAction HackTime;
        public InputAction HackCoins;
        public InputAction HackLevel;
        public InputAction HackShop;
        public InputAction HackHeroSouls;
        public InputAction HackBricks;
        public InputAction HackHero;
        public InputAction HackRandomItem;

        


        private void OnEnable()
        {
            currentTimeScale = 0;
            HackCoins.Enable();
            HackLevel.Enable();
            HackShop.Enable();
            HackHeroSouls.Enable();
            HackBricks.Enable();
            HackHero.Enable();
            HackRandomItem.Enable();
            HackTime.Enable();
        }

        private void Start()
        {
            HackCoins.performed += delegate { DataManager.Instance.AddCoins(1000000); };
           
            HackLevel.performed += delegate
            {
                GameManager.Instance.NextLevel();
            };
            HackShop.performed += delegate
            {
                foreach (string item in GameManager.Instance.itemsData.Keys)
                {
                    DataManager.Instance.GetData().AddItem(item);
                }

                ((ShopPanel)UIManager.Instance.shopPanel).UpdateBall();
                ((ShopPanel)UIManager.Instance.shopPanel).UpdatePin();
                ((ShopPanel)UIManager.Instance.shopPanel).UpdateTheme();
                ((ShopPanel)UIManager.Instance.shopPanel).UpdateTrail();
                ((ShopPanel)UIManager.Instance.shopPanel).UpdateWall();
            };
            HackTime.performed += delegate
            {
                SpeedUp();
            };
        }
        private void OnDisable()
        {
            HackCoins.Disable();
            HackLevel.Disable();
            HackShop.Disable();
            HackHeroSouls.Disable();
            HackBricks.Disable();
            HackHero.Disable();
            HackRandomItem.Disable();
            HackTime.Disable();

            StopAllCoroutines();
        }

        [SerializeField]
        bool runinBack;
        [Button()]
        private void Set()
        {
            Application.runInBackground = runinBack;
        }
        #endif

        public void SpeedUp()
        {
            currentTimeScale++;
            Time.timeScale = timeScaleList[currentTimeScale % timeScaleList.Length];

        }
    }
}
