using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        // Show menu view
        ((MenuStateMachine)owner).UI.ReplayView.ShowView();

        ProjectilePooler.Instance.FreezeAllProjectiles();
        GameManager.Instance.player.ToggleCanMove(false);
    }

    public override void DestroyState()
    {
        // Hide menu view
        ((MenuStateMachine)owner).UI.ReplayView.HideView();

        base.DestroyState();
    }
}
