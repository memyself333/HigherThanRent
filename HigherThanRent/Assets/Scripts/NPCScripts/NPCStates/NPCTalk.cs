using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    private Animator anim; // The NPC animator
    public Animator interactAnim; // The Interact Icon animator
    public DialogueSO dialogueSO; // What conversation this will have

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>(); // Accessing the animator, which is in a child object
    }

    private void OnEnable() // When the script is toggled on
    {
        anim.Play("Idle"); // Set NPC animation to idle while talking to NPC
        interactAnim.Play("Open"); 
    }

    private void OnDisable() // When the script is toggled off
    {
        interactAnim.Play("Close");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.AdvanceDialogue(); // Do I need to pass a scriptable obj to it??
            }
            else
            {
                DialogueManager.Instance.StartDialogue(dialogueSO);
            }
        }
    }

}
