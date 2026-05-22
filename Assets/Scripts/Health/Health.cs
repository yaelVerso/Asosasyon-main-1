using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private Rigidbody2D rb;
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (GameData.SavedHealth < 0)
        {
            currentHealth = startingHealth;
            GameData.SavedHealth = currentHealth;
        }
        else
        {
            currentHealth = GameData.SavedHealth;
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        GameData.SavedHealth = currentHealth;

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                // Play death animation
                anim.SetBool("grounded", true);
                anim.SetBool("run", false);
                anim.ResetTrigger("hurt");
                anim.SetTrigger("die");

                // Disable player movement
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                // Stop physics
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.bodyType = RigidbodyType2D.Static;
                }

                dead = true;

                // Show GameOver screen after death animation (NOT auto-respawn!)
                Invoke("ShowGameOver", 0.4f); // Adjust to your animation length
            }
        }
    }

    private void ShowGameOver()
    {
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null)
            uiManager.GameOver();
    }

    public void AddHealth(float _heart)
    {
        currentHealth = Mathf.Clamp(currentHealth + _heart, 0, startingHealth);
        GameData.SavedHealth = currentHealth;
    }

    public void Respawn()
    {
        // Re-enable physics
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic;

        dead = false;
        AddHealth(startingHealth);

        // Reset animations
        anim.ResetTrigger("die");
        anim.Play("Idle");

        // Re-enable all components
        foreach (Behaviour component in components)
        {
            if (component != null)
                component.enabled = true;
        }
    }


}