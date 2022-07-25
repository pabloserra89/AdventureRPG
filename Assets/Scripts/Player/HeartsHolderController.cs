using UnityEngine;
using UnityEngine.UI;

public class HeartsHolderController : MonoBehaviour
{
    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite threeQuartersHeart;
    public Sprite halfFullHeart;
    public Sprite oneQuartersHeart;
    public Sprite emptyHeart;

    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    void Start()
    {
        InitHearts();
        UpdateHearts();
    }

    public void InitHearts()
    {
        for (int i = 1; i <= heartContainers.value; i++)
        {
            hearts[i-1].gameObject.SetActive(true);
            hearts[i-1].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        Sprite heartSprite;

        for (int i = 1; i <= heartContainers.value; i++)
        {
            if (i <= playerCurrentHealth.value)
                heartSprite = fullHeart;
            else if (i >= playerCurrentHealth.value + 1)
                heartSprite = emptyHeart;
            else if (i == playerCurrentHealth.value + 0.25f)
                heartSprite = threeQuartersHeart;
            else if (i == playerCurrentHealth.value + 0.75f)
                heartSprite = oneQuartersHeart;
            else
                heartSprite = halfFullHeart;

            hearts[i-1].gameObject.SetActive(true);
            hearts[i-1].sprite = heartSprite;
        }
    }
}
