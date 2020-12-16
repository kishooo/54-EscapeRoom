using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{

    public GameObject player;
    public InputField inputField;

    bool bFourTwentyCodeDone = false;
    // Start is called before the first frame update
    void Start()
    {
        bFourTwentyCodeDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bFourTwentyCodeDone && inputField.text == "4200")
        {
            GameObject Safe1 = GameObject.FindWithTag("Safe1");
            Safe1.GetComponent<Animator>().SetBool("Open", true);

            player.GetComponent<playerController>().DisableCodePanel();
            player.GetComponent<playerController>().AddKey();

            inputField.text = "";
            bFourTwentyCodeDone = true;

            // Play sound
            GameObject SuccessSFXMgr = GameObject.FindWithTag("SuccessSFXManager");
            SuccessSFXMgr.GetComponent<AudioSource>().Play();
        }
    }

 
}
