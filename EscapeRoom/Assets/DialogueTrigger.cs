using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Animator animator;
    public void TriggerDialogue()
    {
        
        
    }
    public void Update()
    {
        if (playerController.DoorFlag == true)
        {
            animator.SetBool("open", true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentance();
            }
        }
        
        
    }
}
