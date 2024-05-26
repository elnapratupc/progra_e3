using UnityEngine;
using TMPro; 

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        dialoguePanel.SetActive(false); 
    }

    public void StartDialogue(string message)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = message;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
