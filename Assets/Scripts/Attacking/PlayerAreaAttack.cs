using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerAreaAttack : PlayerAttack {

    public float radius;

    public event AttackFireDelegate areaAttackEvent;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        keyCode = KeyCode.Alpha2;

        //range = 5.5f;
        //cooldown = 2.5f;
        //attackPause = 0;
        //damage = 25;
        //cost = 10;
        //radius = 1.5f;
        damageModifier = GetComponent<CharacterStats>().MagicModifier;
    }


    protected override void PerformAttack()
    {
        if (Vector3.Distance(mouseControl.MouseToWorld(), transform.position) <= range)
        {
            Debug.Log("AreaAttack");
            if (areaAttackEvent != null) { areaAttackEvent(); }
            base.PerformAttack();
        }
    }

    protected override bool IsTargetInRange(GameObject target)
    {
        float distance = Vector3.Distance(mouseControl.MouseToWorld(), target.transform.position);
        
        return (distance <= radius);
    }

    protected override void InitTargeters()
    {
        base.InitTargeters();
        gameObject.AddComponent<AttackRangeTargeter>().Initialize(this);
        gameObject.AddComponent<AttackRadiusTargeter>().Initialize(this);
    }

    protected override void PlayEffects()
    {
        GameObject explosion = Instantiate(effect);
        float duration = explosion.GetComponent<ParticleSystem>().main.duration;
        Destroy(explosion, duration);
    }

    protected override void Animate()
    {
        base.Animate();
    }
}
