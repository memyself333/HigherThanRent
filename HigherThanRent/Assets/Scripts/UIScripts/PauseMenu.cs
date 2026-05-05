using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;
    public Animator foxAnim;
    public Animator continueAnim;
    public Animator escapeAnim;

    public Image continueButton;
    public Button continueAlpha;
    public Image escapeButton;
    public Button escapeAlpha;

    void Awake()
    {
        continueButton.alphaHitTestMinimumThreshold = 0.1f;
        escapeButton.alphaHitTestMinimumThreshold = 0.1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "MainMenu")
            {
                return;
            }
            else
            {
                if (gamePaused)
                {
                    Continue();

                }
                else
                {
                    Pause();
                    foxAnim.Play("FoxSmoking");
                    continueAnim.Play("ContinueAppear");
                    escapeAnim.Play("EscapeAppear");
                }
            }
           
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gamePaused = true;
        pauseMenu.SetActive(true);
        

    }

    public void Escape()
    {
        Time.timeScale = 1;
        gamePaused = false;
        SceneManager.LoadScene("MainMenu");

    }

    public void Continue()
    {
        Time.timeScale = 1;
        gamePaused = false;

        var continueColors = continueAlpha.colors;
        continueColors.normalColor = new Color(continueColors.normalColor.r, continueColors.normalColor.g, continueColors.normalColor.b, 0);
        continueAlpha.colors = continueColors;
        var escapeColors = escapeAlpha.colors;
        escapeColors.normalColor = new Color(escapeColors.normalColor.r, escapeColors.normalColor.g, escapeColors.normalColor.b, 0);
        escapeAlpha.colors = escapeColors;

        pauseMenu.SetActive(false);
        
    }

}
