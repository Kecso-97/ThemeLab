using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAttack : PlayerAttack {

    public event AttackFireDelegate ProjectileAttackEvent;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        keyCode = KeyCode.Alpha3;

        //range = 7.5f;
        //cooldown = 1;
        //attackPause = 0;
        //damage = 10;
        //cost = 5;
        damageModifier = GetComponent<CharacterStats>().MagicModifier;
    }

    protected override void PerformAttack()
    {
        Debug.Log("ProjectileAttack");
        if (ProjectileAttackEvent != null) { ProjectileAttackEvent(); }
        base.PerformAttack();
    }

    protected override bool IsTargetInRange(GameObject target)
    {
        return false;
    }

    protected override void InitTargeters()
    {
        base.InitTargeters();
        gameObject.AddComponent<AttackRangeTargeter>().Initialize(this);
        gameObject.AddComponent<AttackProjectileTargeter>().Initialize(this);
    }

    protected override void PlayEffects()
    {
        GameObject projectile = Instantiate(effect);
        projectile.GetComponent<IceLanceInit>().ProjectileHitEvent += HitTarget;
        float duration = projectile.GetComponent<ParticleSystem>().main.duration;
        Destroy(projectile, duration);
    }

    private void HitTarget(GameObject target)
    {
        if (target.tag == "Enemy")
        {
            DealDamage(target);
        }
    }
}
