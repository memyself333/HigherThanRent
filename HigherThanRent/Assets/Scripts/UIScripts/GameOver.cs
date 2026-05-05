using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    PlayerCombat playerCombat;
    public GameObject player;
    public int health;
    public GameObject gameOverScreen;
    public AudioSource bBall;
    public Animator backgroundAnim;
    public Animator continueAnim;
    public Animator spotlightAnim;
    public Animator gameOverAnim;
    public Image continueButton;
    public float timeScale;


    void Awake()
    {
        continueButton.alphaHitTestMinimumThreshold = 0.1f;
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombat = player.GetComponent<PlayerCombat>();
        timeScale = Time.timeScale; 
        health = playerCombat.playerCurrentHealth;



        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameOverScreen.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "FoxApartment")
        {
            gameOverScreen.SetActive(false);
        }
        else
        {
            if (playerCombat.isPlayerDead == true)
            {
                StartCoroutine(WaitForDeathAnimation());
            }
            else
            {
                gameOverScreen.SetActive(false);
            }
        }
    }

    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(1.7f);
        ShowGameOver();
    }
    public void ShowGameOver()
    {
        Time.timeScale = 0;
        bBall.mute = true;
        gameOverScreen.SetActive(true);
        backgroundAnim.Play("FadeBackground");
        continueAnim.Play("ContinueAppear");
        spotlightAnim.Play("Spotlight");
        gameOverAnim.Play("GameOverFade");

    }

    public void Escape()
    {
        Time.timeScale = 1f;

        if (playerCombat != null)
        {
            playerCombat.playerCurrentHealth = playerCombat.playerMaxHealth;
            playerCombat.isPlayerDead = false;
        }

        StopAllCoroutines(); // stop any pending WaitForDeathAnimation
        gameOverScreen.SetActive(false);
        bBall.mute = false;
        SceneManager.LoadScene("MainMenu");
    }
}
