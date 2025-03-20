using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody rb;

    public float stims;
    public float grenades;
    public float jetpackForce = 2;
    public float jumpForce = 15f;
    public float MaxHeatAmount = 10;
    public float heatcountdown;
    public float movementSpeed = 1;
    public bool jetpackToggle;
    public bool isGrounded;
    public float RotationSpeed = 15f;
    public float Speed = 5f;
    public Vector3 move;
    public GameObject Gun2;
    public GameObject pov;
    public Slider heatsliderObject;
    public Slider healthsliderObject;
    private TMP_Text debugHeat;
    public float Damage;
    public bool heatGain;


    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        Gun2.SetActive(true);
        rb = GetComponent<Rigidbody>();
        debugHeat = GameObject.Find("debugHeat").GetComponent<TMP_Text>();
        Cursor.lockState = CursorLockMode.Locked;           
        heatcountdown = 10;
        heatsliderObject.value = heatcountdown;
        Damage = 10;
    }
    
    // Update is called once per frame
    void Update()
    {

  

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            pov.GetComponent<Camera>().fieldOfView = 40;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            pov.GetComponent<Camera>().fieldOfView = 60;
        }

        if (Input.GetKeyDown("c"))
        {
            gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }

        if (Input.GetKeyUp("c"))
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKeyDown("z"))
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        if (Input.GetKeyUp("z"))
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        /*if(Input.GetKeyDown("g") && grenades >= 4)
        {

        }*/

        if (Input.GetKeyDown("4") && stims >= 1)
        {
            GetComponent<Health>().health += 10;
            stims -= 1;
        }

        string jetpackNum = string.Format("{0:0.00}", heatcountdown);
        debugHeat.text = $"The Current heat is " + jetpackNum;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 rotation = new Vector3(0, horizontal * Time.deltaTime, 0);


        Vector3 rot = pov.transform.eulerAngles += new Vector3 (Input.GetAxis("Mouse Y") * Time.deltaTime *- RotationSpeed, Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeed, 0) ;
        rot.x = ClampAngle(rot.x, -60f, 60f);
        
        //rotate camera only
        pov.transform.eulerAngles = rot;


        Vector3 rot2 = rot;
        rot2.x = 0;
        rot2.z = 0;
        transform.eulerAngles = rot2;
        // transform.forward = pov.transform.forward;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 3f, ground);
        Debug.DrawRay(transform.position, Vector3.down * .15f, Color.red);
        pov.transform.position = transform.position;

        if (jetpackToggle == false )
        {
            if (Input.GetButtonDown("Jump") && isGrounded && rb.velocity.y == 0)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if(Input.GetKey("w"))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey("a"))
            {
                transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);

            }

            if (Input.GetKey("s"))
            {
                transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey("d"))
            {
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            }
        }
        else
        {

            move = (pov.transform.forward * Speed * vertical * Time.deltaTime) + (pov.transform.right * Speed * horizontal * Time.deltaTime);
            move = new Vector3(move.x, 0, move.z);
            rb.AddForce(move, ForceMode.Acceleration);

            if (Input.GetButton("Jump") && heatcountdown > 0)
            {
                rb.AddForce(Vector3.up * jetpackForce, ForceMode.Acceleration);
               heatcountdown -= 2 * Time.deltaTime;


                heatcountdown = Mathf.Clamp(heatcountdown, 0,10);
                heatsliderObject.value = heatcountdown;
                heatGain = false;
            }


            if (!Input.GetButton("Jump"))
            {
                heatGain = true;
            }

            if (heatGain == true)
            {
                heatcountdown +=  1 * Time.deltaTime;
                heatcountdown = Mathf.Clamp(heatcountdown,0, 10);
                heatsliderObject.value = heatcountdown;
            }

        }

        //Debug.Log(heatcountdown);
        if (Input.GetKeyDown(KeyCode.V) && heatcountdown > 0)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().hurtPlayer(10);
            Damage -= 10 * Time.deltaTime;
            healthsliderObject.value = Damage;
        }
    }
    private void OnTriggerEnter(Collider collided)
    {
        if (collided.gameObject.name == "trigger box")
        {
            stims = 4;
        }
    }

    void overheat()
    {
        heatcountdown = MaxHeatAmount;
    }
}
