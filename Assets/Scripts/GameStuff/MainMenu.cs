using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public StringValue sceneToLoad;

    public void NewGame()
    {
        StartCoroutine(NewSceneCo());
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator NewSceneCo()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad.value);

        while (!asyncOperation.isDone)
            yield return null;
    }
}
