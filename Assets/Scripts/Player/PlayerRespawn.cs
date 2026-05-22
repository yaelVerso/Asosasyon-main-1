using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private Vector3 startingPosition;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        startingPosition = transform.position;
    }

    public void CheckRespawn()
    {
        // Move player to checkpoint or start
        if (currentCheckpoint == null)
            transform.position = startingPosition;
        else
            transform.position = currentCheckpoint.position;

        // Restore health and re-enable everything
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Start()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            startingPosition = spawnPoint.transform.position;
        }
    }
}