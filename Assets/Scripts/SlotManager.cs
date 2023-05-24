using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public static event Action OnSpinButtonClicked;
    public static event Action OnSpinStopped;

    [SerializeField]
    public Reel[] reels;

    //this variable is so that it does not check the results multiple times when row stops spinning
    private bool resultsChecked = true;

    [HideInInspector]
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        CheckRows();
        gameManager = FindObjectOfType<GameManager>();
    }

    void CheckRows(){
        //모든 row들이 멈출때까지 기다리기
        if(!reels[0].reelStopped || !reels[1].reelStopped || !reels[2].reelStopped) {

            resultsChecked = false;
        }

        //모든 row들이 멈췄고 아직 result가 체크되지 않았다면
        if(reels[0].reelStopped && reels[1].reelStopped && reels[2].reelStopped && !resultsChecked) {
            resultsChecked = true;
            gameManager.battleManager.currentBattleState = BattleState.PLAYERACTION;
            //OnSpinStopped?.Invoke();

            StartCoroutine(ActivateSlotItems());
           
        }
    }
    
    private IEnumerator ActivateSlotItems() {
         foreach (Action action in OnSpinStopped.GetInvocationList())
            {
                action?.Invoke();
                OnSpinStopped -= action;
                yield return new WaitForSeconds(2f);
            }
            gameManager.battleManager.currentBattleState = BattleState.ENEMYTURN;
            gameManager.battleManager.StartEnemyTurn();
    }

    //also works with finger touches
    //mobile touch function is recommended for more complicated uses
    private void OnMouseDown() {
        if(reels[0].reelStopped && reels[1].reelStopped && reels[2].reelStopped && gameManager.battleManager.currentBattleState == BattleState.PLAYERREADY) {
            gameManager.battleManager.currentBattleState = BattleState.SPINNING;
            OnSpinButtonClicked?.Invoke();
        }
    }
}
