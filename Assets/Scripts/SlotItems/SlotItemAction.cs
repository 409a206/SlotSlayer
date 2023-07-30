using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemAction
{
    SlotManager slotManager;

    private void Defend() {
      slotManager.gameManager.battleManager.Defend();
    }

    public void Attack() {
        slotManager.gameManager.battleManager.Attack();
    }

    private void Heal() {
        slotManager.gameManager.battleManager.Heal();
    }
}
