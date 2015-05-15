using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed = 10f;
    public float turnSpeed = 10f;
    public bool autoMoveForward = true;

    CharacterController controller;
    float currentDir = 0f; // compass indicating direction
    float vertSpeed = 0f; // vertical speed 
    Vector3 currentNormal = Vector3.up; // smoothed terrain normal

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        float turn = horz * turnSpeed * 100*Time.deltaTime;
        
        //currentDir = (currentDir + turn) % 360; // rotate angle modulo 360 according to input
        currentDir += turn; 
        currentDir = Mathf.Clamp(currentDir, -45, 45);
        currentDir %= 360;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -currentNormal, out hit))
        {
            currentNormal = Vector3.Lerp(currentNormal, hit.normal, 4*Time.deltaTime);
            Quaternion groundTilt = Quaternion.FromToRotation(Vector3.up, currentNormal);
            transform.rotation = groundTilt * Quaternion.Euler(0, currentDir, 0);
        }

        float vertInput;
        if (autoMoveForward)
        {
            vertInput = 1.0f;
        }
        else
        {
            vertInput = vert;
        }
        Vector3 movementDir = transform.forward * vertInput * speed;

        // moves the character in horizontal direction (gravity changed!)
        if (controller.isGrounded) vertSpeed = 0; // zero v speed when grounded
        vertSpeed += Physics.gravity.y * Time.deltaTime; // apply gravity
        
        movementDir.y = vertSpeed; // keep the current vert speed
        
        controller.Move(movementDir * Time.deltaTime);
    }

}