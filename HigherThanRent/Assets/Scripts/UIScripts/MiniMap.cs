using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Image minimap;
    public Animator anim;
    public GameObject player;
   
    public PauseMenu pauseMenu;
    public DialogueManager dialogueManager;
    public bool isPlayerDead = false;
    public bool paused = false;
    public bool dialogueActive = false;
    // Awake is called when the script instance is being loaded


    // Update is called once per frame
    void Update()
    {
        isPlayerDead = player.GetComponent<PlayerCombat>().isPlayerDead;
        paused = pauseMenu.gamePaused;
        


        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            minimap.color = new Color(1, 1, 1, 0);
        }
        else if (isPlayerDead == true)
        {
            minimap.color = new Color(1, 1, 1, 0);
        }
        else if (paused == true)
        {
            minimap.color = new Color(1, 1, 1, 0);
        }
        else if (SceneManager.GetActiveScene().name == "FoxApartment")
        {
            dialogueManager = GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<DialogueManager>();
            dialogueActive = dialogueManager.isDialogueActive;
            if (dialogueActive == true)
            {
                minimap.color = new Color(1, 1, 1, 0);
                anim.SetBool("inFoxApartment", false);
                anim.SetBool("inHallway", false);
                anim.SetBool("inMainStaircase1F", false);
                anim.SetBool("inMainStaircase2F", false);
                anim.SetBool("inLobby", false);
                anim.SetBool("inCommonRoom", false);
                anim.SetBool("inWarningRoom", false);
                anim.SetBool("inBossRoom", false);
            }
            else
            {
                minimap.color = new Color(1, 1, 1, 1);
                anim.SetBool("inFoxApartment", true);
                anim.SetBool("inHallway", false);
                anim.SetBool("inMainStaircase1F", false);
                anim.SetBool("inMainStaircase2F", false);
                anim.SetBool("inLobby", false);
                anim.SetBool("inCommonRoom", false);
                anim.SetBool("inWarningRoom", false);
                anim.SetBool("inBossRoom", false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Hallway")
        {
            minimap.color = new Color(1, 1, 1, 1);
            anim.SetBool("inFoxApartment", false);
            anim.SetBool("inHallway", true);
            anim.SetBool("inMainStaircase1F", false);
            anim.SetBool("inMainStaircase2F", false);
            anim.SetBool("inLobby", false);
            anim.SetBool("inCommonRoom", false);
            anim.SetBool("inWarningRoom", false);
            anim.SetBool("inBossRoom", false);
        }
        else if (SceneManager.GetActiveScene().name == "MainStaircase1F")
        {
            minimap.color = new Color(1, 1, 1, 1);
            anim.SetBool("inFoxApartment", false);
            anim.SetBool("inHallway", false);
            anim.SetBool("inMainStaircase1F", true);
            anim.SetBool("inMainStaircase2F", false);
            anim.SetBool("inLobby", false);
            anim.SetBool("inCommonRoom", false);
            anim.SetBool("inWarningRoom", false);
            anim.SetBool("inBossRoom", false);
        }
        else if (SceneManager.GetActiveScene().name == "MainStaircase2F")
        {
            minimap.color = new Color(1, 1, 1, 1);
            anim.SetBool("inFoxApartment", false);
            anim.SetBool("inHallway", false);
            anim.SetBool("inMainStaircase1F", false);
            anim.SetBool("inMainStaircase2F", true);
            anim.SetBool("inLobby", false);
            anim.SetBool("inCommonRoom", false);
            anim.SetBool("inWarningRoom", false);
            anim.SetBool("inBossRoom", false);
        }
        else if (SceneManager.GetActiveScene().name == "Lobby")
        {
            dialogueManager = GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<DialogueManager>();
            dialogueActive = dialogueManager.isDialogueActive;
            if (dialogueActive == true)
            {
                minimap.color = new Color(1, 1, 1, 0);
                anim.SetBool("inFoxApartment", false);
                anim.SetBool("inHallway", false);
                anim.SetBool("inMainStaircase1F", false);
                anim.SetBool("inMainStaircase2F", false);
                anim.SetBool("inLobby", false);
                anim.SetBool("inCommonRoom", false);
                anim.SetBool("inWarningRoom", false);
                anim.SetBool("inBossRoom", false);
            }
            else
            {
                minimap.color = new Color(1, 1, 1, 1);
                anim.SetBool("inFoxApartment", false);
                anim.SetBool("inHallway", false);
                anim.SetBool("inMainStaircase1F", false);
                anim.SetBool("inMainStaircase2F", false);
                anim.SetBool("inLobby", true);
                anim.SetBool("inCommonRoom", false);
                anim.SetBool("inWarningRoom", false);
                anim.SetBool("inBossRoom", false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "CommonRoom")
        {
            minimap.color = new Color(1, 1, 1, 1);
            anim.SetBool("inFoxApartment", false);
            anim.SetBool("inHallway", false);
            anim.SetBool("inMainStaircase1F", false);
            anim.SetBool("inMainStaircase2F", false);
            anim.SetBool("inLobby", false);
            anim.SetBool("inCommonRoom", true);
            anim.SetBool("inWarningRoom", false);
            anim.SetBool("inBossRoom", false);
        }
        else if (SceneManager.GetActiveScene().name == "WarningRoom")
        {
            dialogueManager = GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<DialogueManager>();
            dialogueActive = dialogueManager.isDialogueActive;
            if (dialogueActive == true)
            {
                minimap.color = new Color(1, 1, 1, 0);
                anim.SetBool("inFoxApartment", false);
                anim.SetBool("inHallway", false);
                anim.SetBool("inMainStaircase1F", false);
                anim.SetBool("inMainStaircase2F", false);
                anim.SetBool("inLobby", false);
                anim.SetBool("inCommonRoom", false);
                anim.SetBool("inWarningRoom", false);
                anim.SetBool("inBossRoom", false);
            }
            else
            {
                minimap.color = new Color(1, 1, 1, 1);
                anim.SetBool("inFoxApartment", false);
                anim.SetBool("inHallway", false);
                anim.SetBool("inMainStaircase1F", false);
                anim.SetBool("inMainStaircase2F", false);
                anim.SetBool("inLobby", false);
                anim.SetBool("inCommonRoom", false);
                anim.SetBool("inWarningRoom", true);
                anim.SetBool("inBossRoom", false);
            }

        }
        else if (SceneManager.GetActiveScene().name == "BossRoom")
        {
            minimap.color = new Color(1, 1, 1, 1);
            anim.SetBool("inFoxApartment", false);
            anim.SetBool("inHallway", false);
            anim.SetBool("inMainStaircase1F", false);
            anim.SetBool("inMainStaircase2F", false);
            anim.SetBool("inLobby", false);
            anim.SetBool("inCommonRoom", false);
            anim.SetBool("inWarningRoom", false);
            anim.SetBool("inBossRoom", true);
        }

    }
}
