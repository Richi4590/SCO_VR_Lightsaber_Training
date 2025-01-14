using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflector : MonoBehaviour
{
    public float reflectionForceMultiplier = 1.0f; // Multiplier for reflected laser speed
    public AudioSource deflectionAudioSource;
    public List<AudioClip> laserDeflectionSFX;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a laser projectile
        Projectile laser = collision.gameObject.GetComponent<Projectile>();
        if (laser != null)
        {
            Reflect(collision, laser);
            GameManager.Instance().ProjectileParried();
            PlayRandomDeflectionSound();
        }
    }

    private void Reflect(Collision collision, Projectile laser)
    {
        Rigidbody laserRb = laser.GetComponent<Rigidbody>();
        if (laserRb != null)
        {
            // Get the contact point and normal of the collision
            ContactPoint contact = collision.GetContact(0);
            Vector3 hitNormal = contact.normal;

            // Calculate the reflection direction
            Vector3 incomingVelocity = laserRb.velocity;
            Vector3 reflectedDirection = Vector3.Reflect(incomingVelocity, hitNormal).normalized;

            // Apply the new velocity to the laser
            Vector3 velocityAfterReflection = reflectedDirection * incomingVelocity.magnitude * reflectionForceMultiplier;
            laserRb.velocity = velocityAfterReflection;

            // Freeze the projectile's rotation to align with its velocity
            Quaternion targetRotation = Quaternion.LookRotation(reflectedDirection);
            laser.transform.rotation = targetRotation;
            laser.transform.rotation *= Quaternion.Euler(90, 0, 0);

            // Optionally, lock rotation by disabling Rigidbody angular motion
            laserRb.angularVelocity = Vector3.zero; // Stops spinning
            laserRb.constraints = RigidbodyConstraints.FreezeRotation;

            // Optionally, mark the laser as reflected for game logic
            laser.SetReflected(velocityAfterReflection, true);

            // Debugging: Visualize the reflection
            Debug.DrawRay(contact.point, reflectedDirection, Color.blue, 2.0f);
        }
    }

    private void PlayRandomDeflectionSound()
    {
        deflectionAudioSource.pitch = Random.Range(0.9f, 1.2f);
        deflectionAudioSource.PlayOneShot(laserDeflectionSFX[Random.Range(0, laserDeflectionSFX.Count)]);
    }
}
