using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject shooter = null;
    public GameObject target = null;

    public bool heatSeeking = false;
    public bool reflected = false;
    public bool debugRotation = false;
    public Vector3 rotationOffset = new Vector3(90, 0, 0);

    private Rigidbody rb;

    private Vector3 currentVelocity = Vector3.zero;
    public float heatSeekingStrength = 1.0f; // Strength of the seeking behavior

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!heatSeeking && (rb.velocity != currentVelocity))
        {
            rb.velocity = currentVelocity;
        }

        if (debugRotation)
        {
            ChangeTarget(target);
        }

        if (heatSeeking && target != null)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            // Adjust velocity to slightly move towards the target
            Vector3 newVelocity = Vector3.Lerp(
                rb.velocity.normalized,
                directionToTarget,
                Time.deltaTime * heatSeekingStrength
            ).normalized * currentVelocity.magnitude;

            rb.velocity = newVelocity;


            Quaternion targetRotation = Quaternion.LookRotation(newVelocity);
            Quaternion offsetRotation = Quaternion.Euler(90f, 0f, 0f);  // Add a 90° offset to the X-axis
            targetRotation *= offsetRotation;

            // Smoothly interpolate to the new rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * heatSeekingStrength);
        }
    }

    public void ShootProjectile(GameObject shooter, GameObject target, Vector3 velocity)
    {
        this.shooter = shooter;
        this.target = target;
        ChangeTarget(target);

        currentVelocity = velocity;
        rb.velocity = currentVelocity;
        Destroy(this.gameObject, 10f);
        gameObject.SetActive(true);
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
        if (newTarget != null)
        {
            // Make the object look at the target
            transform.LookAt(newTarget.transform.position, Vector3.up);
            transform.rotation *= Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
        }
    }

    public void SetReflected(Vector3 newVelocity, bool reflected)
    {
        currentVelocity = newVelocity;
        rb.velocity = currentVelocity;
        this.reflected = reflected;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLIDING!");

        if (collision.collider.transform.tag != "Lightsaber")
            Destroy(this.gameObject);
        //else
        //{
        //    Debug.Log("Lightsaber HIT!");
        //}
    }
}