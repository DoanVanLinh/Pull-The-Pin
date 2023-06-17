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
        public InputAction HackScraps;
        public InputAction HackHeroSouls;
        public InputAction HackBricks;
        public InputAction HackHero;
        public InputAction HackRandomItem;

        


        private void OnEnable()
        {
            currentTimeScale = 0;
            HackCoins.Enable();
            HackLevel.Enable();
            HackScraps.Enable();
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
            HackRandomItem.performed += delegate
            {

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
            HackScraps.Disable();
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
