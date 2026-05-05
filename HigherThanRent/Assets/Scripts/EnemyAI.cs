using UnityEngine;

public class EnemyAI : MonoBehaviour
{ 
    //Enemy AI
    public GameObject Player;
    public float speed;

    public Animator anim;
    bool isMoving = false;

    public int hitRange;

    private float distance;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
        anim.Play("EnemyIdle");
    }

    public void EnemyTakeDamage(int damage)
    {
        EnemyCurrentHealth -= damage;

        //Hurt Animation goes here!
        anim.Play("EnemyHurt");

        if (EnemyCurrentHealth <= 0)
        {
            Die();
        }

        anim.SetBool("Hurt", false);
    }

    public void Die()
    {
        //Enemy Die Animation Goes Here!!
        anim.Play("EnemyDead");

        //Disable the enemy 
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Move enemy towards player
        distance = Vector2.Distance(transform.position, Player.transform.position);

        if ((distance < distanceBetween) && (distance > 1))
        {
            isMoving = true; 
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }

        if (Time.time >= nextEnemyAttackTime)
        {
            if (distance <= hitRange)
            {
                EnemyAttack();
                nextEnemyAttackTime = Time.time + 5f / enemyAttackRate;
            }
        }
    }

     public void EnemyAttack()
     {
        //Enemy Attack Animation
        anim.Play("EnemyAttack");

        //Detect if player is in range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackRange, playerLayer);

        //Damage Enemies
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerCombat>().PlayerTakeDamage(enemyAttackDamage);
        }
     }
}

