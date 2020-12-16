using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    private Queue<string> sentances;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();
        if (playerController.DoorFlag == true)
        {
            animator.SetBool("open", true);
        }
        //animator.SetBool("CloseFirst", false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (playerController.DoorFlag == true)
        {
            animator.SetBool("open", true);
            //animator.SetBool("CloseFirst", true);
            //Debug.Log("First Sentance" + dialogue.name);
            sentances.Clear();

            foreach (string sentance in dialogue.sentances)
            {
                sentances.Enqueue(sentance);
            }

            DisplayNextSentance();
        }
    }
    public void DisplayNextSentance()
    {
        if (sentances.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentances.Dequeue();
        //Debug.Log(sentence);
        dialogueText.text = sentence;
    }
    public void EndDialogue()
    {
        animator.SetBool("open", false);
        //Debug.Log("the End");
    }
}
