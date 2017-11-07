using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TreeFallingBehaviour : MonoBehaviour {

    private Transform playerTransform;

    public float distanceToPlayer;

    private bool startMovement = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // finds the player transform and calculates difference between it and object
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= distanceToPlayer)
        {
            //when player is close enough startmovement is set true
            startMovement = true;
        }
        if (startMovement == true)
        {
            //when startmovement is true tree starts falling

        }

    }
}
