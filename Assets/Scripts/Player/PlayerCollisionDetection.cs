using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetection : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JayWalker")
        {
            EndGame();
        }
        if (other.tag == "OtherCar")
        {
            Debug.Log("ControllerColliderHit");
            PlayerPoints.pointsModifier = 1f;
            PlayerPoints.MinusPoints();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("DeathMenuScene");
    }
}
