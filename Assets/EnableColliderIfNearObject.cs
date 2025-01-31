using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnableColliderIfNearObject : MonoBehaviour
{
    public string targetTag = "Target"; // The tag to check for
    public float activationRadius = 5f; // Distance within which collider activates
    public LayerMask layerMask; // Specify which layers to check in the raycast

    private Collider objCollider;
    private bool targetFound = false;

    void Start()
    {
        objCollider = GetComponent<Collider>();
        if (objCollider == null)
        {
            Debug.LogError("No Collider found on the object! Please add one.");
            enabled = false; // Disable script if no collider is found
            return;
        }
        objCollider.enabled = false; // Start with the collider disabled
    }

    void Update()
    {
        if (!targetFound)
            CheckIfNearTarget();

    }

    private void CheckIfNearTarget()
    {
        // Get all colliders within the specified radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, activationRadius, layerMask);


        foreach (Collider col in colliders)
        {
            if (col.CompareTag(targetTag)) // Check if the collider has the target tag
            {
                targetFound = true;
                Debug.Log("target found?: " + targetFound);
                // Enable or disable the collider based on whether the target was found
                objCollider.enabled = targetFound;
                break; // Exit loop if a target is found
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Set Gizmo color
        Gizmos.color = Color.cyan;

        // Visualize the sphere's detection area with a wireframe sphere
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}