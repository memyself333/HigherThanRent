using UnityEngine;

public class HpBar : MonoBehaviour
{
    public int health;
    PlayerCombat playerCombat;
    public GameObject player;
    public Animator barAnim;
     void Awake()
    {
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
            health = playerCombat.playerCurrentHealth;
            barAnim.SetFloat("Health", health);
    }
}
