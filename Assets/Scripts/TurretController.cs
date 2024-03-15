using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 10;
    public delegate void LogMessageDelegate(string message);
    public static event LogMessageDelegate OnLogMessage;
    private bool canMove = false;
    private void Start()
    {
        rotationSpeed = ConstantsLoader.Instance.turretRotationSpeed;
    }
    void Update()
    {
        if (canMove)
        {
            RotateTurret();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    void RotateTurret()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        GameObject projectile = ProjectilePooler.Instance.GetProjectile();
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);
            projectile.GetComponent<ProjectileStateMachine>().StartMovingState(transform.right);
            OnLogMessage?.Invoke("Projectile shot");
        }
    }

    public void ToggleCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
