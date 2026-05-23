using UnityEngine;

public class AutoWalker : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        // Reset any stuck animations
        anim.ResetTrigger("jump");
        anim.SetBool("run", true);
        anim.SetBool("grounded", true);

        // Face right
        transform.localScale = Vector3.one;

        // Freeze Rigidbody if it exists
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // Disable PlayerMovement if it's still on
        PlayerMovement pm = GetComponent<PlayerMovement>();
        if (pm != null)
            pm.enabled = false;
    }
}