using UnityEngine;

public class MainFunnyLogo : MonoBehaviour
{
    // This is the line that lets you drag the other object in!
    public FunnyLogo musicPulser;

    // This is the function your Animation Event will call
    public void TriggerMusicPulse()
    {
        // Check if the pulser was assigned before trying to use it
        if (musicPulser != null)
        {
            musicPulser.StartPulsing();
        }
        else
        {
            Debug.LogWarning("MusicPulser is not assigned!");
        }
    }
}