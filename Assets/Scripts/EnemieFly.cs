using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieFly : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] AudioClip sfx_enemieDeath;
    [SerializeField] AstarPath enemy;
    Animator myAnimator;
    Rigidbody2D _rigidbody;
    [SerializeField] GameManager gm;
    [SerializeField] CircleCollider2D col;
    // Start is called before the first frame update
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        Collider2D chocando = Physics2D.OverlapCircle(transform.position, 6, LayerMask.GetMask("Player"));
        if (chocando == true)
        {
            enemy.enabled = true;
        } else
        {
            //transform.position =  new Vector2(6.41f * Time.deltaTime , 1.02f * Time.deltaTime);
            
            enemy.enabled = false;
            _rigidbody.velocity =  Vector2.zero;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 6);
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colision = collision.gameObject;
        if (colision.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(sfx_enemieDeath, Camera.main.transform.position);
            myAnimator.SetTrigger("destroy");
            gm.Restar();
            col.enabled = false;
 
        }
    }

    void Die()
    {
        
        Destroy(this.gameObject);
    }
}
