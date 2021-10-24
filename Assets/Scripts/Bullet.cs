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
    bool coll = false;
    CapsuleCollider2D myCollider;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _direccion = direction;
        transform.Translate(new Vector2(_direccion * Time.deltaTime * speed, 0));
       
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnimator.SetTrigger("crash");                      
        Destroy(this.gameObject, 0.1f);
          
    }
    

}
