using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum SlotItemAction {
//     ATTACK, HEAL, DEFEND
// }

public enum SlotItemAttribute {
    NONE, FIRE, ICE
}

public enum SlotItemSynergyAttribute {
    ATTACK, DARKMAGIC, WHITEMAGIC
}

[System.Serializable]
[CreateAssetMenu(fileName = "Slot Item", menuName = "Scriptable Object/Slot Item Data", order = int.MaxValue)]
public class SlotItemData : ScriptableObject
{
    [SerializeField]
    private string slotItemName;

    public string SlotItemName {
        get {return slotItemName;}
        set {slotItemName = value;}
    }

    public SlotItemAction slotItemAction;
    public float applyAmount;
    public SlotItemAttribute slotItemAttribute;
    public SlotItemSynergyAttribute[] slotItemSynergyAttributes;
    public SpriteRenderer spriteRenderer;
    
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
}
