using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    public Button saveButton;
    public Button loadButton;
    public AudioSource audioSource;
    public AudioClip ambienceClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // keep this for compatibility but ensure SaveLocation is set in Awake too
        if (string.IsNullOrEmpty(saveLocation))
            saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void Awake()
    {
        // ensure path is initialized before any potential SaveGame calls from listeners wired in Awake
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (SceneManager.GetActiveScene().name == "FoxApartment" || SceneManager.GetActiveScene().name == "Lobby")
        {
            var saveObj = GameObject.FindGameObjectWithTag("SaveButton");
            if (saveObj != null)
            {
                saveButton = saveObj.GetComponent<Button>();
                if (saveButton != null)
                {
                    saveButton.onClick.AddListener(() => {
                        var loader = GameObject.FindGameObjectWithTag("SaveLoader");
                        if (loader != null)
                            loader.GetComponent<SaveController>().SaveGame();
                    });
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            var loadObj = GameObject.FindGameObjectWithTag("LoadButton");
            if (loadObj != null)
            {
                loadButton = loadObj.GetComponent<Button>();
                if (loadButton != null)
                {
                    loadButton.onClick.AddListener(() => {
                        var loader = GameObject.FindGameObjectWithTag("SaveLoader");
                        if (loader != null)
                            loader.GetComponent<SaveController>().LoadGame();
                    });
                }
            }
        }
    }

    public void SaveGame()
    {
        // defensive: ensure saveLocation is valid
        if (string.IsNullOrEmpty(saveLocation))
            saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            axeAcquired = GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().axeAquired,
            enemyDead = GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyAI>().isEnemyDead,
            enemyPosition = GameObject.FindGameObjectWithTag("Enemies").transform.position,
            commonRoomDoorOpen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isCommonRoomDoorBroken,
            lobbyDoorOpen = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isLobbyDoorBroken,
            chestOpen = GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().isOpen,
            hallucinated = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isHallucinated,
        };
        Debug.Log("Game trying to save");

        try
        {
            File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
            Debug.Log("Game saved to: " + saveLocation);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to write save file: " + ex.Message);
        }
    }

    public void LoadGame()
    {
        audioSource.Stop();
        audioSource.clip = ambienceClip;
        audioSource.Play();
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            UnityEngine.SceneManagement.SceneManager.LoadScene(saveData.currentScene);
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().axeAquired = saveData.axeAcquired;
            GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyAI>().isEnemyDead = saveData.enemyDead;
            GameObject.FindGameObjectWithTag("Enemies").transform.position = saveData.enemyPosition;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isCommonRoomDoorBroken = saveData.commonRoomDoorOpen;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isLobbyDoorBroken = saveData.lobbyDoorOpen;
            GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().isOpen = saveData.chestOpen;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isHallucinated = saveData.hallucinated;
            GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyAI>().Load();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Load();
            GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().Load();
        }
        else
        {
            Debug.LogWarning("No save file found at " + saveLocation);
        }
    }
}
