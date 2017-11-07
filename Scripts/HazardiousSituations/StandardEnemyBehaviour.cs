using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyBehaviour : MonoBehaviour {

    private Transform playerTransform;

    public float speed;
    public float distanceToPlayer;

    public Vector3 objectDirection;

    private bool startMovement = false;

    void Start () {

       
    }
	
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
            //when startmovement is true enemy starts moving
            Vector3 movement = objectDirection * speed;
            transform.Translate(movement * Time.deltaTime);
        }

    }
}
