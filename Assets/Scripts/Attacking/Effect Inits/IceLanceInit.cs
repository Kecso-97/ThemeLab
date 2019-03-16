using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ProjectileHitDelegate(GameObject target);

public class IceLanceInit : MonoBehaviour {

    public event ProjectileHitDelegate ProjectileHitEvent;

    private bool hit = false;

    private MouseControl cursor;
    private GameObject player;
    private PlayerProjectileAttack attack;
    private ParticleSystem pSystem;

    private Vector3 startpoint;

    private void Awake()
    {
        cursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        attack = player.GetComponent<PlayerProjectileAttack>();
        pSystem = GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {
        startpoint = player.transform.position + new Vector3(0, 1.33f, 0);
        transform.position = startpoint;
        transform.LookAt(cursor.MouseToWorld() + new Vector3(0, 1.33f, 0));
	}
	
	// Update is called once per frame
	void Update () {
        if (pSystem != null)
        {
            ParticleSystem.Particle[] emittedParticles = new ParticleSystem.Particle[pSystem.main.maxParticles];
            int numParticlesAlive = pSystem.GetParticles(emittedParticles);
            if (Vector3.Distance(startpoint, emittedParticles[0].position) >= attack.range) { Destroy(pSystem); }
            pSystem.SetParticles(emittedParticles, numParticlesAlive);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!hit)
        {
            print("Particle hit" + other.name);
            if (ProjectileHitEvent != null) { ProjectileHitEvent(other); }
            hit = true;
        }
    }
}
