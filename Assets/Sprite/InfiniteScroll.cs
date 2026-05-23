using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f; // How fast background moves
    [SerializeField] private bool scrollLeft = true; // Direction

    private SpriteRenderer spriteRenderer;
    private float width;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the width of the sprite
        width = spriteRenderer.bounds.size.x;

        // Set the tiling size to cover the screen
        spriteRenderer.size = new Vector2(width * 2, spriteRenderer.size.y);
    }

    void Update()
    {
        // Move the background
        float direction = scrollLeft ? -1f : 1f;
        transform.position += Vector3.right * direction * scrollSpeed * Time.deltaTime;

        // Reset position when too far left
        if (scrollLeft && transform.position.x <= -width)
        {
            transform.position += Vector3.right * width * 2;
        }
        // Reset position when too far right
        else if (!scrollLeft && transform.position.x >= width)
        {
            transform.position += Vector3.left * width * 2;
        }
    }
}