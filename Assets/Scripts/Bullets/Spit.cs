using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : Bullet
{
    //Spit bullet, multiple bullets shot very fast.

    [SerializeField] float speed;
    [SerializeField] float timeLapse = 1;
    
    void Update()
    {
        //move bullet upwards
        float currentYPos = transform.position.y;
        transform.position = new Vector3(transform.position.x, (currentYPos + (speed * timeLapse * Time.deltaTime)));
    }
}
