using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionInit : MonoBehaviour {

    private MouseControl cursor;

    private void Awake()
    {
        cursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseControl>();
    }

    // Use this for initialization
    void Start () {
        transform.position = cursor.MouseToWorld() + new Vector3(0,0.5f,0);

        ParticleSystem particle = GetComponent<ParticleSystem>();
        var main = particle.main;
        main.startSize = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAreaAttack>().radius;
        //WHY U NO WORK
        //var size = particle.main.startSize;
        //size.constantMax = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAreaAttack>().radius;
        //size.constantMin = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAreaAttack>().radius / 2;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
