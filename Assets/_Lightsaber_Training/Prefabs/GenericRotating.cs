using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericRotating : MonoBehaviour
{
    [Header("Rotation Axes")]
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    [Header("Rotation Speed")]
    public float rotationSpeed = 10f;

    [Header("Rotation Around Point Settings")]
    public bool rotateAroundPoint = false; // Enable/Disable rotation around a point
    public bool pointIsCenterOfObject = false;
    public Transform rotationPoint; // Point to rotate around
    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around the point
    public Transform parentTransform;

    private Vector3 currentLocalRotation; // Stores cumulative local rotation

    private void Start()
    {
        currentLocalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (rotateAroundPoint)
        {
            if (pointIsCenterOfObject)
                transform.position = rotationPoint.position;

            if (parentTransform == null)
            {
                if (rotationPoint != null)
                {
                    int rotateXInt = rotateX == true ? 1 : 0;
                    int rotateYInt = rotateY == true ? 1 : 0;
                    int rotateZInt = rotateZ == true ? 1 : 0;
                    // Rotate around a specific point in world space
                    transform.RotateAround(rotationPoint.position, new Vector3(rotateXInt, rotateYInt, rotateZInt), rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                    transform.RotateAround(rotationPoint.position, transform.up, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Rotate the object locally based on active axes and speed
            float xRotation = rotateX ? rotationSpeed * Time.deltaTime : 0f;
            float yRotation = rotateY ? rotationSpeed * Time.deltaTime : 0f;
            float zRotation = rotateZ ? rotationSpeed * Time.deltaTime : 0f;

            // Update cumulative local rotation
            currentLocalRotation += new Vector3(xRotation, yRotation, zRotation);
            transform.localRotation = Quaternion.Euler(currentLocalRotation);
        }
    }
}