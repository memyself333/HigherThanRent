using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public float attackRange = 2f;

    public int attackDamage = 1;

    //How many times you can attack per second
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    //Defines which objects are enemies, only attacks objects detected in this layer 
    public LayerMask enemyLayers;

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
        
        //Detect Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }
}
