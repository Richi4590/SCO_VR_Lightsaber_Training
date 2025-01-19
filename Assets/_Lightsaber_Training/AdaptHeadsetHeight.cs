using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionLockOn : MonoBehaviour
{
    public Transform objectToLockOn;
    public bool lockOnX = false;
    public bool lockOnY = false;
    public bool lockOnZ = false;

    public bool lockRotationX = false;
    public bool lockRotationY = false;
    public bool lockRotationZ = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (lockOnX)
            currentPosition.x = objectToLockOn.position.x;

        if (lockOnY)
            currentPosition.y = objectToLockOn.position.y;

        if (lockOnZ)
            currentPosition.z = objectToLockOn.position.z;

        transform.position = currentPosition;

        Vector3 currentRotation = transform.rotation.eulerAngles;

        if (lockRotationX)
            currentRotation.x = objectToLockOn.rotation.eulerAngles.x;

        if (lockRotationY)
            currentRotation.y = objectToLockOn.rotation.eulerAngles.y;

        if (lockRotationZ)
            currentRotation.z = objectToLockOn.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
