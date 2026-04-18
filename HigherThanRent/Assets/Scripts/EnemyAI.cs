using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Enemy AI
    public GameObject Player;
    public float speed;

    private float distance;
    public float distanceBetween;

    //Combat
    public int EnemyMaxHealth = 3;
    int EnemyCurrentHealth;

    float nextEnemyAttackTime = 1f;
    public float enemyAttackRate = 2f;
    public float enemyAttackDamage = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        EnemyCurrentHealth -= damage;

        //Hurt Animation goes here!

        if (EnemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Enemy Die Animation Goes Here!!

        //Disable the enemy 
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Move enemy towards player
        distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        //Enemy Attack 
        /*if (Time.time >= nextEnemyAttackTime)
        {
            if (distanceBetween < 1)
            {
                //Play attack animtion

                //player must take damage (enemy attack) (playerTakeDamage)

                nextEnemyAttackTime = Time.time + 1f / enemyAttackRate;
            }
        }*/
    }
}
