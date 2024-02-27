using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : Bullet
{
    //Tongue bullet, a continuous line like in Pang game. Used also for the two tongues of forked tongue
    //bullet type

    SpriteRenderer sprite;
    [SerializeField] float increment = 0.1f;
    [SerializeField] float timeLapse = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
        
    void Update()
    {
        //tiled sprite which increases in size progressively
        float currentSizeX = sprite.size.x;
        sprite.size = new Vector2((currentSizeX + (increment * Time.deltaTime * timeLapse)), sprite.size.y);
    }

}
