using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object로 추후 변경 필요!!
public class SlotItem : MonoBehaviour
{
    [SerializeField]
    private string slotItemName;

    [SerializeField]
    private bool isSelected = false;
    public bool IsSelected {
        get {return isSelected;}
        set {isSelected = value;}
    }

    public string SlotItemName {
        get {return slotItemName;}
        set {slotItemName = value;}
    }

    [SerializeField]
    private string code;
    
    [SerializeField]
    AudioClip stopSound;

    //컴포넌트 변수
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Color originColor;

    //레퍼런스 변수
    protected SlotManager slotManager;
    
    
    void Start()
    {
       slotManager = FindObjectOfType<SlotManager>();
       _boxCollider2D = this.GetComponent<BoxCollider2D>();
       _spriteRenderer = this.GetComponent<SpriteRenderer>();
       originColor = _spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        if(this.gameObject.transform.position == this.transform.parent.gameObject.transform.position
           && slotManager.gameManager.battleManager.currentBattleState == BattleState.SPINPAUSE){
            if(!isSelected) {
                if(slotManager.selectedSlotItems.Count < slotManager.SlotItemsToSelect) {
                    SlotManager.OnSpinStopped += Activate;
                    slotManager.selectedSlotItems.Add(this.GetComponent<SlotItem>());

                    _spriteRenderer.color = new Color(originColor.r/255f,originColor.g/255f,originColor.b/255f, 100f/255f);
                  
                    Debug.Log(slotManager.selectedSlotItems.Count);
                    isSelected = !isSelected;
                }
            } else {
                SlotManager.OnSpinStopped -= Activate;
                slotManager.selectedSlotItems.Remove(this.GetComponent<SlotItem>());
                _spriteRenderer.color = originColor;
                isSelected = !isSelected;
            }
        }
    }

    public virtual void Activate() {}
}
