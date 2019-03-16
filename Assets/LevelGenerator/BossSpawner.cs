using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : SpawnerScript {

    GameObject boss;

    void Start()
    {
		GameObject enemyList = GameObject.Find("Enemies");
		if(enemyList == null)
		{
			Debug.LogWarning("There is no enemy list in the scene!");
		}
		else
		{
			creature = enemyList.GetComponent<Enemies>().getBoss();
		}
		Spawn();
    }

    protected override int GetSpawnCount()
    {
        return 1;
    }

    public override void Spawn()
    {
        boss = (Instantiate(creature, transform.position, transform.rotation));
        CharacterStats bossStats = boss.GetComponent<CharacterStats>();
        bossStats.CharacterDiedEvent += NotifyBossDied;
    }

    public void NotifyBossDied()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        GameObject engine = GameObject.FindGameObjectWithTag("GameController");
        engine.GetComponent<LevelEngine_home>().NextLevel();
        animator.SetBool("isOpened", true);
    }
}
