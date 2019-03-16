using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMovement : MonoBehaviour
{
    public event StateToggleDelegate BaseAttackStoppedEvent;

    NavMeshAgent agent;
    Animator anim;

    public PlayerState state; //EZT MAJD ÁLLÍTSUK VISSZA
    private PlayerState nextState;
    public PlayerState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            anim.SetInteger("actionState", (int)state);
        }
    }

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        PlayerBaseAttack baseTrigger = GetComponent<PlayerBaseAttack>();
        baseTrigger.attackStartedEvent += setStateToAttack1;
        baseTrigger.attackReadyEvent += setStateToReady;

        PlayerProjectileAttack projectileTrigger = GetComponent<PlayerProjectileAttack>();
        projectileTrigger.ProjectileAttackEvent += setStateToSpecial3;

        PlayerAreaAttack areaTrigger = GetComponent<PlayerAreaAttack>();
        areaTrigger.areaAttackEvent += setStateToSpecial2;

        PlayerConeAttack coneTrigger = GetComponent<PlayerConeAttack>();
        coneTrigger.coneAttackEvent += setStateToSpecial1;

        CharacterStats stats = GetComponent<CharacterStats>();
        stats.CharacterDiedEvent += setStateToDeath;
    }

    // Update is called once per frame
    void Update()
    {
        State = nextState;

        switch (State)
        {
            case PlayerState.Stand:
                if (Input.GetMouseButton(1))
                    nextState = PlayerState.Run;
                break;

            case PlayerState.Run:
                agent.isStopped = false;
                if (Input.GetMouseButton(1)) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        agent.destination = hit.point;
                    }
                }

                if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
                {
                    transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
                }

                if (agent.remainingDistance <= agent.stoppingDistance)
                    nextState = PlayerState.Stand;
                else nextState = PlayerState.Run;
                break;

            case PlayerState.Ready:
                if (Input.GetMouseButton(1))
                {
                    nextState = PlayerState.Run;
                    if (BaseAttackStoppedEvent != null) BaseAttackStoppedEvent();
                }
                break;

            case PlayerState.Attack1:
                if (Input.GetMouseButton(1))
                {
                    nextState = PlayerState.Run;
                    if (BaseAttackStoppedEvent != null) BaseAttackStoppedEvent();
                }
                break;

            case PlayerState.Special1:
                nextState = PlayerState.Stand;
                break;

            case PlayerState.Special2:
            case PlayerState.Special3:
                agent.isStopped = true;
                nextState = PlayerState.Stand;
                break;
        }

    }

    #region State Setters
    public void setStateToReady()
    {
        nextState = PlayerState.Ready;
    }

    public void setStateToAttack1()
    {
        nextState = PlayerState.Attack1;
    }

    public void setStateToSpecial1()
    {
        agent.destination = agent.transform.position;
        nextState = PlayerState.Special1;
    }

    public void setStateToSpecial2()
    {
        agent.destination = agent.transform.position;
        nextState = PlayerState.Special2;
    }

    public void setStateToSpecial3()
    {
        agent.destination = agent.transform.position;
        nextState = PlayerState.Special3;
    }

    public void setStateToDeath()
    {
        //agent.destination = agent.transform.position;
        nextState = PlayerState.Death;
    }
    #endregion

    public enum PlayerState
    {
        Stand = 0,
        Run = 2,
        Ready = 5,
        Attack1 = 108,
        Special1 = 246,
        Special2 = 294,
        Special3 = 267,
        Death = 4
    }

}
