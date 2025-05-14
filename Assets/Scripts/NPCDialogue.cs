using UnityEngine;

[CreateAssetMenu(fileName = "newNPCDialogue", menuName = "NPC Dialogue")] 
public class NPCDialogue : ScriptableObject
{
    public string npcName; 
    public Sprite npcPortrait; 
    public string[] dialogueLines; 
    public bool[] endDialogueLines; 
    public float typingSpeed = 0.05f; 
    public bool[] autoProgressLines; 
    public float autoProgressDelay = 1.5f; 
    public DialogueChoice[] choices; 
    public bool[] nextSceneLines; 

    public bool[] healDialogueLines; 

}

[System.Serializable] 

public class DialogueChoice
{
    public int dialogueIndex; 
    public string[] choices; 
    public int[] nextDialogueIndexes; 
}
