using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    [Header("Game Stuff")]
    public StringValue sceneToLoad;
    public BoolValue houseTimeline;

    [Header("Player Stuff")]
    public BoolValue playerMage;
    public Vector2Value playerPosition;

    public FloatValue playerCurrentHealth;
    public FloatValue playerMaxHealth;

    public FloatValue playerCurrentMana;
    public FloatValue playerMaxMana;

    public IntValue playerCoins;
    public Inventory playerInventory;

    private SaveData saveData;

    public void SaveData()
    {
        saveData = new SaveData
        {
            currentScene = SceneManager.GetActiveScene().name,
            houseTimeline = houseTimeline.value,

            playerMage = playerMage.value,

            playerPositionX = playerPosition.value.x,
            playerPositionY = playerPosition.value.y,

            playerCurrentHealth = playerCurrentHealth.value,
            playerMaxHealth = playerMaxHealth.value,

            playerCurrentMana = playerCurrentMana.value,
            playerMaxMana = playerMaxMana.value,

            playerCoins = playerCoins.value
        };

        saveData.SaveInventory(playerInventory);

        FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", "saveData"));
        BinaryFormatter binary = new BinaryFormatter();
        binary.Serialize(file, (string)JsonUtility.ToJson(saveData));
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", "saveData")))
        {
            saveData = new SaveData();
            
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", "saveData"), FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), saveData);
            file.Close();

            sceneToLoad.value = saveData.currentScene;
            houseTimeline.value = saveData.houseTimeline;

            playerMage.value = saveData.playerMage;

            playerPosition.value.x = saveData.playerPositionX;
            playerPosition.value.y = saveData.playerPositionY;

            playerCurrentHealth.value = saveData.playerCurrentHealth;
            playerMaxHealth.value = saveData.playerMaxHealth;

            playerCurrentMana.value = saveData.playerCurrentMana;
            playerMaxMana.value = saveData.playerMaxMana;

            playerCoins.value = saveData.playerCoins;
            saveData.GetInventory(playerInventory);
        }
        else
        {
            sceneToLoad.value = "HouseScene";
        }
    }

    public void ResetSave()
    {
        if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", "saveData")))
            File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", "saveData"));
    }
}
