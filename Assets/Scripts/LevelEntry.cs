using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}