using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : MonoBehaviour {
    int PotionAmount = 0;
    int PotionHeal = 100;
    GameObject PotionUI;

    CharacterStats playerStats;
	// Use this for initialization
	void Start () {
        playerStats = GetComponent<CharacterStats>();
        CharacterStats.AnyCharacterDiedEvent += AddPotion;
        PotionUI = GameObject.Find("HealPotionCount");
    }
	
	// Update is called once per frame
	void Update () {
        if (playerStats.Alive && Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(PotionAmount > 0)
            {
                --PotionAmount;
                PotionUI.GetComponent<Text>().text = PotionAmount.ToString();
                playerStats.Health += PotionHeal;
            }
        }
	}

    void AddPotion()
    {
        if (!playerStats.Alive) return;
        ++PotionAmount;
        PotionUI.GetComponent<Text>().text = PotionAmount.ToString();
    }
}
