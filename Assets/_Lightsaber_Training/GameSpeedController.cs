using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    [Header("Speed Settings")]
    [Range(0.1f, 3f)] public float currentSpeed = 1f; // Default game speed

    public OVRInput.Button activationButton = OVRInput.Button.Two;
    public float slowDownSpeed = 0.3f;
    public float switchDuration = 0.5f;
    public AudioSource musicSource;
    private float switchTimer = 0f;
    private bool slowedDown = false;
    private bool slowDownInProgress = false;

    private float _currentSpeed { 
                                    get { return _currentSpeed; } 
                                    set {
                                            currentSpeed = value;
                                            Time.timeScale = value;
                                        } 
                                    }

    private void Start()
    {
        // Initialize the slider and game speed
        Time.timeScale = currentSpeed;
    }

    private void Update()
    {
        if (currentSpeed != Time.timeScale)
        {
            _currentSpeed = currentSpeed;
        }

        if (OVRInput.GetDown(activationButton))
        {
            if (!slowDownInProgress)
                StartCoroutine(ToggleSlowDownTime());
        }
    }

    private IEnumerator ToggleSlowDownTime()
    {
        switchTimer = 0f;
        slowDownInProgress = true;

        while (switchTimer < slowDownSpeed) 
        {
            float t = switchTimer / slowDownSpeed;

            if (!slowedDown)
                currentSpeed = Mathf.Lerp(1f, slowDownSpeed, t);
            else
                currentSpeed = Mathf.Lerp(slowDownSpeed, 1f, t);

            musicSource.pitch = currentSpeed;

            switchTimer += Time.deltaTime;
            yield return null;
        }

        slowedDown = !slowedDown;

        currentSpeed = slowedDown ? slowDownSpeed : 1f;
        musicSource.pitch = currentSpeed;

        slowDownInProgress = false;

    }
}