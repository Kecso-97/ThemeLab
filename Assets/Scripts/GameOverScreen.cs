using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour {


	void Start()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().CharacterDiedEvent += Display;
	}

	public void Display()
	{
		Transform[] children = GetComponentsInChildren<Transform>(true);
		foreach(Transform child in children)
		{
			child.gameObject.SetActive(true);
		}
	}

	public void Reset()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
