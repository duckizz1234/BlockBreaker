using UnityEngine;

public class ProjectileFreezeState : BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();
        ((ProjectileStateMachine)owner).Movement.ToggleMove(false);

    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
