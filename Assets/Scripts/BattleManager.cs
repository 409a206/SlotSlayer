using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    public BattleState currentBattleState;
    
	Unit playerUnit;
	Unit enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        currentBattleState = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
