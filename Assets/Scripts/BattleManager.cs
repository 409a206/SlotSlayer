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
	[HideInInspector]
	public BattleHUD playerHUD;
	[HideInInspector]
	public BattleHUD enemyHUD;
	public Transform playerBattleStation;
	public Transform enemyBattleStation;
    
	public TMP_Text dialogueText;

	public GameObject BattleResultPanel;
    
	public PlayableCharacter playerUnit;
	public Enemy enemyUnit;
	
	[HideInInspector]
	public GameManager gameManager;

    void Awake()
    {
		gameManager = FindObjectOfType<GameManager>();
        currentBattleState = BattleState.START;
        StartCoroutine(SetupBattle());
		
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
		playerHUD = playerGO.GetComponentInChildren<BattleHUD>();
		playerGO.transform.parent = playerBattleStation;
		playerUnit = playerGO.GetComponent<PlayableCharacter>();
		

		GameObject enemyGO = Instantiate(enemyPrefab);
		enemyHUD = enemyGO.GetComponentInChildren<BattleHUD>();
		enemyGO.transform.parent = enemyBattleStation;
		enemyUnit = enemyGO.GetComponent<Enemy>();
		
		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		dialogueText.text = enemyUnit.unitName + " approaches! Beware!";

		yield return new WaitForSeconds(2f);

		PlayerTurn();
    }
	void PlayerTurn()
	{
		dialogueText.text = "Ready To Spin!";
		currentBattleState = BattleState.PLAYERREADY;

		gameManager.slotManager.selectedSlotItems.Clear();

		playerUnit.damage = 0;
		playerUnit.defence = 0;
		playerUnit.IsControllable = true;
		playerHUD.SetHUD(playerUnit);
	}

	IEnumerator PlayerAttack()
	{
		
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
		
		//Debug.Log("isEnemyDead: " + isDead);

		enemyHUD.SetHP(enemyUnit);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);
	}

	IEnumerator EnemyTurn()
	{
		enemyUnit.damage = 30;
		//enemyUnit.activationPool.heal = 1;

		dialogueText.text = enemyUnit.unitName + " attacks!";

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit);

		// yield return new WaitForSeconds(1f);



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

	}
	IEnumerator PlayerDefend()
	{
		dialogueText.text = "Player Defends!";

		yield return new WaitForSeconds(2f);

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

	public void StartEnemyTurn() {
		currentBattleState = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}
}
