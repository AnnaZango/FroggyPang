using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    //Tongue bullet, a continuous line like in Pang game. Used also for the two tongues of forked tongue
    //bullet type

    SpriteRenderer sprite;
    [SerializeField] float increment = 0.1f;
    [SerializeField] float timeLapse = 1;
    [SerializeField] GameObject parent;

    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
       
        sprite = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        //tiled sprite which increases in size progressively
        float currentSizeX = sprite.size.x;
        sprite.size = new Vector2((currentSizeX + (increment * Time.deltaTime * timeLapse)), sprite.size.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "ball")
        {
            //when colliding with ball, call respective methods to instantiate new balls and destroy
            other.gameObject.GetComponent<BallMovement>().CollideWithBullet();
        }
        Destroy(parent);
    }
}
