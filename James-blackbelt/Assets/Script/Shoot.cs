using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public PlayerMove playerMove;

    public GameObject Hit;
    public GameObject Gun;
    public GameObject bullet;
    public GameObject barrel;
    public GameObject CameraRotation;
    public GameObject Flash;
    public bool flash;
    public float range = 100;
    public float bulletSpeed;
    public float recoil;

    float previousShot = 0;
    float firerate = 0.1f;
    private RaycastHit hit;

    public Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        flash = Flash;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red, range);
        if (Input.GetMouseButton(0) && Gun.activeInHierarchy)
        {
            RaycastHit hit;

            if (flash == true)
            {
                flash = false;
            }
            else
            {
                flash = false;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Time.time - previousShot > firerate)
            {
                //to do recoil
                playerMove.recoil = recoil;

                m_Animator.SetTrigger("shoot");
                //LayerMask mask = LayerMask.GetMask("enemy");
                if (Physics.Raycast(ray, out hit, range))
                {

                    /*hit.collider.GetComponent<enemie>().hurtPlayer(15);*/
                    GameObject partical = Instantiate(Hit, hit.point, transform.rotation);
                    Destroy(partical,1);

                    IDamageable HitObj = hit.collider.GetComponent<IDamageable>();
                    if (HitObj != null)
                    {
                        HitObj.Damage(15);
                    }
                }
                previousShot = Time.time;
            }  
        }
        playerMove.recoil -= Time.deltaTime*5;
        if (playerMove.recoil < 0)
        {
            playerMove.recoil = 0;
        }
        
        if (Input.GetKeyDown("r"))
        {
            m_Animator.SetTrigger("reload");
        }
    }
}

