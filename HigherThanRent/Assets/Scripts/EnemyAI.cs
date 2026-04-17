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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        EnemyCurrentHealth -= damage;

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
        distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        /*if (distanceBetween < 1)
        {
            remove 1 hp from player (need to establish hp for player in another script assigned to player - must be public)
            give time until enemy can attack again
        }
        */
    }
}
