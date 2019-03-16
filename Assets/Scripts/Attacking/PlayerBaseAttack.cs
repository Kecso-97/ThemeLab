using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateToggleDelegate();
public class PlayerBaseAttack : Attack {

    public event StateToggleDelegate attackStartedEvent;
    public event StateToggleDelegate attackReadyEvent;

    private GameObject target;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        //range = 1.0f;
        //attackPause = 1;
        //damage = 2;
        //cost = 0;
        damageModifier = GetComponent<CharacterStats>().DamageModifier;

        GetComponent<playerMovement>().BaseAttackStoppedEvent += Stop;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(ray, out hitInfo);
            if (hit && hitInfo.transform.tag == "Enemy")
            {
                target = hitInfo.transform.gameObject;
            }
        }

        if (!isTargetAlive()) Stop();

        if (CanAttack())
        {
            if (IsTargetInRange(target))
            {
                if (attackStartedEvent != null)
                {
                    attackStartedEvent();
                }
                PerformAttack();
            }
        }
        else if (IsTargetInRange(target))
        {
            if (attackReadyEvent != null)
            {
                attackReadyEvent();
            }
        }
    }

    protected override void PerformAttack()
    {
        DealDamage(target);
        base.PerformAttack();
    }

    protected override void Animate()
    {
        transform.LookAt(target.transform.position);
    }

    protected override bool IsTargetInRange(GameObject target)
    {
        if (target == null) return false;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= range;
    }

    protected override void PlayEffects()
    {
        //Todo?
    }

    private bool isTargetAlive()
    {
        if (target == null) return false;
        if (target.GetComponent<CharacterStats>().Alive) return true;
        return false;
    }

    private void Stop()
    {
        target = null;
    }
}
