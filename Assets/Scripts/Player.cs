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
    // Start is called before the first frame update

    float longDash = 15;
    Animator myAnimator;
    Vector2 _movement;
    Rigidbody2D  _rigibody;
    BoxCollider2D myCollider;

    float duracion = 0;
    float dashRate = 1;

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
        if(isDashing == false)
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
                        _rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                       
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
    void DobleSalto()
    {
        if (dobleSalto == true && isDashing == false)
        {
            myAnimator.SetBool("falling", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                dobleSalto = false;

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


