using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public PlayerMove playerMove;

    public GameObject Hit;
    public GameObject muzzleFlash;
    public GameObject Gun;
    public GameObject bullet;
    public GameObject barrel;
    public GameObject CameraRotation;
    public GameObject Flash;

    public bool flash;
    public bool canShoot;

    public float flashRotation;
    public float range = 100;
    public float bulletSpeed;
    public float recoil;
    public float reloadTime = 1.5f;
    public float reloadTimer;

    public int bullets;

    float previousShot = 0;
    float firerate = 0.1f;
    private RaycastHit hit;

    public Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        flash = Flash;
        bullets = 30;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot == false)
        {
            reloadTimer -= Time.deltaTime;


            if (reloadTimer <= 0)
            {
                bullets = 30;
                canShoot = true;
            }
        }


        Debug.DrawRay(transform.position, transform.forward, Color.red, range);
        if (Input.GetMouseButton(0) && Gun.activeInHierarchy && bullets > 0)
        {
            if (Time.time - previousShot > firerate)
            {
                GameObject MuzzleFlash = Instantiate(muzzleFlash, barrel.transform.position, Gun.transform.rotation);
                MuzzleFlash.transform.SetParent(barrel.transform);
                MuzzleFlash.transform.Rotate(0, 0, Random.Range(-90, 90));
                Destroy(MuzzleFlash, 0.1f);
                //to do recoil
                playerMove.recoil = recoil;
                bullets -= 1; print(bullets);

                if (flash == true)
                {
                    flash = false;
                }
                else
                {
                    flash = false;
                }

                m_Animator.SetTrigger("shoot");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, range))
                {
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
            canShoot = false;
            reloadTimer = reloadTime;
        }
    }
}   

