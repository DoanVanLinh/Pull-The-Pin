using System.Collections;
using UnityEngine;
//using Assets.Scripts.Player;

public class TimerSystem : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameState.Gameplay) return;

        //EnemyController.Instance?.OnUpdate(Time.fixedDeltaTime);
        //BasePlayer.Instance?.OnUpdate(Time.fixedDeltaTime);
    }
}
