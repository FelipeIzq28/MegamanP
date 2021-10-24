using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float dashForce;
    [SerializeField] BoxCollider2D floorPoint;
    [SerializeField] BoxCollider2D personaje;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    // Start is called before the first frame update

    float longDash = 15;
    Animator myAnimator;
    Vector2 _movement;
    Rigidbody2D  _rigibody;
    BoxCollider2D myCollider;
    public float direccionBullet;

    float duracion = 0;
    float dashRate = 0.7f;

    float duracionDisp = 0;
    float fireRate = 1;


    bool isDashing = false;
    bool facingRight = true;
    bool dobleSalto = false;

   

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direccionBullet = transform.localScale.x;
        if (isDashing == false)
        {
            float direccion = Input.GetAxisRaw("Horizontal");
            if (direccion < 0 && facingRight == true)
            {
                Flip();
            }
            else if (direccion > 0 && facingRight == false)
            {
                Flip();
            }
        }                      
        Correr();
        Caer();
        Dash();
        Saltar();
        Disparar();

    
    }

    private void LateUpdate()
    {
        
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Dash"))
        {
            isDashing = true;

        }
        else
        {
            isDashing = false;

        }
    }
    void Disparar()
    {

        float direccion = transform.localScale.x; ;

        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= duracionDisp)
        {
            duracionDisp = Time.time + fireRate;
            myAnimator.SetLayerWeight(1, 1);
           GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation) as GameObject;
            Bullet bulletC = bullet.GetComponent<Bullet>();
            bulletC.direction = direccion;


        }
        if(Time.time >= duracionDisp)
        {
            myAnimator.SetLayerWeight(1, 0);
            
        }
    }
    void Shoot()
    {
        GameObject myBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;
        Bullet bulletComponent = myBullet.GetComponent<Bullet>();
        if (personaje.transform.localScale.x < 0f)
        {
            //Bala hacia la Izquierda
            //bulletComponent.direction = Vector2.left;
        }
        else
        {
            //bulletComponent.direction = Vector2.right;
        }
    }


    void Correr()
    {
        float direccion = Input.GetAxisRaw("Horizontal");
        if (direccion != 0 && isDashing == false)
        {
            
            myAnimator.SetBool("isRunning", true);
            transform.Translate(new Vector2(direccion * Time.deltaTime * speed, 0));
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }
    void Caer()
    {
        if(_rigibody.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true);
        }
    }
    void Saltar()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (EnSuelo() && isDashing == false)
            {
                myAnimator.SetBool("falling", false);
                _rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                dobleSalto = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                   
                    if (dobleSalto == true)
                    {
                        myAnimator.SetBool("falling", false);
                        myAnimator.SetTrigger("jumping");
                        _rigibody.AddForce(Vector2.up * (jumpForce/2), ForceMode2D.Impulse);
                       
                        dobleSalto = false;
                    }
                }
            }
        }
       
  
        //if (EnSuelo() && isDashing == false)
        //{
        //    myAnimator.SetBool("falling", false);
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        _rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //        myAnimator.SetTrigger("jumping");
        //        dobleSalto = true;
        //    }

        //}

    }
    bool EnSuelo()
    {
        return floorPoint.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Dash()
    {

        if (EnSuelo() )
        {
            float direccion = transform.localScale.x;
            myAnimator.SetBool("falling", false);
            if (Input.GetKey(KeyCode.X) && Time.time >= duracion)
            {
                isDashing = true;
                longDash++;
               

                myAnimator.SetBool("isDashing", true);
               
                    // _rigibody.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
                    transform.Translate(new Vector2(direccion * Time.deltaTime * dashForce, 0));
                    

                    if (longDash > 1)
                    {
                        myAnimator.SetTrigger("longDash");
                    }

                if (personaje.IsTouchingLayers(LayerMask.GetMask("Ground")))
                {
                    duracion = Time.time + dashRate;
                    _rigibody.velocity = new Vector2(0, _rigibody.velocity.y);
                    myAnimator.SetBool("isDashing", false);
                    isDashing = false;

                }
            } else
            {
                _rigibody.velocity = new Vector2(0, _rigibody.velocity.y);
                myAnimator.SetBool("isDashing", false);
                isDashing = false;
            }
            

        }

    }
   
    void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

}


