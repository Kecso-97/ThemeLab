using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Play()
    {
        SceneManager.LoadScene("Alpha");
    }

    public void Quit()
    {
        print("App quit");
        Application.Quit();
    }
}
