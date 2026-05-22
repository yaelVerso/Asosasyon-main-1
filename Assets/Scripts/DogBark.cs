using UnityEngine;

public class DogBark : MonoBehaviour
{
    [SerializeField] private AudioClip[] barkSounds; // Drag multiple bark sounds
    [SerializeField] private float minBarkDelay = 1f;  // Minimum seconds between barks
    [SerializeField] private float maxBarkDelay = 15f; // Maximum seconds between barks

    private AudioSource audioSource;
    private float nextBarkTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set random first bark time
        nextBarkTime = Time.time + Random.Range(minBarkDelay, maxBarkDelay);
    }

    private void Update()
    {
        // Check if it's time to bark
        if (Time.time >= nextBarkTime)
        {
            Bark();
            // Schedule next bark
            nextBarkTime = Time.time + Random.Range(minBarkDelay, maxBarkDelay);
        }
    }

    private void Bark()
    {
        if (barkSounds.Length > 0 && audioSource != null)
        {
            // Pick random bark sound
            AudioClip randomBark = barkSounds[Random.Range(0, barkSounds.Length)];

            // Play the bark
            audioSource.PlayOneShot(randomBark);
        }
    }
}