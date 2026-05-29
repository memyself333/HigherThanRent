using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform playerDetector;
    public float playerDetectorRange = 1f;
    public LayerMask playerLayer;
    public GameObject player;
    public GameObject chest;
    public Animator chestAnim;
    public bool isOpen = false;
    public bool axeAquired = false;
    public PlayerCombat playerCombat;
    public EnemyAI enemyAI;
    public bool enemyIsDead = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombat = player.GetComponent<PlayerCombat>();
        chest.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.isEnemyDead == true)
        {
            enemyIsDead = true;

        }
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
