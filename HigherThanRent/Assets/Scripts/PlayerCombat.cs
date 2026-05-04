using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    //Player Health
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;

    public Transform attackPoint;

    public AudioSource audioSource;
    public AudioClip[] combatSounds;

    //animation
    public Animator weaponAnim;
    public Animator playerAnim;
    public Animator flowerAnim;

    //Attacks
    public float attackRange = 2f;
    public int attackDamage = 1;

    //How many times you can attack per second
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    //Defines which objects are enemies, only attacks objects detected in this layer 
    public LayerMask enemyLayers;



   

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        //Attack Aimation Goes Here!!
        weaponAnim.Play("Attack");
        audioSource.PlayOneShot(combatSounds[0]);

        //Detect Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().EnemyTakeDamage(attackDamage);
        }

        weaponAnim.SetBool("attacking", false); 
    }

    public void PlayerTakeDamage(int damage)
    {
        playerCurrentHealth -= damage;

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

    public void PlayerDeath()
    {
        //Reset player to last save state

        //Reset Player Health
        playerCurrentHealth = playerMaxHealth;
    }
}
