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

    public Transform tets;
    private void Update()
    {
        if (Ball.hieghtestBall != null)
            tets.position = Ball.hieghtestBall.transform.position;
        if (GameManager.Instance.currentGameState == GameState.NormalMode)
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
