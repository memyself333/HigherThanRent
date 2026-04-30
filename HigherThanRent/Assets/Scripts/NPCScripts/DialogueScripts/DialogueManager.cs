using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // makes the script globally available to other scripts

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Animator borderAnim;
    public Animator nameAnim;
    public Animator portraitAnim;
    public Animator canvasAnim;





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
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void StartDialogue(DialogueSO dialogueSO) // Starts a new convo
    {
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        ShowDialogue();
    }

    public void AdvanceDialogue() // Moves an active convo one step forward
    {
        if(dialogueIndex < currentDialogue.lines.Length) // If there are more lines to show
        {
            ShowDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowDialogue() // Updating everything on the dialogue canvas
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        if (line.speaker.side == "Left")
        {
            borderAnim.Play("BorderToLeft");
            nameAnim.Play("NameToLeft");
            StartCoroutine(WaitForSecondsLeft());

        }
        else if (line.speaker.side == "Right")
        {
            borderAnim.Play("BorderToRight");
            nameAnim.Play("NameToRight");
            StartCoroutine(WaitForSecondsRight());   
        }



        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        canvasAnim.Play("DialogueFadeIn");
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex ++; // Incrementing each time a new line is shown.
    }

    IEnumerator WaitForSecondsLeft()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        dialogueText.alignment = TextAlignmentOptions.Left;
        portraitAnim.Play("PortraitToLeft");
        yield return new WaitForSeconds(0.2f);
        portrait.sprite = line.speaker.portrait;
        yield return new WaitForSeconds(0.2f);
        dialogueText.rectTransform.TransformVector(new Vector2(-140, -140));

    }

    IEnumerator WaitForSecondsRight()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        dialogueText.alignment = TextAlignmentOptions.Right;
        portraitAnim.Play("PortraitToRight");
        yield return new WaitForSeconds(0.2f);
        portrait.sprite = line.speaker.portrait;
        yield return new WaitForSeconds(0.2f);
        dialogueText.rectTransform.TransformVector(new Vector2(700, -140));

    }
    private void EndDialogue()
    {
        dialogueIndex = 0;

        canvasAnim.Play("DialogueFadeOut");
        canvasGroup.alpha = 0;
        isDialogueActive = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
