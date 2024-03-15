using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateMachine : BaseStateMachine
{
    // Reference to UI root that holds references to different views
    [SerializeField]
    private UIRoot ui;
    public UIRoot UI => ui;

    /// <summary>
    /// Unity method called on first frame
    /// </summary>
    private void Start()
    {
        // Start game in menu state
        ChangeState(new MenuState());
    }

    public void OnStartGamePressed()
    {
        ChangeState(new GameState());
    }
}
