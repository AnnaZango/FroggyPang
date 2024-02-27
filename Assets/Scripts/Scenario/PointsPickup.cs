using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPickup : MonoBehaviour
{
    //Controls point pickups. They appear when a destructible element is destroyed. It has a 
    //self destruction component attached to it, so after a few seconds, it disappears

    [SerializeField] int points = 600;
    [SerializeField] float timeToStartFlickering = 2;
    float timeAlive;
    Animator animator;
    SelfDestroy selfDestroy;

    [SerializeField] GameObject textPopup;

    private void Awake()
    {
        selfDestroy = GetComponent<SelfDestroy>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        timeAlive = selfDestroy.GetTimeToSelfDestruction();
    }

    
    void Update()
    {
        timeAlive -= Time.deltaTime;
        if (timeAlive <= timeToStartFlickering)
        {
            //flickers when it is about to be destroyed, to notify player
            animator.SetTrigger("flicker");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //intsantiate popup message to notify player of the points achieved
            GameObject popupPoints = Instantiate(textPopup, transform.position, Quaternion.identity);
            popupPoints.GetComponentInChildren<TextMesh>().text = points.ToString();

            other.GetComponent<PlayerPoints>().ChangePoints(points);
            other.GetComponent<PlayerPoints>().PlayPickupSound();
            Destroy(gameObject);
        }
    }
}
