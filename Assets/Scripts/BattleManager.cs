using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERREADY, SPINNING, SPINPAUSE, PLAYERACTION, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    public BattleState currentBattleState;
    
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
    
	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;
	public Transform playerBattleStation;
	public Transform enemyBattleStation;
    
	public TMP_Text dialogueText;

	public GameObject BattleResultPanel;
    
	public Unit playerUnit;
	public Unit enemyUnit;
	
	[HideInInspector]
	public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
		gameManager = FindObjectOfType<GameManager>();
        currentBattleState = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
		playerGO.transform.parent = playerBattleStation;
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab);
		enemyGO.transform.parent = enemyBattleStation;
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		PlayerTurn();
    }
	void PlayerTurn()
	{
		dialogueText.text = "Ready To Spin!";
		currentBattleState = BattleState.PLAYERREADY;
	}

	IEnumerator PlayerAttack()
	{
		
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
		
		//Debug.Log("isEnemyDead: " + isDead);

		enemyHUD.SetHP(enemyUnit);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		// if(isDead)
		// {
		// 	currentBattleState = BattleState.WON;
		// 	EndBattle();
		// }

	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			currentBattleState = BattleState.LOST;
			EndBattle();
		} else
		{
			currentBattleState = BattleState.PLAYERREADY;
			PlayerTurn();
		}

	}

	public void EndBattle()
	{
		if(currentBattleState == BattleState.WON)
		{
			//dialogueText.text = "You won the battle!";
		} else if (currentBattleState == BattleState.LOST)
		{
			//dialogueText.text = "You were defeated.";
		}

		OpenBattleResultWindow(currentBattleState);
	}

    private void OpenBattleResultWindow(BattleState currentBattleState)
    {
		BattleResultPanel.SetActive(true);
		if(currentBattleState == BattleState.WON) {
			BattleResultPanel.GetComponentInChildren<TMPro.TMP_Text>().text = "Congratulations! You Won!";
		} else if (currentBattleState == BattleState.LOST) {
			BattleResultPanel.GetComponentInChildren<TMPro.TMP_Text>().text = "Too Bad. You Lost";
		}
    }

    IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit);
		dialogueText.text = "Your HP is restored!";

		yield return new WaitForSeconds(2f);

		//state = BattleState.ENEMYTURN;
		//StartCoroutine(EnemyTurn());
	}
	IEnumerator PlayerDefend()
	{
		dialogueText.text = "Player Defends!";

		yield return new WaitForSeconds(2f);

		//state = BattleState.ENEMYTURN;
		//StartCoroutine(EnemyTurn());
	}
	public void SlotItemSelection() {
		dialogueText.text = "Select Slot Items";
		currentBattleState = BattleState.SPINPAUSE;
	}

	public void Attack()
	{
		StartCoroutine(PlayerAttack());
		Debug.Log("Attack!");
	}

	public void Heal()
	{
		StartCoroutine(PlayerHeal());
		Debug.Log("Heal!");
	}
	public void Defend() {
		StartCoroutine(PlayerDefend());
		Debug.Log("Defend!");
	}
	public void StartEnemyTurn() {
		currentBattleState = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}
}
