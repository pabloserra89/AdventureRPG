using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public FadeImageController fadeImage;

    [Header("Player")]
    public Vector2 playerNewPosition;
    public Vector2Value playerStoragePosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().InTransition();
            StartCoroutine(NewSceneCo());
        }
    }

    private IEnumerator NewSceneCo()
    {
        fadeImage.Fade();
        
        yield return new WaitForSeconds(0.33f);

        playerStoragePosition.value = playerNewPosition;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
            yield return null;
    }
}
