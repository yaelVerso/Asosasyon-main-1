using UnityEngine;
using System.Collections.Generic;

public class CreditsFlashDogs : MonoBehaviour
{
    [Header("Dog Settings")]
    public GameObject[] dogBreeds;  // Drag all dog breeds here
    public float spawnInterval = 0.15f;  // How often dogs spawn
    public float fadeTime = 0.8f;  // How long until dog fades
    public int maxDogs = 5;  // Maximum dogs on screen at once

    private List<GameObject> activeDogs = new List<GameObject>();
    private float spawnTimer = 0f;

    void Start()
    {
        // Start spawning immediately
        spawnTimer = 0f;
    }

    void Update()
    {
        // Spawn dogs continuously
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnDog();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnDog()
    {
        // Remove oldest dog if we have too many
        if (activeDogs.Count >= maxDogs)
        {
            GameObject oldestDog = activeDogs[0];
            if (oldestDog != null)
                Destroy(oldestDog);
            activeDogs.RemoveAt(0);
        }

        // Pick a random dog breed
        if (dogBreeds.Length == 0) return;
        int randomIndex = Random.Range(0, dogBreeds.Length);
        GameObject randomBreed = dogBreeds[randomIndex];

        // Spawn at player position
        GameObject dog = Instantiate(randomBreed, transform.position, transform.rotation);
        dog.transform.localScale = transform.localScale;

        // Fade and destroy
        StartCoroutine(FadeAndDestroy(dog));
        activeDogs.Add(dog);
    }

    System.Collections.IEnumerator FadeAndDestroy(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        float timer = fadeTime;
        Color color = sr.color;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            color.a = timer / fadeTime;
            sr.color = color;
            yield return null;
        }

        Destroy(obj);
        activeDogs.Remove(obj);
    }

    // Optional: Clear all dogs (call this when credits end)
    public void ClearAllDogs()
    {
        foreach (GameObject dog in activeDogs)
        {
            if (dog != null)
                Destroy(dog);
        }
        activeDogs.Clear();
    }
}