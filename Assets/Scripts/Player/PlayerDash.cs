using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;

    private Rigidbody2D body;
    private PlayerMovement playerMovement;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Handle cooldown
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        // Handle dash duration
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                // Re-enable the movement script
                if (playerMovement != null)
                    playerMovement.enabled = true;
            }
            return;
        }

        // Dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer <= 0)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        cooldownTimer = dashCooldown;

        // Disable movement script while dashing
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Dash in facing direction
        float direction = transform.localScale.x;
        body.linearVelocity = new Vector2(direction * dashSpeed, 0);
    }
}