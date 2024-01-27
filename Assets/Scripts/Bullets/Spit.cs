using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    //Spit bullet, multiple bullets shot very fast.

    [SerializeField] float speed;
    [SerializeField] float timeLapse = 1;


    void Start()
    {
        
    }

    
    void Update()
    {
        //move bullet upwards
        float currentYPos = transform.position.y;
        transform.position = new Vector3(transform.position.x, (currentYPos + (speed * timeLapse * Time.deltaTime)));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ball")
        {
            //when colliding with ball, call respective methods to instantiate new balls and destroy
            other.gameObject.GetComponent<BallMovement>().CollideWithBullet();
        }
        Destroy(gameObject);
    }
}
