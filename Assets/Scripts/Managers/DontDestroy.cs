//using Dragon.SDK;
using System;
using System.Collections;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    #region Singleton
    public static DontDestroy Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //intAdsCapping = float.Parse(FireBaseRemoteConfig.GetStringConfig("time_post_inter", "30"));
    }
    #endregion

    #region AdsCapping
    public float intAdsCapping = 30f;
    public float curentAdsCapping;

    public float timePlay;
    #endregion

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.Gameplay)
        {
            curentAdsCapping += Time.unscaledDeltaTime;
            timePlay += Time.deltaTime;
        }
    }

    public void ShowInterAds(Action action = null)
    {
        if (curentAdsCapping >= intAdsCapping)
        {
            //GameManager.Instance.ShowAdsInter(Helper.Upg_Level_Placement, delegate
            //{
            //    curentAdsCapping = 0;
            //    action?.Invoke();
            //});

        }
    }
}
