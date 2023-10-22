using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//needs to be customized
public class Unit : MonoBehaviour
{
	public string unitName;
	
	
	public int damage = 0;
	public int defence = 0;

	public int maxHP;
	public int currentHP;

	public BattleManager battleManager;

	private void Awake() {
		battleManager = FindObjectOfType<BattleManager>();
	}

	public bool TakeDamage(int dmg)
	{

		defence -= dmg;

		if(defence < 0) {
			currentHP += defence;
			defence = 0;
		}


		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

   
}
