using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieTorret : MonoBehaviour
{
    Animator myAnimator;
    bool activo = true;
    float nextFire;
    float fireRate = 1;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip sfx_enemieDeath;
    public BoxCollider2D coll;
    [SerializeField] GameManager gm;
    // Start is called before the first frame update
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activo == true)
        {
            if (player.transform.position.x <= transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
        Collider2D chocando = Physics2D.OverlapCircle(transform.position, 10, LayerMask.GetMask("Player"));
        if (chocando && activo == true)
        {
            Shoot();
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colision = collision.gameObject;
        if (colision.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(sfx_enemieDeath, Camera.main.transform.position);
            myAnimator.SetTrigger("destroy");
            activo = false;
            gm.Restar();
            coll.enabled = false;
        }
    }
    void Shoot()
    {
        float direccion = transform.localScale.x;
        if(Time.time > nextFire)
        {
            GameObject bulletT = Instantiate(bullet, firePoint.position, transform.rotation) as GameObject;
            BulletT bulletC = bullet.GetComponent<BulletT>();
            bulletC.direction = direccion;
            nextFire = Time.time + fireRate;
        }
        
    }
}
