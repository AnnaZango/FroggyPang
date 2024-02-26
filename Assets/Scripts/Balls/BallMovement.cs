using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //Controls ball movement and instantiation of new balls when hit

    [Header("Movement forces")]    

    [SerializeField] float startingForceX = 3;
    [SerializeField] float minForceX = 5;
    [SerializeField] float maxForceX = 9;
    [SerializeField] float forceY = 10;


    [SerializeField] float distanceRadiusBall = 1;
    [SerializeField] bool hitDown;

    Rigidbody2D rb;
    LayerMask layerMovement;

    [SerializeField] bool beginningBall = false;

    [SerializeField] GameObject[] ballsPrefabs;
    [SerializeField] int indexBall = 0;
    [SerializeField] int points = 300;

    PlayerStats playerStats;

    bool ballsInstantiated = false;

    EndGameController endGameController;

    [SerializeField] GameObject popupText;

    AudioSource soundPop;

    private void Awake()
    {
        CastReferences();
    }

    private void CastReferences()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = FindObjectOfType<PlayerStats>();
        endGameController = FindObjectOfType<EndGameController>();
        layerMovement = (1 << LayerMask.NameToLayer("Limits"));
    }

    void Start()
    {
        //the radius size is a tenth of the scale, approximately
        distanceRadiusBall = gameObject.transform.localScale.x / 10;
        
        if (beginningBall) //apply force if is at beginning of the level
        {
            rb.velocity = new Vector2(startingForceX, 0);
        }
        soundPop = GameObject.FindGameObjectWithTag("bubbleSound").GetComponent<AudioSource>();
    }

    
    void Update()
    {
        hitDown = rb.RaycastRayHit(Vector2.down, distanceRadiusBall, layerMovement);

        if (hitDown) //apply a Y force when hitting down so ball jumps a minimum high allowing
            //the player to move below the balls
        {
            rb.velocity = new Vector2(rb.velocity.x, forceY);
        }

        //set velocity within a range to prevent it going too fast/slow
        SetXvelocityWithinRange();

        SetYVelocityWithinRange();
    }

    private void SetXvelocityWithinRange()
    {
        if (Mathf.Abs(rb.velocity.x) < minForceX) //we want a minimum force X so it is not bumping vertical
        {
            SetXForceInRange(minForceX);
        }
        else if (Mathf.Abs(rb.velocity.x) > maxForceX) //we want a maximum force X so it is not horizontal
        {
            SetXForceInRange(maxForceX);
        }
    }

    private void SetXForceInRange(float forceToSetTo)
    {
        if (rb.velocity.x >= 0) //going to the right or straight up
        {
            rb.velocity = new Vector2(forceToSetTo, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-forceToSetTo, rb.velocity.y);
        }
    }

    private void SetYVelocityWithinRange()
    {
        if (Mathf.Abs(rb.velocity.y) > forceY)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, forceY);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -forceY);
            }
        }
    }


    public void CollideWithBullet() //called when collision with any bullet occurs
    {
        playerStats.ChangePoints(points);
        
        //instantiate popup points
        GameObject popupPoints = Instantiate(popupText, transform.position, Quaternion.identity);
        popupPoints.GetComponentInChildren<TextMesh>().text = points.ToString();

        soundPop.Play();

        if (indexBall != 0)
        {
            if (!ballsInstantiated)
            {
                InstantiateNewBalls();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        endGameController.CheckNumberBalls();
    }

    private void InstantiateNewBalls()
    {
        ballsInstantiated = true;

        GameObject newBall1 = Instantiate(ballsPrefabs[indexBall - 1], transform.position, Quaternion.identity);
        GameObject newBall2 = Instantiate(ballsPrefabs[indexBall - 1], transform.position, Quaternion.identity);
        
        //one goes to the right, the other to the left
        newBall1.GetComponent<BallMovement>().InitializeBallForce(1, 5);
        newBall2.GetComponent<BallMovement>().InitializeBallForce(-1, 5);

        Destroy(gameObject);
    }

    

    public void InitializeBallForce(float signForceToX, float forceToY)
    {
        startingForceX = startingForceX * signForceToX;
        rb.velocity = new Vector2(startingForceX, forceToY);
    }
}
