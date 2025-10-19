using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public RectTransform[] visualizerBars; // An array to hold all your bar transforms

    [Header("Visualization Settings")]
    public int spectrumSampleSize = 128; // Must be a power of 2
    public float maxHeight = 50f;        // The maximum height a bar can reach
    public float visualizerScaleMultiplier = 500f; // Multiplier to make bars visible

    private float[] spectrumData;

    void Start()
    {
        // Initialize the spectrum data array
        spectrumData = new float[spectrumSampleSize];
    }

    void Update()
    {
        // Get the spectrum data from the audio source
        // FFTWindow.Rectangular is a good default
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Loop through each of your visualizer bars
        for (int i = 0; i < visualizerBars.Length; i++)
        {
            // Get the current bar
            RectTransform bar = visualizerBars[i];

            // Calculate the target height based on the spectrum data
            // We use a simple mapping here. For a more advanced version, you could average samples.
            float targetHeight = spectrumData[i] * visualizerScaleMultiplier;

            // Clamp the height to the maximum value
            targetHeight = Mathf.Clamp(targetHeight, 0f, maxHeight);

            // Smoothly change the bar's height for a nicer effect
            float newHeight = Mathf.Lerp(bar.sizeDelta.y, targetHeight, Time.deltaTime * 10f);

            // Apply the new height
            bar.sizeDelta = new Vector2(bar.sizeDelta.x, newHeight);
        }
    }
}