using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private StringValue sceneToLoad;

    [SerializeField] private GameObject controlsScreen;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            HideControls();
    }

    public void NewGame()
    {
        StartCoroutine(NewSceneCo());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        controlsScreen.SetActive(true);
    }

    private void HideControls()
    {
        controlsScreen.SetActive(false);
    }

    private IEnumerator NewSceneCo()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad.value);

        while (!asyncOperation.isDone)
            yield return null;
    }
}
