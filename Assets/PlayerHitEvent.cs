using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEvent : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Laser")
            GameManager.Instance().PlayerHit();
    
    }
}
