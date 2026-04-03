using UnityEngine;
using UnityEngine.Experimental.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;

    
    void FixedUpdate()
    {
        //Get horizontal and vertical inputs from player, assign them to variables which will either equal 1, 0,or -1
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Set paramaters in animator to match horizontal and vertical inputs
        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("vertical", vertical); 
        
        // Set player velocity based on horizsontal and vertical variables, multplied by the playerSpeed set in the inspector
        rb.linearVelocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
    }
}
