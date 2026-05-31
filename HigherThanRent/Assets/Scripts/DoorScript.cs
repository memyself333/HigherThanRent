using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isBroken = false;
    public Animator anim;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        isBroken = player.GetComponent<PlayerCombat>().isLobbyDoorBroken;
        if (isBroken)
        {
            gameObject.SetActive(false);
        }
    }

    public void BreakDoor()
    {
        anim.Play("DoorBreak");
    }

    public void FinishBreaking()
    {
        player.GetComponent<PlayerCombat>().isLobbyDoorBroken = true;
        
    }
}
