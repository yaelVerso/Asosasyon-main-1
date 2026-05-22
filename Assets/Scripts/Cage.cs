using UnityEngine;

public class Cage : MonoBehaviour
{
    public Sprite openCageSprite;
    private SpriteRenderer sr;
    private bool opened = false;

    void Start() => sr = GetComponent<SpriteRenderer>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!opened && other.CompareTag("Player"))
        {
            sr.sprite = openCageSprite;
            opened = true;
        }
    }
}
