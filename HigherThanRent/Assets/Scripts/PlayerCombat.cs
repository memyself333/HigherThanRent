using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    //Player Health
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;
    public PlayerMovement playerMovement;
    public Chest chest;
    public int direction;

    public Transform axeAttackPoint;
    public Transform foxAttackPoint;


    public AudioSource audioSource;
    public AudioClip[] combatSounds;

    //animation
    public Animator playerAnim;
    public Animator flowerAnim;

    //How many times you can attack per second
    public float axeAttackRange = 2f;
    public float foxAttackRange = 0.5f;
    public int axeAttackDamage = 1;
    public int foxAttackDamage = 1;

    //How many times you can attack per second
    public float foxAttackRate = 2f;
    public float axeAttackRate = 1f;

    //Attacks
    float nextAttackTime = 0f;


    //Defines which objects are enemies, only attacks objects detected in this layer 
    public LayerMask enemyLayers;
    public LayerMask doorLayers;

    public bool isPlayerDead = false;

    public AudioSource musicSource;
    public AudioClip gameOverClip;

    public bool isLobbyDoorBroken = false;





    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        isPlayerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "CommonRoom")
        {
            chest = GameObject.Find("Chest").GetComponent<Chest>(); ;
        }
        direction = playerMovement.direction;
        if (direction == 0)
        {
            foxAttackPoint.localPosition = new Vector3(0, -1f, 0);
        }
        else if (direction == 1)
        {
            foxAttackPoint.localPosition = new Vector3(1f, 0, 0);
        }
        else if (direction == 2)
        {
            foxAttackPoint.localPosition = new Vector3(0, 1f, 0);
        }
        else if (direction == 3)
        {
            foxAttackPoint.localPosition = new Vector3(-1f, 0, 0);
        }
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (chest.axeAquired)
                {
                    AxeAttack();
                    nextAttackTime = Time.time + 1f / axeAttackRate;
                }
                else
                {
                    FoxAttack();
                    nextAttackTime = Time.time + 1f / foxAttackRate;
                }
            }
        }
    }
    void FoxAttack()
    {
        //Attack Aimation Goes Here!!
        if (direction == 0)
        {
            playerAnim.Play("FoxAttackDown");
        }
        else if (direction == 1)
        {
            playerAnim.Play("FoxAttackRight");
        }
        else if (direction == 2)
        {
            playerAnim.Play("FoxAttackUp");
        }
        else if (direction == 3)
        {
            playerAnim.Play("FoxAttackLeft");
        }
        audioSource.PlayOneShot(combatSounds[0]);
        //Detect Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(foxAttackPoint.position, foxAttackRange, enemyLayers);

        //Damage Enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().EnemyTakeDamage(foxAttackDamage);
        }

    }
    void AxeAttack()
    {
        //Attack Aimation Goes Here!!
        playerAnim.Play("AxeAttack");

        audioSource.PlayOneShot(combatSounds[0]);

        //Detect Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(axeAttackPoint.position, axeAttackRange, enemyLayers);


        //Damage Enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().EnemyTakeDamage(axeAttackDamage);
        }

        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            Collider2D[] breakDoors = Physics2D.OverlapCircleAll(foxAttackPoint.position, foxAttackRange, doorLayers);
            //Damage Enemies
            foreach (Collider2D door in breakDoors)
            {
                door.GetComponent<DoorScript>().BreakDoor();
            }
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        if (playerCurrentHealth > 0)
        {
            playerCurrentHealth -= damage;
        }

        //Hurt Animation goes here!
        playerAnim.Play("PlayerHurt");
        flowerAnim.Play("HpFlowerFast");
        audioSource.PlayOneShot(combatSounds[1]);


        playerAnim.SetBool("hurt", false);


        if (playerCurrentHealth <= 0)
        {
            PlayerDeath();
        }

    }

    public void PlayerHeal()
    {
        Debug.Log("The Player Heal function is being called");

        if (playerCurrentHealth < playerMaxHealth)
        {
            playerCurrentHealth += 1;
        }

        //    flowerAnim.Play("HPFlowerFast");
    }

    public void PlayerDeath()
    {
        musicSource.Stop();
        musicSource.clip = gameOverClip;
        musicSource.loop = false;
        musicSource.Play();
        isPlayerDead = true;
        playerAnim.updateMode = AnimatorUpdateMode.UnscaledTime; // Ensures the death animation plays even when time is stopped
        playerAnim.Play("PlayerDeath");
        playerAnim.updateMode = AnimatorUpdateMode.Normal; // Resets the update mode to normal after the death animation has played
    }

    public void AquireAxe()
    {
        playerAnim.Play("AquireAxe");
    }
}
