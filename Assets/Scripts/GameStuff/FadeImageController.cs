using System.Collections;
using UnityEngine;

public class FadeImageController : MonoBehaviour
{
    public GameObject fadeImage;
    public bool fadeIn;

    private IEnumerator coroutine;

    void OnEnable()
    {
        fadeImage.GetComponent<Animator>().SetBool("fadeIn", fadeIn);
    }

    public void Fade()
    {
        if (coroutine != null)
        {
            print("StopCoroutine");
            StopCoroutine(coroutine);
        }

        fadeImage.SetActive(true);

        coroutine = LateCall();
        StartCoroutine(coroutine);
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(1f);
        fadeImage.SetActive(false);
    }
}
