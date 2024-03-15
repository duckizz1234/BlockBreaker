using UnityEngine;

/// <summary>
/// State Machine implementation.
/// Uses BaseState as base class for storing currently operating state.
/// </summary>
public class ProjectileStateMachine : BaseStateMachine
{
    /// <summary>
    /// Reference to movement script of our projectile. 
    /// </summary>
    [SerializeField]
    private Movement movement;
    public Movement Movement => movement;

    /// <summary>
    /// delegate events to send log messages to the UIConsole
    /// </summary>
    /// <param name="message"></param>
    public delegate void LogMessageDelegate(string message);
    public static event LogMessageDelegate OnLogMessage;

    /// <summary>
    /// Lifespan of the projectile in seconds. Value will be set by Constants.json
    /// </summary>
    private float lifespan = 3f;

    /// <summary>
    /// Used to countdown the timer
    /// </summary>
    private float lifeTimer;

    /// <summary>
    /// Direction where the projectile is moving towards
    /// </summary>
    private Vector3 currentDirection;

    private bool canMove = false;

    /// <summary>
    /// Triggered to start moving process
    /// </summary>
    /// <param name="turretDirection"></param>
    public void StartMovingState(Vector3 turretDirection)
    {
        currentDirection = turretDirection;
        ProjectileMoveState projectileMoveState = new ProjectileMoveState();
        projectileMoveState.moveDirection = turretDirection.normalized;
        lifespan = ConstantsLoader.Instance.projectileLifeSpan;
        lifeTimer = lifespan;
        canMove = true;
        ChangeState(projectileMoveState);
    }

    /// <summary>
    /// FixedUpdate is typically used for physics-related calculations because it's called at a fixed interval, 
    /// making it more predictable and consistent for physics simulations. 
    /// </summary>
    private void FixedUpdate()
    {
        if (canMove)
        {
            UpdateTimer();
        }
    }
    
    /// <summary>
    /// Decrements the counter and when it reaches 0, returns the projectile to the pool
    /// </summary>
    private void UpdateTimer()
    {
        lifeTimer -= Time.fixedDeltaTime;
        if (lifeTimer <= 0f)
        {
            ProjectilePooler.Instance.ReturnProjectile(gameObject);
        }
    }

    /// <summary>
    /// Triggered when the projectile collides with something
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canMove && (collision.gameObject.CompareTag(ConstantsLoader.BlockTag) || collision.gameObject.CompareTag(ConstantsLoader.WallTag)))
        {
            // Calculate reflection direction
            Vector3 normal = collision.GetContact(0).normal;
            currentDirection = Vector3.Reflect(currentDirection, normal);
            // With the new direction, trigger moving again
            StartMovingState(currentDirection);

            if (collision.gameObject.CompareTag(ConstantsLoader.BlockTag))
            {
                OnLogMessage.Invoke("Projectile hit block");
            }            
        }
    }

    /// <summary>
    /// Freezes the projectile
    /// </summary>
    public void FreezeProjectile()
    {
        canMove = false;
        ChangeState(new ProjectileFreezeState());
    }
}

