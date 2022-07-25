using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public EventSystem eventSystem;
    public Button firstButton; 

    void OnEnable()
    {
        Time.timeScale = 0f;
        UIController.AssignEventSystem(eventSystem, firstButton);
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
        UIController.UnassignEventSystem(eventSystem);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
