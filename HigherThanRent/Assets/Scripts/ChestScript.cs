using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

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
    public ParticleSystem chestExplosion;
    public PlayableDirector chestTimeline;
    public AudioSource audioSource;
    public AudioClip chestOpenSound;
    public Sprite chestOpen;


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
            if (Vector2.Distance(player.transform.position, playerDetector.position) <= playerDetectorRange)
            {
                if (!isOpen)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        chestAnim.Play("ChestOpen");
                        audioSource.PlayOneShot(chestOpenSound);
                        axeAquired = true;

                    }
                }
            }
        }
        else
        {
            enemyIsDead = false;
            chest.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

    }

    void FinishOpeningChest()
    {
        playerCombat.AquireAxe();
        isOpen = true;
    }

    public void Appear()
    {
        chest.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        chestExplosion.Play();
    }

    public void PlayTimeline()
    {
        chestTimeline.Play();
    }   
    public void Load()
    {
        if (isOpen)
        {
            chest.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            chestAnim.SetBool("Open", true);
        }
            else
            {
                chest.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                chestAnim.SetBool("Open", false);
        }
    }
}
