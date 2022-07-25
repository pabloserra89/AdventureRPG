using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceTextScript : MonoBehaviour
{
    private Text placeText;
    private IEnumerator coroutine;

    public void Show(string text)
    {
        if(placeText == null)
            placeText = GetComponent<Text>();

        if(coroutine != null)
            StopCoroutine(coroutine);
        
        placeText.text = text;
        placeText.CrossFadeAlpha(1f, 0f, false);
        gameObject.SetActive(true);

        coroutine = ShowText();
        StartCoroutine(coroutine);
    }

    private IEnumerator ShowText()
    {
        placeText.CrossFadeAlpha(0f, 3f, false);

        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
