using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void NewLevelGeneratedDelegate();
public class LevelEngine_home : MonoBehaviour {
    public event NewLevelGeneratedDelegate LevelGeneratedEvent;

	public float PARTSIZE = 300;
    public int MAPSIZE = 100;

    private int size = 0;

	private float startingZPos = 0;
	private int levelNo = 1;

    public GameObject path;
    public GameObject curve;
    public GameObject deadend;
    public GameObject Xcross;
    public GameObject Ycross;
    public GameObject Gate;

    private NavMeshSurface navMeshSurface;

    public GameObject player;

    private List<LevelPartModel_home> level = new List<LevelPartModel_home>();
	private List<LevelPartModel_home> oldLevel = new List<LevelPartModel_home>();

    
	// Use this for initialization
	void Start () {
        navMeshSurface = GetComponent<NavMeshSurface>();
		LevelPartModel_home.path = path;
        LevelPartModel_home.curve = curve;
        LevelPartModel_home.deadend = deadend;
        LevelPartModel_home.Xcross = Xcross;
        LevelPartModel_home.Ycross = Ycross;
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region coordinate-vector conversion

	private Vector3 GetWorldPos(Vector2Int mapPos)
	{
		return new Vector3(PARTSIZE *(mapPos.x - size / 2), 0, startingZPos + mapPos.y * PARTSIZE);
	}

    private AbstractLevelPartModel GetListElementFromRelative(Vector2 relativePos)
    {
        if((relativePos.x < 0) || (relativePos.x > size-1)
            || (relativePos.y < 0) || (relativePos.y > size-1))
        {
            return new EmptyLevelPartModel();
        }
        else
        {
            int index = (int)(relativePos.x + (relativePos.y * size));
            return level[index];
        }
    }

    private AbstractLevelPartModel GetListElementFromWorld(Vector2 worldPos)
    {
        int offset = (size / 2);
        if ((worldPos.x < -(offset) * PARTSIZE) || (worldPos.x > (offset + 1) * PARTSIZE)
            || (worldPos.y < (startingZPos)) || (worldPos.y > (startingZPos) + (size * PARTSIZE)))
        {
            return new EmptyLevelPartModel();
        }
        else
        {
            int index = (int)(worldPos.x + offset + (worldPos.y * size / PARTSIZE));
            return level[index];
        }
    }
    #endregion

    private void MarkLevelAsOld()
	{
        oldLevel = level;

    }

	//Adding old size to the base value
	private void IncrementStartingPos()
	{
		startingZPos += (size * PARTSIZE) + PARTSIZE;
	}

	private void CalculateSize()
	{
		size = MAPSIZE*2 +1;//random+l?
	}

	public void Generate()
	{
        if (LevelGeneratedEvent != null) { LevelGeneratedEvent(); }

        CalculateSize();

		//Fills up a new list of empty map parts
		level = new List<LevelPartModel_home>();
		for(int i = 0; i < size * size; i++)
		{
			Vector2Int position = new Vector2Int((i % size), (i / size));
			level.Add(new LevelPartModel_home(GetWorldPos(position)));
		}

        //Sets the neighbors of the parts
        for (int i = 0; i < size * size; i++)
        {
            Vector2Int position = new Vector2Int((i % size), (i / size));
            level[i].SetNeighbor(Vector2Int.up,    GetListElementFromRelative(position + Vector2.up));
            level[i].SetNeighbor(Vector2Int.down,  GetListElementFromRelative(position + Vector2.down));
            level[i].SetNeighbor(Vector2Int.left,  GetListElementFromRelative(position + Vector2.left));
            level[i].SetNeighbor(Vector2Int.right, GetListElementFromRelative(position + Vector2.right));
        }
        
        //Adding the gates on both sifes of the level
		level[size/2].SetDirection(Vector2Int.down, true);
		level[(size/2)+(size*(size-1))].SetDirection(Vector2Int.up, true);

        //Generating path
        level[size / 2].FindPath(Vector2Int.up);

        //Generate parts as objects
        foreach (LevelPartModel_home part in level)
		{
			part.GenerateObject();
		}

        //Creating end gate
        Instantiate(Gate, GetWorldPos(new Vector2Int(size/2, size)), Quaternion.identity);


        //bake the mesh for the map
        navMeshSurface.BuildNavMesh();

        //Spawn enemies
        SpawnerScript.IncrementLevel();
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject s in spawners)
        {
            SpawnerScript sc = s.GetComponent<SpawnerScript>();
            //if (sc == null) sc = s.GetComponent<BossSpawner>();
            //sc.Spawn();
        }

    }

	public void RemoveOld()
	{
        foreach(LevelPartModel_home part in oldLevel)
        {
            Destroy(part.instance);
        }
	}

	public void NextLevel()
	{
		RemoveOld();
        MarkLevelAsOld();
		IncrementStartingPos();
		Generate();
	}
}
