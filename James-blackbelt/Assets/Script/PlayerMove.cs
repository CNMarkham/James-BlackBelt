using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody rb;

    public float jumpForce = 15f;
    public bool isGrounded;
    public float RotationSpeed = 15f;
    public float Speed = 5f;
    public Vector3 move;
    public GameObject pov;

    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 rotation = new Vector3(0, horizontal * Time.deltaTime, 0);
        move = pov.transform.forward * Speed * Time.deltaTime * vertical;
        rb.AddForce(move, ForceMode.VelocityChange);

        move = pov.transform.right * Speed * Time.deltaTime * horizontal;
        rb.AddForce(move, ForceMode.VelocityChange);

            pov.transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeed, 0);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, ground);
        Debug.DrawRay(transform.position, Vector3.down * .15f, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
}
