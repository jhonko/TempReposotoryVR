using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModder : MonoBehaviour {

    public Transform player;

    [SerializeField]
    private Vector3 offSet;

    [SerializeField]
    private Space offsetPositionSpace;

    [SerializeField]
    private bool lookAt = true;

    // Use this for initialization
    void Start () {
       // transform.rotation = Quaternion.Euler(-10, 0, 0);
        // offSet = transform.position - player.transform.position;



    }

    // Update is called once per frame
    void Update()
    {
        //camare follows player
       // moveVector = lookAt.position + startOffSet;
        //x
        // range in with camer can move x as
        // moveVector.x = Mathf.Clamp(moveVector.x, -10, 10);
        //y
        // range in with camer can move y as
        //moveVector.y = Mathf.Clamp(moveVector.y, 3, 30);
        //z
        //transform.position = moveVector;

        /* if (transition > 1.0f)
         {
             transform.position = moveVector;
         }
         else
         {
             //start of game camera animation
             transform.position = Vector3.Lerp(moveVector + animationOffSet, moveVector, transition);
             transition += Time.deltaTime * 1 / animationDuration;
             transform.LookAt(lookAt.position + Vector3.up);
         }*/
    }

    void LateUpdate()
    {
        Refresh();
        //   transform.position = player.transform.position + offSet;
       // transform.rotation = Quaternion.Euler(-10,0,0);
    }

    public void Refresh()
    {
        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = player.TransformPoint(offSet);
        }
        else
        {
            transform.position = player.position + offSet;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(player);
            transform.Rotate(-10,0,0);

    
        }
        else
        {
           // Quaternion.Euler(-10,0,0);
            transform.rotation = player.rotation;
          //  transform.rotation = Quaternion.Euler(-60,0,0);
        }
    }
}
