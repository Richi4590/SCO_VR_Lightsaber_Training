using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI fpsText;

    [Header("Settings")]
    public float updateInterval = 0.5f; // Time between FPS updates

    private float timer = 0f;
    private int frameCount = 0;
    private float fps = 0f;

    private void Update()
    {
        frameCount++;
        timer += Time.unscaledDeltaTime;

        if (timer >= updateInterval)
        {
            // Calculate FPS
            fps = frameCount / timer;

            // Update the UI text only when needed
            if (fpsText != null)
            {
                fpsText.text = Mathf.RoundToInt(fps).ToString();
            }

            // Reset timer and frame count
            frameCount = 0;
            timer = 0f;
        }
    }
}