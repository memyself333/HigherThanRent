using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfinerFinder : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    PlayerCombat playerCombat;
    public GameObject player;


    // When enabled, subscribe the function OnSceneLoaded to the sceneLoaded event, so that when a new scene is loaded, the function will be called and the confiner will be assigned to the camera
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // When disabled, unsubscribe the function OnSceneLoaded from the sceneLoaded event
    private void OnDisable()
    { 
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Function to find the confiner in the scene and assign it to the CinemachineConfiner2D component on the camera
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pauseMenu.SetActive(false); // Ensures the pause menu is closed when a new scene is loaded
        gameOverScreen.SetActive(false); // Ensures the game over screen is closed when a new scene is loaded
        playerCombat = player.GetComponent<PlayerCombat>(); 
        playerCombat.isPlayerDead = false; // Resets the player's death status when a new scene is loaded
        Time.timeScale = 1; // Resets the time scale to normal when a new scene is loaded
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = GameObject.FindGameObjectWithTag("Confiner").GetComponent<BoxCollider2D>();
    }
}