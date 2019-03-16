using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConeAttack : EnemyAttack {

    public float arc;

    protected override void PerformAttack()
    {
        DealDamage(target);
        base.PerformAttack();
        Debug.Log("Enemy" + transform.gameObject.name + "attacked");
    }

    protected override bool IsTargetInRange(GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        Vector3 dir = (target.transform.position - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);

        return (distance <= range && direction >= arc);
    }

}
