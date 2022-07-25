using UnityEngine;
using UnityEngine.UI;

public class ManaHolderController : MonoBehaviour
{
    public FloatValue maxMana;
    public FloatValue currentMana;

    private RectTransform rectTransform;
    private Slider manaBar;

    private float manaFactor = 10f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        manaBar = GetComponent<Slider>();
    }

    void Start()
    {
        UpdateMana();
    }

    public void UpdateMana()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * maxMana.value / manaFactor, rectTransform.sizeDelta.y); 
        manaBar.value = currentMana.value / maxMana.value;
    }
}
