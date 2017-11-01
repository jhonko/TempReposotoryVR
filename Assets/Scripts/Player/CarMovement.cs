using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 steering;
    
    private float acceleration = 10f;
    private float brakeAcceleration = 15f;

    private float resistance = 0.075f;
    private float speed;
    private float maxSpeed = 700.0f;
    private float handbrakeStrength = 0.3f;

    private float steeringAngle;
    private float steeringInput = 1f;
    private float maxSteering = 60f;
    private float steeringNormalizer = 1f;

    public float groundDistance;
    public LayerMask layer;

    private bool forward = true;
    private bool backward;
    private bool right;
    private bool left;
    private bool handbrake;
    private bool isGrounded;

    public Rigidbody thisRB;
    public Vector3 cOM;


    void Start () {
        thisRB = GetComponent<Rigidbody>();
        thisRB.centerOfMass = cOM;
		
	}
	

	void Update ()
    {
        //Debug.Log(gameObject.transform.rotation.x + "x");
      //  Debug.Log(gameObject.transform.rotation.y + "y");
        //Debug.Log(gameObject.transform.rotation.z + "z");

        UpdateGrounded();

        UpdateFlipped();
        

        // velocoty
        //forward
       /* if (Input.GetKeyUp("w"))
        {
            forward = false;
        }
        if (Input.GetKeyDown("w"))
        {
            forward = true;          
        }*/
        //backward
        if (Input.GetKeyUp("s"))
        {
            backward = false;
            forward = true;
        }
        if (Input.GetKeyDown("s"))
        {
            backward = true;
            forward = false;
        }
        //handbrake
        if (Input.GetKeyUp("space"))
        {
            handbrake = false;
        }
        if (Input.GetKeyDown("space"))
        {
            handbrake = true;
        }
        //forward
        if (forward && isGrounded)
        {
           // Debug.Log("jeys");
            speed += acceleration;
        }
        else
        {
           // Debug.Log("noooo");
            if (speed > 0) {
                speed -= resistance;
                if (speed < 0 )
                {
                    speed = 0;
                }
            }
        }
        //backward
        if (backward && isGrounded )
        {
            if (speed > 0)
            {
                speed -= brakeAcceleration;

            }
        }
        else
        {
            if (speed < 0)
            {
                speed += resistance;
                if (speed > 0)
                {
                    speed = 0;
                }
            }
        }
        //handbrake
        if (handbrake && isGrounded)
        {
            if (speed > 0)
            {
                speed -= handbrakeStrength;
                if (speed < 0)
                {
                    speed = 0;
                }
            }
            else 
            {
                speed += handbrakeStrength;
                if (speed > 0)
                {
                    speed = 0;
                }      
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
        //right
        if (right  && isGrounded && speed!=0)
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
        if (left  && isGrounded && speed!=0)
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
        //forward
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
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

    public void UpdateFlipped()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            //Debug.Log("flipped");
        }
    }







}
