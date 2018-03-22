using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarMovement : MonoBehaviour {

    private float speed;
    private float maxSpeed = 700.0f;

    private float steeringAngle;
    private float steeringInput = 1f;
    private float maxSteering = 60f;
    private float steeringNormalizer = 1f;

    public float groundDistance;
    public LayerMask layer;

    private bool buttonOneLock = false;
    private bool buttonTwoLock = false;

    private bool right;
    private bool left;

    public int carMovementState = 0;
    public int speedState = 0;
    
    private bool isGrounded;

    public Rigidbody thisRB;
    public Vector3 cOM;


    void Start () {
        thisRB = GetComponent<Rigidbody>();
        thisRB.centerOfMass = cOM;
		
	}
	

	void Update ()
    {
        OVRInput.Update();
        OVRInput.FixedUpdate();

        UpdateGrounded();

        CheckForInput();

        ApplyInput();
    }

    public void UpdateGrounded()
    {
        if (isGrounded)
        {
            groundDistance = 25f;
        }
        else
        {
            groundDistance = 35f;
        }
        if (Physics.Raycast(transform.position - new Vector3(0, 0.1f, 0), -transform.up, groundDistance, layer))
            isGrounded = true;
        else
            isGrounded = false;
    }

    public void CheckForInput()
    {
        if (buttonOneLock == false)
        {
            buttonOneLock = true;
            if (OVRInput.GetDown(OVRInput.Button.One))
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

        if (buttonOneLock == true)
        {
            buttonOneLock = false;
            if (OVRInput.GetUp(OVRInput.Button.One))
            {
                carMovementState = speedState;
            }
        }  

        if (buttonTwoLock == false) {
                buttonTwoLock = true;
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {               
                if (carMovementState == 0)
                {
                    speedState = 1;
                }
                if (carMovementState == 1)
                {              
                    speedState = 2;
                } 
            }  
        }

        if (buttonTwoLock == true)
        {
                buttonTwoLock = false;
            if (OVRInput.GetUp(OVRInput.Button.Two))
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
    }
}
