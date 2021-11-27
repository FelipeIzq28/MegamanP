using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletF1 : MonoBehaviour
{
    float speed = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(1 * Time.deltaTime * speed, Time.deltaTime*10));
    }
}
