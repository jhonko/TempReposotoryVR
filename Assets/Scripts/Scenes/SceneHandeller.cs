using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneHandeller : MonoBehaviour {

	void Start () {
        		
	}
	
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Level0.1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsScene");
    } 
}
