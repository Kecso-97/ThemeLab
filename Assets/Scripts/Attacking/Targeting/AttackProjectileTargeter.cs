using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectileTargeter : MonoBehaviour {

    private Transform arrowObject;
    private MeshRenderer arrowRender;
    private MouseControl cursor;
    private bool targeting;

    public void Initialize(PlayerProjectileAttack attack)
    {
        arrowObject = attack.transform.Find("ProjectileTargeter");
        if (arrowObject == null)
        {
            Debug.LogError("No projectile targeter attached to Player!");
        }
        arrowRender = arrowObject.GetComponentInChildren<MeshRenderer>();
        if(arrowRender == null)
        {
            Debug.LogError("Projectile Mesh Renderer not found!");
        }

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cursor = mainCamera.GetComponent<MouseControl>();

        attack.TargetingOn += TargetingOn;
        attack.TargetingOff += TargetingOff;
    }

    public void TargetingOff()
    {
        targeting = false;
        arrowRender.enabled = false;
    }

    public void TargetingOn()
    {
        targeting = true;
        arrowRender.enabled = true;
    }

    private void Update()
    {
        if (targeting)
        {
            Vector3 dir = cursor.MouseToWorld();
            arrowObject.transform.LookAt(new Vector3(dir.x, 0.5f, dir.z));
        }
    }
}
