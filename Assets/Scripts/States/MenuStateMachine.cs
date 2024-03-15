using UnityEngine;

/// <summary>
/// State machine that controls the UI being displayed
/// </summary>
public class MenuStateMachine : BaseStateMachine
{
    // Reference to UI root that holds references to different views
    [SerializeField]
    private UIRoot ui;
    public UIRoot UI => ui;

    private void Start()
    {
        // Start game in menu state
        ChangeState(new MenuState());
    }

    /// <summary>
    /// When Start & Replay buttons are pressed
    /// </summary>
    public void OnStartGamePressed()
    {
        ChangeState(new GameState());
    }
}
