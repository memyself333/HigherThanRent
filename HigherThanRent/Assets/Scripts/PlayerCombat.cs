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

    public bool isPlayerDead = false;





    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        isPlayerDead = false;
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerTakeDamage(1);
        }
    }
    void Attack()
    {
        //Attack Aimation Goes Here!!
        playerAnim.Play("PlayerAttack");

        audioSource.PlayOneShot(combatSounds[0]);

        //Detect Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().EnemyTakeDamage(attackDamage);
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

        playerCurrentHealth += 1; 
        
    //    flowerAnim.Play("HPFlowerFast");
    }

    public void PlayerDeath()
    {
        isPlayerDead = true;
        playerAnim.updateMode = AnimatorUpdateMode.UnscaledTime; // Ensures the death animation plays even when time is stopped
        playerAnim.Play("PlayerDeath");
        playerAnim.updateMode = AnimatorUpdateMode.Normal; // Resets the update mode to normal after the death animation has played
    }
}
