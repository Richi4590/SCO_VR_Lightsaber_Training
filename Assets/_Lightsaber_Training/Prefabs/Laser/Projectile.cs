using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject shooter = null;
    public GameObject target = null;

    public bool heatSeeking = false;
    public bool reflected = false;
    public bool debugRotation = false;
    public Vector3 rotationOffset = new Vector3(90, 0, 0);

    public List<GameObject> sparksPrefabs;
    public List<GameObject> bulletHoleDecals;

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
            contact.point,
            Quaternion.LookRotation(contact.normal) // Align rotation to surface normal
        );

        //Debug.DrawRay(contact.point, contact.normal, Color.red, 5f);
        //Debug.Log($"Contact Point: {contact.point}, Normal: {contact.normal}");

        Destroy(bulletDecal, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLIDING!");

        Debug.Log(collision.collider.gameObject.name);

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
        }
        //else
        //{
        //    Debug.Log("Lightsaber HIT!");
        //}
    }
}