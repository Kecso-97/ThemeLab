using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffectInit : MonoBehaviour {

    private GameObject player;
    private PlayerConeAttack attack;
    private ParticleSystem pSystem;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attack = player.GetComponent<PlayerConeAttack>();
        pSystem = GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {
        var shape = pSystem.shape;
        shape.length = attack.range;

        transform.position = player.transform.position + new Vector3(0, 1.33f, 0);
        transform.rotation = player.transform.rotation;
        transform.Rotate(Vector3.up * 30);
    }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * -600 * Time.deltaTime);
	}
}
