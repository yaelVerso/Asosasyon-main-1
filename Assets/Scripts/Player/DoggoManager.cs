using UnityEngine;
using UnityEngine.UI;

public class DoggoManager : MonoBehaviour
{
    public int doggoCount;
    public Text dogText;
    public GameObject Box;
    void Start()
    {
        doggoCount = GameData.SavedDoggos;
    }

    // Update is called once per frame
    void Update()
    {
        dogText.text = ": " + doggoCount.ToString();
        GameData.SavedDoggos = doggoCount;
        if (doggoCount == 4)
        {
            Box.GetComponent<Rigidbody2D>().mass = 10f;
        }
    }
}
