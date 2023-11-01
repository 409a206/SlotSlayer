using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType {
    INSTANT_ACTIVATION, ACTIVATED_AT_THE_END_OF_TURN
}

public enum SlotItemAttribute {
    NONE, FIRE, ICE
}

public enum SlotItemSynergyAttribute {
    ATTACK, DARKMAGIC, WHITEMAGIC
}
public enum TargetType {
    GLOBAL, PLAYER, ENEMYSINGLE
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
    
    [SerializeField]
    public string code;

    public int applyAmount;
    public int cost;
    public TargetType targetType;
    public ActionType actionType;
    public SlotItemAttribute slotItemAttribute;
    public SlotItemSynergyAttribute[] slotItemSynergyAttributes;
    
    [SerializeField]
    AudioClip stopSound;

}
