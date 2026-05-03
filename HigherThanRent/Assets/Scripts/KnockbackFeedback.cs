using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d; // reference to what rigidbody the force will be applied to

    [SerializeField]
    private float strength = 16, delay = 0.15f; // how powerful it is, the delay after which you can move again

    public UnityEvent OnBegin, OnDone; 

    public void PlayFeedback(GameObject sender) // what is knocking this object back
    {
        StopAllCoroutines(); 
        OnBegin?.Invoke(); // in case we apply additional logic when we start knockback feedback

        Vector2 direction = (transform.position - sender.transform.position).normalized; // the direction from the sender
        rb2d.AddForce(direction*strength, ForceMode2D.Impulse); // the actual knockback

        StartCoroutine(Reset());
    }

    private IEnumerator Reset() // after the delay the knockback will stop working on the rigidbody
    {
        yield return new WaitForSeconds(delay);

        rb2d.linearVelocityX = 0;
        rb2d.linearVelocityY = 0; 

        OnDone?.Invoke(); // in the event of applying additional logic after the knockback feedback is finished
    }
}
