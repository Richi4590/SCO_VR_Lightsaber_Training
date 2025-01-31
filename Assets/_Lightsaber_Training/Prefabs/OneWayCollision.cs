using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OneWayCollision : MonoBehaviour
{
    public Collider objectCollider; // Reference to the object's collider
    public float checkDistance = 1f; // Distance for the raycast check
    public LayerMask collisionLayer; // Layer to check for collisions
    public Vector3 rayDirection = Vector3.zero;
    public bool flipDirection = false;
    public bool enableColliderPermanently = false;

    private bool enabledPermanently = false;

    private void Update()
    {
        if (!enableColliderPermanently || (enableColliderPermanently && !enabledPermanently))
            CheckCollision();
    }

    void CheckCollision()
    {
        // Determine primary ray direction
        Vector3 primaryRayDir = flipDirection ? (-transform.up + rayDirection) : (transform.up + rayDirection);

        // Cast a ray forward and collect all hits
        RaycastHit[] hits = Physics.RaycastAll(transform.position, primaryRayDir, checkDistance, collisionLayer);

        bool shouldEnableCollider = false;

        foreach (RaycastHit hit in hits)
        {
            Vector3 hitNormal = hit.normal;
            Vector3 directionToObject = (transform.position - hit.point).normalized;

            // Check if the normal is facing towards the object
            bool isNormalFacingObject = Vector3.Dot(hitNormal, directionToObject) > 0;

            // Additional check: Is there another object in the normal's direction?
            bool isFacingAnotherObject = Physics.Raycast(hit.point + hitNormal * 0.01f, hitNormal, out RaycastHit normalHit, 0.5f, collisionLayer);

            if (isNormalFacingObject && isFacingAnotherObject)
            {
                shouldEnableCollider = true;

                if (enableColliderPermanently)
                    enabledPermanently = true;
                else
                    enabledPermanently = false;

                break; // No need to check further if we already know to enable
            }
        }

        // Enable or disable collider based on normal direction and facing object check
        objectCollider.enabled = shouldEnableCollider;
    }

    private void OnDrawGizmos()
    {
        // Determine primary ray direction
        Vector3 primaryRayDir = flipDirection ? (-transform.up + rayDirection) : (transform.up + rayDirection);

        // Draw the main ray for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, primaryRayDir * checkDistance);
    }
}