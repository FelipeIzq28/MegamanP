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
    [SerializeField] AudioClip sfx_bullet;
    [SerializeField] AudioClip sfx_dash;
    [SerializeField] AudioClip sfx_death;
    [SerializeField] AudioClip sfx_jump;
    [SerializeField] AudioClip sfx_landing;
    // Start is called before the first frame update
    public bool gameOver = false;
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

    bool caer = false;

    public bool gamePaused = false;

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
        if(gamePaused == false)
        {
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
            AudioSource.PlayClipAtPoint(sfx_bullet, Camera.main.transform.position);
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
            caer = true;
        }
        if(caer == true && _rigibody.velocity.y == 0)
        {
            AudioSource.PlayClipAtPoint(sfx_landing, Camera.main.transform.position);
            caer = false;
        }
        
        
    }
    void Saltar()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (EnSuelo() && isDashing == false)
            {
                AudioSource.PlayClipAtPoint(sfx_jump, Camera.main.transform.position);
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
            if (Input.GetKeyDown(KeyCode.X))
            {
                AudioSource.PlayClipAtPoint(sfx_dash, Camera.main.transform.position);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colision = collision.gameObject;
        if (colision.tag != "Ground")
        {
            AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
            Destroy(this.gameObject);
            gameOver = true;
        }
    }

}


