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
    private Vector3 originPos;
    
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
       originPos = this.transform.position;
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

    public void Activate() {
        switch (slotItemData.actionType)
        {
            case ActionType.ATTACK: slotManager.gameManager.battleManager.Attack(); return;
            case ActionType.DEFEND: slotManager.gameManager.battleManager.Defend(); return;
            case ActionType.HEAL: slotManager.gameManager.battleManager.Heal(); return;
            default: return;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag Called");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag Called");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag Called");
    }
}
