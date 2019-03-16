using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProjectileAttack : EnemyAttack
{
    protected override bool IsTargetInRange(GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= range;
    }

    protected override void PerformAttack()
    {
        base.PerformAttack();
        Debug.Log("Enemy" + transform.gameObject.name + "attacked");
    }

    protected override void PlayEffects()
    {
        GameObject projectile = Instantiate(effect);

        ArrowInit initScript = projectile.GetComponent<ArrowInit>();
        initScript.Initialize(this);
        initScript.ProjectileHitEvent += HitTarget;

        float duration = projectile.GetComponent<ParticleSystem>().main.duration;
        Destroy(projectile, duration);
    }

    private void HitTarget(GameObject target)
    {
        if (target.tag == "Player")
        {
            DealDamage(target);
        }
    }
}
