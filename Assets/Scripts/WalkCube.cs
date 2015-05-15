using UnityEngine;
using System.Collections;

public class WalkCube : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5f;
    public float gravitySpeed = 300f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(horz, 0, vert);
        input = transform.TransformVector(input);
        
        transform.Translate(input * speed * Time.deltaTime);
        CubeGravity();
        //transform.rotation = Quaternion.identity;
    }
    
    void CubeGravity()
    {
        float distForward = Mathf.Infinity;
        RaycastHit hitForward;
        if (Physics.SphereCast(transform.position - transform.up, 0.25f, -transform.up + transform.forward, out hitForward, 5))
        {
            distForward = hitForward.distance;
        }
        float distDown = Mathf.Infinity;
        RaycastHit hitDown;
        if (Physics.SphereCast(transform.position - transform.up, 0.25f, -transform.up, out hitDown, 5))
        {
            distDown = hitDown.distance;
        }
        float distBack = Mathf.Infinity;
        RaycastHit hitBack;
        if (Physics.SphereCast(transform.position - transform.up, 0.25f, -transform.up + -transform.forward, out hitBack, 5))
        {
            distBack = hitBack.distance;
        }

        if (distForward < distDown && distForward < distBack)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitForward.normal), hitForward.normal), Time.deltaTime * 5.0f);
        }
        else if (distDown < distForward && distDown < distBack)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitDown.normal), hitDown.normal), Time.deltaTime * 5.0f);
        }
        else if (distBack < distForward && distBack < distDown)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitBack.normal), hitBack.normal), Time.deltaTime * 5.0f);
        }

        rb.AddForce(-transform.up * Time.deltaTime * gravitySpeed);
    }
}
