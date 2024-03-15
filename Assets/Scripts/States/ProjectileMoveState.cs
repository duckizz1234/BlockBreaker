using UnityEngine;

public class ProjectileMoveState : BaseState
{
    // Store picked direction to go to
    public Vector2 moveDirection;

    public override void PrepareState()
    {
        base.PrepareState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // Passing direction to our SimpleMovement component
        ((ProjectileStateMachine)owner).Movement.Move(moveDirection);
    }
}
