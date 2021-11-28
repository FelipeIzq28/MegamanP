using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieTorret2 : MonoBehaviour
{
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject bullet1;
    [SerializeField] GameObject bullet2;
    [SerializeField] AudioClip sfx_enemieDeath;
    [SerializeField] GameManager gm;
    float nextFire;
    float fireRate = 2;
    bool active = true;
    Animator myAnimator;
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
        Collider2D chocando = Physics2D.OverlapCircle(transform.position, 14, LayerMask.GetMask("Player"));
        if(chocando && active == true)
        {
            Shoot();
        }
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 14);
    }
    void Shoot()
    {
        if (Time.time > nextFire)
        {
            myAnimator.SetBool("disparo",true);
            GameObject bulletF1 = Instantiate(bullet1, firePoint1.position, transform.rotation) as GameObject;
            BulletF1 bulletC = bullet1.GetComponent<BulletF1>();
            GameObject bulletF2 = Instantiate(bullet2, firePoint2.position, transform.rotation) as GameObject;
            BulletF2 bulletC2 = bullet2.GetComponent<BulletF2>();
            nextFire = Time.time + fireRate;
        }
        else
        {
            myAnimator.SetBool("disparo", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colision = collision.gameObject;
        if (colision.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(sfx_enemieDeath, Camera.main.transform.position);
            myAnimator.SetTrigger("destroy");
            active = false;
            gm.Restar();
        }
    }
    void Die()
    {
        
        active = false;
        Destroy(this.gameObject);
    }
}
