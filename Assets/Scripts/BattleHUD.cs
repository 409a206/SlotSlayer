using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

	public TMP_Text nameTMP_Text;
	public TMP_Text levelTMP_Text;
	public Image hpBar;
	private BattleManager battleManager;

	public void SetHUD(Unit unit)
	{
		nameTMP_Text.text = unit.unitName;
		levelTMP_Text.text = "Lvl " + unit.unitLevel;

		battleManager = GameObject.FindObjectOfType<BattleManager>();
		SetHP(unit);
	}

	public void SetHP(Unit unit)
	{
		hpBar.fillAmount = (float) unit.currentHP / (float) unit.maxHP;
	}

}
