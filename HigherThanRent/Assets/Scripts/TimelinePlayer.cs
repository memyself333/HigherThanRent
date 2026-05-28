using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    private BoxCollider2D boxCollider2D;



    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            director = GameObject.Find("DCommonRoom1").GetComponent<PlayableDirector>();
            boxCollider2D = GameObject.Find("CCommonRoom1").GetComponent<BoxCollider2D>();
        }
        else if (SceneManager.GetActiveScene().name == "Hallway")
        {
            director = GameObject.Find("DHallway1").GetComponent<PlayableDirector>();
            boxCollider2D = GameObject.Find("CHallway1").GetComponent<BoxCollider2D>();
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartTimeline();
            this.gameObject.SetActive(false);
        }
    }
    public void StartTimeline()
    {
        director.Play();
    }
}