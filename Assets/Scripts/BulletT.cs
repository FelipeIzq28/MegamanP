using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletT : MonoBehaviour
{
    public float direction;
    float _direccion;
    Animator myAnimator;
    Rigidbody2D myBody;
    float speed = 15;
    // Start is called before the first frame update
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        
    }
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction > 0)
        {
            _direccion = -1;
            transform.localScale = new Vector3(1, 1, 1);
        } else if (direction < 0)
        {
            _direccion = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }

       
        //if (_direccion > 0)
        //{
        //    myBody.velocity = new Vector2(speed, 0);
        //}
        //else if (_direccion < 0)
        //{
        //    myBody.velocity = new Vector2(-speed, 0);
        //}
        transform.Translate(new Vector2(_direccion * Time.deltaTime * speed, 0));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
