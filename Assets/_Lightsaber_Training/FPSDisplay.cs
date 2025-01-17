using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI fpsText;
    public Transform fpsRoot;

    [Header("Settings")]
    public float updateInterval = 0.5f; // Time between FPS updates
    public Transform cameraTransform;  // Reference to the camera
    public Vector3 offset = new Vector3(0, 1f, 2f); // Offset from the camera

    private float timer = 0f;
    private int frameCount = 0;
    private float fps = 0f;

    private void Start()
    {
        // Default to the main camera if no reference is provided
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        UpdateFPS();
        UpdateTextPosition();
    }

    private void UpdateFPS()
    {
        frameCount++;
        timer += Time.unscaledDeltaTime;

        if (timer >= updateInterval)
        {
            // Calculate FPS
            fps = frameCount / timer;

            // Update the UI text with only the FPS value
            if (fpsText != null)
            {
                fpsText.text = Mathf.RoundToInt(fps).ToString();
            }

            // Reset timer and frame count
            frameCount = 0;
            timer = 0f;
        }
    }

    private void UpdateTextPosition()
    {
        if (cameraTransform == null || fpsText == null)
            return;

        // Set the position in front of the camera with an offset
        fpsRoot.transform.position = cameraTransform.position + cameraTransform.forward * offset.z
                                     + cameraTransform.up * offset.y;

        // Rotate the text to always face the camera
        fpsRoot.transform.rotation = Quaternion.LookRotation(fpsText.transform.position - cameraTransform.position);
    }
}