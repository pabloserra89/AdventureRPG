using UnityEngine;

public class GameController : MonoBehaviour
{
    public FadeImageController fadeImage;

    public static Player player; 
    public static Transform playerTransform;

    public void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerTransform = player.gameObject.transform;
    }

    public void Start()
    {
        if(fadeImage != null)
            fadeImage.Fade();
    }
}
