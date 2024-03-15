using UnityEngine;

/// <summary>
/// State Machine implementation.
/// Uses BaseState as base class for storing currently operating state.
/// </summary>
public class ProjectileStateMachine : BaseStateMachine
{
    // Reference to movement script of our projectile.
    [SerializeField]
    private Movement movement;
    public Movement Movement => movement;

    private float lifespan = 3f;
    private float lifeTimer;

    public void StartState(Vector3 turretDirection)
    {
        ProjectileMoveState projectMoveState = new ProjectileMoveState();
        projectMoveState.moveDirection = turretDirection.normalized;
        lifespan = ConstantsLoader.Instance.projectileLifeSpan;
        lifeTimer = lifespan;
        ChangeState(projectMoveState);
    }

    private void ReturnToPool()
    {
        ProjectilePooler.Instance.ReturnProjectile(gameObject);
    }

    // FixedUpdate is typically used for physics-related calculations because it's called at a fixed interval, making it more predictable and consistent for physics simulations.
    private void FixedUpdate()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        lifeTimer -= Time.fixedDeltaTime;
        if (lifeTimer <= 0f)
        {
            ReturnToPool();
        }
    }
}

