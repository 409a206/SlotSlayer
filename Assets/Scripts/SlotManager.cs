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
    [Tooltip("남은 스핀 횟수")]
    private int spinsLeft = 3;

    [SerializeField]
    [Tooltip("선택할 수 있는 총 슬롯 아이템 갯수")]
    private int slotItemsToSelect = 5;
    public int SlotItemsToSelect {
        get {return slotItemsToSelect;}
        set {slotItemsToSelect = value;}
    }

    [Tooltip("각 슬롯 활성화 사이의 간격(초)")]
    [SerializeField]
    public float intervalBetweenEachSlotActivation = 2f;

    //선택된 슬롯 아이템
    public List<SlotItem> selectedSlotItems;

    [SerializeField]
    public Reel[] reels;

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
        for (int i = 0; i < reels.Length; i++)
        {
            if(!reels[i].reelStopped) resultsChecked = false;
        }

        //모든 row들이 멈췄고 아직 result가 체크되지 않았다면
        if(reels[0].reelStopped && reels[1].reelStopped && reels[2].reelStopped && !resultsChecked) {
            resultsChecked = true;
            gameManager.battleManager.SlotItemSelection();
            
        }
    }
    
    //슬롯 아이템 활성화 코루틴
    private IEnumerator ActivateSlotItems() {
        gameManager.battleManager.currentBattleState = BattleState.PLAYERACTION;
         foreach (Action action in OnSpinStopped?.GetInvocationList()?? new Delegate[0])
            {
                action.Invoke();
                OnSpinStopped -= action;
                yield return new WaitForSeconds(intervalBetweenEachSlotActivation);
            }
        
        if(gameManager.battleManager.enemyUnit.currentHP > 0) {
            gameManager.battleManager.currentBattleState = BattleState.ENEMYTURN;
            gameManager.battleManager.StartEnemyTurn();
        }
    }

    public void Spin() {
        if(reels[0].reelStopped && reels[1].reelStopped && reels[2].reelStopped 
           && (gameManager.battleManager.currentBattleState == BattleState.PLAYERREADY || gameManager.battleManager.currentBattleState == BattleState.SPINPAUSE)) {
            //만약 남은 스핀 횟수가 0보다 크다면 스핀하는 로직
            if(spinsLeft > 0) {
                spinsLeft--;
                Debug.Log("spins left: " + spinsLeft);
                gameManager.battleManager.currentBattleState = BattleState.SPINNING;
                OnSpinButtonClicked?.Invoke();
            } 
            //만약 남은 스핀 횟수가 0이라면 선택된 슬롯 아이템 활성화 로직
            else {
                StartCoroutine(ActivateSlotItems());
                spinsLeft = 3;
                selectedSlotItems.Clear();
                OnSpinStopped -= OnSpinStopped;
            }
        }
    }

    /*
    //also works with finger touches
    //mobile touch function is recommended for more complicated uses
    private void OnMouseDown() {
        if(reels[0].reelStopped && reels[1].reelStopped && reels[2].reelStopped 
           && (gameManager.battleManager.currentBattleState == BattleState.PLAYERREADY || gameManager.battleManager.currentBattleState == BattleState.SPINPAUSE)) {
            if(spinsLeft > 0) {
                spinsLeft--;
                Debug.Log("spins left: " + spinsLeft);
                gameManager.battleManager.currentBattleState = BattleState.SPINNING;
                OnSpinButtonClicked?.Invoke();
            } else {
                StartCoroutine(ActivateSlotItems());
                spinsLeft = 3;
                selectedSlotItems.Clear();
                OnSpinStopped -= OnSpinStopped;
            }
        }
    }
    */
}
