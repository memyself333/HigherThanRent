using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public bool isBroken = false;
    public Animator anim;
    public GameObject player;
    public AudioSource audioSource;
    public AudioClip breakDoor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            isBroken = player.GetComponent<PlayerCombat>().isLobbyDoorBroken;
        }
        else if (SceneManager.GetActiveScene().name == "CommonRoom")
        {
            isBroken = player.GetComponent<PlayerCombat>().isCommonRoomDoorBroken;
        }
        if (isBroken)
        {
            gameObject.SetActive(false);
        }
    }

    public void BreakDoor()
    {
        anim.Play("DoorBreak");
        audioSource.PlayOneShot(breakDoor);
    }

    public void FinishBreaking()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            player.GetComponent<PlayerCombat>().isLobbyDoorBroken = true;
        }
        if (SceneManager.GetActiveScene().name == "CommonRoom")
        {
            player.GetComponent<PlayerCombat>().isCommonRoomDoorBroken = true;
        }
    }
}
