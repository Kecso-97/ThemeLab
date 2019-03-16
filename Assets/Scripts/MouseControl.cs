using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

	private GameObject target;
	//GameObject oldTarget;
	public GameObject testObject;
	Plane ground = new Plane(Vector3.up, Vector3.zero);
	Ray ray;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = new RaycastHit();
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool hit = Physics.Raycast(ray, out hitInfo);

			if (hit)
			{
				//oldTarget = target;
				target = hitInfo.transform.gameObject;

				Debug.Log("Hit " + target.name);//printing the name 
				
				if (target.tag == "Chest")
				{
					//openChest();
				}
			}
		}
	}

	public Vector3 MouseToWorld()
	{
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distToGround = -1f;
		ground.Raycast(ray, out distToGround);
		Vector3 worldPos = ray.GetPoint(distToGround);

		//Debug.Log("Mouse at: " + worldPos);
		return worldPos;
	}

	public GameObject[] getEnemies(int radius)//TODO
	{
		return null;
	}

	private void openChest()//TODO
	{

	}
}
