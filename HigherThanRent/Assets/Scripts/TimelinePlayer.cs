using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        director = GameObject.Find("Director").GetComponent<PlayableDirector>();
        boxCollider2D = GameObject.Find("Trigger").GetComponent<BoxCollider2D>();
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