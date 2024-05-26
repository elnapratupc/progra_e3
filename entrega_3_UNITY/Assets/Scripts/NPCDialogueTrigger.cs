using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Colisión con el jugador detectada"); 
            dialogueManager.StartDialogue("HOLA, SOY UN NPC ENCANTADO");
        }
    }
}
