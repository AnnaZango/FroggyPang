using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    //Script to destroy game object after a certain delay, used in multiple occasions
    [SerializeField] float timeToSelfDestroy = 5f;

    void Start()
    {
        Destroy(gameObject, timeToSelfDestroy);
    }

    public float GetTimeToSelfDestruction()
    {
        return timeToSelfDestroy;
    }

}
