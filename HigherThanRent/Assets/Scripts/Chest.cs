using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform playerDetector;
    public float playerDetectorRange = 1f;
    public LayerMask playerLayer;
    public GameObject player;
    public Animator chestAnim;
    public bool isOpen = false;
    public bool axeAquired = false;
    public PlayerCombat playerCombat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerLayer = LayerMask.GetMask("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, playerDetector.position) <= playerDetectorRange)
        {
            if (!isOpen)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    chestAnim.Play("ChestOpen");
                    axeAquired = true;

                }
            }
        }
    }

    void FinishOpeningChest()
    {
        playerCombat.AquireAxe();
        isOpen = true;
    }
}
