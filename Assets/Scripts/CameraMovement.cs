using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private Transform playerCoords;
	public Vector3 offset;
	private Transform myTransform;

	public void Awake()
	{
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		playerCoords = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if(playerCoords == null)
        {
            playerCoords = GameObject.FindGameObjectWithTag("Player").transform;
        }
		myTransform.position = playerCoords.position + offset;
        myTransform.LookAt(playerCoords);
    }
}
