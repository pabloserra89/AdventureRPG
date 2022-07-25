using UnityEngine;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour
{
    public Text coinsText;
    public IntValue playerCoins;

    void Start()
    {
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        coinsText.text = playerCoins.value.ToString();
    }
}
