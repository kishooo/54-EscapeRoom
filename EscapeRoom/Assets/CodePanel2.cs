using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel2 : MonoBehaviour
{
    public static bool solved = false;
    public GameObject player;
    public InputField inputField;
    public GameObject Dialogue;

    bool bFourTwentyCodeDone = false;
    // Start is called before the first frame update
    void Start()
    {
        bFourTwentyCodeDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bFourTwentyCodeDone && inputField.text == "042")
        {
            solved = true;
            GameObject secondDoor = GameObject.FindWithTag("SecondDoor");
            //Safe1.GetComponent<Animator>().SetBool("Open", true);

            player.GetComponent<playerController>().DisableCodePanel2();
            player.GetComponent<playerController>().AddKey();
            playerController.secDoorFlag = true;
            Debug.Log("door opened");
            player.GetComponent<playerController>().PlayTaskCelebrationAnimation();
            player.GetComponent<playerController>().DisableCodePanel2();
            playerController.DoorFlag = false;
            Dialogue.GetComponent<DialogueManager>().EndDialogue();
            //playerController.PlayTaskCelebrationAnimation();

            inputField.text = "";
            bFourTwentyCodeDone = true;

            // Play sound
            GameObject SuccessSFXMgr = GameObject.FindWithTag("SuccessSFXManager");
            SuccessSFXMgr.GetComponent<AudioSource>().Play();
        }
    }


}

