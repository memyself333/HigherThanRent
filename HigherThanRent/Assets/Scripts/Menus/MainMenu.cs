
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    
    public Image startButton;
    public Image loadButton;
    public Image quitButton;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    PlayerCombat playerCombat;
    public AudioSource audioSource;
    public AudioClip mainMenuClip;
    public AudioClip ambienceClip;

    private void Awake()
    {
        Time.timeScale = 0;
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = mainMenuClip;
        audioSource.loop = true;
        audioSource.Play(); 
        startButton.alphaHitTestMinimumThreshold = 0.1f;
        loadButton.alphaHitTestMinimumThreshold = 0.1f;
        quitButton.alphaHitTestMinimumThreshold = 0.1f;
    }


    public void Play()
    {
        Time.timeScale = 1;
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        playerCombat.playerCurrentHealth = playerCombat.playerMaxHealth;
        playerCombat.isPlayerDead = false;
        GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().axeAquired = false;
        GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyAI>().isEnemyDead = false;
        GameObject.FindGameObjectWithTag("Enemies").transform.position = new Vector2(-15.5f, 43f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isCommonRoomDoorBroken = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isLobbyDoorBroken = false;
        GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().isOpen = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isHallucinated = false;
        GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyAI>().Load();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Load();
        GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>().Load();
        audioSource.Stop();
        audioSource.clip = ambienceClip;
        audioSource.Play();
        SceneManager.LoadScene(gameSceneName);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
