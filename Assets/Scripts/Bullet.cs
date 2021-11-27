using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Animator myAnimator;
    public float direction;
    float _direccion;
    float speed = 30;
    CapsuleCollider2D myCollider;
    Rigidbody2D myBody;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        
    }
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _direccion = direction;
        if(_direccion > 0)
        {
            myBody.velocity = new Vector2(speed, 0);
        } else if(_direccion < 0)
        {
            myBody.velocity = new Vector2(-speed, 0);
        }
       // transform.Translate(new Vector2(_direccion * Time.deltaTime * speed, 0));
      
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        myAnimator.SetTrigger("crash");
        myBody.velocity = Vector2.zero;
    }



    void Die()
    {
        Destroy(this.gameObject);
    }
}
