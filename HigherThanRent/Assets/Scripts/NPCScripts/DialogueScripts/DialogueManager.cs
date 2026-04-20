using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // makes the script globally available to other scripts

    [Header("UI References")]
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;

    public bool isDialogueActive;

    private DialogueSO currentDialogue; // Keeping track of what conversation/dialogue we're in.
    private int dialogueIndex; // Keeps track of what line of the dialogue we're in.

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensuring there is only ever ONE version of this script in the game
        }
    }

    public void StartDialogue(DialogueSO dialogueSO) // Starts a new convo
    {
        currentDialogue = dialogueSO;
        ShowDialogue();
    }

    public void AdvanceDialogue() // Moves an active convo one step forward
    {
        if(dialogueIndex < currentDialogue.lines.Length) // If there are more lines to show
        {
            ShowDialogue();
        }
    }

    private void ShowDialogue() // Updating everything on the dialogue canvas
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        portrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        dialogueIndex ++; // Incrementing each time a new line is shown.
    }
}
