using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static event Action OnSpinButtonClicked = delegate {};

    [SerializeField]
    public Reel[] reels;

    //this variable is so that it does not check the results multiple times when row stops spinning
    private bool resultsChecked = true;

    [HideInInspector]
    public BattleManager battleManager;

    // Update is called once per frame
    void Update()
    {
        CheckRows();
        battleManager = FindObjectOfType<BattleManager>(); 
    }

    void CheckRows(){
        //모든 row들이 멈출때까지 기다리기
        if(!reels[0].reelStopped || !reels[1].reelStopped || !reels[2].reelStopped) {

            resultsChecked = false;
        }

        //모든 row들이 멈췄고 아직 result가 체크되지 않았다면
        if(reels[0].reelStopped && reels[1].reelStopped && reels[2].reelStopped && !resultsChecked &&  battleManager.currentBattleState == BattleState.SPINNING ) {
            CheckResults();
            battleManager.currentBattleState = BattleState.PLAYERACTION;
        }
    }

    //also works with finger touches
    //mobile touch recommended for more complicated uses
    private void OnMouseDown() {
        if(reels[0].reelStopped && reels[1].reelStopped && reels[2].reelStopped) {
            OnSpinButtonClicked();
            battleManager.currentBattleState = BattleState.SPINNING;
        }
    }

    //바꿔야함
    private void CheckResults()
    {
       Debug.Log("CheckResults Called");

       #region Example
        // if(rows[0].stoppedSlot == "Diamond"
        //    && rows[1].stoppedSlot == "Diamond"
        //    && rows[2].stoppedSlot == "Diamond") {

        //     prizeValue = 200;

        // } else if(rows[0].stoppedSlot == "Crown"
        //           && rows[1].stoppedSlot == "Crown"
        //           && rows[2].stoppedSlot == "Crown") {

        //           prizeValue = 400;

        // } else if(rows[0].stoppedSlot == "Melon"
        //           && rows[1].stoppedSlot == "Melon"
        //           && rows[2].stoppedSlot == "Melon") {

        //           prizeValue = 600;

        // } else if(rows[0].stoppedSlot == "Bar"
        //           && rows[1].stoppedSlot == "Bar"
        //           && rows[2].stoppedSlot == "Bar") {

        //           prizeValue = 800;

        // } else if(rows[0].stoppedSlot == "Seven"
        //           && rows[1].stoppedSlot == "Seven"
        //           && rows[2].stoppedSlot == "Seven") {

        //           prizeValue = 1500;

        // } else if(rows[0].stoppedSlot == "Cherry"
        //           && rows[1].stoppedSlot == "Cherry"
        //           && rows[2].stoppedSlot == "Cherry") {

        //           prizeValue = 3000;

        // } else if(rows[0].stoppedSlot == "Lemon"
        //           && rows[1].stoppedSlot == "Lemon"
        //           && rows[2].stoppedSlot == "Lemon") {

        //           prizeValue = 5000;

        // }
        #endregion
        
        resultsChecked = true;
    }
}
