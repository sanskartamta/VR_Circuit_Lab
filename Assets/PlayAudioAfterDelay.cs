using UnityEngine;

public class PlayAudioAfterDelay : MonoBehaviour
{
    public AudioSource audioSource; // Assign your AudioSource component here
    public float delay = 3f; // Delay time in seconds

    private void Start()
    {
        // Start the delayed audio playback
        Invoke("PlayAudio", delay);
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }
}
