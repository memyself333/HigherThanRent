using UnityEngine;

public class HealthRecovery : MonoBehaviour
{   
    public GameObject Player;

    //Defines which objects is the player, only attacks objects detected in this layer 
    public LayerMask playerLayer;

    public int recoveryPoints;
    public float healingRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Healing()
     {
        //Detect if player is in range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, healingRange, playerLayer);

        //Damage Enemies
        foreach (Collider2D player in hitPlayer)
        {
    //        player.GetComponent<PlayerCombat>().PlayerHeal(recoveryPoints);
        }
     }

}
