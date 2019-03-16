using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInit : MonoBehaviour {

    public event ProjectileHitDelegate ProjectileHitEvent;

    private bool hit = false;

    private GameObject player;
    private EnemyProjectileAttack attack;
    private ParticleSystem pSystem;

    private Vector3 startpoint;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pSystem != null)
        {
            ParticleSystem.Particle[] emittedParticles = new ParticleSystem.Particle[pSystem.main.maxParticles];
            int numParticlesAlive = pSystem.GetParticles(emittedParticles);
            if (numParticlesAlive > 0 && Vector3.Distance(startpoint, emittedParticles[0].position) >= attack.range) { Destroy(pSystem); }
            pSystem.SetParticles(emittedParticles, numParticlesAlive);
        }
    }

    public void Initialize(EnemyProjectileAttack attack)
    {
        this.attack = attack;

        startpoint = attack.transform.position + new Vector3(0, 1.33f, 0);
        transform.position = startpoint;
        transform.forward = (player.transform.position - attack.transform.position);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!hit)
        {
            print("Arrow hit" + other.name);
            if (ProjectileHitEvent != null) { ProjectileHitEvent(other); }
            hit = true;
        }
    }
}
