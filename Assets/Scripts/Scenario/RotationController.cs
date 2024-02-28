using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    //It controls the rotation of the scenario every few seconds.

    [SerializeField] float rotationStep = 90;
    [SerializeField] float secondsToRotation = 15;
    [SerializeField] float rotationSpeed = 2;

    private float rotationToAdd;
    private float rotationAngleGoal = 90;
    private float currentAngle = 0;
    private float currentAngleRounded;

    private bool rotationEnabled = false;

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Start()
    {        
        StartCoroutine(RotationCoroutine()); //coroutine called every X time, defined by secondsToRotation
    }

    void Update()
    {
        if (rotationEnabled)
        {
            Rotate();

            if(currentAngle >= rotationAngleGoal)
            {
                FinishRotation();
            }
        }     
    }

    private void Rotate()
    {
        rotationToAdd = Mathf.Lerp(0f, rotationStep, Time.deltaTime * rotationSpeed);
        gameObject.transform.Rotate(new Vector3(0, 0, rotationToAdd));
        currentAngle += rotationToAdd;
    }

    private void FinishRotation()
    {
        currentAngleRounded = ((int)currentAngle / 10) * 10; //to round to nearest 10
        gameObject.transform.eulerAngles = new Vector3(0, 0, currentAngleRounded);
        currentAngle = currentAngleRounded;

        rotationEnabled = false;

        rotationAngleGoal = currentAngle + rotationStep;

        if(rotationAngleGoal > 360)
        {
            currentAngle = 0;
            gameObject.transform.eulerAngles = new Vector3(0, 0, currentAngle);
            rotationAngleGoal = rotationStep;
        }
    }

    IEnumerator RotationCoroutine()
    {
        yield return new WaitForSeconds(secondsToRotation);
        if (!GameManager.GetIfGameFinished())
        {
            rotationEnabled = true;
            playerHealth.SetCanBeHurt(false); //prevent hurting player while rotating
            StartCoroutine(RotationCoroutine()); //calls itself to repeat every X seconds
        }
    }

}
