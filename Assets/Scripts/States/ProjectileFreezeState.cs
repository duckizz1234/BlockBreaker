/// <summary>
/// State where the projectile is frozen on screen
/// </summary>
public class ProjectileFreezeState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();
        // Triggers projectile to stop moving
        ((ProjectileStateMachine)owner).Movement.ToggleMove(false);
    }
}
