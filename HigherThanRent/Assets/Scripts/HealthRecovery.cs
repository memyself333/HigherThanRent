using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthRecovery : MonoBehaviour
{   
    public GameObject Player;

    //Defines which objects is the player, only attacks objects detected in this layer 
    public LayerMask playerLayer;

    public int recoveryPoints;
    public float healingRange;
    public float nextHealingTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//    public void Healing()
//     {  
//        //Detect if player is in range
//        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, healingRange, playerLayer);

        //Damage Enemies
//        foreach (Collider2D player in hitPlayer)
//        {
//            player.GetComponent<PlayerCombat>().PlayerHeal(recoveryPoints);
//        }

//        gameObject.SetActive(false);

//     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "CommonRoom")
        {
        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<PlayerCombat>().PlayerHeal();
            Debug.Log("The collision was detected."); // Ok at least I know it's being detected.
            gameObject.SetActive(false);
        }
        }
    }

}
