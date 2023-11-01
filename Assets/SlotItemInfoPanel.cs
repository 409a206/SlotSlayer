using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotItemInfoPanel : MonoBehaviour
{
    public TMP_Text slotItemNameText;
    public TMP_Text descriptionText;
    public TMP_Text cost;

    private void Awake() {
        UnShow();
    }

    public void UnShow() {
        slotItemNameText.text = "";
        descriptionText.text = "";
        cost.text = "";
    }

    public void Show(SlotItemData slotItemData) {
        slotItemNameText.text = slotItemData.SlotItemName;
        descriptionText.text = slotItemData.description;
        cost.text = slotItemData.cost.ToString();
    }
}
