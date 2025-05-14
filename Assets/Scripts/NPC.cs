using UnityEngine;
using System.Collections; 
using UnityEngine.UI; 
using TMPro; 

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData; 
    private DialogueController dialogueUI; 
    private int dialogueIndex; 
    private bool isTyping, isDialogueActive; 
    public GameObject nextSceneButton; 
    public PlayerHealth playerHealth; 

    private void Start()
    {
        dialogueUI = DialogueController.Instance; 
    }

    public bool CanInteract() 
    { 
        return !isDialogueActive; 
    }

    public void Interact()
    {
        if(dialogueData == null || (PauseController.isGamePaused && !isDialogueActive))
            return; 

        if(isDialogueActive) 
        {
            if(dialogueData.healDialogueLines.Length > dialogueIndex && dialogueData.healDialogueLines[dialogueIndex])
            {
            playerHealth.RegainHealth(); 
            return; 
            }
            NextLine(); 
        }
        else 
        {
            StartDialogue();
        }
    }

    void StartDialogue() 
    { 
        isDialogueActive = true; 
        dialogueIndex = 0; 

        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait); 
        dialogueUI.ShowDialogueUI(true); 

        PauseController.SetPause(true); 

        DisplayCurrentLine(); 
    }

    void NextLine()
    {
        if (isTyping) 
        {
            StopAllCoroutines(); 
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]); 
            isTyping = false; 
        }

        dialogueUI.ClearChoices(); 

        if(dialogueData.nextSceneLines.Length > dialogueIndex && dialogueData.nextSceneLines[dialogueIndex])
        {
            nextSceneButton.SetActive(true); 
            return; 
        }

        if(dialogueData.healDialogueLines.Length > dialogueIndex && dialogueData.healDialogueLines[dialogueIndex])
        {
            playerHealth.RegainHealth(); 
            return; 
        }

        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue(); 
            return; 
        }

        if(++dialogueIndex < dialogueData.dialogueLines.Length) 
        {
            DisplayCurrentLine(); 
        }
        else 
        {
            EndDialogue(); 
        }
    }

    IEnumerator TypeLine() 
    {
        isTyping = true; 
        dialogueUI.SetDialogueText(""); 

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed); 
        }

        isTyping = false; 

        foreach(DialogueChoice dialogueChoice in dialogueData.choices) 
        { 
            if(dialogueChoice.dialogueIndex == dialogueIndex) 
            { 
                DisplayChoices(dialogueChoice);
                yield break; 
            }
        }

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay); 
            NextLine(); 
        }
    }

    void DisplayChoices(DialogueChoice choice) 
    {
        for(int i = 0; i < choice.choices.Length; i++) 
        {
            int nextIndex = choice.nextDialogueIndexes[i]; 
            dialogueUI.CreateChoiceButton(choice.choices[i],() => chooseOption(nextIndex));
        }
    }

    void chooseOption(int nextIndex) 
    { 
        dialogueIndex = nextIndex; 
        dialogueUI.ClearChoices();
        DisplayCurrentLine(); 
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines(); 
        StartCoroutine(TypeLine()); 
    }

    public void EndDialogue() 
    {
        StopAllCoroutines(); 
        isDialogueActive = false; 
        dialogueUI.SetDialogueText(""); 
        dialogueUI.ShowDialogueUI(false); 
        PauseController.SetPause(false); 
    }
}
