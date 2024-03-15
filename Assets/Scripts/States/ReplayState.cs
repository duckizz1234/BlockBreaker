/// <summary>
/// Triggers the replay state. This is when all the blocks have been destroyed
/// </summary>
public class ReplayState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        // Show menu view
        ((MenuStateMachine)owner).UI.ReplayView.ShowView();

        // Freezes all existing projectiles on the screen
        ProjectilePooler.Instance.FreezeAllProjectiles();

        // Freezes the player as well
        GameManager.Instance.player.ToggleCanMove(false);
    }

    public override void DestroyState()
    {
        // Hide menu view
        ((MenuStateMachine)owner).UI.ReplayView.HideView();

        base.DestroyState();
    }
}
