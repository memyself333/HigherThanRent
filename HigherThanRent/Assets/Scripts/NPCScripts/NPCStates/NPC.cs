using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;

public class NPC : MonoBehaviour
{
    // This script is for toggling the other NPC script on and off, a state machine of sorts
    public enum NPCState {Default, Idle, Talk}
    public NPCState currentState = NPCState.Default; // Tracks current state 
    private NPCState defaultState;

    public NPCTalk talk; // Reference to the Talk script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultState = currentState; // What state is applied when the game starts 
        SwitchState(currentState);
    }

    public void SwitchState(NPCState newState) // Function to load new states and unload the old ones
    {
        currentState = newState;

        talk.enabled = newState == NPCState.Talk; // Checking if the state passed in was the Talk state
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Player")) // Checking if player collides with the NPC
        {
            SwitchState(NPCState.Talk);
        }
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SwitchState(defaultState);
        }
    }

}
