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
    public Animator dialogueAnim;





    public bool isDialogueActive;

    private DialogueSO currentDialogue; // Keeping track of what conversation/dialogue we're in.
    private int dialogueIndex; // Keeps track of what line of the dialogue we're in.

    public bool isTutorialActive; // Checking if the tutorial is completed

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
            NextDialogue();
        }
    }

    private void NextDialogue()
    {
        if(currentDialogue.nextconvo.Length > 0) // Checking if there are followup conversations
        {
            if(currentDialogue.name == "Tutorial Intro")
            {
                isTutorialActive = true; 
                EndDialogue();

                var next = currentDialogue.nextconvo[0];
                Debug.Log("Checked that the current dialogue is Tutorial Intro");

                   StartDialogue(next.nextDialogue); 
 
            }
            else if(currentDialogue.name == "Tutorial Attack")
            {
                EndDialogue();

                var next = currentDialogue.nextconvo[0];
                Debug.Log("Checked that the current dialogue is Tutorial Attack");


                   StartDialogue(next.nextDialogue);

            }
            else if(currentDialogue.name == "Tutorial Interact")
            {   
                isTutorialActive = false;
                EndDialogue();

            }
        }
        else
        {
            EndDialogue();
        }
    }

//    private void TutorialActionComplete(DialogueSO dialogueSO) // if they do the step of the tutorial necessary
//    {
//        if(dialogueSO == null)
//        {
//            EndDialogue();
//        }
//        else
//        {
//            StartDialogue(dialogueSO);
//        }
//    }

    private void ShowDialogue() // Updating everything on the dialogue canvas
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        if (line.speaker.side == "Left")
        {
            borderAnim.Play("BorderToLeft");
            nameAnim.Play("NameToLeft");
            dialogueAnim.Play("DialogueToLeft");
            StartCoroutine(WaitForSecondsLeft());
            dialogueText.alignment = TextAlignmentOptions.TopLeft;

        }
        else if (line.speaker.side == "Right")
        {
            borderAnim.Play("BorderToRight");
            nameAnim.Play("NameToRight");
            dialogueAnim.Play("DialogueToRight");
            StartCoroutine(WaitForSecondsRight());
            dialogueText.alignment = TextAlignmentOptions.TopRight;
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

        
        portraitAnim.Play("PortraitToLeft");
        yield return new WaitForSeconds(0.2f);
        portrait.sprite = line.speaker.portrait;
        yield return new WaitForSeconds(0.2f);
      

    }

    IEnumerator WaitForSecondsRight()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];


        portraitAnim.Play("PortraitToRight");
        yield return new WaitForSeconds(0.2f);
        portrait.sprite = line.speaker.portrait;
        yield return new WaitForSeconds(0.2f);
        

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
