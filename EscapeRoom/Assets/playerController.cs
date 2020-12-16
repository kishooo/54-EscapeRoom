using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public float  speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public Transform cam;
    public static bool DoorFlag = false;
    public static bool secDoorFlag = false;
    public GameObject selfTable;
    public GameObject riddle1;
    public GameObject pressE;
    public AudioSource audio;
    public AudioClip intense;


    public GameObject CodePaneee;
    public GameObject CodePanel2;
    public int keys = 0;

    float OriginalYPos;

    // Celebration
    float TimeToStopAnim = 0;
    void Start()
    {
        keys = 0;
        CodePaneee.SetActive(false);
        CodePanel2.SetActive(false);
        audio.Play();

        OriginalYPos = transform.position.y;
    }


    // Update is called once per frame
    void Update()
    {
        if (done){
            pressE.SetActive(false);
        }
        HandleMovement();
        HandleCheats();

        if(transform.position.y >= OriginalYPos+0.5f)
        {
            transform.position = new Vector3(transform.position.x, OriginalYPos, transform.position.z);
        }

       if(Input.GetKeyDown(KeyCode.T))
       {
            DisableCodePanel();
            DisableCodePanel2();
       }

        // Stopping celebration animation
        TimeToStopAnim += Time.deltaTime;
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    public void rightAnswer()
    {
        AddKey();
    }
    void HandleCheats()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            AddKey();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayTaskCelebrationAnimation();
        }
    }

    public void AddKey()
    {
        keys++;

        if(keys >= 3)
        {
            keys = 3;
        }
    }

    public void RemoveKey()
    {
        keys--;

        if(keys<=0)
        {
            keys = 0;
        }
    }

    void ShowInteractionKey()
    {

    }

    void HideInteractionKey()
    {

    }
   
    
    public void EnableCodePanel()
    {
        CodePaneee.SetActive(true);
    }

    public void EnableCodePanel2()
    {
        CodePanel2.SetActive(true);
    }

    public void DisableCodePanel()
    {
        CodePaneee.SetActive(false);
    }

    public void DisableCodePanel2()
    {
        CodePanel2.SetActive(false);
    }

    public void doorFlagTrue(){
        DoorFlag = true;
    }
    public void playSound(){
        GameObject SuccessSFXMgr = GameObject.FindWithTag("SuccessSFXManager");
        SuccessSFXMgr.GetComponent<AudioSource>().Play();
    }
    bool done=false;
    public void taskDone(){
        done =true;
    }

    
    private void OnTriggerStay(Collider other)
    {
        HideInteractionKey();   
        if (other.gameObject.CompareTag("Table")){
            Debug.Log("triggeredd");
            pressE.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)){
                if (!done){
                    pressE.SetActive(false);
                    riddle1.SetActive(true);
                    done=true;
                }
            }
        }
        if (other.gameObject.CompareTag("bath") )
        {
           if (keys>=1 && Input.GetKey(KeyCode.E)){
                other.gameObject.GetComponent<Animator>().SetBool("openbath", true);
                DoorFlag=true;
                playSound();
                PlayTaskCelebrationAnimation();
           }
        }
        if (other.gameObject.CompareTag("ExitDoor"))
        {
            ShowInteractionKey();
            if (keys >= 2 && Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
                playSound();
                GetComponentInChildren<Animator>().SetBool("finish", true);

            }
        }

        if (other.gameObject.CompareTag("KeyDoor"))
        {
            ShowInteractionKey();
            if (keys >= 1 && Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<Animator>().SetBool("Open", true);
                RemoveKey();
                PlayTaskCelebrationAnimation();
                TimeToStopAnim = 0;
                //DoorFlag = true;
            }
            if(keys == 0 && Input.GetKey(KeyCode.E))
            {
                if(TimeToStopAnim>0.5)
                  PlayAngryAnimation();
            }
        }

        if (other.gameObject.CompareTag("NoKeyDoor"))
        {
            ShowInteractionKey();
            if (Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<Animator>().SetBool("Open", true);
            }
        }

        if (other.gameObject.CompareTag("Safe1"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                EnableCodePanel();
            }
        }
        if (other.gameObject.CompareTag("SecondDoor") && secDoorFlag == false)
        {
            
            //Debug.Log("door opened");
            if (Input.GetKey(KeyCode.E))
            {
                EnableCodePanel2();
            }
        }
    }

    public void PlayTaskCelebrationAnimation()
    {
        GetComponentInChildren<Animator>().SetTrigger("CelebrateTask");
        GameObject SuccessSFXMgr = GameObject.FindWithTag("SuccessSFXManager");
        SuccessSFXMgr.GetComponent<AudioSource>().Play();
    }
    public void PlayAngryAnimation()
    {
        GetComponentInChildren<Animator>().SetTrigger("AngryTask");
    }
}
