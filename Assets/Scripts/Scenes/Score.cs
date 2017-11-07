using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

   
    public Text displayfinalScore;
    public string stringfinalScore;
    public int playerScore;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerScore = PlayerPrefs.GetInt("PlayerScore");
        stringfinalScore = playerScore.ToString();
        displayfinalScore.text = "Final Score " + stringfinalScore;


    }
}
