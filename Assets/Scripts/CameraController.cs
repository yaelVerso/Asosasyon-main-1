using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camera follows the player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    private float lookAhead;

    private void Start()
    {
        // If the player slot is empty, search the scene for the object with the "Player" tag
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");

            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                Debug.LogError("Camera couldn't find a GameObject with the tag 'Player'!");
            }
        }
    }
    private void Update()
    {
        // Horizontal following with look ahead
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        // Vertical following (direct)
        float targetY = player.position.y;

        // Apply both horizontal and vertical positions
        transform.position = new Vector3(player.position.x + lookAhead, targetY, transform.position.z);
    }

   
}