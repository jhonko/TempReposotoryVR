using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetection : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        // what happens when you hit something with your car
        if (other.tag == "JayWalker" || other.tag == "OtherCar")
        {
            EndGame();
        }

        // for when a point system will be implemented
        /*if (other.tag == "OtherCar")
        {
            Debug.Log("ControllerColliderHit");
            PlayerPoints.pointsModifier = 1f;
            PlayerPoints.MinusPoints();
        }*/
    }

    public void EndGame()
    {
        SceneManager.LoadScene("DeathMenuScene");
    }
}
