using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SlotItem
{
    public override void Activate() {
        HealPlayer();
    }
    private void HealPlayer() {
        slotManager.gameManager.battleManager.Heal();
    }
}
