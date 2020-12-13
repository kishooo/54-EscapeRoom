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

    int keys = 0;

    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        HandleCheats();

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ExitDoor"))
        {
            if(keys >= 3)
            {
                other.gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
            }
        }
    }
}
