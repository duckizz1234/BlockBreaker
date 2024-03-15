using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        // Show menu view
       // ((MenuStateMachine)owner).UI.MenuView.ShowView();
    }

    public override void DestroyState()
    {
        // Hide menu view
        ((MenuStateMachine)owner).UI.MenuView.HideView();

        base.DestroyState();
    }
}
