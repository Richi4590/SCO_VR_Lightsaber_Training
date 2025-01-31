using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject shooter = null; //  The object that fired the projectile (e.g., a player or enemy)
    public GameObject target = null; // The object the projectile should follow (if heat-seeking is enabled).

    public bool heatSeeking = false; // Toggles the heat-seeking behavior. When `true`, the projectile adjusts its trajectory toward the target.
    public bool reflected = false; // Indicates whether the projectile has been reflected (e.g., by a lightsaber).
    public bool debugRotation = false; //  Enables debugging by allowing manual adjustment of the projectile's rotation during runtime.

    public Vector3 rotationOffset = new Vector3(90, 0, 0); //  A rotation offset applied to the projectile's orientation for customization (e.g., aligning with models).
    public float reflectionForceMultiplier = 1.0f; // Multiplier for reflected laser speed
    public float heatSeekingStrength = 1.0f; // Determines how strongly the projectile follows the target. Higher values make the trajectory sharper.
    public float destroyLaserAfterNSeconds = 10f;
    public float destroyDecalsAfterNSeconds = 10f;

    public List<GameObject> sparksPrefabs; // A list of spark effects to instantiate upon collision.
    public List<GameObject> bulletHoleDecals; // A list of decals (e.g., bullet holes) to apply to surfaces on impact.
    public List<AudioClip> laserDeflectionSFX; // A list of sound effects for when the projectile is deflected.

    private Rigidbody rb;
    private Collider coll;
    private Vector3 currentVelocity = Vector3.zero; // Stores the projectile's current velocity for heat-seeking and reflection calculations.
    private bool applyFinalVelocity = false; // A flag to apply the reflected velocity in the next physics update.


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void Update()
    {

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

    private void FixedUpdate()
    {
        if (applyFinalVelocity)
        {
            rb.velocity = currentVelocity;
            applyFinalVelocity = false; // Reset after applying
        }
    }

    public void ShootProjectile(GameObject shooter, GameObject target, Vector3 velocity)
    {
        this.shooter = shooter;
        ChangeTarget(target);

        //transform.LookAt(newTarget.transform.position, Vector3.up);
        transform.rotation = Quaternion.LookRotation(velocity.normalized);
        transform.rotation *= Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
        Debug.DrawRay(transform.position, velocity, Color.yellow, 2);

        currentVelocity = velocity;
        rb.velocity = currentVelocity;
        Destroy(this.gameObject, destroyLaserAfterNSeconds);
        gameObject.SetActive(true);
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void SetReflected(Vector3 newVelocity, bool reflected)
    {
        currentVelocity = newVelocity;
        applyFinalVelocity = true;
        this.reflected = reflected;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag != "Lightsaber")
        {
            if (collision.collider.transform.tag != "Player")
            {
                // Get the contact point and normal of the collision
                ContactPoint contact = collision.GetContact(0);

                InstantiateSpark(contact);
                InstantiateDecal(contact);
            }
            Destroy(this.gameObject);
            return;
        }

        //Lightsaber Blade Hit!
        Reflect(collision);
    }

    public void Reflect(Collision bladeCollision)
    {
        LayerMask layersToIgnore = LayerMask.GetMask("Laser", "Player");
        rb.excludeLayers = layersToIgnore;
        coll.excludeLayers = layersToIgnore;

        GameManager.Instance().ProjectileParried();
        PlayRandomDeflectionSound(bladeCollision.collider.GetComponent<AudioSource>());
        InstantiateSpark(bladeCollision.GetContact(0));

        // Prevent angular motion
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Capture the velocity before Unity modifies it
        Vector3 incomingVelocity = currentVelocity;
        float incomingSpeed = incomingVelocity.magnitude; // Preserve the speed

        // Get the contact point and normal
        ContactPoint contact = bladeCollision.GetContact(0);
        Vector3 hitNormal = contact.normal;

        // Calculate the reflection direction
        Vector3 reflectedDirection = Vector3.Reflect(incomingVelocity.normalized, hitNormal);

        // Align the projectile's rotation with the reflected velocity
        Quaternion targetRotation = Quaternion.LookRotation(reflectedDirection);
        transform.rotation = targetRotation * Quaternion.Euler(90, 0, 0);

        // Calculate the new velocity while preserving speed
        Vector3 velocityAfterReflection = reflectedDirection * incomingSpeed * reflectionForceMultiplier;

        // Mark the laser as reflected
        SetReflected(velocityAfterReflection, true);
        // Debugging: Visualize the reflection
        Debug.DrawRay(contact.point, reflectedDirection, Color.blue, 2.0f);
        
    }

    private void PlayRandomDeflectionSound(AudioSource deflectionAudioSource)
    {
        deflectionAudioSource.pitch = Time.timeScale * Random.Range(0.9f, 1.2f);
        deflectionAudioSource.PlayOneShot(laserDeflectionSFX[Random.Range(0, laserDeflectionSFX.Count)]);
    }

    private void InstantiateSpark(ContactPoint contact)
    {
        GameObject sparkSFX = Instantiate(sparksPrefabs[Random.Range(0, sparksPrefabs.Count)], contact.point, Quaternion.LookRotation(contact.normal));

        float longestWaitingTime = 0;

        int timesSmaller = 10;
        sparkSFX.transform.localScale = sparkSFX.transform.localScale / timesSmaller;

        longestWaitingTime = sparkSFX.GetComponent<ParticleSystem>().main.duration;

        //Make all sub sfx the same size
        for (int i = 0; i < sparkSFX.transform.childCount; i++)
        {
            GameObject child = sparkSFX.transform.GetChild(i).gameObject;
            child.transform.localScale = sparkSFX.transform.localScale / timesSmaller;

            if (child.GetComponent<ParticleSystem>().main.duration > longestWaitingTime)
                longestWaitingTime = child.GetComponent<ParticleSystem>().main.duration;
        }

        //Debug.Log("Longest Particle duration of: " + longestWaitingTime);

        Destroy(sparkSFX, longestWaitingTime);
    }

    private void InstantiateDecal(ContactPoint contact)
    {
        GameObject bulletDecal = Instantiate(
            bulletHoleDecals[Random.Range(0, bulletHoleDecals.Count)],
            contact.point + (Vector3.up/100f),
            Quaternion.LookRotation(contact.normal) // Align rotation to surface normal
        );

        //Debug.DrawRay(contact.point, contact.normal, Color.red, 5f);
        //Debug.Log($"Contact Point: {contact.point}, Normal: {contact.normal}");

        Destroy(bulletDecal, destroyDecalsAfterNSeconds);
    }
}