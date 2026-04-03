using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;

    // Function to check when player collides with the trigger, if so, trigger fade animation and start coroutine to load next scene  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeAnim.Play("FadeToBlack");
            StartCoroutine (DelayFade());
        }
    }
    // Coroutine to wait for fade animation to finish before loading next scene
    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
