using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;
    public Vector2 newPlayerPosition;
    private Transform player;
    public string currentScene;
    public AudioSource audioSource;
    public AudioClip[] audioClips;


    // Function to check when player collides with the trigger, if so, trigger fade animation and start coroutine to load next scene     
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentScene = SceneManager.GetActiveScene().name;
            player = other.transform;
            fadeAnim.Play("FadeToBlack");
            StartCoroutine (DelayFade());
        }
    }
    // Coroutine to wait for fade animation to finish before loading next scene and moving player to new position in next scene
    IEnumerator DelayFade()
    {
        if (currentScene == "MainStaircase1F")
        {
            if (sceneToLoad == "MainStaircase2F")
            {
                audioSource.PlayOneShot(audioClips[1]);
            }
            else
            {
                audioSource.PlayOneShot(audioClips[0]);
            }
        }
        else if (currentScene == "MainStaircase2F")
        {
            if (sceneToLoad == "MainStaircase1F")
            {
                audioSource.PlayOneShot(audioClips[1]);
            }
            else
            {
                audioSource.PlayOneShot(audioClips[0]);
            }
        }
        else if (currentScene == "WarningRoom")
        {
            if (sceneToLoad == "BossRoom")
            {
                audioSource.PlayOneShot(audioClips[1]);
            }
            else
            {
                audioSource.PlayOneShot(audioClips[0]);
            }
        }
        else if (currentScene == "BossRoom")
        {
            if (sceneToLoad == "WarningRoom")
            {
                audioSource.PlayOneShot(audioClips[1]);
            }
            else
            {
                audioSource.PlayOneShot(audioClips[0]);
            }
        }
        else
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
            yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
        player.position = newPlayerPosition;
    }
}
