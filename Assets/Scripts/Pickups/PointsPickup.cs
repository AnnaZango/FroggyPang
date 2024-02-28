using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPickup : MonoBehaviour, ICollectable
{
    //Controls point pickups. They appear when a destructible element is destroyed. It has a 
    //self destruction component attached to it, so after a few seconds, it disappears

    [SerializeField] int points = 600;
    [SerializeField] float timeToStartFlickering = 2;
    [SerializeField] GameObject textPopup;

    float timeAlive;
    Animator animator;
    SelfDestroy selfDestroy;
    PlayerPoints playerPoints;

    private void Awake()
    {
        selfDestroy = GetComponent<SelfDestroy>();
        animator = GetComponentInChildren<Animator>();
        playerPoints = FindObjectOfType<PlayerPoints>();
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

    public void Collect()
    {
        GameObject popupPoints = Instantiate(textPopup, transform.position, Quaternion.identity);
        popupPoints.GetComponentInChildren<TextMesh>().text = points.ToString();

        playerPoints.ChangePoints(points);
        playerPoints.PlayPickupSound();
        Destroy(gameObject);
    }
}
