using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConeAttack : PlayerAttack {

    public event AttackFireDelegate coneAttackEvent;

    public float arc;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        keyCode = KeyCode.Alpha1;

        //range = 2.5f;
        //arc = 0.5f; //cos(30) => Player will hit in a 60 degree arc
        damageModifier = GetComponent<CharacterStats>().DamageModifier;
	}

    // Update is called once per frame
    protected override void Update()
    {
        if (CanAttack())
        {
            if (Input.GetKeyDown(keyCode)) //Targeting is not necessary here
            {
                PerformAttack();
            }
        }
    }

    protected override void PerformAttack()
    {
        Debug.Log("ConeAttack");
        if (coneAttackEvent != null) { coneAttackEvent(); }
        base.PerformAttack();
        //TODO Effects, particles
    }

    protected override bool IsTargetInRange(GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        Vector3 dir = (target.transform.position - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);

        return (distance <= range && direction >= arc);
    }

    protected override void PlayEffects()
    {
        GameObject slash = Instantiate(effect);
        float duration = slash.GetComponent<ParticleSystem>().main.duration;
        Destroy(slash, duration);
    }

    protected override void Animate()
    {
        base.Animate();
    }
}
