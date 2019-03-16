using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> bosses = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getEnemy()
	{
		return enemies[Random.Range(0, enemies.Capacity)];
	}

	public GameObject getBoss()
	{
		return bosses[Random.Range(0, bosses.Capacity)];
	}
}
