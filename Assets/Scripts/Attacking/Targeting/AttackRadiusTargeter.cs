using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusTargeter : MonoBehaviour
{
    private PlayerAreaAttack attack;
    private Transform radiusCircle;
    private MouseControl cursor;
    private bool targeting;

    public void Initialize(PlayerAreaAttack attack)
    {
        this.attack = attack;

        radiusCircle = attack.transform.Find("AreaTargetingCircle");
        if (radiusCircle == null)
        {
            Debug.LogError("No area targeting circle attached to Player!");
        }

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cursor = mainCamera.GetComponent<MouseControl>();

        attack.TargetingOn += TargetingOn;
        attack.TargetingOff += TargetingOff;
    }

    public void TargetingOff()
    {
        targeting = false;
        radiusCircle.GetComponent<MeshRenderer>().enabled = false;
    }

    public void TargetingOn()
    {
        targeting = true;
        radiusCircle.localScale = new Vector3(attack.radius * 2, attack.radius * 2, 1);
        radiusCircle.GetComponent<MeshRenderer>().enabled = true;
    }

    private void Update()
    {
        if (targeting)
        {
            radiusCircle.transform.position = cursor.MouseToWorld() + new Vector3(0, 0.5f, 0);
        }
    }
}
