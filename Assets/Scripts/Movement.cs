using UnityEngine;

/// <summary>
/// Simple Movement component.
/// Uses rigidbody to move around.
/// Its receiving input as a direction where it should go.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    // Reference to rigidbody
    private Rigidbody2D rb;

    // Storing input for movement in FixedUpdate
    private Vector2 input;

    // Movement speed
    [SerializeField]
    private float moveSpeed = 3;

    private bool canMove = false;

    /// <summary>
    /// Initialization of movement.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;
    }

    private void Start()
    {
        moveSpeed = ConstantsLoader.Instance.projectileSpeed;
    }

    /// <summary>
    /// Physics Update.
    /// Used to move object around.
    /// </summary>
    private void FixedUpdate()
    {
        if (canMove)
        {
            input *= moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + input);
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Passing direction as input where Object should fly.
    /// </summary>
    /// <param name="input">Input - direciton.</param>
    public void Move(Vector2 input)
    {
        ToggleMove(true);
        if (input.magnitude > 1)
        {
            input.Normalize();
        }

        this.input = input;
    }

    public void ToggleMove(bool canMove)
    {
        this.canMove = canMove;
    }
}

