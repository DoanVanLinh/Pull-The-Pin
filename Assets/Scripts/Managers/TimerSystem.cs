using System.Collections;
using UnityEngine;
//using Assets.Scripts.Player;

public class TimerSystem : MonoBehaviour
{
    private void Start()
    {
        DataManager.Instance.Init();
        GameManager.Instance.Init();
        UIManager.Instance.Init();
    }
}
