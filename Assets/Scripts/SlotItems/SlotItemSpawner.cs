using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotItemType
{
    ATTACK, HEAL, DEFEND
}

public class SlotItemSpawner : MonoBehaviour
{
    [SerializeField]
    private List<SlotItemData> slotItemDatas;
    [SerializeField]
    private GameObject slotItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SlotItem SpawnSlotItem(SlotItemType slotItemType) {
        var newSlotItem = Instantiate(slotItemPrefab).GetComponent<SlotItem>();
        newSlotItem.slotItemData = slotItemDatas[(int)slotItemType];
        newSlotItem.name = newSlotItem.slotItemData.SlotItemName;
        return newSlotItem;
    }
}
