using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPoints : MonoBehaviour {

    public static float playerScore = 1000f;

    public static float pointsModifier;


    public static void MinusPoints()
    {
        if (pointsModifier == 1f)
        {
            playerScore -= 10f;
            Debug.Log(playerScore);
            EndGame();
        }
    }

    public static void EndGame()
    {
        SceneManager.LoadScene("DeathMenuScene");
    }
}
