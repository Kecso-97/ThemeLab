using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EnemySpawnDelegate(GameObject obj);
public class SpawnerScript : MonoBehaviour {
    public static event EnemySpawnDelegate EnemySpawnEvent;

    private static int level = 0;

    private List<GameObject> children = new List<GameObject>();

    public GameObject creature = null;

	// Use this for initialization
	void Start () {
		GameObject enemyList = GameObject.Find("Enemies");
		if (enemyList == null)
		{
			Debug.LogWarning("There is no enemy list in the scene!");
		}
		else
		{
			creature = enemyList.GetComponent<Enemies>().getEnemy();
		}
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void IncrementLevel()
    {
        level++;
    }

    protected virtual int GetSpawnCount()
    {
        return (int)Random.Range(0, (1 + level*0.2f));
    }

    public virtual void Spawn()
    {
        int maxCount = GetSpawnCount();
        for (int i = 0; i < 1; i++)
        {
            children.Add(Instantiate(creature, transform.position, transform.rotation));
        }

        foreach (GameObject o in children)
        {
            CharacterStats stats = o.GetComponent<CharacterStats>();
            stats.SetLevel(level);

            if (EnemySpawnEvent != null) { EnemySpawnEvent(o); }
        }
    }

    public void OnDestroy()
    {
        foreach(GameObject o in children)
        {
            Destroy(o);
        }
    }
}
