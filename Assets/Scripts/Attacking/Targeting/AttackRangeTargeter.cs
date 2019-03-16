using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeTargeter : MonoBehaviour
{
    private PlayerAttack attack;
    private Transform rangeCircle;
    

    public void Initialize(PlayerAttack attack)
    {
        this.attack = attack;

        rangeCircle = transform.Find("RangeTargetingCircle");
        if (rangeCircle == null)
        {
            Debug.LogError("No range targeting circle attached to Player!");
        }

        attack.TargetingOn += TargetingOn;
        attack.TargetingOff += TargetingOff;
    }

    public void TargetingOff()
    {
        rangeCircle.GetComponent<MeshRenderer>().enabled = false;
    }

    public void TargetingOn()
    {
        rangeCircle.localScale = new Vector3(attack.range * 2, attack.range * 2, 1);
        rangeCircle.GetComponent<MeshRenderer>().enabled = true;
    }
}
