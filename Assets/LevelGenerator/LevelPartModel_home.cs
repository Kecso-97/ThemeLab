using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartModel_home : AbstractLevelPartModel {

	private Vector3 position;

	private bool visited = false;

	private Dictionary<Vector2Int, AbstractLevelPartModel> neighbors = new Dictionary<Vector2Int, AbstractLevelPartModel>();
	private Dictionary<Vector2Int, bool> directions = new Dictionary<Vector2Int, bool>();

    public static GameObject path;
    public static GameObject curve;
    public static GameObject deadend;
    public static GameObject Xcross;
    public static GameObject Ycross;

    public LevelPartModel_home(Vector3 position1)
	{
		position = position1;

        directions.Add(Vector2Int.up, false);
        directions.Add(Vector2Int.down, false);
        directions.Add(Vector2Int.left, false);
        directions.Add(Vector2Int.right, false);
    }

	public Vector3 Position
	{
		get
		{
			return position;
		}

		set
		{
			position = value;
		}
	}

    public void SetNeighbor(Vector2Int dir, AbstractLevelPartModel neighbor)
    {
        neighbors[dir] = neighbor;
    }

    public void SetDirection(Vector2Int dir, bool active)
    {
        directions[dir] = active;
    }

    public void GenerateObject()//There must be a far easier and cleaner way to do this...
	{
		if (directions[Vector2Int.up])
		{
			if (directions[Vector2Int.down])
			{
				if (directions[Vector2Int.left])
				{
					if(directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(Xcross, position, Quaternion.Euler(0, 0, 0));
					}
					else
					{
						instance = GameObject.Instantiate(Ycross, position, Quaternion.Euler(0, 180, 0));
					}
				}
				else
				{
					if(directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(Ycross, position, Quaternion.Euler(0, 0, 0));
					}
					else
					{
						instance = GameObject.Instantiate(path, position, Quaternion.Euler(0, 0, 0));
					}
				}
			}
			else
			{
				if (directions[Vector2Int.left])
				{
					if ( directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(Ycross, position, Quaternion.Euler(0, -90, 0));
					}
					else
					{
						instance = GameObject.Instantiate(curve, position, Quaternion.Euler(0, 180, 0));
					}
				}
				else
				{
					if ( directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(curve, position, Quaternion.Euler(0, -90, 0));
					}
					else
					{
						instance = GameObject.Instantiate(deadend, position, Quaternion.Euler(0, 0, 0));
					}
				}
			}
		}
		else
		{
			if (directions[Vector2Int.down])
			{
				if (directions[Vector2Int.left])
				{
					if (directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(Ycross, position, Quaternion.Euler(0, 90, 0));
					}
					else
					{
						instance = GameObject.Instantiate(curve, position, Quaternion.Euler(0, 90, 0));
					}
				}
				else
				{
					if (directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(curve, position, Quaternion.Euler(0, 0, 0));
					}
					else
					{
						instance = GameObject.Instantiate(deadend, position, Quaternion.Euler(0, 180, 0));
					}
				}
			}
			else
			{
				if (directions[Vector2Int.left])
				{
					if ( directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(path, position, Quaternion.Euler(0, 90, 0));
					}
					else
					{
						instance = GameObject.Instantiate(deadend, position, Quaternion.Euler(0, -90, 0));
					}
				}
				else
				{
					if (directions[Vector2Int.right])
					{
						instance = GameObject.Instantiate(deadend, position, Quaternion.Euler(0, 90, 0));
					}
				}
			}
		}
	}

    public override bool FindPath(Vector2Int enterDir)
    {
        if (visited)
        {
            //Debug.Log("Position (" + position.x + "," + position.y + ") has returned with FALSE");
            return false;
        }
        else
        {
            visited = true;
            directions[enterDir * -1] = true;

            Vector2Int[] newDirections = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            int n = newDirections.Length;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                Vector2Int value = newDirections[k];
                newDirections[k] = newDirections[n];
                newDirections[n] = value;
            }

            //Vector2Int newDir = Vector2Int.zero;
			foreach(Vector2Int dir in newDirections)
            {
                if (directions[dir] == false)
                {
                    if (neighbors[dir].FindPath(dir))
                    {
                        directions[dir] = true;
                    }

                }
			}
            return true;
        }
    }
}