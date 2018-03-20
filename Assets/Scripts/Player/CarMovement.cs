using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 steering;
    
    private float brakeAcceleration = 15f;

    private float resistance = 0.075f;
    private float speed;
    private float maxSpeed = 700.0f;

    private float m_lastPressed;


    private float steeringAngle;
    private float steeringInput = 1f;
    private float maxSteering = 60f;
    private float steeringNormalizer = 1f;

    public float groundDistance;
    public LayerMask layer;

    private bool wKeyLock = false;
    private bool sKeyLock = false;



    private bool right;
    private bool left;

    public int carMovementState = 1;
    public int speedState = 1;
    
    private bool isGrounded;

    public Rigidbody thisRB;
    public Vector3 cOM;


    void Start () {
        thisRB = GetComponent<Rigidbody>();
        thisRB.centerOfMass = cOM;
		
	}
	

	void Update ()
    {
        UpdateGrounded();

        CheckForInput();

        ApplyInput();
        //Debug.Log("speed: " + speed);
        Debug.Log("carMovementState: "+ carMovementState);
        Debug.Log("speedState: " + speedState);
    }

    public void UpdateGrounded()
    {
        if (isGrounded)
        {
            groundDistance = 25f;
            //Debug.Log(groundDistance);
        }
        else
        {
            groundDistance = 35f;
          //  Debug.Log(groundDistance);
        }
        if (Physics.Raycast(transform.position - new Vector3(0, 0.1f, 0), -transform.up, groundDistance, layer))
            isGrounded = true;
        else
            isGrounded = false;
    }

    public void CheckForInput()
    {
        if (sKeyLock == false)
        {
            sKeyLock = true;
            if (Input.GetKeyDown("s"))
            {
                if (carMovementState == 2)
                {
                    speedState = 1;
                }
                if (carMovementState == 1)
                {
                    speedState = 0;
                }
                
            }
        }

        if (sKeyLock == true)
        {
            sKeyLock = false;
            if (Input.GetKeyUp("s"))
            {
                carMovementState = speedState;
                
            }
            
        }

        if (wKeyLock == false) {

            wKeyLock = true;
            if (Input.GetKeyDown("w"))
            {
                
                if (carMovementState == 0)
                {

                    speedState = 1;
                }
                if (carMovementState == 1)
                {
                    Debug.Log("wtf komt ie hier");
                    speedState = 2;
                }
                
            }
            
        }

        if (wKeyLock == true)
        {
            wKeyLock = false;
            if (Input.GetKeyUp("w"))
            {
                carMovementState = speedState;

            }
           
        }

        //steering
        //right
        if (Input.GetKeyUp("d"))
        {
            right = false;
        }
        if (Input.GetKeyDown("d"))
        {
            right = true;
        }
        //left
        if (Input.GetKeyUp("a"))
        {
            left = false;
        }
        if (Input.GetKeyDown("a"))
        {
            left = true;
        }
    }

    public void ApplyInput()
    {
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        switch (speedState)
        {
            case 0:
                while (speed > 0)
                {
                    speed--;
                }
                speed = 0;
                break;
            case 1:
                while (speed > (maxSpeed / 2))
                {
                    speed--;
                }
                while (speed < (maxSpeed / 2))
                {
                    speed++;
                }

                speed = (maxSpeed / 2);
                break;
            case 2:
                Debug.Log("750?");
                while (speed < maxSpeed)
                {
                    speed++;
                }
                speed = maxSpeed;
                break;
        }

        //right
        if (right && isGrounded && speed != 0)
        {
            steeringAngle += steeringInput;
        }
        else
        {
            if (steeringAngle > 0)
            {
                steeringAngle -= steeringNormalizer;
                if (steeringAngle < 0)
                {
                    steeringAngle = 0;
                }
            }
        }
        //left
        if (left && isGrounded && speed != 0)
        {
            steeringAngle -= steeringInput;
        }
        else
        {
            if (steeringAngle < 0)
            {
                steeringAngle += steeringNormalizer;
                if (steeringAngle > 0)
                {
                    steeringAngle = 0;
                }
            }
        }

        // clamp speeds
        //steering
        steeringAngle = Mathf.Clamp(steeringAngle, -maxSteering, maxSteering);

        // set speeds to vectors
        //forward
        Vector3 velocity = Vector3.forward * speed;
        //steering
        Vector3 steering = Vector3.up * steeringAngle;

        // move car
        //forward
        transform.Translate(velocity * Time.deltaTime);
        //steering
        transform.Rotate(steering * Time.deltaTime);

        // Debug.Log(isGrounded);
    }









}
