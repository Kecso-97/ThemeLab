using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public delegate void StatsChangedDelegate();

public class CharacterStats : MonoBehaviour {

	private int health;
	private int maxHealth = 1000;

	private float mana;
	private int maxMana = 100;
	private float manaRegen = 10;

	private int damageModifier = 0;
    private int magicModifier = 0;
	private int armorModifier = 0;

	private float attackSpeed = 1;
    private float castSpeed = 1;

    public float offset = 2.2f;
    private Vector3 offset3D;
    public Transform healthBar;
    public Slider healthFill;
    public Slider manaFill;

    public static event StatsChangedDelegate AnyCharacterDiedEvent;
    public event StatsChangedDelegate CharacterDiedEvent;

    #region properties
    public int DamageModifier
	{
		get
		{
			return damageModifier;
		}

		set
		{
			//TODO inventory check
			damageModifier = value;
		}
	}

    public int MagicModifier
    {
        get
        {
            return magicModifier;
        }

        set
        {
            //TODO inventory check
            magicModifier = value;
        }
    }

    public int ArmorModifier
	{
		get
		{
			return armorModifier;
		}

		set
		{
			//TODO inventory check
			armorModifier = value;
		}
	}

    public bool Alive
    {
        get { return Health > 0; }
    }

	public int Health
	{
		get
		{
			return health;
		}

		set
		{
			//if(health != 0)
			//{
				health = value;
				if(health > maxHealth)
				{
					health = maxHealth;
				}
				if(health <= 0)
				{
					health = 0;
					if(CharacterDiedEvent != null) { CharacterDiedEvent(); }
					if(AnyCharacterDiedEvent != null) { AnyCharacterDiedEvent(); }
				}
				healthFill.value = (float)health / (float)maxHealth;
			//}
		}
	}

	public int MaxHealth
	{
		get
		{
			return maxHealth;
		}

		set
		{
			maxHealth = value;
			if (health > maxHealth)
			{
				health = maxHealth;
			}
		}
	}

	public float Mana
	{
		get
		{
			return mana;
		}

		set
		{
			mana = value;
			if (mana > maxMana)
			{
				mana = maxMana;
			}
			if (mana <= 0)
			{
				mana = 0;
			}
            manaFill.value = (float)mana / (float)maxMana;
		}
	}

	public int MaxMana
	{
		get
		{
			return maxMana;
		}

		set
		{
			maxMana = value;
			if(maxMana < mana)
			{
				mana = maxMana;
			}
		}
	}

	public float ManaRegen
	{
		get
		{
			return manaRegen;
		}

		set
		{
			manaRegen = value;
			if(manaRegen < 0)
			{
				ManaRegen = 0;
				Debug.LogError("Mana regeneration is set to negative value!");
			}
		}
	}

	public float AttackSpeed
	{
		get
		{
			return attackSpeed;
		}

		set
		{
			attackSpeed = value;
			if (attackSpeed < 0)
			{
				attackSpeed = 0;
				Debug.LogError("Attack speed is set to negative value!");
			}
		}
	}

    public float CastSpeed
    {
        get
        {
            return castSpeed;
        }

        set
        {
            castSpeed = value;
            if (castSpeed < 0)
            {
                castSpeed = 0;
                Debug.LogError("Cast speed is set to negative value!");
            }
        }
    }
    #endregion

    #region display
    private void positionHealthBar()
    {
		if(healthBar != null)
		{
			Vector3 currentPos = transform.position;
			healthBar.position = currentPos + offset3D;

			//healthBar.LookAt(Camera.main.transform);
			healthBar.rotation = Camera.main.transform.rotation;
		}
    }
    #endregion

    private void Awake()
    {
        
        healthBar = transform.Find("HealthBarTextured");
        if(healthBar != null)
        {
            Slider[]  sliders = GetComponentsInChildren<Slider>();
            healthFill = sliders[0];
            manaFill = sliders[1];
        }
        else
        {
            Debug.LogWarning("No HealthBar found attached to the character!");
        }
        
    }

    // Use this for initialization
    void Start () {
		Mana = MaxMana;
		Health = MaxHealth;
        offset3D = new Vector3(0, offset, 0);
		CharacterDiedEvent += IncrementKills;
        CharacterDiedEvent += RemoveCollider;
	}

    private void RemoveCollider()
    {
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }

	private void IncrementKills()
	{
		GameObject counter = GameObject.Find("KillCounter");
		string text = counter.GetComponent<Text>().text;
		string[] splitted = text.Split(' ');
		int incremented;
		int.TryParse(splitted[1], out incremented);
		incremented++;
		Debug.Log(incremented.ToString());
		counter.GetComponent<Text>().text = "Kills: " + incremented.ToString();

		GameObject player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Mana < MaxMana) Mana += (ManaRegen * Time.deltaTime);
        positionHealthBar();
	}

    public void TakeDamage(int dmg)
    {
        Health -= (dmg - ArmorModifier);
    }

    public void SetLevel(int level)
    {
        MaxHealth = (int)(MaxHealth * Mathf.Sqrt(level));
        MaxMana = (int)(MaxMana * Mathf.Sqrt(level));

        DamageModifier += level * 2;
        MagicModifier += level;
        ArmorModifier += level * 2;
    }
}
