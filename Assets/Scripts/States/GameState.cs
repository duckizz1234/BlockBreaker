/// <summary>
/// Start playing game state
/// </summary>
public class GameState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        // Show Game view
        var listOfGameViews = ((MenuStateMachine)owner).UI.GameView;

        // Potentially there can be several views that make up this state so trigger each of them to show
        foreach (var gameView in listOfGameViews)
        {
            gameView.ShowView();
        }

        // Start the level
        GameManager.Instance.StartLevel();
    }
}
