using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject shooter = null;
    public GameObject target = null;

    public bool heatSeeking = false;

    private Rigidbody rb;

    private Vector3 initialVelocityOnShoot = Vector3.zero;
    public float heatSeekingStrength = 1.0f; // Strength of the seeking behavior

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (heatSeeking && target != null)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            // Adjust velocity to slightly move towards the target
            Vector3 newVelocity = Vector3.Lerp(
                rb.velocity.normalized,
                directionToTarget,
                Time.deltaTime * heatSeekingStrength
            ).normalized * initialVelocityOnShoot.magnitude;

            rb.velocity = newVelocity;

            // Optionally, rotate the projectile to face the target
            Quaternion targetRotation = Quaternion.LookRotation(newVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * heatSeekingStrength);
        }
    }

    public void ShootProjectile(GameObject shooter, GameObject target, Vector3 velocity)
    {
        this.shooter = shooter;
        this.target = target;
        initialVelocityOnShoot = velocity;
        rb.velocity = initialVelocityOnShoot;
        Destroy(this.gameObject, 10f);

        ChangeTarget(target);
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
        if (newTarget != null)
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = (newTarget.transform.position - transform.position).normalized;

            // Calculate the rotation to align the object's local +Y axis with the target direction
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, directionToTarget);

            // Apply the rotation to the object
            transform.rotation = targetRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLIDING!");

        if (collision.transform.tag != "Lightsaber")
            Destroy(this.gameObject);

    }
}