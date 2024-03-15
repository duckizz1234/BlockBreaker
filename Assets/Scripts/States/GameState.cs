using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        // Show Game view
        var listOfGameViews = ((MenuStateMachine)owner).UI.GameView;
        foreach (var gameView in listOfGameViews)
        {
            gameView.ShowView();
        }
        GameManager.Instance.StartLevel();
    }

    public override void DestroyState()
    {
        // Hide menu view

        base.DestroyState();
    }
}
