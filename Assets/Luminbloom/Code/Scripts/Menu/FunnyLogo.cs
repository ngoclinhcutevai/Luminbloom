using UnityEngine;
using UnityEngine.UI;

public class FunnyLogo : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;
    private RectTransform imageRectTransform;

    [Header("Pulse Settings")]
    public float minScale = 1.0f;
    public float maxScale = 1.2f;
    public float loudnessMultiplier = 5.0f;
    public float smoothing = 10.0f;

    // We use this to control when the pulsing starts
    private bool canPulse = false;
    private float[] samples = new float[128];

    void Start()
    {
        // Get the RectTransform of this UI image
        imageRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // If canPulse is false, do nothing.
        if (!canPulse)
        {
            return;
        }

        // Get the raw audio waveform data
        audioSource.GetOutputData(samples, 0);
        float currentLoudness = 0f;

        // Calculate the average volume from the samples
        foreach (float sample in samples)
        {
            currentLoudness += Mathf.Abs(sample);
        }
        currentLoudness /= samples.Length;

        // Map the loudness to our desired scale range
        float targetScale = Mathf.Lerp(minScale, maxScale, currentLoudness * loudnessMultiplier);

        // Smoothly change the current scale to the target scale
        float newScale = Mathf.Lerp(imageRectTransform.localScale.x, targetScale, Time.deltaTime * smoothing);

        // Apply the new scale to the image
        imageRectTransform.localScale = new Vector3(newScale, newScale, 1f);
    }

    // This is the special function we will call from our animation!
    public void StartPulsing()
    {
        canPulse = true;
    }
}