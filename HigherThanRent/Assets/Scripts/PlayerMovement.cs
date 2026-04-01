using UnityEngine;
using UnityEngine.Experimental.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;

    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("vertical", vertical); 
        
        rb.linearVelocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
    }
}
