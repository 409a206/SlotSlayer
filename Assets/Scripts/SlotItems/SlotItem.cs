using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



//Scriptable Object로 추후 변경 필요!!
public class SlotItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private bool isSelected = false;
    public bool IsSelected {
        get {return isSelected;}
        set {isSelected = value;}
    }
    private bool isOnCorrespondingSlot = false;
    private Vector3 originPos;
    private Vector3 onDragEndPos;
    
    public GameObject[] targets;

    //컴포넌트 변수
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Color originColor;

    //레퍼런스 변수
    protected SlotManager slotManager;
    public SlotItemData slotItemData;
    
    void Awake()
    {
       slotManager = FindObjectOfType<SlotManager>();
       _boxCollider2D = this.GetComponent<BoxCollider2D>();
       _spriteRenderer = this.GetComponent<SpriteRenderer>();
       originColor = _spriteRenderer.color;
    }

    // private void OnMouseDown() {
    //     if(this.gameObject.transform.position == this.transform.parent.gameObject.transform.position
    //        && slotManager.gameManager.battleManager.currentBattleState == BattleState.SPINPAUSE){
           
    //         //나중에 파일 경로및 이름 수정 필요
    //         SlotItem instantiatedSlotItem = Resources.Load<SlotItem>("Prefabs/Dummy/SlotItems/Instantiated SlotItems/" + this.slotItemData.SlotItemName);
            
    //         if(!isSelected) {
    //             if(slotManager.selectedSlotItems.Count < slotManager.SlotItemsToSelect) {
    //                 SlotManager.OnSpinStopped += Activate;
    //                 _spriteRenderer.color = new Color(originColor.r/255f,originColor.g/255f,originColor.b/255f, 100f/255f);
    //                 slotManager.selectedSlotItems.Add(instantiatedSlotItem);
                  
    //                 Debug.Log(slotManager.selectedSlotItems.Count);
    //                 isSelected = !isSelected;
    //             }
    //         } else {
    //             SlotManager.OnSpinStopped -= Activate;
    //             slotManager.selectedSlotItems.Remove(instantiatedSlotItem);
    //             _spriteRenderer.color = originColor;
    //             Debug.Log(slotManager.selectedSlotItems.Count);
    //             isSelected = !isSelected;
    //         }
    //     }
    // }

    //Obsolete
    public void Activate() {
        // switch (slotItemData.actionType)
        // {
        //     case ActionType.ATTACK: slotManager.gameManager.battleManager.Attack(); return;
        //     case ActionType.DEFEND: slotManager.gameManager.battleManager.Defend(); return;
        //     case ActionType.HEAL: slotManager.gameManager.battleManager.Heal(); return;
        //     default: return;
        // }

        slotManager.gameManager.battleManager.Attack();

    }

    public void OnDrag(PointerEventData eventData)
    {
       if(isOnCorrespondingSlot && slotManager.gameManager.battleManager.currentBattleState == BattleState.SPINPAUSE) {
           Vector3 mouseWorldPosition = slotManager.gameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
           transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);
           this._spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
       }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(slotManager.gameManager.battleManager.currentBattleState == BattleState.SPINPAUSE) {
            originPos = this.transform.position;
            // if(this.transform.position == this.transform.parent.gameObject.transform.position) {
            //     isOnCorrespondingSlot = true;
            // } else {
            //     isOnCorrespondingSlot = false;
            // }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onDragEndPos = this.transform.position;

        if(isOnCorrespondingSlot && slotManager.gameManager.battleManager.currentBattleState == BattleState.SPINPAUSE) {

            //Debug.Log("OnEndDrag Called");
            
            
            transform.position = originPos;
            this._spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

            
            if(!isSelected && CalculateYDistFromOriginPos() > 2.0f && slotManager.selectedSlotItems.Count < slotManager.SlotItemsToSelect) {
                 //나중에 파일 경로및 이름 수정 필요
                    GameObject instantiatedSlotItem = Resources.Load<GameObject>("Prefabs/Dummy/SlotItems/Instantiated SlotItems/" + this.slotItemData.SlotItemName);

                    slotManager.selectedSlotItems.Add(instantiatedSlotItem.GetComponent<SlotItem>());

                    Debug.Log(slotManager.selectedSlotItems.Count); 

                    //코인 차감
                    slotManager.SetCoinCount(slotManager.coinsLeft - this.slotItemData.cost);
                    Debug.Log("slotManager.coinsLeft: " + slotManager.coinsLeft);

                    ApplySlotItem();
                    isSelected = !isSelected;
                    _boxCollider2D.enabled = false;
                    _spriteRenderer.color = new Color(originColor.r/255f,originColor.g/255f,originColor.b/255f, 0f);
                
                //if(slotManager.selectedSlotItems.Count < slotManager.SlotItemsToSelect) {
                    //SlotManager.OnSpinStopped += Activate;
                    

                    //나중에 파일 경로및 이름 수정 필요
                    // GameObject instantiatedSlotItem = Resources.Load<GameObject>("Prefabs/Dummy/SlotItems/Instantiated SlotItems/" + this.slotItemData.SlotItemName);

                    // slotManager.selectedSlotItems.Add(instantiatedSlotItem.GetComponent<SlotItem>());
                    

                    //Debug.Log(slotManager.selectedSlotItems.Count); 

                //}
            } 
            
        }
    }

    private void ApplySlotItem()
    {
        switch (slotItemData.SlotItemName)
        {
            case "Attack" :
                slotManager.gameManager.battleManager.playerUnit.damage += slotItemData.applyAmount;
                slotManager.gameManager.battleManager.playerHUD.attack_Text.text = 
                "Attack: " +  slotManager.gameManager.battleManager.playerUnit.damage;
                break;
            
            case "Defend" : 
                slotManager.gameManager.battleManager.playerUnit.defence += slotItemData.applyAmount;
                slotManager.gameManager.battleManager.playerHUD.defence_Text.text = 
                "Defence: " +  slotManager.gameManager.battleManager.playerUnit.defence;
                break;
            
            case "Heal" : 
                slotManager.gameManager.battleManager.playerUnit.Heal(slotItemData.applyAmount);
                slotManager.gameManager.battleManager.playerHUD.SetHP(slotManager.gameManager.battleManager.playerUnit);
                break;
            
            case "Stab" : 
                slotManager.gameManager.battleManager.Attack();
                break;

            default: 
                Debug.LogError("slotItem name not applicable");
                break;
        }
    }

    //SlotItem이 슬롯으로부터 얼마나 드래그 되었는지 확인하는 함수.
    public float CalculateYDistFromOriginPos() {
        float yDistFromOriginPos = 0f;
        
        yDistFromOriginPos = onDragEndPos.y- originPos.y;

        Debug.Log("yDistFromOriginPos: " + yDistFromOriginPos);

        return yDistFromOriginPos;
    }

    private void OnMouseEnter() {
        if(isOnCorrespondingSlot) {
            // Debug.Log("MouseEntered");
            ToggleInfoPanel(true);
        }
    }

    private void OnMouseExit() {
        if(isOnCorrespondingSlot) {
            //Debug.Log("MouseExited");
            ToggleInfoPanel(false);
        }
    }

    private void ToggleInfoPanel(bool isMouseHovering)
    {
        if(isMouseHovering) {
            slotManager.slotItemInfoPanel.Show(this.slotItemData);
            Debug.Log("Toggle On");
        } else {
            slotManager.slotItemInfoPanel.UnShow();
            Debug.Log("Toggle Off");
        }
    }

    public void CheckIfSlotItemIsOnCorrespondingSlot() {
        if(transform.localPosition == Vector3.zero) {
            isOnCorrespondingSlot = true;
        } else {
            isOnCorrespondingSlot = false;
        }
    }
}
