using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : Attack {

    private bool alive = true;
    private EnemyAI.AIState AIState;

    public event StateToggleDelegate attackStartedEvent;
    public event StateToggleDelegate attackReadyEvent;
    public event StateToggleDelegate attackOutOfRangeEvent;

    public GameObject target;


    // Use this for initialization
    protected override void Start () {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");

        //range = 3.5f;
        //attackPause = 1.5f;
        //cost = 2;
        //arc = 0.5f; // cos(30) => Enemy will hit in a 60 degree arc
        damageModifier = GetComponent<CharacterStats>().DamageModifier;

        AIState = GetComponent<EnemyAI>().State;

        CharacterStats stats = GetComponent<CharacterStats>();
        stats.CharacterDiedEvent += Die;
    }

    protected override void Update()
    {
        if (alive)
        {
            if (CanAttack() /*&& AIState == EnemyAI.AIState.Ready*/)
            {
                if (IsTargetInRange(target))
                {
                    if (attackStartedEvent != null) { attackStartedEvent(); }
                    PerformAttack();
                }
            }
            else if (IsTargetInRange(target))
            {
                if (attackReadyEvent != null) { attackReadyEvent(); }
            }
            else
            {
                if (attackOutOfRangeEvent != null) { attackOutOfRangeEvent(); }
            }
        }
    }


    protected override bool IsTargetInRange(GameObject target)
    {
        return false;
    }

    protected override void PlayEffects()
    {

    }

    protected override void Animate()
    {
        
    }

    private void Die()
    {
        alive = false;
    }
}
