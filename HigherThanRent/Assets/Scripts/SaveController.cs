using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    public Button saveButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "FoxApartment")
        {
            saveButton = GameObject.FindGameObjectWithTag("SaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(() => {
                GameObject.FindGameObjectWithTag("SaveLoader").GetComponent<SaveController>().SaveGame();
            });
        }
        
    }
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            UnityEngine.SceneManagement.SceneManager.LoadScene(saveData.currentScene);
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
        }
        else
        {
            Debug.LogWarning("No save file found at " + saveLocation);
        }
    }
}
