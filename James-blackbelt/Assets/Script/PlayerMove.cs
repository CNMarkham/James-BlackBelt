using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody rb;

    public float jetpackForce = 2;
    public float jumpForce = 15f;
    public float MaxHeatAmount;
    public float heatcountdown;
    public bool jetpackToggle;
    public bool isGrounded;
    public float RotationSpeed = 15f;
    public float Speed = 5f;
    public Vector3 move;
    public GameObject pov;
    public Slider sliderObject;
    private TMP_Text debugHeat;
    
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        debugHeat = GameObject.Find("debugHeat").GetComponent<TMP_Text>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
      
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 rotation = new Vector3(0, horizontal * Time.deltaTime, 0);
        move = (pov.transform.forward * Speed  * vertical * Time.deltaTime) + (pov.transform.right * Speed  * horizontal * Time.deltaTime);
        rb.AddForce(move, ForceMode.Acceleration);

        Vector3 rot = pov.transform.eulerAngles += new Vector3 (Input.GetAxis("Mouse Y") * Time.deltaTime *- RotationSpeed, Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeed, 0) ;
        rot.x = ClampAngle(rot.x, -60f, 60f);

        pov.transform.eulerAngles = rot;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, ground);
        Debug.DrawRay(transform.position, Vector3.down * .15f, Color.red);

        if (jetpackToggle == false)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(Vector3.up * jetpackForce, ForceMode.Acceleration);
                heatcountdown -= 1 * Time.deltaTime;
                debugHeat.text = $"The Current heat is {heatcountdown}";
                sliderObject.value = heatcountdown;

                
                if (heatcountdown <= 0)
                {
                    jetpackToggle = false;

                    Invoke("overheat", 2.0f);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && heatcountdown > 0)
        {
            if (jetpackToggle == true)
            {
                jetpackToggle = false;
            }
            else
            {
                jetpackToggle = true;
            }
        }
    }
    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    void overheat()
    {
        heatcountdown = MaxHeatAmount;
    }
}
