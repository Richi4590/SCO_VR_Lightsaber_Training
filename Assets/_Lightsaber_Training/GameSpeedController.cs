using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    [Header("Speed Settings")]
    [Range(0.1f, 3f)] public float defaultSpeed = 1f; // Default game speed

    private void Start()
    {
        // Initialize the slider and game speed
        Time.timeScale = defaultSpeed;
    }

    private void Update()
    {
        if (defaultSpeed != Time.timeScale) 
            Time.timeScale = defaultSpeed;
    }
}