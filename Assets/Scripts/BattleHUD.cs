using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

	public TMP_Text nameTMP_Text;
	public TMP_Text attack_Text;
	public TMP_Text defence_Text;
	public TMP_Text hp_Text;
	public Image hpBar;
	private BattleManager battleManager;

	public void SetHUD(Unit unit)
	{
		nameTMP_Text.text = unit.unitName;
		attack_Text.text = "Attack: " + 0;
		defence_Text.text = "Defence: " + 0;
		battleManager = GameObject.FindObjectOfType<BattleManager>();
		SetHP(unit);
	}

	public void SetHP(Unit unit)
	{
		if(unit.currentHP < 0) unit.currentHP = 0;

		hpBar.fillAmount = (float) unit.currentHP / (float) unit.maxHP;
		hp_Text.text = unit.currentHP + " / " + unit.maxHP;
	}

}
