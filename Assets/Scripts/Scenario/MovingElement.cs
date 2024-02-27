using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElement : MonoBehaviour
{
    // It controls the moving platforms

    [SerializeField] Transform[] points; //array of target points
    [SerializeField] float speed;
    [SerializeField] int indexStartingPoint;
    [SerializeField] float minDistance = 0.02f;
    int currentIndex;


    void Start()
    {
        transform.position = points[indexStartingPoint].position;
    }

    
    void Update()
    {
        //when the platform reaches the next point, it moves to the next index of the array of points
        if(Vector2.Distance(transform.position, points[currentIndex].position) < minDistance)
        {
            currentIndex++;
            if(currentIndex >= points.Length)
            {
                currentIndex = 0; 
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[currentIndex].position,
            speed * Time.deltaTime);
    }
}
