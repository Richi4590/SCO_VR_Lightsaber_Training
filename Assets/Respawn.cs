using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public Transform player;
    public Transform teleportHeight;

    private OVRPlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the OVRPlayerController
        playerController = player.GetComponent<OVRPlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.enabled = false;
            player.transform.position = teleportHeight.position;
            playerController.enabled = true;
        }
    }


}
