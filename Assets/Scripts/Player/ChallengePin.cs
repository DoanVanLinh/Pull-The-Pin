using System.Collections;
using UnityEngine;

public class ChallengePin : Pin
{
    private ChallengeLevel owner;

    public void Init(ChallengeLevel owner)
    {
        this.owner = owner;
        base.Init();
    }
    protected override void OnPlayerTouchOn()
    {
        if (owner.CurrentMove <= 0)
            return;

        base.OnPlayerTouchOn();
        owner.CurrentMove--;
    }
}
