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
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "FoxApartment" || SceneManager.GetActiveScene().name == "Lobby")
        {
            saveButton = GameObject.FindGameObjectWithTag("SaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(() => {
                GameObject.FindGameObjectWithTag("SaveLoader").GetComponent<SaveController>().SaveGame();
            });

        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            loadButton = GameObject.FindGameObjectWithTag("LoadButton").GetComponent<Button>();
            loadButton.onClick.AddListener(() => {
                GameObject.FindGameObjectWithTag("SaveLoader").GetComponent<SaveController>().LoadGame();
            });
        }

    }

    public void SaveGame()
    {
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
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log("Game saved");
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
