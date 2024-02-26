using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject parent;

    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ball")
        {
            //when colliding with ball, call respective methods to instantiate new balls and destroy
            other.gameObject.GetComponent<BallMovement>().CollideWithBullet();
        }
        Destroy(parent);
    }
}
