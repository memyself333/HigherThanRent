using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    // KNOCKBACK
     [SerializeField]
    private Rigidbody2D rb2d; // reference to what rigidbody the force will be applied to

    [SerializeField]
    private float strength = 16, delay = 0.15f; // how powerful it is, the delay after which you can move again

    // END KNOCKBACK
    //Enemy AI
    public GameObject Player;
    public float speed;

    public Animator anim;

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

        // Changes
    //    Vector2 direction = (transform.position - Player.transform.position).normalized; // the direction from the sender
    //    rb2d.AddForce(direction*strength, ForceMode2D.Impulse); // the actual knockback
        // End Changes

        if (EnemyCurrentHealth <= 0)
        {
            Die();
        }

        anim.SetBool("hurt", false);
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
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        if (Time.time >= nextEnemyAttackTime)
        {
            if (distance <= 1)
            {
                EnemyAttack();
                nextEnemyAttackTime = Time.time + 5f / enemyAttackRate;
            }
        }
    }

     public void EnemyAttack()
     {
        //Enemy Attack Animation

        //Detect if player is in range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackRange, playerLayer);

        //Damage Enemies
        foreach (Collider2D player in hitPlayer)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            player.GetComponent<PlayerCombat>().PlayerTakeDamage(enemyAttackDamage);
        }

        rb2d.constraints = RigidbodyConstraints2D.None;
     }

}

