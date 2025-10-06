using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviour
{

    private TMP_Text debugHeat;

    private Rigidbody rb;

    private float targetFOV;
    private float targetScopeOpacity;

    public float ScopeFov = 40;
    public float DefaultFov = 60;
    public float stims;
    public float jetpackForce = 2;
    public float jumpForce = 15f;
    public float MaxHeatAmount = 10;
    public float heatcountdown;
    public float MovementSpeed = 0.25f;
    public float SideMovementSpeed = 0.15f;
    public float RotationSpeed = 10f;   
    public float Speed = 5f;
    public float FOVChangeSpeed = 5;
    public float ScopechangeSpeed = 10;  
    public float Damage;
    public float recoil;
    public Vector3 camRotation;

    public bool jetpackToggle;
    public bool isGrounded;
    public bool heatGain;

    public Vector3 move;

    public GameObject Gun2;
    public GameObject pov;
    public GameObject scope;

    public Slider heatsliderObject;
    public Slider healthsliderObject;
   


    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        Gun2.SetActive(true);
        rb = GetComponent<Rigidbody>();
        debugHeat = GameObject.Find("debugHeat").GetComponent<TMP_Text>();
        Cursor.lockState = CursorLockMode.Locked;

        heatcountdown = 10;
        heatsliderObject.value = heatcountdown;
        Damage = 10;
        targetFOV = 60;
        targetScopeOpacity = 0;
        Gun2.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            targetScopeOpacity = 1;
            targetFOV = ScopeFov;
            Gun2.transform.localScale = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            targetScopeOpacity = 0;
            targetFOV = DefaultFov;
            Gun2.transform.localScale = new Vector3(1, 1, 1);   
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



        if (Input.GetKeyDown("4") && stims >= 1)
        {
            GetComponent<Health>().health += 10;
            stims -= 1;
        }

        if (jetpackToggle == false)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        }
        if (Input.GetKeyDown(KeyCode.V) && heatcountdown > 0)
        {

           // jetpackToggle = !jetpackToggle;
            if (jetpackToggle == true)
            {

                jetpackToggle = false;
            }
            else
            {

                jetpackToggle = true;
            }
        }
        camRotation += new Vector3(Input.GetAxis("Mouse Y") * -RotationSpeed, Input.GetAxis("Mouse X") * RotationSpeed, 0);
        camRotation.x = ClampAngle(camRotation.x, -90f, 90f);
        //rotate camera only
        pov.transform.eulerAngles = camRotation - new Vector3(recoil, 0, 0);
    }
    void FixedUpdate()
    {
        string jetpackNum = string.Format("{0:0.00}", heatcountdown);

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //transform.eulerAngles = rot2;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.5f, ground);
        Debug.DrawRay(transform.position, Vector3.down * .15f, Color.red);
        pov.transform.position = transform.position;

       
        if (jetpackToggle == true)
        {

            move = (pov.transform.forward * Speed * vertical) + (pov.transform.right * Speed * horizontal);
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

        }else
        {
            move = (pov.transform.forward * MovementSpeed* vertical) + (pov.transform.right * SideMovementSpeed * horizontal);
            move = new Vector3(move.x, 0, move.z);
            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
        }

        //Debug.Log(heatcountdown);
     

        pov.GetComponent<Camera>().fieldOfView = Mathf.Lerp(pov.GetComponent<Camera>().fieldOfView, targetFOV, Time.deltaTime * FOVChangeSpeed);
        Color ScopeColour = scope.GetComponent<Image>().color;
        ScopeColour.a = Mathf.Lerp(ScopeColour.a, targetScopeOpacity, Time.deltaTime * ScopechangeSpeed);

        scope.GetComponent<Image>().color = ScopeColour;
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
