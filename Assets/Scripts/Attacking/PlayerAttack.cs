using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TargetingToggleDelegate();
public delegate void AttackFireDelegate();

public abstract class PlayerAttack : Attack {

    public event TargetingToggleDelegate TargetingOn;
    public event TargetingToggleDelegate TargetingOff;


    public List<GameObject> targets = new List<GameObject>();
    protected MouseControl mouseControl;
    public KeyCode keyCode;
    private bool targeting = false;
    public bool Targeting
    {
        get { return targeting; }
        set
        {
            targeting = value;
            if (targeting)
            {
                if(TargetingOn != null)
                {
                    TargetingOn();
                }
            }
            else
            {
                if(TargetingOff != null)
                {
                    TargetingOff();
                }
            }
        }
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mouseControl = (MouseControl)mainCamera.GetComponent("MouseControl");

        GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelEngine_home>().LevelGeneratedEvent += RemoveTargets;
        SpawnerScript.EnemySpawnEvent += AddTarget;

        InitTargeters();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (CanAttack())
        {
            if (Input.GetKeyDown(keyCode))
            {
                foreach (PlayerAttack attack in GetComponents<PlayerAttack>())
                {
                    attack.Targeting = false;
                }
                Targeting = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Targeting = false;
            }

            if (Targeting)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PerformAttack();
                    Targeting = false;
                }
            }
        }
    }

    protected override void PerformAttack()
    {
        foreach (GameObject target in targets.ToArray())
        {
            if (!target.GetComponent<CharacterStats>().Alive) { targets.Remove(target);}
            else if (IsTargetInRange(target))
            {
                DealDamage(target);
                Debug.Log("Enemy " + target.gameObject.name + " hit");
            }
        }

            base.PerformAttack(); //Attack goes off whether or not it hits anyone
    }

    protected virtual void InitTargeters()
    {
        
    }

    //private void InitTargets()
    //{
    //    targets = new List<GameObject>();
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    foreach (GameObject enemy in enemies)
    //    {
    //        targets.Add(enemy);
    //    }
    //}
    public void AddTarget(GameObject target)
    {
        targets.Add(target);
    }

    private void RemoveTargets()
    {
        targets = new List<GameObject>();
    }

    protected override void Animate()
    {
        transform.LookAt(mouseControl.MouseToWorld());
    }
}
