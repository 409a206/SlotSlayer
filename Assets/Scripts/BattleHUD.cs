using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

	public TMP_Text nameTMP_Text;
	public TMP_Text levelTMP_Text;
	public Slider hpSlider;

	public void SetHUD(Unit unit)
	{
		nameTMP_Text.text = unit.unitName;
		levelTMP_Text.text = "Lvl " + unit.unitLevel;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}
