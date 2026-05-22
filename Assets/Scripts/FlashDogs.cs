using UnityEngine;
using System.Collections.Generic;

public class FlashDogs : MonoBehaviour
{
    public GameObject[] dogBreeds;  // Drag breeds in ORDER (index 0 = first to unlock)
    public float spawnInterval = 0.1f;
    public float fadeTime = 0.5f;
    public float runThreshold = 0.5f;
    public int maxDogs = 3;

    public DoggoManager doggoManager;  // Drag your DoggoManager here

    private Rigidbody2D body;
    private List<GameObject> afterImages = new List<GameObject>();
    private float spawnTimer = 0f;
    private bool wasRunning = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // Find DoggoManager if not assigned
        if (doggoManager == null)
            doggoManager = FindAnyObjectByType<DoggoManager>();

    }

    void Update()
    {
        // No dogs collected yet? No after-images
        if (doggoManager == null || doggoManager.doggoCount == 0)
            return;

        float currentSpeed = Mathf.Abs(body.linearVelocity.x);
        bool isRunning = currentSpeed > runThreshold;

        if (isRunning)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnAfterImage();
                spawnTimer = spawnInterval;
            }
        }
        else if (wasRunning && !isRunning)
        {
            ClearAllAfterImages();
        }

        wasRunning = isRunning;
    }

    void SpawnAfterImage()
    {
        // Check if we already have max dogs
        if (afterImages.Count >= maxDogs)
        {
            GameObject oldestDog = afterImages[0];
            if (oldestDog != null)
                Destroy(oldestDog);
            afterImages.RemoveAt(0);
        }

        // Get how many breeds are UNLOCKED (based on doggoCount)
        int unlockedCount = doggoManager.doggoCount;

        // Limit to array size (in case you have more dogs than breeds)
        if (unlockedCount > dogBreeds.Length)
            unlockedCount = dogBreeds.Length;

        // No unlocked breeds? Don't spawn
        if (unlockedCount == 0) return;

        // Pick a random breed ONLY from unlocked indexes (0 to unlockedCount-1)
        int randomIndex = Random.Range(0, unlockedCount);
        GameObject randomBreed = dogBreeds[randomIndex];

        // Spawn it
        GameObject afterImage = Instantiate(randomBreed, transform.position, transform.rotation);
        afterImage.transform.localScale = transform.localScale;

        // Fade and destroy
        StartCoroutine(FadeAndDestroy(afterImage));
        afterImages.Add(afterImage);
    }

    System.Collections.IEnumerator FadeAndDestroy(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        float timer = fadeTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (sr != null)
            {
                Color color = sr.color;
                color.a = timer / fadeTime;
                sr.color = color;
            }
            yield return null;
        }

        Destroy(obj);
        afterImages.Remove(obj);
    }

    void ClearAllAfterImages()
    {
        foreach (GameObject img in afterImages)
        {
            if (img != null)
                Destroy(img);
        }
        afterImages.Clear();
    }
}