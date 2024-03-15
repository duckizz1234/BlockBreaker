using UnityEngine;

/// <summary>
/// Move state for the projectile
/// </summary>
public class ProjectileMoveState : BaseState
{
    /// <summary>
    /// Store picked direction to go to 
    /// </summary>
    public Vector2 moveDirection;

    public override void UpdateState()
    {
        base.UpdateState();

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // Passing direction to our Movement component
        ((ProjectileStateMachine)owner).Movement.Move(moveDirection);
    }
}
