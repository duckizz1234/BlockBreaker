using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 10;


    private void Start()
    {
        rotationSpeed = ConstantsLoader.Instance.turretRotationSpeed;
    }
    void Update()
    {
        RotateTurret();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
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
            projectile.GetComponent<ProjectileStateMachine>().StartState(transform.right);
            // Set projectile direction and speed...
        }
    }
}
