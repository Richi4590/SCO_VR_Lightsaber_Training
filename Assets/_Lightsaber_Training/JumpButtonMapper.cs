using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButtonMapper : MonoBehaviour
{
    public OVRPlayerController controller;
    public OVRInput.Button jumpButton = OVRInput.Button.One; // Button to activate/deactivate (X Button)

    // Start is called before the first frame update
    void Start()
    {
        if (controller == null)
            controller = GetComponent<OVRPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(jumpButton))
        {
            controller.Jump();
        }
    }
}
