using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 3f;
    public AudioClip[] footstepStoneClips;
    public AudioClip[] footstepCarpetClips;
    public AudioSource audioSource;
    private int lastClipIndex = -1;
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

        if (rb.linearVelocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                return;
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "FoxApartment")
                {
                    PlayCarpetFootstep();
                }
                else
                {
                    PlayStoneFootstep();
                }
            }
        }
    }

    public void PlayStoneFootstep()
    {
        if (footstepStoneClips.Length == 0) return;

        int nextClipIndex;
        do
        {
            nextClipIndex = Random.Range(0, footstepStoneClips.Length);
        } while (nextClipIndex == lastClipIndex); // Avoid repeating the same sound

        lastClipIndex = nextClipIndex;
        audioSource.PlayOneShot(footstepStoneClips[nextClipIndex]);
    }
    public void PlayCarpetFootstep()
    {
        if (footstepCarpetClips.Length == 0) return;

        int nextClipIndex;
        do
        {
            nextClipIndex = Random.Range(0, footstepCarpetClips.Length);
        } while (nextClipIndex == lastClipIndex); // Avoid repeating the same sound

        lastClipIndex = nextClipIndex;
        audioSource.PlayOneShot(footstepCarpetClips[nextClipIndex]);
    }
}
