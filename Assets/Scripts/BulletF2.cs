using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletF2 : MonoBehaviour
{
    float speed = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(Time.deltaTime * speed* -1, Time.deltaTime * 10));
    }
}
