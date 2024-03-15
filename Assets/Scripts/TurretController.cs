using UnityEngine;

/// <summary>
/// Controls the player which is the Turret including rotating and shooting
/// </summary>
public class TurretController : MonoBehaviour
{
    /// <summary>
    /// delegate events to send log messages to the UIConsole
    /// </summary>
    /// <param name="message"></param>
    public delegate void LogMessageDelegate(string message);
    public static event LogMessageDelegate OnLogMessage;

    /// <summary>
    /// Sets if the player has control on the turret
    /// </summary>
    private bool canMove = false;

    /// <summary>
    /// Speed at which the turret rotates. This is the default value and actual value will be loaded from Constants.Json
    /// </summary>
    private float rotationSpeed = 10;


    private void Start()
    {
        rotationSpeed = ConstantsLoader.Instance.turretRotationSpeed;
    }

    private void Update()
    {
        if (canMove)
        {
            RotateTurret();
            CheckShootAction();
        }
    }

    /// <summary>
    /// Checks if shoot action was triggered
    /// </summary>
    private void CheckShootAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Rotates the turret based on where the mouse is on the screen
    /// </summary>
    private void RotateTurret()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Rotates the turret with a smoothing effect
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Shoots a projectile out of the turret
    /// </summary>
    private void Shoot()
    {
        GameObject projectile = ProjectilePooler.Instance.GetProjectile();
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);
            // Starts the projectile moving
            projectile.GetComponent<ProjectileStateMachine>().StartMovingState(transform.right);
            // Sends a log to the console
            OnLogMessage?.Invoke("Projectile shot");
        }
    }

    /// <summary>
    /// Indicates if the turret is under player control
    /// </summary>
    /// <param name="canMove">True if player has control. False otherwise</param>
    public void ToggleCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
