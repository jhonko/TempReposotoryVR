using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarMovement : MonoBehaviour {

    private float speed;
    private float maxSpeed = 700.0f;

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

    // lane swapping
    private float lane = 25f;
    private float laneModifier = 2f;
    private float virtualLane;
    private bool goLeftInput = false;
    private bool goRightInput = false;
    private bool swappingLane = false;


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
        // checks if the car is grounded
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
        // when the player presses the brake button, there are three speed states for the car 0= standing still, 1= driving normally, 2= driving fast
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

        // makes sure that the game only gets the input for braking once each time the button is pressed
        if (buttonOneLock == true)
        {
            buttonOneLock = false;
            if (OVRInput.GetUp(OVRInput.Button.One))
            {
                carMovementState = speedState;
            }
        }  

        // same as braking but then with acceleration
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

        // lane swapping
        //right
        if (OVRInput.GetUp(OVRInput.Button.Four) && swappingLane == false)
        {
            goRightInput = true;
        }
        if (goRightInput == true && lane < 125)
        {
            swappingLane = true;
            lane += laneModifier;
            virtualLane += laneModifier;
            if (virtualLane == 100)
            {
                swappingLane = false;
                goRightInput = false;
                virtualLane = 0;
               
            }
        }

        //left
        if (OVRInput.GetUp(OVRInput.Button.Three) && swappingLane == false)
        {
            goLeftInput = true;
        }
        if (goLeftInput && lane > -175)
        {
            Debug.Log("jojo");
            swappingLane = true;
            lane -= laneModifier;
            virtualLane += laneModifier;
            if (virtualLane == 100)
            {
                swappingLane = false;
                goLeftInput = false;
                virtualLane = 0;
                
            }
        }


        /*//steering
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
        }*/
    }

    public void ApplyInput()
    {
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        // switch case for the different driving states
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

        /*//right
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
        }*/

        // set speeds to vectors
        //forward
        Vector3 velocity = Vector3.forward * speed;
        //steering
        Vector3 laneVector = transform.position;
        laneVector.x = lane;

        // move car
        //forward
        transform.Translate(velocity * Time.deltaTime);
        //steering
        transform.position = laneVector;
        transform.Rotate(Vector3.up, 0.0f);
        transform.Translate(velocity * Time.deltaTime);

    }
}
