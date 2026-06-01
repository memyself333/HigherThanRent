using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{ 
    //Enemy AI
    public GameObject Player;
    public Rigidbody2D rb;
    public float speed;

    public Animator anim;
    public Sprite dead;
    public SpriteRenderer sprite;
    bool isMoving = false;
    bool isHurting = false;
    bool isAttacking = false;

    public int hitRange;
    public bool combatAudioReady = false;

    public float distance;
    public float distanceBetween;

    //Combat
    public int EnemyMaxHealth = 3;
    int EnemyCurrentHealth;

    float nextEnemyAttackTime = 0f;
    public float enemyAttackRange = 0.5f; 
    public float enemyAttackRate = 5f;
    public int enemyAttackDamage = 1;

    public Transform enemyAttackPoint;

    //Defines which objects is the player, only attacks objects detected in this layer 
    public LayerMask playerLayer;

    public AudioSource audioSource;
    public AudioClip ambienceClip;
    public AudioClip combatClip;
    public AudioClip gameOverClip;
    public bool isEnemyDead = false;
    public bool isPlayerDead = false;
    public Chest chestScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
        anim.Play("EnemyIdle");
    }

    public void EnemyTakeDamage(int damage)
    {
        isHurting = true;
        isAttacking = false;
        EnemyCurrentHealth -= damage;

        //Hurt Animation goes here!
        anim.SetBool("Attack", false);
        anim.SetBool("Hurt", true);

        if (EnemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void FinishHurt()
    {
        isHurting = false;
        anim.SetBool("Hurt", false);
    }

    public void Die()
    {
        //Enemy Die Animation Goes Here!!
        rb.linearVelocity = Vector2.zero;
        anim.Play("EnemyDead");

        //Disable the enemy 
        GetComponent<Collider2D>().enabled = false;
        isEnemyDead = true;
        audioSource.Stop();
        audioSource.clip = ambienceClip;
        audioSource.Play();
        chestScript.PlayTimeline();
        this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDead = Player.GetComponent<PlayerCombat>().isPlayerDead;
        //Move enemy towards player
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        if (isHurting == true)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if (isAttacking == true)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            if ((distance < distanceBetween) && (distance > 1))
            {
                isMoving = true;
                rb.linearVelocity = direction;
                anim.SetBool("Move", true);
            }
            else
            {
                anim.SetBool("Move", false);
                isMoving = false;
                rb.linearVelocity = Vector2.zero;
            }
        }

        if (Time.time >= nextEnemyAttackTime)
        {
            if (distance <= hitRange)
            {
                EnemyAttack();
                nextEnemyAttackTime = Time.time + 5f / enemyAttackRate;
            }
        }

        if (SceneManager.GetActiveScene().name == "CommonRoom")
        {
            if (isEnemyDead == false)
            {
                if (isPlayerDead == true)
                {
                    if (audioSource.clip != gameOverClip)
                    {
                        audioSource.Stop();
                        audioSource.clip = gameOverClip;
                        audioSource.Play();
                    }   
                }
            }
        }
    }

     public void EnemyAttack()
     {
        //Enemy Attack Animation
        isAttacking = true;
        anim.SetBool("Hurt", false);
        anim.SetBool("Attack", true);

        //Detect if player is in range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackRange, playerLayer);

        //Damage Enemies
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerCombat>().PlayerTakeDamage(enemyAttackDamage);
        }
    }

    public void FinishAttack()
    {
        isAttacking = false;
        anim.SetBool("Attack", false);
    }

    public void Load()
    {
       if (isEnemyDead)
        { 
            sprite.sprite = dead;
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }
    
    public void StopAudio()
    {         audioSource.Stop();
    }
    public void CombatAudioReady()
    {
        if (audioSource.clip != combatClip)
        {
            audioSource.Stop();
            audioSource.clip = combatClip;
            audioSource.Play();
        }
    }

}

