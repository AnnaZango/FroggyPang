using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{   
    //It controls the rotation of the scenario every few seconds.

    [SerializeField] float rotationValue;
    [SerializeField] float temp;

    [SerializeField] bool rotate = false;
    [SerializeField] float rotationAngleGoal = 90;
    [SerializeField] float rotationStep = 90;
    [SerializeField] float currentAngle = 0;
    [SerializeField] float currentAngleInt;
    [SerializeField] float secondsToRotation = 15;
    [SerializeField] float rotationSpeed = 2;

    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        StartCoroutine(RotationProcess()); //coroutine called every X time, defined by secondsToRotation
    }

    private void OnEnable()
    {
        EndGameController.OnGameFinished += StopRotations; //prevent rotation when game ends
    }

    private void OnDisable()
    {
        EndGameController.OnGameFinished -= StopRotations;
    }


    void Update()
    {
        if (rotate)
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
        rotationValue = Mathf.Lerp(0f, rotationStep, Time.deltaTime * rotationSpeed);
        gameObject.transform.Rotate(new Vector3(0, 0, rotationValue));
        currentAngle += rotationValue;
    }

    private void FinishRotation()
    {
        currentAngleInt = ((int)currentAngle / 10) * 10; //to round to nearest 10
        gameObject.transform.eulerAngles = new Vector3(0, 0, currentAngleInt);
        currentAngle = currentAngleInt;

        rotate = false;

        rotationAngleGoal = currentAngle + rotationStep;

        if (Mathf.Abs(currentAngle) >= 360)
        {
            currentAngle = 0; //reset angle back to 0
            gameObject.transform.eulerAngles = new Vector3(0, 0, currentAngle);
            rotationAngleGoal = rotationStep;
        }
    }

    private void StartRotation()
    {
        rotate = true;
        playerHealth.SetCanBeHurt(false); //prevent player from getting hurt when rotation is ocurring
    }

    IEnumerator RotationProcess()
    {
        yield return new WaitForSeconds(secondsToRotation);
        if (!GameManager.GetIfGameFinished())
        {
            StartRotation();
            StartCoroutine(RotationProcess()); //calls itself to repeat every X seconds
        }
    }

    private void StopRotations()
    {
        //Debug.Log("Stop rotations!");
        //StopCoroutine(RotationProcess());
        //StopCoroutine("RotationProcess");
    }
}
