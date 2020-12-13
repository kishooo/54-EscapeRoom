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

    public int keys = 0;
    bool bInteractionKeyPressed = false;
    void Start()
    {
        keys = 0;
        bInteractionKeyPressed = false;
    }


    // Update is called once per frame
    void Update()
    {
        HandleInteraction();
        HandleMovement();
        HandleCheats();

    }

    void HandleInteraction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            bInteractionKeyPressed = true;
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            bInteractionKeyPressed = false;
        }
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

    void HandleCheats()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            AddKey();
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
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        HideInteractionKey();   

        if (other.gameObject.CompareTag("ExitDoor"))
        {
            ShowInteractionKey();
            if (keys >= 3 && Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
            }
        }

        if (other.gameObject.CompareTag("KeyDoor"))
        {
            ShowInteractionKey();
            if (keys >= 1 && Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<Animator>().SetBool("Open", true);
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
    }
}
