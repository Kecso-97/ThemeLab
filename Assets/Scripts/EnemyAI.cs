using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private bool alive = true;
    public float followRange = 10;
    protected Animator anim;
    NavMeshAgent agent;

    public GameObject target;

    public AIState state; //EZT MAJD ÁLLÍTSUK VISSZA
    private AIState nextState;

    public AIState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            anim.SetInteger("movementState", (int)state);
        }
    }

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");

        EnemyAttack enemytrigger = GetComponent<EnemyAttack>();
        enemytrigger.attackReadyEvent += setStateToReady;
        enemytrigger.attackStartedEvent += setStateToAttack;
        enemytrigger.attackOutOfRangeEvent += setStateToRun;

        CharacterStats stats = GetComponent<CharacterStats>();
        stats.CharacterDiedEvent += setStateToDeath;
    }
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            State = nextState;

            float distance = Vector3.Distance(transform.position, target.transform.position);

            switch (State)
            {
                case AIState.Stand:
                    agent.isStopped = true; //doesn't move
                    if (distance <= followRange)
                    {
                        agent.destination = target.transform.position;
                        nextState = AIState.Run;
                    }
                    break;

                case AIState.Run:
                    if (distance > followRange)
                    {
                        nextState = AIState.Stand;
                    }
                    else
                    {
                        agent.isStopped = false;
                        agent.destination = target.transform.position;
                    }
                    break;

                case AIState.Ready:
                    agent.isStopped = true;
                    transform.LookAt(target.transform.position);
                    break;

                case AIState.Attack1:
                    //agent.isStopped = true;
                    transform.LookAt(target.transform.position);
                    break;

                case AIState.Death:
                    break;

                default:
                    break;
            }
        }
        /*

        Vector3 dir = (target.transform.position - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);

        
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }*/
    }

    #region State Setters
    private void setStateToAttack()
    {
        nextState = AIState.Attack1;
    }

    private void setStateToReady()
    {
        if(State != AIState.Death)
        nextState = AIState.Ready;
    }

    private void setStateToRun()
    {
        nextState = AIState.Run;
    }

    private void setStateToDeath()
    {
        alive = false;
        //agent.isStopped = true;
        State = AIState.Death;
    }
    #endregion

    public enum AIState
    {
        Stand = 0,
        Run = 1,
        Ready = 2,
        Attack1 = 3,
        Death = 4
    }
}
