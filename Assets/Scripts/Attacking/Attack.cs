using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour {

    //TODO attribute visibility
    public float range = 5;
    public float cooldown = 0;
    public float cooldownTimer = 0;
    public float attackPause = 1;
    public float attackPauseTimer = 0;
    public int damage = 10;
    public int cost = 1;
    public int damageModifier = 0;
    private CharacterStats stats;
    private CharacterStats playerStats;

    public GameObject effect;
    protected Animator anim;

	// Use this for initialization
	protected virtual void Start () {
        stats = GetComponent<CharacterStats>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if (CanAttack())
        {
            PerformAttack();
        }
	}

    protected virtual void PerformAttack()
    {
        ResetCooldown();
        ResetAttackTimer();
        DeductCost();

        Animate();
        PlayEffects();
    }

    protected abstract void PlayEffects();
    protected abstract void Animate();

    protected void DealDamage(GameObject target)
    {
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        targetStats.TakeDamage(damage + damageModifier);
    }
    
    protected abstract bool IsTargetInRange(GameObject target);

    protected bool CanAttack()
    {
        return IsPlayerAlive() && CheckCooldown() & CheckAttackTimer() & CheckCost(); //Do not change to && to avoid short-circuit
    }

    private bool IsPlayerAlive()
    {
        return playerStats.Alive;
    }

    private bool CheckCooldown()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= (stats.CastSpeed * Time.deltaTime);

        if (cooldownTimer < 0)
            cooldownTimer = 0;

        return cooldownTimer == 0;
    }

    private void ResetCooldown()
    {
        cooldownTimer = cooldown;
    }

    private bool CheckAttackTimer()
    {
        if (attackPauseTimer > 0)
            attackPauseTimer -= (stats.AttackSpeed * Time.deltaTime);

        if (attackPauseTimer < 0)
            attackPauseTimer = 0;

        return attackPauseTimer == 0;
    }

    private void ResetAttackTimer()
    {
        attackPauseTimer = attackPause;
    }

    private bool CheckCost()
    {
        return stats.Mana >= cost;
    }

    private void DeductCost()
    {
        stats.Mana -= cost;
    }
}
